using System;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class PilihPenerima : Form
    {
        #region === Constructor ===
        
        // Output
        public int IdPenerima;

        private readonly PenggunaController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        public PilihPenerima()
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
                if (ComboPenerima.Text.Length == 0) throw new Exception("Pilih nama penerima.");
                

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
            await _controller.FillComboResponder(ComboPenerima, this);
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

            IdPenerima = int.Parse(ComboPenerima.SelectedValue.ToString());

            Close();

        }

        #endregion

    }
}
