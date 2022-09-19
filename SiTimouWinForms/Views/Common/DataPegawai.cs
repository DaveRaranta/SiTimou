using System;
using System.Diagnostics;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class DataPegawai : Form
    {
        #region === Constructor ===

        private readonly PenggunaController _controller = new();
        private readonly DatabaseHelper _dbHelper = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _nipPegawai;
        private string _namaPegawai;
        private string _status;

        public DataPegawai()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataPegawai(DataGGC, this);
            TsmStatus.Enabled = Globals.UserGrup == "0";
            TsmResetPassword.Enabled = Globals.UserGrup == "0";
        }

        #endregion

        #region === FORM ===
        private void DataPegawaiAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataPegawaiAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataPegawaiAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTambah_Click(object sender, EventArgs e)
        {
            var win = new InputPegawai();
            win.ShowDialog(this);

            if(win.IsDataSaved) _controller.GetDataPegawai(DataGGC, this);
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan diedit.");
                return;
            }

            if (_nipPegawai == "admin")
            {
                _notifHelper.MsgBoxWarning(@"Anda tidak bisa mengubah akun 'admin'.");
                return;
            }

            var win = new InputPegawai
            {
                IdPegawai = _idData.Value,
                IsEdit = true
            };

            win.ShowDialog(this);

            if (win.IsDataSaved) _controller.GetDataPegawai(DataGGC, this);
        }

        private void ButtonHapus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan dihpaus.");
                return;
            }

            if (_nipPegawai == "admin")
            {
                _notifHelper.MsgBoxWarning(@"Anda tidak bisa menghapus akun 'admin'.");
                return;
            }

            if (_notifHelper.MsgBoxQuestion($"Hapus data pegawai a/n {_namaPegawai}?") == DialogResult.No) return;

            var result = _dbHelper.ExecuteSqlString($"DELETE FROM pegawai WHERE user_id = {_idData}");

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal hapus data pegawai.");
                return;
            }

            // Refresh
            _controller.GetDataPegawai(DataGGC, this);
            _idData = null;

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataPegawai(DataGGC, this);
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            cmsMenu.Show(this, ButtonMenu.Left, ButtonMenu.Top + ButtonMenu.Height);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //
        // Menu
        //
        private void TsmStatus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan diubah status.");
                return;
            }

            if (_nipPegawai == "admin")
            {
                _notifHelper.MsgBoxWarning(@"Anda tidak bisa mengubah status akun 'admin'.");
                return;
            }

            
            var sql = _status == "N" 
                ? $"UPDATE pegawai SET flg = 'X' WHERE user_id = {_idData}" 
                : $"UPDATE pegawai SET flg = 'N' WHERE user_id = {_idData}";


            var result = _dbHelper.ExecuteSqlString(sql);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal ubah status pegawai.");
                return;
            }

            _controller.GetDataPegawai(DataGGC, this);
            _idData = null;

        }

        private void TsmResetPassword_Click(object sender, EventArgs e)
        {

        }

        private void TsmExportXls_Click(object sender, EventArgs e)
        {
            if (!DataGGC.Visible) return;

            var result = DataGridHelper.ExportDataGridToXls(DataGGC, Text);

            if (string.IsNullOrEmpty(result)) return;

            Process.Start(result);
        }

        private void TsmExportPdf_Click(object sender, EventArgs e)
        {
            if (!DataGGC.Visible) return;

            var result = DataGridHelper.ExportDataGridToPdf(DataGGC, Text);

            if (string.IsNullOrEmpty(result)) return;

            Process.Start(result);
        }

        #endregion

        #region === Data Grid ===

        private void DataGGC_TableControlCurrentCellActivating(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCurrentCellActivatingEventArgs e)
        {
            e.Inner.ColIndex = 0;
        }

        private void DataGGC_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, "user_id");
            _nipPegawai = DataGridHelper.GetCellValue<string>(DataGGC, "N.I.P");
            _namaPegawai = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Lengkap");
            _status = DataGridHelper.GetCellValue<string>(DataGGC, "flg");

            TsmStatus.Text = _status == "N" ? "Nonaktifkan..." : "Aktifkan...";
        }





        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {
                _controller.BindData.Filter = $"([Nama Lengkap]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[N.I.P] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Jabatan] LIKE '%{TextCari.Texts.Trim()}%'";
            }
        }

        
    }
}
