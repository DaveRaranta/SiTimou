using System;
using System.Reflection;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
//using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using gov.minahasa.sitimou.RestApi;
using gov.minahasa.sitimou.Views;

namespace gov.minahasa.sitimou
{
    public sealed partial class Login : Form
    {

        #region === Constructor ===

        private readonly AuthController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        private bool isLogin;

        public Login()
        {
            InitializeComponent();

            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Functions & Procedures ===

        #endregion
        
        #region === Form ===

        private void Login_Load(object sender, EventArgs e)
        {
            TextNip.Text = RegistryHelper.ReadSettings("last_user");

            // Reset status login
            isLogin = false;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLogin) Application.Exit();
        }

        #endregion

        #region === Button ===

        private void ButtonMasuk_Click(object sender, EventArgs e)
        {
            if (TextNip.Text == string.Empty || TextPassword.Text == string.Empty)
            {
                _notifHelper.MsgBoxWarning("NIP dan Password tidak boleh kosong.");
                return;
                //System.Media.SystemSounds.Exclamation.Play();
            }

            using (new WaitCursor(this))
            {
                try
                {
                    var result = _controller.Login(TextNip.Text.Trim(), TextPassword.Text.Trim());

                    if (!result) throw new Exception("Login gagal. Hubungi admin untuk info selanjutnya.");

                    // Get API Token
                    var resultToken = _controller.GetApiToken(TextNip.Text.Trim(), TextPassword.Text.Trim());
                    if (string.IsNullOrEmpty(resultToken)) throw new Exception("Gagal ambil Token.");

                    // Debug Api
                    Console.WriteLine(@$"[LOGIN] - ApiToken: {resultToken}");

                    Globals.ApiToken = resultToken;
                    RegistryHelper.SaveSettings("last_user", Globals.UserNip);

                    var win = new MainForm();
                    win.Show();

                    // Close form
                    isLogin = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    DebugHelper.ShowError("LOGIN", this.Name, MethodBase.GetCurrentMethod()?.Name, ex);
                }
            }
        }

        private void LabelCopyright_Click(object sender, EventArgs e)
        {
            //var result = new PegawaiController().SimpanDataPegawai(true, 0, "admin", "Admin", 0, 0, "Admin", this, "112345");
        }







        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           //
        }
    }
}
