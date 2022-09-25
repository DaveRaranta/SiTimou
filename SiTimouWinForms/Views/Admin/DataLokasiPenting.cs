using System;
using System.Diagnostics;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou.Views.Admin
{
    //
    // TODO: Tambah detail lokasi saat klik 2x data
    // 

    public sealed partial class DataLokasiPenting : Form
    {
        #region === Constructor ===
        
        private readonly LokasiController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _namaLokasi;
        // private double _gpsLat;
        // private double _gpsLng;

        public DataLokasiPenting()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataLokasiPenting(DataGGC, this);
        }

        #endregion

        #region === FORM ===
        private void DataLokasiPentingAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataLokasiPentingAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataLokasiPentingAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTambah_Click(object sender, EventArgs e)
        {
            var win = new InputLokasiPenting();
            win.ShowDialog();

            if(win.IsDataSaved) _controller.GetDataLokasiPenting(DataGGC, this);
        }

        private async  void ButtonHapus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan dihpaus.");
                return;
            }

            var result = await _controller.HapusLokasiKhusus(_idData.Value, _namaLokasi, this);

            if (!result) return;
            
            _controller.GetDataLokasiPenting(DataGGC, this);
            _idData = null;

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataLokasiPenting(DataGGC, this);
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
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, "lokasi_id");
            _namaLokasi = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Lokasi");
            //_gpsLat = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lat");
            //_gpsLng = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lng");
            //_namaDesakec = DataGridHelper.GetCellValue<string>(DataGGC, JenisData == "1" ? "Nama Kecamatan" : "Nama Desa");
        }

        private void DataGGC_TableControlCellDoubleClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            if (_idData == null) return;

            var win = new InfoLokasi
            {
                IdData = _idData.Value,
            };

            win.ShowDialog();
        }

        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {

                _controller.BindData.Filter = $"([Nama Lokasi]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Alamat] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Desa] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Kecamatan] LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[No. Telp] LIKE '%{TextCari.Texts.Trim()}%'";

            }
        }

        
    }
}
