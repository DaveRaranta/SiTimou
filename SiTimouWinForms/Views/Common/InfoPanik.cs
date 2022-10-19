using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.RestApi;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class InfoPanik : Form
    {
        #region === Constructor ===

        // Input
        public int IdLaporan;
        public int IdPelapor;
        public string NamaPelapor;
        public string NikPelapor;
        public string NoTelp;
        public string AlamatPelapor;
        public double GpsLat;
        public double GpsLng;
        


        // Output
        public bool IsDataSaved;
        
        private readonly ImageHelper _imageHelper = new();

        public InfoPanik()
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

        }

        #endregion

        #region === Form ===
        private void DisposisiPanik_Load(object sender, EventArgs e)
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

        #endregion

        
    }
}
