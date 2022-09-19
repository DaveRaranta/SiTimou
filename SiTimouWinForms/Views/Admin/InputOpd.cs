using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class InputOpd : Form
    {
        #region === Constructor ===

        // Input
        public bool IsEdit = false;
        public int IdOpd;

        // Output
        public bool IsDataSaved;

        private readonly OpdController _controller = new();
        private readonly NotifHelper _notifHelper = new();
        private readonly AppHelper _appHelper = new();

        public InputOpd()
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
                if (ComboJenis.Text.Length == 0) throw new Exception("Pilih Dinas atau Kelurahan.");
                if (TextNamaSingkat.Text.Length == 0) throw new Exception("Masukan nama singkat OPD");
                if (TextNamaLengkap.Text.Length == 0) throw new Exception("Masukan nama lengkap OPD.");

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }

        private void LoadDataForEdit()
        {
            // Ambil data
            var resultEdit = _controller.EditDataOpd(IdOpd, this);

            ButtonSimpan.Enabled = resultEdit;

            if (!resultEdit) return;

            ComboJenis.SelectedIndex = _controller.JenisOpd;
            TextNamaSingkat.Text = _controller.NamaSingkat;
            TextNamaLengkap.Text = _controller.NamaLengkap;
            TextEmail.Text = _controller.Email;
            TextGpsLat.Text = _controller.GpsLat.ToString(Globals.CultureInfo);
            TextGpsLng.Text = _controller.GpsLng.ToString(Globals.CultureInfo);

        }

        private void InitData()
        {
            this.Text = IsEdit ? @"Edit Data OPD" : "Input Data OPD";
            LabelH1.Text  = IsEdit ? @"Edit Data OPD" : "Input Data OPD";

            if(IsEdit) LoadDataForEdit();
        }

        #endregion

        #region === Form ===
        private void InputOpd_Load(object sender, EventArgs e)
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

            var result = _controller.SimpanDataOpd(
                !IsEdit,
                IdOpd,
                ComboJenis.SelectedIndex.ToString(),
                TextNamaSingkat.Text,
                TextNamaLengkap.Text,
                double.Parse(TextGpsLat.Text),
                double.Parse(TextGpsLng.Text),
                TextEmail.Text,
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
