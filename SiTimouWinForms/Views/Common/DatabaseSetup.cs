using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class DatabaseSetup : Form
    {
        #region === Constructor ===
        

        // Classes
        private readonly NotifHelper _notifHelper = new();

        public DatabaseSetup()
        {
            InitializeComponent();
            // BackColor = Globals.PrimaryBgColor;
            AppHelper.SetBackgroundColor(this.Controls);
        }

        #endregion

        #region === Functions ===

        private bool ValidasiInput()
        {
            try
            {
                if (TextServer.Text.Length == 0) throw new Exception("Isi Nama atau IP server.");
                if (TextPort.Text.Length == 0) throw new Exception("Isi Port server.");
                if (TextNamaDb.Text.Length == 0) throw new Exception("Isi Nama database.");
                if (TextUserDb.Text.Length == 0) throw new Exception("Isi Nama pengguna.");
                if (TextPwdDb.Text.Length == 0) throw new Exception("Isi password database.");

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }

        private bool TestKoneksi()
        {
            using (new WaitCursor(this))
            {
                var connString = $"server={TextServer.Text.Trim()}; " +
                                 $"port={TextPort.Text.Trim()}; " +
                                 $"user id={TextUserDb.Text.Trim()}; " +
                                 $"password={TextPwdDb.Text.Trim()}; " +
                                 $"database={TextNamaDb.Text.Trim()}; " +
                                 "default command timeout=600";

                var conn = new MySqlConnection(connString);

                try
                {
                    conn.Open();

                    return true;
                }
                catch (MySqlException ex)
                {
                    DebugHelper.ShowError("DATABASE SETUP", this.Name, MethodBase.GetCurrentMethod()?.Name, ex);
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }


        #endregion

        #region === Button ===

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            if (!TestKoneksi())
            {
                _notifHelper.MsgBoxWarning(@"Koneksi ke database gagal. Periksa kembali pengaturan database dan coba kembali.");
                ButtonSimpan.Enabled = false;
                return;
            }

            _notifHelper.MsgBoxInfo(@"Tes koneksi berhasil. Simpan pengaturan sebelum melanjutkan.");

            ButtonSimpan.Enabled = true;
        }

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var registry = new RegistryCore()
            {
                SubKey = "SOFTWARE\\Minahasa\\" + Application.ProductName + "\\db"
            };

            registry.Write("db_server", CryptoHelper.EncryptString(TextServer.Text.Trim()), RegistryValueKind.String);
            registry.Write("db_port", CryptoHelper.EncryptString(TextPort.Text.Trim()), RegistryValueKind.String);
            registry.Write("db_catalog", CryptoHelper.EncryptString(TextNamaDb.Text.Trim()), RegistryValueKind.String);
            registry.Write("db_user", CryptoHelper.EncryptString(TextUserDb.Text.Trim()), RegistryValueKind.String);
            registry.Write("db_pwd", CryptoHelper.EncryptString(TextPwdDb.Text.Trim()), RegistryValueKind.String);

            _notifHelper.MsgBoxInfo(@"Pengaturan sudah disimpan. Aplikasi akan direstart.");
            Application.Restart();
            Environment.Exit(0);
        }


        #endregion

        private void TextPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void DatabaseSetup_Load(object sender, EventArgs e)
        {

        }
    }
}
