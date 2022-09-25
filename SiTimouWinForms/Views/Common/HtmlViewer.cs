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

namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class HtmlViewer : Form
    {
        #region === Constructor ===

        // Input
        public string FormTitle;
        public string ContentTitle;
        public string HtmlContent;
        
        private readonly NotifHelper _notifHelper = new();

        public HtmlViewer()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }


        #endregion

        #region === Methode ===
        

        private void InitData()
        {
            Text = FormTitle;
            LabelH1.Text = FormTitle;
            LabelJudul.Text = ContentTitle;
            HtmlUiViewer.LoadFromString(HtmlContent);
        }

        #endregion

        #region === Form ===
        private void InputPegawai_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion

    }
}
