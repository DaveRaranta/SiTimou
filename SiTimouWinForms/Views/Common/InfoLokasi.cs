using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GMap.NET;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Properties;
using gov.minahasa.sitimou.RestApi;
using Syncfusion.Windows.Forms.Grid;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class InfoLokasi : Form
    {
        #region === Constructor ===

        // Input
        public int IdData;
        
        private readonly LokasiController _controller = new();
        private readonly AppHelper _appHelper = new();

        public InfoLokasi()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }


        #endregion

        #region === Methode ===


        private async void InitData()
        {
            var result = await _controller.GetDetailLokasi(IdData, this);

            if (!result) Close();

            // isi Text
            TextJenisLokasi.Text = _controller.JenisLokasi;
            TextNamaLokasi.Text = _controller.NamaLokasi;
            TextAlamat.Text = _controller.AlamatLokasi;
            TextDesa.Text = _controller.NamaDesa;
            TextKecamatan.Text = _controller.NamaKecamatan;
            TextNoTelp.Text = _controller.NoTelp;
            TextKet.Text = _controller.Keterangan;
            TextGpsLat.Text = _controller.GpsLat.ToString(CultureInfo.InvariantCulture);
            TextGpsLng.Text = _controller.GpsLng.ToString(CultureInfo.InvariantCulture);

            // Ambil foto
            var foto = await new LokasiRest().DownloadFotoLokasi(IdData);

            if (foto == null) return;

            var img = new ImageHelper().BytesToImage(foto);
            PictureLokasi.Image = img;
        }

        #endregion

        #region === Form ===
        private void InputLokasiPenting_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion

        #region === TextBox & ComboBox === 

        // 
        // TextBox
        //

        private void TextGpsLat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextGpsLat.Text)) e.Handled = true;
        }

        private void TextGpsLng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextGpsLng.Text)) e.Handled = true;
        }

        #endregion

        #region === Button ===

        private void ButtonPeta_Click(object sender, EventArgs e)
        {
            var win = new MapViewer
            {
                GpsLatitude = Convert.ToDouble(_controller.GpsLat),
                GpsLongitude = Convert.ToDouble(_controller.GpsLng),
                JudulForm = "Peta Lokasi",
                ToolTipText = _controller.NamaLokasi,
            };
            win.ShowDialog();
        }

        #endregion


    }
}
