using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Tools;

namespace gov.minahasa.sitimou.Controllers
{
    internal class DashboardController : DbConnection
    {

        // Counter Masuk / Harian / All
        public int TotLaporanMasuk { get; private set; } = 0;
        public int TotLaporanMasukHari { get; private set; } = 0;
        public int TotPanikMasuk { get; private set; } = 0;
        public int TotPanikMasukHari { get; private set; } = 0;
        public int TotLaporan { get; private set; } = 0;
        public int TotPanik { get; private set; } = 0;
        public int TotMasuk { get; private set; } = 0;

        private readonly AppHelper _appHelper = new();
        private readonly string _soundFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds");


        #region === Warning ===

        public void PlayWarning(int laporanMasuk, int panikMasuk, Control ctrlLaporanMasuk, Control ctrlPanikMasuk)
        {
            // Indikator
            // Start blinking
            
            //_appHelper.SoftBlink(ctrlPanikMasuk, Color.OrangeRed, Color.FromArgb(255, 39, 43, 74), 3000, false);

            ctrlLaporanMasuk.Visible = laporanMasuk > 0;
            ctrlPanikMasuk.Visible = TotPanikMasuk > 0;

            // Alarm
            if (panikMasuk > 0)
            {
                if (!File.Exists(Path.Combine(_soundFolder, "panik.wav"))) return;
                System.Media.SoundPlayer player = new (Path.Combine(Path.Combine(_soundFolder, "panik.wav")));
                player.Play();
                return;
            }

            if (laporanMasuk > 0)
            {
                if (!File.Exists(Path.Combine(_soundFolder, "lapor.wav"))) return;
                System.Media.SoundPlayer player = new(Path.Combine(Path.Combine(_soundFolder, "lapor.wav")));
                player.Play();
                return;
            }

        }


        #endregion

        #region === Dashboard ===



