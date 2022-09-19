using System;
using System.Diagnostics;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class DataOpd : Form
    {
        #region === Constructor ===

        private readonly OpdController _controller = new();
        private readonly DatabaseHelper _dbHelper = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _namaOpd;
        private double _gpsLat;
        private double _gpsLng;

        public DataOpd()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataOpd(DataGGC, this);
        }

        #endregion

        #region === FORM ===
        private void DataOpdAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataOpdAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataOpdAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTambah_Click(object sender, EventArgs e)
        {
            var win = new InputOpd();
            win.ShowDialog(this);

            if(win.IsDataSaved) _controller.GetDataOpd(DataGGC, this);
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan diedit.");
                return;
            }

            var win = new InputOpd
            {
                IdOpd = _idData.Value,
                IsEdit = true,
            };
            win.ShowDialog(this);

            _controller.GetDataOpd(DataGGC, this);
        }

        private void ButtonHapus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan dihpaus.");
                return;
            }

            if (_notifHelper.MsgBoxQuestion($"Hapus data OPD: {_namaOpd}?") == DialogResult.No) return;

            var result = _controller.HapusDataOpd(_idData.Value, this);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal hapus data OPD.");
                return;
            }

            // Refresh
            _controller.GetDataOpd(DataGGC, this);
            _idData = null;

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataOpd(DataGGC, this);
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
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, "opd_id");
            _gpsLat = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lat");
            _gpsLng = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lng");
            _namaOpd = DataGridHelper.GetCellValue<string>(DataGGC, "Nama OPD");
        }
        
        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {
                _controller.BindData.Filter = $"([Nama OPD]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Nama Singkat] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Nama Lengkap] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Email] LIKE '%{TextCari.Texts.Trim()}%'";
            }
        }

        
    }
}
