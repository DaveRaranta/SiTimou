using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class DataRiwayatLaporan : Form
    {
        #region === Constructor ===

        public string GridTitle;
        public string JenisData;
        public int IdData;
        

        private readonly LaporanController _controller = new();
        private readonly DatabaseHelper _dbHelper = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;

        public DataRiwayatLaporan()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            // _controller.GetDataAturan("0", DataGGC, this);
            _controller.GetDataRiwayatLaporan(JenisData, IdData, GridTitle, DataGGC, this);
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
        

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataRiwayatLaporan(JenisData, IdData, GridTitle, DataGGC, this);
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
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, "laporan_id");
        }

        private void DataGGC_TableControlCellDoubleClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            
        }

        #endregion

        private void TextCari__TextChanged(object sender, EventArgs e)
        {
            if (DataGGC.Visible)
            {
                _controller.BindData.Filter = $"([Nama Pelapor]) LIKE '%{TextCari.Texts.Trim()}%' OR " +
                                              $"[Tentang] LIKE '%{TextCari.Texts.Trim()}%'";
            }
        }

        
    }
}
