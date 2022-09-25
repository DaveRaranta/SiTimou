using System;
using System.Globalization;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Admin;
using gov.minahasa.sitimou.Views.Common;
using gov.minahasa.sitimou.Views.Dinas;
using gov.minahasa.sitimou.Views.Dispatcher;
using Notification.Wpf;

namespace gov.minahasa.sitimou.Views
{
    public partial class MainForm : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        #region === Constructor ===

        [DllImport("User32.Dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("User32.Dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private const int GWL_EXSTYLE = (-20);
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_FRAMECHANGED = 0x0020;

        private bool _isLogout;
        private readonly CultureInfo _cultureInfo = new System.Globalization.CultureInfo("id-ID");

        public MainForm()
        {
            InitializeComponent();

            RibbonMain.Height = 175;
            PanelMain.BackColor = Globals.PrimaryBgColor;
        }

        public bool ShowPanel
        {
            set => PanelMain.Visible = value;
        }

        #endregion

        #region === Functions & Procedures ===

        private void SetPermission()
        {
            // Ribbon Tab
            RtsAdmin.Visible = Globals.UserGrup is "0" or "1";
            RtsDispatcher.Visible = Globals.UserGrup == "2";
            RtsDinas.Visible = Globals.UserGrup is "3" or "4" or "5";

            // Active tab
            RtsAdmin.Checked = Globals.UserGrup is "0" or "1";
            RtsDispatcher.Checked = Globals.UserGrup == "2";
            RtsDinas.Checked = Globals.UserGrup is "3" or "4" or "5";

            // Ribbon Strip
            TsOperatorOpd.Enabled = Globals.UserGrup is "3";
            TsFrOpd.Enabled = Globals.UserGrup is "4";

            // Button
            SButtonOpdPegawai.Enabled = Globals.UserGrup is "5";

        }

        private void CloseChildForms()
        {
            foreach (var frm in this.MdiChildren)
            {
                frm.Close();
                frm.Dispose();
            }
        }

        public void RemoveChildBorder()
        {
            foreach (Control c in this.Controls)
            {
                if (c is not MdiClient) continue;

                var windowLong = GetWindowLong(c.Handle, GWL_EXSTYLE);
                windowLong &= ~WS_EX_CLIENTEDGE;
                SetWindowLong(c.Handle, GWL_EXSTYLE, windowLong);
                SetWindowPos(c.Handle, IntPtr.Zero, 0, 0, 0, 0,
                    SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
                    SWP_NOOWNERZORDER | SWP_FRAMECHANGED);

                //c.Width += 1;
                break;
            }
        }

        private async Task DoTanggalJam()
        {
            // string ampm;

            while (true)
            {
                var tglJam = DateTime.Now;

                labelJam.Text = tglJam.ToString("HH") + @":" + tglJam.ToString("mm");
                labelHari.Text = tglJam.ToString("dddd", _cultureInfo);
                labelTanggal.Text = tglJam.ToString("dd/MM/yyyy");

                // LabelSideJam.Text = tglJam.ToString("HH") + @":" + tglJam.ToString("mm");
                // LabelSideTanggal.Text = tglJam.ToString("dd/MM/yyyy");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        #endregion

        #region === Forms ===

        private void MainForm_Load(object sender, EventArgs e)
        {
            _ = DoTanggalJam();
            SetPermission();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLogout)
            {
                var win = new Login();
                win.Show();
            }
            else
            {
                Application.Exit();
            }
        }


        #endregion

        #region === Ribbon Menu ===

        // Admin Tab
        private void SButtonAdminPegawai_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataPegawai { MdiParent = this };
            mdi.Show();
        }

        private void SButtonAdminPenduduk_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataPenduduk { MdiParent = this };
            mdi.Show();
        }

        private void SButtonAdminOpd_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataOpd { MdiParent = this };
            mdi.Show();
        }

        private void SButtonAdminKecamatan_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataDesaKec { MdiParent = this, JenisData = "1"};
            mdi.Show();
        }

        private void SButtonAdminDesa_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataDesaKec { MdiParent = this, JenisData = "2" };
            mdi.Show();
        }

        private void SButtonAdminLokasi_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLokasiPenting { MdiParent = this };
            mdi.Show();
        }
        private void SButtonAdminInfo_Click(object sender, EventArgs e)
        {
            var win = new InputPengumuman();
            win.ShowDialog();


        }

        //
        // Dispatcher
        //

        private void SButtonDispaLapor_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLaporanMasuk { MdiParent = this };
            mdi.Show();
        }

        private void SButtonDispaPanik_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataPanikMasuk { MdiParent = this };
            mdi.Show();
        }

        private void SButtonOpLokasi_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLokasi { MdiParent = this };
            mdi.Show();
        }

        //
        // Dinas
        //

        // Dispatch Dinas
        private void SButtonOpdLapor_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLaporanMasukOpd { MdiParent = this };
            mdi.Show();
        }

        private void SButtonOpdPanik_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataPanikMasukOpd { MdiParent = this };
            mdi.Show();
        }

        

        // ER
        private void SButtonErMasuk_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLaporanMasukFr { MdiParent = this };
            mdi.Show();
        }

        // Lain2
        private void SButtonOpdPegawai_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataPegawai { MdiParent = this };
            mdi.Show();
        }

        private void SButtonAturanOpd_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataAturanOpd { MdiParent = this };
            mdi.Show();
        }

        private void SButtonOpdLokasi_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataLokasi { MdiParent = this };
            mdi.Show();
        }

        // 
        // Lain-lain
        // 
        private void SButtonDashboard_Click(object sender, EventArgs e)
        {
            var win = new Dashboard();
            win.ShowDialog();
        }

        private void SButtonInfoAturan_Click(object sender, EventArgs e)
        {
            CloseChildForms();
            var mdi = new DataAturan { MdiParent = this };
            mdi.Show();

        }

        private void SButtonGantiPwd_Click(object sender, EventArgs e)
        {
            var win = new GantiPassword();
            win.ShowDialog();
        }

        private void SButtonLogout_Click(object sender, EventArgs e)
        {
            var result = new NotifHelper().MsgBoxQuestion("Ganti pengguna?");
            if (result != DialogResult.Yes) return;

            _isLogout = true;
            Close();
        }

        #endregion

        /*
            var c = new NotificationContent
            {
                Title = "Sample notification",
                Message =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Type = NotificationType.Error,
                RowsCount = 3, //Will show 3 rows and trim afte
            };

            NotificationManager x = new NotificationManager();
            x.Show(c);
         */


    }
}
