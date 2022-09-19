using System;
using System.Windows.Forms;

namespace gov.minahasa.sitimou.Helper.Interfaces
{
    public class WaitCursor : IDisposable
    {
        private readonly Form _srcForm;

        public WaitCursor(Form form, bool appStarting = false)
        {
            // Wait
            Cursor.Current = appStarting ? Cursors.AppStarting : Cursors.WaitCursor;

            if (appStarting)
            {
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                _srcForm = form;
                // form.Enabled = false;


                // Application.UseWaitCursor = true;
            }

            /*
            if (applicationCursor)
            {
                Application.UseWaitCursor = true;
                _srcForm = form;
                form.Enabled = false;
            }
            */
        }

        public void Dispose()
        {
            // Reset
            Cursor.Current = Cursors.Default;
            // _srcForm.Enabled = true;

            // Application.UseWaitCursor = false;
        }
    }
}
