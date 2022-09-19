using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class InputKecamatan : Form
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

        public InputKecamatan()
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
                if (TextNamaLengkap.Text.Length == 0) throw new Exception("Masukan nama Kecamatan.");

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

            TextNamaLengkap.Text = _controller.NamaLengkap;
            TextGpsLat.Text = _controller.GpsLat.ToString(Globals.CultureInfo);
            TextGpsLng.Text = _controller.GpsLng.ToString(Globals.CultureInfo);

        }

        private void InitData()
        {
            this.Text = IsEdit ? @"Edit Data Kecamatan" : "Input Data Kecamatan";
            LabelH1.Text  = IsEdit ? @"Edit Data Kecamatan" : "Input Data Kecamatan";

            if (IsEdit) LoadDataForEdit();
        }

        #endregion

        #region === Form ===
        private void InputKecamatan_Load(object sender, EventArgs e)
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

            var result = _controller.SimpanDataDesaKec("1", "-", 0, 
                TextNamaLengkap.Text.Trim(),
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
