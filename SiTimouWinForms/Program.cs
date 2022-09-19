using System;
using System.Threading;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou
{
    static class Program
    {
                                                                
        private static readonly Mutex mutex = new Mutex(true, "{d7f58ea2-e06e-4c73-a788-94d9f5127cca}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Cek registry
                var registry = new RegistryCore()
                {
                    SubKey = "SOFTWARE\\Minahasa\\" + Application.ProductName
                };

                if (registry.SubKeyCount() == 0)
                {
                    var win = new DatabaseSetup();
                    win.ShowDialog();
                    //Application.Run(new DatabaseSetup());
                }
                else
                {
                    var login = new Login
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    login.Show();
                    Application.Run();

                    //Application.Run(new Login());
                }

                mutex.ReleaseMutex();

                
            }
            else
            {
                MessageBox.Show(@"Aplikasi MITRa-FS sedang berjalan.", @"MITRa-FS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
