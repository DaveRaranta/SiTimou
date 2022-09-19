using System;
using System.Drawing;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Properties;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class InputLokasiPenting : Form
    {
        #region === Constructor ===

        // Input
        public bool IsEdit = false;

        // Output
        public bool IsDataSaved;

        private readonly LokasiController _controller = new();
        private readonly OpdController _opdController = new();
        private readonly NotifHelper _notifHelper = new();
        private readonly AppHelper _appHelper = new();

        public InputLokasiPenting()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }


        #endregion

        #region === Methode ===

        private bool ValidasiInput()
        {
            try
            {
                if (ComboJenis.Text.Length == 0) throw new Exception("Pilih jenis lokasi.");
                if (TextNamaLokasi.Text.Length == 0) throw new Exception("Masukan nama lokasi");
                if (TextAlamat.Text.Length == 0) throw new Exception("Masukan alamat lokasi.");
                if (ComboDesa.Text.Length == 0) throw new Exception("Pilih desa.");
                if (ComboKecamatan.Text.Length == 0) throw new Exception("Pilih Kecamatan.");
                if (PictureLokasi.Tag == null) throw new Exception("Pilih foto lokasi.");
                if (TextGpsLat.Text.Length == 0) throw new Exception("Masukan koordinat Latitude lokasi.");
                if (TextGpsLng.Text.Length == 0) throw new Exception("Masukan koordinat Longitude lokasi.");

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }

        private async void InitData()
        {
            this.Text = IsEdit ? @"Edit Data Lokasi Penting" : "Input Data Lokasi Penting";
            LabelH1.Text  = IsEdit ? @"Edit Data Lokasi Penting" : "Input Data Lokasi Penting";

            // Combo Kecamatan
            await _opdController.FillComboDesaKec(ComboKecamatan, this);
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

        private void TextNoTelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextNoTelp.Text)) e.Handled = true;
        }

        private void TextGpsLat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextGpsLat.Text)) e.Handled = true;
        }

        private void TextGpsLng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextGpsLng.Text)) e.Handled = true;
        }

        // 
        // ComboBox
        //

        private async void ComboKecamatan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboKecamatan.Text.Length <= 0) return;
            await _opdController.FillComboDesaByKec(int.Parse(ComboKecamatan.SelectedValue.ToString()), ComboDesa, this);
            //Console.WriteLine(ComboKecamatan.SelectedValue);
        }

        #endregion

        #region === Button ===

        private void PictureLokasi_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = @"Pilih foto lokasi",
                Filter = @"JPEG File|*.jpg;"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            PictureLokasi.Tag = openFileDialog.FileName;
            PictureLokasi.Image = Image.FromFile(openFileDialog.FileName);

            LabelHapusFoto.Visible = true;
        }

        private void LabelHapusFoto_Click(object sender, EventArgs e)
        {
            PictureLokasi.Tag = null;
            PictureLokasi.Image = Resources.no_image;

            LabelHapusFoto.Visible = false;
        }

        private async void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            try
            {
                // var fileName = "";
                
                var fileName = PictureLokasi.Tag == null ? "" : PictureLokasi.Tag.ToString();

                var result = await _controller.SimpanLokasiPenting(
                    (ComboJenis.SelectedIndex + 1).ToString(), TextNamaLokasi.Text.Trim(), TextAlamat.Text.Trim(),
                    int.Parse(ComboDesa.SelectedValue.ToString()), int.Parse(ComboKecamatan.SelectedValue.ToString()),
                    TextNoTelp.Text.Trim(), double.Parse(TextGpsLat.Text.Trim()), double.Parse(TextGpsLng.Text.Trim()),
                    TextKet.Text.Trim(), fileName, this);

                if (!result) return;

                if (IsEdit)
                {
                    Close();
                }
                else
                {
                    AppHelper.ClearInput(this.Controls);

                    // Reset Picture
                    PictureLokasi.Image = Resources.no_image;
                    PictureLokasi.Tag = null;
                }

                IsDataSaved = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                
            }
        }




        #endregion

        
    }
}
