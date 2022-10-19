using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using gov.minahasa.sitimou.RestApi;
using gov.minahasa.sitimou.Views.Common;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class InfoLaporanFr : Form
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

        private readonly string _tempFolder = $"{Path.GetTempPath()}{Path.GetRandomFileName()}";

        public InfoLaporanFr()
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
                ButtonSimpan.Enabled = Status == "N";
                ButtonBatal.Enabled = Status == "N";
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
                var resultAlamat = GeoHelper.FindAddress(_controller.GpsLat, _controller.GpsLng);

                LabelDurasi.Text = _controller.DurasiLaporan;
                TextAlamatLapor.Text = resultAlamat.display_name;
                TextPerihal.Text = _controller.PerihalLaporan;
                TextIsiLapor.Text = _controller.IsiLaporan;
            }
        }

        #endregion

        #region === Form ===
        private void DisposisiLaporan_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InfoLaporanFr_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(_tempFolder)) Directory.Delete(_tempFolder, true);
        }

        #endregion

        #region === Button ===

        private void LabelRefresh_Click(object sender, EventArgs e)
        {
            var resultAlamat = GeoHelper.FindAddress(_controller.GpsLat, _controller.GpsLng);
            TextAlamatLapor.Text = resultAlamat.display_name;
        }

        private void LabelPeta_Click(object sender, EventArgs e)
        {
            var win = new MapViewer
            {
                GpsLatitude = Convert.ToDouble(_controller.GpsLat),
                GpsLongitude = Convert.ToDouble(_controller.GpsLng),
                JudulForm = "Peta Laporan",
                ToolTipText = NamaPelapor,
            };
            win.Show();
        }

        private void PictureFoto_Click(object sender, EventArgs e)
        {
            _controller.DownloadFotoLaporan(_tempFolder, IdLaporan, this);
        }

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

        
    }
}
