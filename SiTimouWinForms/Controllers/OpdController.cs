using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace gov.minahasa.sitimou.Controllers
{
    internal class OpdController : DbConnection
    {
        #region === Constructor ===

        public bool IsDataExist;
        public BindingSource BindData = new();

        // Clases
        private readonly NotifHelper _notif = new();
        private readonly DatabaseHelper _db = new();

        // Properties
        public int JenisOpd { get; private set; }
        public string NamaSingkat { get; private set; }
        public string NamaLengkap { get; private set; }
        public double GpsLat { get; private set; }
        public double GpsLng { get; private set; }
        public string Email { get; private set; }

        #endregion

        #region === OPD === 

        public async Task FillComboDinasKel(ComboBox comboBox, Form form)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.DataSource = null;
                comboBox.IntegralHeight = false;
            }

            const string sql = "SELECT nama_lengkap, opd_id FROM msopd ORDER BY nama_lengkap";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows) return;

                                var dsCombo = new List<ComboBoxHelper>();

                                while (await reader.ReadAsync())
                                {
                                    dsCombo.Add(new ComboBoxHelper(reader[0].ToString(), reader[1].ToString()));
                                }

                                comboBox.DataSource = dsCombo;
                                comboBox.DisplayMember = "Description";
                                comboBox.ValueMember = "Value";
                                comboBox.IntegralHeight = true;
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                        }
                    }
                }
            }
        }

        public async Task FillComboDesaByKec(int idKecamatan, ComboBox comboBox, Form form)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.DataSource = null;
                comboBox.IntegralHeight = false;
            }

            const string sql = "SELECT nama_desa, desa_id FROM msdesa WHERE kecamatan_id = @p_kec_id ORDER BY nama_desa";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_kec_id", idKecamatan);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows) return;

                                var dsCombo = new List<ComboBoxHelper>();

                                while (await reader.ReadAsync())
                                {
                                    dsCombo.Add(new ComboBoxHelper(reader[0].ToString(), reader[1].ToString()));
                                }

                                comboBox.DataSource = dsCombo;
                                comboBox.DisplayMember = "Description";
                                comboBox.ValueMember = "Value";
                                comboBox.IntegralHeight = true;
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                        }
                    }
                }
            }
        }


        public void GetDataOpd(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_opd", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"Data OPD [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";
                                    
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("opd_id");

                                    DataGridHelper.FormatTable(dataGrid);
                                }

                                IsDataExist = dt.Rows.Count > 0;
                                dataGrid.Visible = IsDataExist;

                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                        }
                    }
                }
            }

        }

        public bool SimpanDataOpd(bool newData, int idOpd, string jenisOpd, string namaSingkat, string namaLengkap,
            double gpsLat, double gpsLng, string email, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_opd", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_exec", newData ? "N" : "E");
                            cmd.Parameters.AddWithValue("@p_opd_id", newData ? (object)DBNull.Value : idOpd);
                            cmd.Parameters.AddWithValue("@p_jenis", jenisOpd);
                            cmd.Parameters.AddWithValue("@p_nama_short", namaSingkat.Trim());
                            cmd.Parameters.AddWithValue("@p_nama_full", namaLengkap.Trim());
                            cmd.Parameters.AddWithValue("@p_gps_lat", gpsLat);
                            cmd.Parameters.AddWithValue("@p_gps_lng", gpsLng);
                            cmd.Parameters.AddWithValue("@p_email", email.Trim());

                            var result = Convert.ToInt16(cmd.ExecuteScalar());

                            switch (result)
                            {
                                case 0:
                                    _notif.MsgBoxWarning("Gagal simpan data OPD.");
                                    return false;

                                case 1:
                                    _notif.MsgBoxWarning("Nama OPD sudah ada.");
                                    return false;

                                case 2:
                                    return true;
                                default:
                                    _notif.MsgBoxWarning("Gagal simpan data OPD.");
                                    return false;
                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                            return false;
                        }
                    }
                }
            }
        }

        public bool EditDataOpd(int opdId, Form form)
        {
            const string sql = "SELECT nama_singkat, nama_lengkap, email, jenis_opd, gps_lat, gps_lng FROM msopd WHERE opd_id = @p_opd_id";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_opd_id", opdId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    _notif.MsgBoxWarning(@"Data OPD tidak ditemukan.");
                                    return false;
                                }

                                reader.Read();

                                // Assign value
                                NamaSingkat = reader.GetString(0);
                                NamaLengkap = reader.GetString(1);
                                Email = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                JenisOpd = int.Parse(reader.GetString(3));
                                GpsLat = reader.GetDouble(4);
                                GpsLng = reader.GetDouble(5);

                                return true;
                            }

                        }
                        catch (Exception e)
                        {
                            _notif.MsgBoxWarning(@"Gagal load data OPD.");
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                            return false;
                        }
                    }
                }
            }
        }

        public bool HapusDataOpd(int idOpd, Form form)
        {
            var sql = $"DELETE FROM msopd WHERE opd_id = {idOpd}";

            using (new WaitCursor(form))
            {
                return _db.ExecuteSqlString(sql);
            }
        }

        #endregion

        #region === Desa / Kecamatan ===

        public async Task FillComboDesaKec(ComboBox comboBox, Form form)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.DataSource = null;
                comboBox.IntegralHeight = false;
            }

            const string sql = "SELECT nama_kecamatan, kecamatan_id FROM mskecamatan ORDER BY nama_kecamatan";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows) return;

                                var dsCombo = new List<ComboBoxHelper>();

                                while (await reader.ReadAsync())
                                {
                                    dsCombo.Add(new ComboBoxHelper(reader[0].ToString(), reader[1].ToString()));
                                }

                                comboBox.DataSource = dsCombo;
                                comboBox.DisplayMember = "Description";
                                comboBox.ValueMember = "Value";
                                comboBox.IntegralHeight = true;
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                        }
                    }
                }
            }
        }

        public void GetDataDesaKecamatan(string jenisData, GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_kec_desa", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_data", jenisData);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    var caption = jenisData == "1" ? "Data Kecamatan" : "Data Desa";
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"{caption} [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("desa_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("kecamatan_id");

                                    DataGridHelper.FormatTable(dataGrid);

                                }

                                IsDataExist = dt.Rows.Count > 0;
                                dataGrid.Visible = IsDataExist;

                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                        }
                    }
                }
            }

        }

        public bool SimpanDataDesaKec(string jenisData, string namaDesa, int idKecamatan, string namaKecamatan, double gpsLat, double gpsLng, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_desa_kec", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_data", jenisData);
                            cmd.Parameters.AddWithValue("@p_nama_desa", namaDesa);
                            cmd.Parameters.AddWithValue("@p_kecamatan_id", idKecamatan);
                            cmd.Parameters.AddWithValue("@p_nama_kecamatan", namaKecamatan);
                            cmd.Parameters.AddWithValue("@p_gps_lat", gpsLat);
                            cmd.Parameters.AddWithValue("@p_gps_lng", gpsLng);

                            cmd.ExecuteNonQuery();

                            return true;
                        }
                        catch (Exception e)
                        {
                            _notif.MsgBoxWarning(@"Gagal simpan data pengumuman.");

                            DebugHelper.ShowError(@"PENGUMUMAN", @"PengumumanController", MethodBase.GetCurrentMethod()?.Name, e, null, true);

                            return false;
                        }
                    }
                }
            }
        }

        public bool HapusDataDesaKecamatan(string jenisData, int idData, Form form)
        {
            var sql = jenisData == "1"
                ? $"DELETE FROM mskecamatan WHERE kecamatan_id = {idData}"
                : $"DELETE FROM msdesa WHERE desa_id = {idData}";

            using (new WaitCursor(form))
            {
                return _db.ExecuteSqlString(sql);
            }
        }

        #endregion
    }
}
