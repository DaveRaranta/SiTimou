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

        private bool ValidasiInput()
        {
            try
            {
                if (TextJudul.Text.Length == 0) throw new Exception("Masukan perihal penanganan.");
                if (TextUraian.Text.Length == 0) throw new Exception("Masukan uraian hasil penanganan.");
                if (TextLampiran.Tag == null) throw new Exception("Pilih foto lampiran.");

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

        private void LabelInfo_Click(object sender, EventArgs e)
        {
            _notifHelper.MsgBoxInfo(_controller.PerihalLaporan);
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = @"Pilih file foto",
                Filter = @"JPG File|*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            TextLampiran.Text = openFileDialog.FileName;
            TextLampiran.Tag = openFileDialog.FileName;
        }

        private async void ButtonBatal_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = await _controller.SimpanProsesLaporan("1", IdLaporan,
                TextJudul.Text.Trim(), TextUraian.Text.Trim(), "B",
                TextLampiran.Tag.ToString(), IdDisposisi, this);

            if (!result) return;

            IsDataSaved = true;
            Close();

        }

        private async void ButtonSimpan_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            var result = await _controller.SimpanProsesLaporan("1", IdLaporan,
                TextJudul.Text.Trim(), TextUraian.Text.Trim(), "S",
                TextLampiran.Tag.ToString(), IdDisposisi, this);

            if (!result) return;

            IsDataSaved = true;
            Close();
        }





        #endregion

        #region === Textbox ===

        private void TextUraian_TextChanged(object sender, EventArgs e)
        {
            if (TextUraian.Text.Length <= 0)
            {
                LabelCount.Text = @"5000";
                return;
            }


            LabelCount.Text = (TextUraian.MaxLength - TextUraian.Text.Length).ToString();
        }

        #endregion




    }
}
