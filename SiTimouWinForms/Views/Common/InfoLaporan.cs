using System;
using System.IO;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.RestApi;
using gov.minahasa.sitimou.Views.Dispatcher;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class InfoLaporan : Form
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

        private readonly string _tempFolder = $"{Path.GetTempPath()}{Path.GetRandomFileName()}";

        public InfoLaporan()
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

        private void DisposisiLaporan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(_tempFolder)) Directory.Delete(_tempFolder, true);
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

        private void PictureFoto_Click(object sender, EventArgs e)
        {
            _controller.DownloadFotoLaporan(_tempFolder, IdLaporan, this);
        }

        #endregion

        

        
    }
}
