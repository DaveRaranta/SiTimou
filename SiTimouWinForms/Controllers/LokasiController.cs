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
using gov.minahasa.sitimou.RestApi;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace gov.minahasa.sitimou.Controllers
{
    internal class LokasiController : DbConnection
    {
        #region === Constructor ===

        public bool IsDataExist;
        public BindingSource BindData = new();

        // Clases
        private readonly LokasiRest _rest = new();
        private readonly NotifHelper _notifHelper = new();
        private readonly DatabaseHelper _db = new();

        #endregion

        #region === Lokasi Khusus ===

        public void GetDataLokasiPenting(GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_list_lokasi", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_lokasi_id", 0);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"Lokasi Penting [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";
                                    

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("lokasi_id");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("jenis_lokasi");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lat");
                                    dataGrid.TableDescriptor.VisibleColumns.Remove("gps_lng");

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

        public async Task<bool> SimpanLokasiPenting(string jenisLokasi, string namaLokasi, string alamat, int idDesa,
            int idKec, string noTelp, double gpsLat, double gpsLng, string ket, string fileName, Form form)
        {
            using (new WaitCursor(form))
            {
                var resultApi = await _rest.SimpanLokasiKhusus(jenisLokasi, namaLokasi, alamat, idDesa, idKec, noTelp,
                    gpsLat, gpsLng, ket, fileName);

                if (resultApi) return true;

                _notifHelper.MsgBoxWarning(@"Gagal simpan data Lokasi Penting");
                return false;
            }
        }

        public async Task<bool> HapusLokasiKhusus(int idLokasi, string namaLokasi, Form form)
        {
            using (new WaitCursor(form))
            {
                var msg = _notifHelper.MsgBoxQuestion(@$"Hapus data Lokasi: {namaLokasi}?");
                if (msg != DialogResult.Yes) return false;

                var result = await _rest.HapusLokasiKhusus(idLokasi);

                if (result) return true;

                _notifHelper.MsgBoxWarning(@"Gagal hapus data Lokasi.");
                return false;

            }
            
        }

        #endregion
    }
}
