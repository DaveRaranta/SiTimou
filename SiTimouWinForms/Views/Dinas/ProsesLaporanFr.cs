using System;
using System.Drawing;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using gov.minahasa.sitimou.RestApi;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class ProsesLaporanFr : Form
    {
        #region === Constructor ===

        // Input
        public int IdDisposisi;
        public int IdLaporan;
        public int IdPelapor;
        public string NamaPelapor;
        public string NikPelapor;
        public string NoTelp;
        public string AlamatPelapor;
        public string Status;

        // Output
        public bool IsDataSaved;

        private readonly LaporanController _controller = new();
        private readonly ImageHelper _imageHelper = new();
        private readonly NotifHelper _notifHelper = new();
        private readonly DatabaseHelper _dbHelper = new();

        public ProsesLaporanFr()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }


        #endregion

        #region === Methode ===
        

        private async void InitData()
        {
            using (new WaitCursor(this))
            {
                // Set Kontrol
                LabelDurasi.ForeColor = Status == "N" ? Color.OrangeRed : Color.DodgerBlue;

                // Get data
                await _controller.GetDetailLaporanFr("1", IdLaporan, this);
                
                // Set User Info
                LabelNama.Text = NamaPelapor;
                LabelNik.Text = NikPelapor;
                LabelNoTelp.Text = NoTelp;
                LabelAlamat.Text = AlamatPelapor;

                var resultImg = await new PegawaiRest().DownloadFotoProfil(IdPelapor);
                PictureProfil.Image = _imageHelper.BytesToImage(resultImg);

                // Laporan
                LabelDurasi.Text = _controller.DurasiLaporan;
                TextPerihal.Text = _controller.PerihalLaporan;
            }
        }

        #endregion

        #region === Form ===
        private void DisposisiLaporan_Load(object sender, EventArgs e)
        {
            InitData();
        }

        #endregion
        
        #region === Button ===

        private void ButtonBatal_Click(object sender, EventArgs e)
        {
            if(_notifHelper.MsgBoxQuestion("Batal laporan ini?") != DialogResult.Yes) return;

            var result = _controller.BatalLaporan("3", IdLaporan, this);

            if (!result)
            {
                _notifHelper.MsgBoxWarning("Gagal batal laporan masuk. Hubungi admin untuk info selanjutnya.");
                return;
            }

            IsDataSaved = true;
            Close();

        }

        private void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (_notifHelper.MsgBoxQuestion("Terima dan proses laporan ini?") != DialogResult.Yes) return;

            var sql = $"UPDATE disposisi_er SET flg = 'P' WHERE disposisi_id = {IdDisposisi}";

            var result = _dbHelper.ExecuteSqlString(sql);

            if (!result)
            {
                _notifHelper.MsgBoxError("Gagal proses terima laporan.");
                return;
            }

            IsDataSaved = true;
            Close();
        }





        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TextUraian_TextChanged(object sender, EventArgs e)
        {
            if (TextUraian.Text.Length <= 0)
            {
                LabelCount.Text = @"5000";
                return;
            }
                

            LabelCount.Text = (TextUraian.MaxLength - TextUraian.Text.Length).ToString();
        }
    }
}