        public void GetDataJumlah(Control ctrlLaporanMasuk, Control ctrlPanikMasuk)
        {
            using (var conn = GetDbConnection())
            {
                using (var cmd = new MySqlCommand("sp_dashboard_count", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        conn.Open();

                        cmd.Parameters.AddWithValue("@p_grup", Globals.UserGrup);
                        cmd.Parameters.AddWithValue("@p_opd_id", Globals.UserOpdId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows) return;


                            reader.Read();
                                

                            // Assign value
                            TotLaporanMasuk = reader.GetInt16(0);
                            TotLaporanMasukHari = reader.GetInt16(1);
                            TotPanikMasuk = reader.GetInt16(2);
                            TotPanikMasukHari = reader.GetInt16(3);
                            TotMasuk = reader.GetInt32(4);
                            TotLaporan = reader.GetInt32(5);
                            TotPanik = reader.GetInt32(5);

                            PlayWarning(TotLaporanMasuk, TotPanikMasuk, ctrlLaporanMasuk, ctrlPanikMasuk);

                        }

                    }
                    catch (Exception e)
                    {

                        DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e);
                    }
                }
            }
        }

        /*
            
        // >>> Chart <<<

        private void GetChartData(string tipeChart)
        {
            // tiepCart: 1 = Mingguan, 2 = Tahunan
            using (new WaitCursor(this))
            {
                using (var conn = new MySqlConnection(_db.ConnectionString()))
                {
                    using (var cmd = new MySqlCommand("DashboardChart", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            cmd.Parameters.AddWithValue("@TipeChart", tipeChart);

                            using (var reader = cmd.ExecuteReader())
                            {
                                switch (tipeChart)
                                {
                                    case "1":
                                        elementHost1.Visible = reader.HasRows;
                                        break;
                                    case "2":
                                        elementHost2.Visible = reader.HasRows;
                                        break;
                                }

                                if (!reader.HasRows) return;

                                switch (tipeChart)
                                {
                                    case "1":
                                        _chartMingguX.Clear();
                                        _chartMingguY.Clear();
                                        break;
                                    case "2":
                                        _chartTahunX.Clear();
                                        _chartTahunY.Clear();
                                        break;
                                    default:
                                        return;
                                }

                                while (reader.Read())
                                {
                                    switch (tipeChart)
                                    {
                                        case "1":
                                            _chartMingguX.Add(reader.GetString("tgl"));
                                            _chartMingguY.Add(reader.GetDouble("total"));
                                            break;
                                        case "2":
                                            _chartTahunX.Add(reader.GetString("bulan"));
                                            _chartTahunY.Add(reader.GetDouble("total"));
                                            break;
                                    }
                                }

                                switch (tipeChart)
                                {
                                    case "1":
                                        GenChartMinggu();
                                        break;
                                    case "2":
                                        GenChartTahun();
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppHelper.ShowError(this.Name, MethodBase.GetCurrentMethod().Name, ex);
                        }
                    }
                }
            }

        }

        private void GenChartMinggu()
        {
            // var values = new List<double> { 4, 6, 5, 2, 7, 5, 9 };
            // var tanggal = new List<string> {"23/01", "24/01", "25/01", "26/01", "27/01", "28/01", "29/01"};

            CartesianChartMinggu.Series = new SeriesCollection
            {
                
                new LineSeries
                {
                    Title = "Tagihan Masuk",
                    Values = new ChartValues<double>(_chartMingguY), // new ChartValues<double>(_chartMingguY),
                    StrokeThickness = 2,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                    PointGeometrySize = 15,
                },
            };

            CartesianChartMinggu.AxisX.Add(new Axis
            {
                Title = "Tanggal",
                Labels = _chartMingguX, 
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });

            CartesianChartMinggu.AxisY.Add(new Axis
            {
                Title = "Total Masuk",
                LabelFormatter = value => value.ToString("N0"),
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
        }

        private void GenChartTahun()
        {
            //var values = new List<double> { 7, 3, 9, 12, 17, 15, 19 };
            //var tanggal = new List<string> { "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul" };
            

            CartesianChartTahun.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Tagihan Masuk",
                    Values = new ChartValues<double>(_chartTahunY), // new ChartValues<double>(_chartTahunY),
                    StrokeThickness = 2,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                    PointGeometrySize = 15,
                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(35,235, 168, 59)),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(235, 168, 59)),

                },
            };

            CartesianChartTahun.AxisX.Add(new Axis
            {
                Title = "Bulan",
                Labels = _chartTahunX, //new[] { "25/01", "26/01", "27/01", "28/01", "29/01" },
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });

            CartesianChartTahun.AxisY.Add(new Axis
            {
                Title = "Total Masuk",
                LabelFormatter = value => value.ToString("N0"),
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
        }

        // >>> Chart END <<<


        private void HitungTagihan()
        {
            using (new WaitCursor(this))
            {
                using (var conn = new MySqlConnection(_db.ConnectionString()))
                {
                    using (var cmd = new MySqlCommand("DashboardCount", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        try
                        {
                            conn.Open();

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows) return;

                                reader.Read();

                                // Tagihan
                                LabelTagihanMasuk.Text = reader.GetInt16("totTagihanMasuk").ToString("N0", _cultureInfo);
                                LabelTagihanMasukHari.Text = reader.GetInt16("totTagihanMasukHari").ToString("N0", _cultureInfo);
                                LabelTagihanVerifikasi.Text = reader.GetInt16("totTagihanVerifikasi").ToString("N0", _cultureInfo);
                                LabelTagihanVerifikasiHari.Text = reader.GetInt16("totTagihanVerifikasiHari").ToString("N0", _cultureInfo);

                                // Anggaran
                                LabelAnggaran.Text = reader.GetDouble("totAnggaran").ToString("N0", _cultureInfo);
                                LabelAnggaranDau.Text = reader.GetDouble("totAnggaranDau").ToMil();
                                LabelAnggaranDak.Text = reader.GetDouble("totAnggaranDak").ToMil();
                                LabelAnggaranDal.Text = reader.GetDouble("totAnggaranDal").ToMil();

                            }
                        }
                        catch (Exception ex)
                        {
                            AppHelper.ShowError(this.Name, MethodBase.GetCurrentMethod().Name, ex);
                        }
                    }
                }
            }
        }

         */

        #endregion
    }

}
