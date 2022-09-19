using System;
using System.Diagnostics;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class DataDesaKec : Form
    {
        #region === Constructor ===

        public string JenisData;

        private readonly OpdController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _namaDesakec;
        private double _gpsLat;
        private double _gpsLng;

        public DataDesaKec()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            this.Text = JenisData == "1"
                ? "Data Kecamatan"
                : "Data Desa";

            _controller.GetDataDesaKecamatan(JenisData, DataGGC, this);
        }

        #endregion

        #region === FORM ===
        private void DataDesaKecAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataDesaKecAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataDesaKecAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTambah_Click(object sender, EventArgs e)
        {
            if (JenisData == "1")
            {
                var winK = new InputKecamatan();
                winK.ShowDialog();
                if (winK.IsDataSaved) _controller.GetDataDesaKecamatan(JenisData, DataGGC, this);

            } else if (JenisData == "2")
            {
                var winD = new InputDesa();
                winD.ShowDialog();
                if (winD.IsDataSaved) _controller.GetDataDesaKecamatan(JenisData, DataGGC, this);
            }
        }

        private void ButtonHapus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan dihpaus.");
                return;
            }

            var qPesan = JenisData == "1"
                ? $"Hapus data Kecamatan: {_namaDesakec}?"
                : $"Hapus data Desa: {_namaDesakec}?";

            if (_notifHelper.MsgBoxQuestion(qPesan) == DialogResult.No) return;

            
            var result = _controller.HapusDataDesaKecamatan(JenisData, _idData.Value, this);

            if (!result)
            {
                var ePesan = JenisData == "1"
                    ? "Gagal hapus data Kecamatan."
                    : "Gagal hapus data Desa.";

                _notifHelper.MsgBoxWarning(ePesan);
                return;
            }
            

            // Refresh
            _controller.GetDataDesaKecamatan(JenisData, DataGGC, this);
            _idData = null;

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataDesaKecamatan(JenisData, DataGGC, this);
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
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, JenisData == "1" ? "kecamatan_id": "desa_id");
            _gpsLat = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lat");
            _gpsLng = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lng");
            _namaDesakec = DataGridHelper.GetCellValue<string>(DataGGC, JenisData == "1" ? "Nama Kecamatan" : "Nama Desa");
        }
        
        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {
                if (JenisData == "1")
                {
                    _controller.BindData.Filter = $"[Nama Kecamatan] LIKE '%{TextCari.Texts.Trim()}%'";

                } else if (JenisData == "2")
                {
                    _controller.BindData.Filter = $"([Nama Desa]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                                  $"[Kecamatan] LIKE '%{TextCari.Texts.Trim()}%'";
                }
                
            }
        }

        
    }
}
