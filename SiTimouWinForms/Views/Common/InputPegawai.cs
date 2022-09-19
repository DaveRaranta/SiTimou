using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class InputPegawai : Form
    {
        #region === Constructor ===

        // Input
        public bool IsEdit = false;
        public int IdPegawai;

        // Output
        public bool IsDataSaved;

        private readonly PenggunaController _controller = new();
        private readonly OpdController _opdController = new();
        private readonly AppHelper _appHelper = new();
        private readonly NotifHelper _notifHelper = new();

        public InputPegawai()
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
                if (TextNip.Text.Length == 0) throw new Exception("Masukan nama N.I.P Pegawai.");
                if (TextNama.Text.Length == 0) throw new Exception("Masukan nama lengkap Pegawai.");
                if (ComboOpd.Text.Length == 0) throw new Exception("Pilih OPD pegawai.");
                if (ComboGrup.Text.Length == 0) throw new Exception("Pilih grup pegawai.");
                if (TextJabatan.Text.Length == 0) throw new Exception("Masukan jabatan Pegawai.");

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
            var resultEdit = _controller.EditDataPegawai(IdPegawai, this);

            ButtonSimpan.Enabled = resultEdit;

            if (!resultEdit) return;

            TextNip.Text = _controller.NipPegawai;
            TextNama.Text = _controller.NamaLengkap;
            ComboOpd.SelectedValue = _controller.IdOpd.ToString();
            ComboGrup.SelectedIndex = _controller.IdGrup;
            TextJabatan.Text = _controller.Jabatan;

        }

        private async void InitData()
        {
            this.Text = IsEdit ? @"Edit Data Pegawai" : "Input Data Pegawai";
            LabelH1.Text  = IsEdit ? @"Edit Data Pegawai" : "Input Data Pegawai";

            await _opdController.FillComboDinasKel(ComboOpd, this);

            if(IsEdit) LoadDataForEdit();

            ComboOpd.Enabled = Globals.UserGrup == "0";
        }

        #endregion

        #region === Form ===
        private void InputPegawai_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion

        #region === TextBox === 
        private void TextNip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_appHelper.IsNumber(e.KeyChar, TextNip.Text)) e.Handled = true;
        }

        #endregion

        #region === Button ===

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = _controller.SimpanDataPegawai(!IsEdit, IdPegawai, TextNip.Text.Trim(), TextNama.Text.Trim(),
                int.Parse(ComboOpd.SelectedValue.ToString()), ComboGrup.SelectedIndex, TextJabatan.Text.Trim(), this);

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
