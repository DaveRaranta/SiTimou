using System;
using System.Diagnostics;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class DataPenduduk : Form
    {
        #region === Constructor ===

        private readonly PenggunaController _controller = new();
        private readonly DatabaseHelper _dbHelper = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _namaPengguna;
        private string _status;

        public DataPenduduk()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataPenduduk(DataGGC, this);
        }

        #endregion

        #region === FORM ===
        private void DataPendudukAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataPendudukAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataPendudukAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataPenduduk(DataGGC, this);
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


            var sql = _status == "N" 
                ? $"UPDATE pengguna SET flg = 'X' WHERE user_id = {_idData}" 
                : $"UPDATE pengguna SET flg = 'N' WHERE user_id = {_idData}";


            var result = _dbHelper.ExecuteSqlString(sql);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal ubah status penduduk.");
                return;
            }

            _controller.GetDataPenduduk(DataGGC, this);
            _idData = null;

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
            _namaPengguna = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Lengkap");
            _status = DataGridHelper.GetCellValue<string>(DataGGC, "flg");

            TsmStatus.Text = _status == "N" ? "Nonaktifkan..." : "Aktifkan...";
        }
        

        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {
                _controller.BindData.Filter = $"([Nama Lengkap]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[N.I.K] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Umur] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Alamat] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Desa] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Kecamatan] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[No. Telp] LIKE '%{TextCari.Texts.Trim()}%'";
            }
        }

        
    }
}
