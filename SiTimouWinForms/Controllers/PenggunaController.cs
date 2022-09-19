using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Grid.Grouping;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;


namespace gov.minahasa.sitimou.Controllers
{
    internal class PenggunaController : DbConnection
    {
        #region === Constructor ===

        public bool IsDataExist;
        public BindingSource BindData = new();

        // Clases
        private readonly NotifHelper _notifHelper = new();
        private readonly CryptoHelper _crypto = new();
        private readonly DatabaseHelper _dbHelper = new();
        // private readonly PegawaiRest _rest = new();

        // Property
        public int IdOpd { get; private set; }
        public string NipPegawai { get; private set; }
        public string NamaLengkap { get; private set; }
        public int IdGrup { get; private set; }
        public string Jabatan { get; private set; }


        #endregion

        #region === Data Pegawai ===

        public async Task FillComboResponder(ComboBox comboBox, Form form)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.DataSource = null;
                comboBox.IntegralHeight = false;
            }

            const string sql = "SELECT nama_lengkap, user_id FROM pegawai WHERE opd_id = @id_opd AND grup = '4' ORDER BY nama_lengkap";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@id_opd", Globals.UserOpdId);

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

        public void GetDataPegawai(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_pegawai", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_user_id", Globals.UserId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"Data Pegawai [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";
                                    
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("user_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("grup");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("jenis_opd");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("flg");

                                    DataGridHelper.FormatTable(dataGrid);

                                    IsDataExist = true;
                                }
                                else
                                {
                                    IsDataExist = false;
                                }

                                dataGrid.Visible = dt.Rows.Count > 0;


                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                        }
                    }
                }
            }

        }

        public bool HapusDataPegawai(int idPegawai, Form form)
        {
            var sql = $"DELETE FROM pegawai WHERE user_id = {idPegawai}";

            using (new WaitCursor(form))
            {
                return _dbHelper.ExecuteSqlString(sql);
            }
        }

        public void GetInfoPegawai(int idUser, Form form)
        {
            var sql =
                $"SELECT nik, nip, nama_lengkap, opd_id, grup, jabatan, email, opd_admin, plt_grup, plt_opd FROM pegawai WHERE user_id = @p_user_id";

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_user_id", idUser);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    _notifHelper.MsgBoxWarning(@"Data Pegawai tidak ditemukan.");
                                    return;
                                }

                                reader.Read();

                                // Assign value
                                NipPegawai = reader.GetString(0);
                                NamaLengkap = reader.GetString(2);
                                IdOpd = reader.GetInt16(3);
                                IdGrup = reader.GetInt16(4);
                                Jabatan = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            }

                        }
                        catch (Exception e)
                        {
                            _notifHelper.MsgBoxWarning(@"Gagal load data Pegawai.");
                            DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                        }
                    }
                }
            }
        }

        public bool SimpanDataPegawai(bool newData, int idUser, string login, string namaLengkap, int idOpd,
            int grup, string jabatan, Form form, string pwd = "12345")
        {

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_pegawai", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            // Create password
                            _crypto.CreatePasswordHash(pwd, out var pwdHash, out var pwdSalt);

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_exec", newData ? "N" : "E");
                            cmd.Parameters.AddWithValue("@p_user_id", newData ? (object)DBNull.Value : idUser);
                            cmd.Parameters.AddWithValue("@p_login", login.Trim());
                            cmd.Parameters.AddWithValue("@p_nama", namaLengkap.Trim());
                            cmd.Parameters.AddWithValue("@p_opd_id", idOpd);
                            cmd.Parameters.AddWithValue("@p_grup", grup);
                            cmd.Parameters.AddWithValue("@p_jabatan", jabatan.Trim());
                            cmd.Parameters.AddWithValue("@p_pwd_hash", pwdHash);
                            cmd.Parameters.AddWithValue("@p_pwd_salt", pwdSalt);

                            var result = Convert.ToInt16(cmd.ExecuteScalar());

                            switch (result)
                            {
                                case 0:
                                    _notifHelper.MsgBoxWarning("Gagal simpan data pegawai.");
                                    return false;

                                case 1:
                                    _notifHelper.MsgBoxWarning("Login / NIK Pegawai sudah ada.");
                                    return false;

                                case 2:
                                    return true;
                                default:
                                    _notifHelper.MsgBoxWarning("Gagal simpan data pegawai.");
                                    return false;
                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e);
                            return false;
                        }
                    }
                }
            }
        }

        public bool EditDataPegawai(int idUser, Form form)
        {
            var sql =
                "SELECT login, nama_lengkap, opd_id, grup, jabatan FROM pegawai WHERE user_id = @p_user_id";

            Console.WriteLine(sql);

            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_user_id", idUser);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    _notifHelper.MsgBoxWarning(@"Data Pegawai tidak ditemukan.");
                                    return false;
                                }

                                reader.Read();

                                // Assign value
                                NipPegawai = reader.GetString(0);
                                NamaLengkap = reader.GetString(1);
                                IdOpd = reader.GetInt16(2);
                                IdGrup = reader.GetInt16(3);
                                Jabatan = reader.GetString(4);

                                return true;
                            }

                        }
                        catch (Exception e)
                        {
                            _notifHelper.MsgBoxWarning(@"Gagal load data Pegawai.");
                            DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e);
                            return false;
                        }
                    }
                }
            }

        }
        #endregion

        #region === Data Pengguna / Penduduk ===

        public void GetDataPenduduk(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_pengguna", conn) { CommandType = CommandType.StoredProcedure })
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
                                    DataGridHelper.FormatTable(dataGrid);

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("user_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("raw_tanggal_lahir");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("flg");

                                    IsDataExist = true;
                                }
                                else
                                {
                                    IsDataExist = false;
                                }

                                dataGrid.Visible = dt.Rows.Count > 0;


                            }
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                        }
                    }
                }
            }

        }

        #endregion
    }
}
