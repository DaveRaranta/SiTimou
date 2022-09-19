using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Dispatcher;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class DataPanikMasukOpd : Form
    {
        #region === Constructor ===

        private readonly LaporanController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;

        public DataPanikMasukOpd()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataDisposisiOpd("2",DataGGC, this);

            
            _ = DoTanggalJam();
        }

        private async Task DoTanggalJam()
        {
            // string ampm;

            while (true)
            {
                var tglJam = DateTime.Now;

                LabelJam.Text = tglJam.ToString("HH") + @":" + tglJam.ToString("mm");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        #endregion

        #region === FORM ===
        private void DataPanikMasuk_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataPanikMasuk_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataPanikMasuk_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTeruskan_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan teruskan.");
                return;
            }

            var alamat = $"{DataGridHelper.GetCellValue<string>(DataGGC, "alamat")}, " +
                         $"{DataGridHelper.GetCellValue<string>(DataGGC, "nama_desa")}, " +
                         $"{DataGridHelper.GetCellValue<string>(DataGGC, "nama_kecamatan")}";

            var win = new DisposisiPanikOpd
            {
                IdLaporan = _idData.Value,
                IdDisposisiOpd = DataGridHelper.GetCellValue<int>(DataGGC, "disposisi_id"),
                IdPelapor = DataGridHelper.GetCellValue<int>(DataGGC, "id_pelapor"),
                NamaPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Pelapor"),
                NikPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "nik"),
                NoTelp = DataGridHelper.GetCellValue<string>(DataGGC, "Nomor Telp"),
                AlamatPelapor = alamat,
                GpsLat = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lat"),
                GpsLng = DataGridHelper.GetCellValue<double>(DataGGC, "gps_lng"),
            };

            win.ShowDialog();

            if(win.IsDataSaved) _controller.GetDataDisposisiOpd("2", DataGGC, this);
        }

        private void ButtonHapus_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan dihpaus.");
                return;
            }

            if (_notifHelper.MsgBoxQuestion("Batal laporan ini?") != DialogResult.Yes) return;

            var result = _controller.BatalPanik("2", _idData.Value, this);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal batal laporan PANIK. Hubungi admin untuk info selanjutnya.");
                return;
            }

            _controller.GetDataDisposisiOpd("2", DataGGC, this);
            _idData = null;

        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataDisposisiOpd("2", DataGGC, this);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
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
            if (_idData == null) return;

            Console.WriteLine(_idData.Value);

            ButtonTambah.PerformClick();
        }

        #endregion


    }
}
