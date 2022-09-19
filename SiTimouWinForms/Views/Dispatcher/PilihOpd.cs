using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Dispatcher
{
    public sealed partial class PilihOpd : Form
    {
        #region === Constructor ===
        
        // Output
        public int IdOpd;

        private readonly OpdController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        public PilihOpd()
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
                if (ComboOpd.Text.Length == 0) throw new Exception("Pilih Dinas atau Kelurahan.");
                

                return true;
            }
            catch (Exception e)
            {
                _notifHelper.MsgBoxWarning(e.Message);
                return false;
            }
        }

        private async void InitData()
        {
            await _controller.FillComboDinasKel(ComboOpd, this);
        }

        #endregion

        #region === Form ===
        private void InputDisposisiLaporan_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion
        
        #region === Button ===

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            IdOpd = int.Parse(ComboOpd.SelectedValue.ToString());

            Close();

        }

        #endregion

    }
}
