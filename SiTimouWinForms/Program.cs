using System;
using System.Threading;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou
{
    static class Program
    {
                                                                
        private static readonly Mutex mutex = new Mutex(true, "{9f748845-4af9-471d-a6b8-63f2477eada7}");

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
                MessageBox.Show(@"Aplikasi SI-TIMOU sedang berjalan.", @"SI-TIMOU", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
