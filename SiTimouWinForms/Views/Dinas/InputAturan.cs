using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class InputAturan : Form
    {
        #region === Constructor ===

        // Input
        public bool IsEdit = false;
        public int IdAturan = 0;

        // Output
        public bool IsDataSaved;

        private readonly InfoController _controller = new();
        private readonly AppHelper _appHelper = new();
        private readonly NotifHelper _notifHelper = new();

        public InputAturan()
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
                //var content = ;
                if (TextJudul.Text.Length == 0) throw new Exception("Masukan nama N.I.P Pegawai.");
                if (HtmlAturan.Document.Body?.InnerText == null) throw new Exception("Masukan isi/uraian aturan.");

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }

        private void InitData()
        {
            this.Text = IsEdit ? @"Edit Aturan" : "Input Data Aturan";
            LabelH1.Text  = IsEdit ? @"Edit Aturan" : "Input Data Aturan";
            
            if(IsEdit) LoadDataForEdit();

        }

        private void LoadDataForEdit()
        {
            // Ambil data
            
            var resultEdit = _controller.GetDetailAturan(IdAturan, this);

            ButtonSimpan.Enabled = resultEdit;

            if (!resultEdit) return;

            TextJudul.Text = _controller.JudulAturan;
            HtmlAturan.Html = _controller.IsiAtruan;
        }

        #endregion

        #region === Form ===
        private void InputPegawai_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion

        #region === TextBox === 
        
        #endregion

        #region === Button ===

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = _controller.SimpanDataAturan(TextJudul.Text, HtmlAturan.DocumentText, this, IdAturan);

            if (!result) return;
            
            IsDataSaved = true;
            Close();


        }

        #endregion

        
    }
}
