using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class GantiPassword : Form
    {
        #region === Constructor ===

        public int UserId;

        private readonly AuthController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        public GantiPassword()
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
                // cek textbox
                if (TextPwdLama.Text.Length == 0) throw new Exception("Masukan password lama anda.");
                if (TextPwdBaru.Text.Length == 0) throw new Exception("Masukan password baru anda.");
                if (TextPwdKonfirm.Text.Length == 0) throw new Exception("Masukan konfirmasi password baru anda.");

                // Cek pwd lama
                if (!_controller.CheckPassword(TextPwdLama.Text.Trim())) throw new Exception("Password lama anda belum benar.");

                // Cek password baru
                if (TextPwdBaru.Text.Length < 7) throw new Exception("Password baru anda anda harus minimal 7 huruf.");
                if (TextPwdLama.Text.Trim() == TextPwdBaru.Text.Trim()) throw new Exception("Password baru tidak boleh sama dengan password lama anda.");
                if (TextPwdBaru.Text.Trim() != TextPwdKonfirm.Text.Trim()) throw new Exception("Password baru anda belum cocok.");



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

        private void GantiPasswordForm_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region === Buttons ===

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = _controller.UpdatePassword(TextPwdKonfirm.Text.Trim());

            if (result) Close();

            _notifHelper.MsgBoxWarning("Gagal ganti password anda.");

        }

        #endregion



    }
}
