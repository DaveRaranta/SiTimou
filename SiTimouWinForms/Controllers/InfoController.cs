using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;

namespace gov.minahasa.sitimou.Controllers
{
    internal class InfoController : DbConnection
    {
        #region === Constructor ===

        public bool IsDataExist;
        public BindingSource BindData = new();

        // Property
        public string TanggalAturan { get; private set; }
        public string JudulAturan { get; private set; }
        public string IsiAtruan { get; private set; }
        public string OpdAturan { get; private set; }
        public string NamaUserAturan { get; private set; }

        // 
        private readonly NotifHelper _notifHelper = new();

        #endregion

        #region === Info Aturan ===

        public void GetDataAturan(string jenis, GridGroupingControl dataGrid, Form form)
        {
            using (new WaitCursor(form))
            {
                var dt = new DataTable();

                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_data_aturan", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@p_jenis", jenis);
                            cmd.Parameters.AddWithValue("@p_opd_id", Globals.UserOpdId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                BindData.DataSource = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    dataGrid.DataSource = BindData;
                                    dataGrid.GridGroupDropArea.DragColumnHeaderText =
                                        @$"Info Aturan [ Total: {dt.Rows.Count} ]. Tarik judul kolom ke area ini untuk grup.";

                                    dataGrid.TableDescriptor.VisibleColumns.Remove("aturan_id");

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

        public bool SimpanDataAturan(string judul, string content, Form form, int idData = 0 )
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_save_aturan", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {

                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_exec", idData == 0 ? "N" : "E");
                            cmd.Parameters.AddWithValue("@p_aturan_id", idData == 0 ? DBNull.Value : idData);
                            cmd.Parameters.AddWithValue("@p_opd_id", Globals.UserOpdId);
                            cmd.Parameters.AddWithValue("@p_user_id", Globals.UserId);
                            cmd.Parameters.AddWithValue("@p_judul", judul);
                            cmd.Parameters.AddWithValue("@p_isi", content);

                            var result = cmd.ExecuteNonQuery();

                            return true;
                        }
                        catch (Exception e)
                        {
                            DebugHelper.ShowError("INFO", @"InfoController", MethodBase.GetCurrentMethod()?.Name, e);
                            return false;
                        }
                    }
                }
            }
        }

        public bool GetDetailAturan(int idAturan, Form form)
        {
            using (new WaitCursor(form))
            {
                using (var conn = GetDbConnection())
                {
                    using (var cmd = new MySqlCommand("sp_detail_aturan", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@p_aturan_id", idAturan);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    _notifHelper.MsgBoxWarning(@"Data Aturan tidak ditemukan.");
                                    return false;
                                }

                                reader.Read();

                                // Assign value

                                TanggalAturan = reader.GetString(0);
                                JudulAturan = reader.GetString(1);
                                IsiAtruan = reader.GetString(2);
                                OpdAturan = reader.GetString(3);
                                NamaUserAturan= reader.GetString(4);

                                return true;
                            }

                        }
                        catch (Exception e)
                        {
                            _notifHelper.MsgBoxWarning(@"Gagal load data Aturan.");
                            DebugHelper.ShowError(@"INFO", @"InfoController", MethodBase.GetCurrentMethod()?.Name, e, null, true);
                            return false;
                        }
                    }
                }
            }
        }

        public bool HapusDataAturan(int idIdData, Form form)
        {
            const string sql = $"DELETE FROM info_aturan WHERE aturan_id = @p_data_id";

            using (new WaitCursor(form))
            {
                return new DatabaseHelper().ExecuteSqlStringWithParm(sql, "p_data_id", "idData");
            }
        }

        #endregion

        #region === Pengumuman ===

        public bool SimpanPengumuman(string pengumuman)
        {
            const string sql = @"UPDATE mstbl SET TBTXT1 = @p_pengumuman WHERE TBCOD = 'APP' AND TBKEY = 'HOME' AND TBTXT0 = 'INFO'";

            var result = new DatabaseHelper().ExecuteSqlStringWithParm(sql, "p_pengumuman", pengumuman);

            return result;


        }


        #endregion
    }
}
