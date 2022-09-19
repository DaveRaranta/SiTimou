using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class InputDesa : Form
    {
        #region === Constructor ===

        // Input
        public bool IsEdit = false;
        //public int IdOpd;

        // Output
        public bool IsDataSaved;

        private readonly OpdController _controller = new();
        private readonly NotifHelper _notifHelper = new();
        private readonly AppHelper _appHelper = new();

        public InputDesa()
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
                if (ComboKecamatan.Text.Length == 0) throw new Exception("Pilih nama Kecamatan.");
                if (TextNamaLengkap.Text.Length == 0) throw new Exception("Masukan nama Desa.");

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
            this.Text = IsEdit ? @"Edit Data Desa" : "Input Data Desa";
            LabelH1.Text  = IsEdit ? @"Edit Data Desa" : "Input Data Desa";

            // fill combo kecamatan.
            await _controller.FillComboDesaKec(ComboKecamatan, this);
        }

        #endregion

        #region === Form ===
        private void InputDesa_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion

        #region === TextBox === 

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

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = _controller.SimpanDataDesaKec("2", TextNamaLengkap.Text.Trim(),
                int.Parse(ComboKecamatan.SelectedValue.ToString()), "-",
                double.Parse(TextGpsLat.Text),
                double.Parse(TextGpsLng.Text),
                this);

            if (!result) return;

            if (IsEdit)
            {
                Close();
            }
            else
            {
                AppHelper.ClearInput(this.Controls);
            }

            IsDataSaved = true;

        }






        #endregion
        
    }
}
