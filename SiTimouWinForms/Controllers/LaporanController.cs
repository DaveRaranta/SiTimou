using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using gov.minahasa.sitimou.RestApi;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace gov.minahasa.sitimou.Controllers
{
    internal class LaporanController : DbConnection
    {
        #region === Constructor ===

        public bool IsDataExist;
        public BindingSource BindData = new();

        // Clases
        private readonly LaporanRest _rest = new();
        private readonly NotifHelper _notifHelper = new();

        // Properties
        public int IdLaporan { get; private set; }
        public int IdPelapor { get; private set; }
        public string TanggalLaporan { get; private set; }
        public string NamaPelapor { get; private set; }
        public string NikPelapor { get; private set; }
        public string NoTelp { get; private set; }
        public string AlamatPelapor { get; private set; }
        public string  DurasiLaporan { get; private set; }
        public string PerihalLaporan { get; private set; }
        public string IsiLaporan { get; private set; }
        public double GpsLat { get; private set; }
        public double GpsLng { get; private set; }
        public string StatusLaporan { get; private set; }

        // View?



        #endregion

        #region === Common ===

        public bool BatalLaporan(string posisi, int idData, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_batal_laporan", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_posisi", posisi);
                            cmd.Parameters.AddWithValue("@p_data_id", idData);
                            cmd.Parameters.AddWithValue("@p_user_id", Globals.UserId);

                            var result = cmd.ExecuteNonQuery();

                            return true;
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

        public bool BatalPanik(string posisi, int idData, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_batal_panik", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_posisi", posisi);
                            cmd.Parameters.AddWithValue("@p_data_id", idData);
                            cmd.Parameters.AddWithValue("@p_user_id", Globals.UserId);

                            var result = cmd.ExecuteNonQuery();

                            return true;
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

        public async void DownloadFotoLaporan(string tempFolder, int idLaporan, Form form, bool openFile = true)
        {

            using (new WaitCursor(form))
            {
                try
                {
                    var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

                    // Cek folder
                    if (!Directory.Exists(tempFolder)) Directory.CreateDirectory(tempFolder);
                    var destFn = Path.Combine(tempFolder, $"{timestamp}.jpg");

                    // Download File
                    var result = await _rest.DownloadFotoLaporan(idLaporan);

                    if (result == null)
                    {
                        _notifHelper.MsgBoxWarning(@"Gagal download foto laporan.");
                        return;
                    }

                    // Proses file
                    var saveFn = new FileHelper().BytesToFile(result, destFn);

                    if (!saveFn)
                    {
                        _notifHelper.MsgBoxWarning(@"Gagal buka foto laporan.");
                        return;
                    }

                    if (openFile)
                    {
                        Process.Start(destFn);
                    }
                    else
                    {
                        var fileExt = Path.GetExtension(destFn).Replace(".", "");
                        var saveDialog = new SaveFileDialog
                        {
                            Title = @"Simpan file lampiran",
                            Filter = @$"{fileExt.ToUpper()} file|*.{fileExt}",
                            DefaultExt = fileExt,
                            AddExtension = true
                        };

                        if (saveDialog.ShowDialog() == DialogResult.OK && saveDialog.CheckPathExists)
                        {
                            File.Copy(destFn, saveDialog.FileName, true);
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugHelper.ShowError("LAPORAN", @"LaporanController", MethodBase.GetCurrentMethod()?.Name, e);
                }
            }

        }

        public async  Task<bool> GetDetailLaporan(int idLaporan, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_detail_laporan_by_id", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_data", "1");
                            cmd.Parameters.AddWithValue("@p_laporan_id", idLaporan);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows) return false;

                                await reader.ReadAsync();

                                IdLaporan = reader.GetInt32(0);
                                TanggalLaporan = reader.GetString(1);
                                NamaPelapor = reader.GetString(2);
                                NikPelapor = reader.GetString(3);
                                NoTelp = reader.GetString(4);
                                PerihalLaporan = reader.GetString(5);
                                IsiLaporan = reader.GetString(6);
                                GpsLat = reader.GetDouble(10);
                                GpsLng = reader.GetDouble(11);
                                StatusLaporan = reader.GetString(12);
                                IdPelapor = reader.GetInt32(13);

                                AlamatPelapor = $"{reader.GetString(7)}, {reader.GetString(8)}, {reader.GetString(9)}";

                                return true;

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

        public async Task<bool> GetDetailPanik(int idLaporan, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_detail_laporan_by_id", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_data", "2");
                            cmd.Parameters.AddWithValue("@p_laporan_id", idLaporan);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (!reader.HasRows) return false;

                                await reader.ReadAsync();

                                IdLaporan = reader.GetInt32(0);
                                TanggalLaporan = reader.GetString(1);
                                NamaPelapor = reader.GetString(2);
                                NikPelapor = reader.GetString(3);
                                NoTelp = reader.GetString(4);
                                GpsLat = reader.GetDouble(8);
                                GpsLng = reader.GetDouble(9);
                                StatusLaporan = reader.GetString(10);
                                IdPelapor = reader.GetInt32(11);

                                AlamatPelapor = $"{reader.GetString(5)}, {reader.GetString(6)}, {reader.GetString(7)}";

                                return true;

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

        #endregion

        #region === Dispatcher ===

        public void GetDataLaporanMasuk(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_laporan_masuk", conn) { CommandType = CommandType.StoredProcedure })
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
                                        @$"Laporan Masyarakat [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";
                                    
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("laporan_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("user_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nik");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("alamat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_desa");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_kecamatan");

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

        public void GetDataPanikMasuk(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_panik_masuk", conn) { CommandType = CommandType.StoredProcedure })
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
                                        @$"DARURAT (PANIK) [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("laporan_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("user_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nik");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("alamat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_desa");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_kecamatan");

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

        public bool SimpanLaporanMasuk(string jenisLaporan, int idLaporan, int idOpd, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_disposisi", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_laporan", jenisLaporan);
                            cmd.Parameters.AddWithValue("@p_laporan_id", idLaporan);
                            cmd.Parameters.AddWithValue("@p_opd_id", idOpd);
                            cmd.Parameters.AddWithValue("@p_user_id", Globals.UserId);

                            var result = cmd.ExecuteNonQuery();

                            return true;
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

        #endregion

        #region === Dispatcher OPD ===

        public void GetDataDisposisiOpd(string jenisLaporan, GridGroupingControl dataGrid, Form form)
        {
            var title = jenisLaporan == "1" ? "Laporan Masyarakat" : "RURAT (PANIK)";
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_disposisi_opd", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_laporan", jenisLaporan);
                            cmd.Parameters.AddWithValue("@p_opd_id", Globals.UserOpdId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"{title} [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("laporan_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("user_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nik");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("alamat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_desa");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nama_kecamatan");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("id_pelapor");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("disposisi_id");

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
        
        public bool SimpanLaporanOpd(string jenisLaporan, int idLaporan, int idPenerima, int idDisposisiOpd, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_disposisi_opd", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_laporan", jenisLaporan);
                            cmd.Parameters.AddWithValue("@p_laporan_id", idLaporan);
                            cmd.Parameters.AddWithValue("@p_opd_id", Globals.UserOpdId);
                            cmd.Parameters.AddWithValue("@p_penerima_id", idPenerima);
                            cmd.Parameters.AddWithValue("@p_pengirim_id", Globals.UserId);
                            cmd.Parameters.AddWithValue("@p_disposisi_id", idDisposisiOpd);

                            var result = cmd.ExecuteNonQuery();

                            // Send Notifikasi
                            _rest.SendNotifikasi(idPenerima, idLaporan, jenisLaporan);

                            return true;
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

        #endregion

        #region === First Responder ===

        public void GetDataFirstResponder(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_disposisi_fr", conn) { CommandType = CommandType.StoredProcedure })
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
                                        @$"Laporan Masuk [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("pelapor_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("laporan_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("disposisi_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("jenis_laporan");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("nik");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("flg");

                                    DataGridHelper.FormatTableDisposisiFr(dataGrid);
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

        public async Task GetDetailLaporanFr(string jenisLaporan, int idLaporan, Form form)
        {
            using (var conn = GetDbConnection())
            {
                using (var cmd = new MySqlCommand("sp_detail_laporan_fr", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        conn.Open();

                        cmd.Parameters.AddWithValue("@p_jenis_laporan", jenisLaporan);
                        cmd.Parameters.AddWithValue("@p_laporan_id", idLaporan);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                            {
                                _notifHelper.MsgBoxWarning(@"Data laporan tidak ditemukan.");
                                return;
                            }

                            reader.Read();

                            // Assign value
                            TanggalLaporan = reader.GetString(0);
                            DurasiLaporan = reader.GetString(1);

                            if (jenisLaporan == "1")
                            {
                                PerihalLaporan = reader.GetString(2);
                                IsiLaporan = reader.GetString(3);
                                GpsLat = reader.GetDouble(4);
                                GpsLng = reader.GetDouble(5);
                            }
                            else
                            {
                                GpsLat = reader.GetDouble(2);
                                GpsLng = reader.GetDouble(3);
                            }


                        }

                    }
                    catch (Exception e)
                    {
                        _notifHelper.MsgBoxWarning(@"Gagal load data Pegawai.");
                        DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e);
                    }
                }
            }
            
        }

        public async Task<bool> SimpanProsesLaporan(string jenisLaporan, int idLaporan, string judul,
            string uraian, string status, string fileName, int idDisposisi, Form form)
        {
            using (new WaitCursor(form))
            {
                var resultApi = await _rest.SimpanProsesLaporan( jenisLaporan, idLaporan, judul, 
                    uraian, status, fileName, idDisposisi);

                if (resultApi) return true;

                _notifHelper.MsgBoxWarning(@"Gagal simpan Proses Laporan");
                return false;
            }
        }

        #endregion

        #region === Riwayat Laporan ===

        public void GetDataRiwayatLaporan(string jenisData, int idData, string gridTitle, GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_riwayat_laporan", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_jenis_data", jenisData);
                            cmd.Parameters.AddWithValue("@p_data_id", idData);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"{gridTitle} [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("laporan_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("disposisi_id");

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

        #endregion

    }
}
