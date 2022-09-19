using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.RestApi;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou.Views.Dispatcher
{
    public sealed partial class DisposisiLaporan : Form
    {
        #region === Constructor ===

        // Input
        public int IdLaporan;
        public int IdPelapor;
        public string NamaPelapor;
        public string NikPelapor;
        public string NoTelp;
        public string AlamatPelapor;
        public string PerihalLaporan;
        public string IsiLaporan;
        public double GpsLat;
        public double GpsLng;
        


        // Output
        public bool IsDataSaved;

        private readonly LaporanController _controller = new();
        private readonly ImageHelper _imageHelper = new();
        private readonly NotifHelper _notifHelper = new();

        public DisposisiLaporan()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }


        #endregion

        #region === Methode ===
        

        private async void InitData()
        {
            // Set User Info
            LabelNama.Text = NamaPelapor;
            LabelNik.Text = NikPelapor;
            LabelNoTelp.Text = NoTelp;
            LabelAlamat.Text = AlamatPelapor;

            var resultImg = await new PegawaiRest().DownloadFotoProfil(IdPelapor);
            PictureProfil.Image = _imageHelper.BytesToImage(resultImg);

            // Laporan
            var resultAlamat = GeoHelper.FindAddress(GpsLat, GpsLng);
            TextAlamatLapor.Text = resultAlamat.display_name;
            TextPerihal.Text = PerihalLaporan;
            TextIsiLapor.Text = IsiLaporan;

        }

        #endregion

        #region === Form ===
        private void DisposisiLaporan_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion
        
        #region === Button ===

        private void LabelRefresh_Click(object sender, EventArgs e)
        {
            var resultAlamat = GeoHelper.FindAddress(GpsLat, GpsLng);
            TextAlamatLapor.Text = resultAlamat.display_name;
        }

        private void LabelPeta_Click(object sender, EventArgs e)
        {
            var win = new MapViewer
            {
                GpsLatitude = Convert.ToDouble(GpsLat),
                GpsLongitude = Convert.ToDouble(GpsLng),
                JudulForm = "Peta Laporan",
                ToolTipText = NamaPelapor,
            };
            win.Show();
        }

        private void ButtonBatal_Click(object sender, EventArgs e)
        {
            if(_notifHelper.MsgBoxQuestion("Batal laporan ini?") != DialogResult.Yes) return;

            var result = _controller.BatalLaporan("1", IdLaporan, this);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal batal laporan masuk. Hubungi admin untuk info selanjutnya.");
                return;
            }

            IsDataSaved = true;
            Close();

        }

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            var win = new PilihOpd();
            win.ShowDialog(this);

            if (win.IdOpd <= 0) return;

            // Simpan / disposis

            var result = _controller.SimpanLaporanMasuk("1", IdLaporan, win.IdOpd, this);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal disposisi laporan masuk. Hubungi admin untuk info selanjutnya.");
                return;
            }

            IsDataSaved = true;
            Close();
        }




        #endregion

        
    }
}
