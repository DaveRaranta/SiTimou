using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using AppHelper = gov.minahasa.sitimou.Helper.AppHelper;

namespace gov.minahasa.sitimou.Views.Admin
{
    public sealed partial class InputPengumuman : Form
    {
        #region === Constructor ===
        
        private readonly InfoController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        public InputPengumuman()
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
                if (TextPengumuman.Text.Length == 0) throw new Exception("Masukan isi pengumuman.");

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }
        

        #endregion

        #region === Form ===
        private void InputKecamatan_Load(object sender, EventArgs e)
        {
        }

        #endregion

        
        #region === Button ===

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = _controller.SimpanPengumuman(TextPengumuman.Text.Trim());

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal simpan pengumuman.");
                return;
            }

            Close();


        }






        #endregion

        
    }
}
