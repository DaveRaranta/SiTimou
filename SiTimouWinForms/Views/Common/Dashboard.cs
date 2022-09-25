using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using gov.minahasa.sitimou.Helper.WeatherWebAPI;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using Color = System.Drawing.Color;

namespace gov.minahasa.sitimou.Views.Common
{
    public partial class Dashboard : Form
    {
        #region === Constructor ===

        // Input


        private readonly DashboardController _controller = new();
        private readonly CultureInfo _cultureInfo = new ("id-ID");

        private double _interval = 1;

        

        // Alram
        

        public Dashboard()
        {
            InitializeComponent();
        }

        #endregion

        #region === Functions and Procedures ===

        private void InitData()
        {
            _controller.GetDataJumlah(LabelWarningLaporan, LabelWarningPanik);

            LabelLaporanMasuk.Text = _controller.TotLaporanMasuk.ToString();
            LabelLaporanMasukHari.Text = _controller.TotLaporanMasukHari.ToString();
            LabelPanikMasuk.Text = _controller.TotPanikMasuk.ToString();
            LabelPanikMasukHari.Text = _controller.TotPanikMasukHari.ToString();
            LabelTotalMasuk.Text = _controller.TotMasuk.ToString();
            LabelTotalLaporan.Text = _controller.TotLaporan.ToString();
            LabelTotalPanik.Text = _controller.TotPanik.ToString();

            new AppHelper().SoftBlink(LabelWarningLaporan, Color.OrangeRed, Color.FromArgb(255, 39, 43, 74), 2000, false);
            new AppHelper().SoftBlink(LabelWarningPanik, Color.OrangeRed, Color.FromArgb(255, 39, 43, 74), 2000, false);
        }

        private void PrepScreen()
        {
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            if (width == 1920)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
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

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private async Task DoWeather()
        {
            while (true)
            {
                var client = new WeatherApi("67aa3cabcb3e1100603422484bf64b9e");
                var query = client.Query("Tondano, ID");
                var imgUrl = $@"http://openweathermap.org/img/wn/{query.Weathers.First().Icon}@2x.png";

                PicCuaca.LoadAsync(imgUrl);
                LabelSuhu.Text = $@"{query.Main.Temperature.Celsius}°C";

                await Task.Delay(TimeSpan.FromHours(1));
            }
                
        }

        private async Task DoDashboard()
        {
            while (true)
            {
                //HitungTagihan();
                // GetChartData("1");
                // GetChartData("2");
                
                _controller.GetDataJumlah(LabelWarningLaporan, LabelWarningPanik);
                _controller.GetChartData("1", elementHost1, CartesianChartLapor);
                _controller.GetChartData("2", elementHost2, CartesianChartPanik);
                _controller.GetMapData("1", MapLaporan);
                _controller.GetMapData("2", MapPanik);

                // Update Tampilan
                LabelLaporanMasuk.Text = _controller.TotLaporanMasuk.ToString();
                LabelLaporanMasukHari.Text = _controller.TotLaporanMasukHari.ToString();
                LabelPanikMasuk.Text = _controller.TotPanikMasuk.ToString();
                LabelPanikMasukHari.Text = _controller.TotPanikMasukHari.ToString();
                LabelTotalMasuk.Text = _controller.TotMasuk.ToString();
                LabelTotalLaporan.Text = _controller.TotLaporan.ToString();
                LabelTotalPanik.Text = _controller.TotPanik.ToString();

                await Task.Delay(TimeSpan.FromMinutes(_interval));
            }
        }

        

        #endregion

        #region === FORMS ===

        private  void Dashboard_Load(object sender, EventArgs e)
        {
            InitData();

            _ = DoTanggalJam();
            _ = DoWeather();
            _ = DoDashboard();

            PrepScreen();

            Console.WriteLine($@"REFRESH INTERVALS: {_interval}");

            
        }
        private void PicBack_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LabelTitle_Click(object sender, EventArgs e)
        {
            // _helper.MsgBoxInfo(_soundFolder);
            //var win = new DashboardSettings();
            //win.ShowDialog(this);

            //_interval = _helper.ReadSettings("DashboardInterval", "settings") == null ? 5 : double.Parse(_helper.ReadSettings("DashboardInterval", "settings"));
            // _ = DoDashboard();

        }








        #endregion

    }
}
