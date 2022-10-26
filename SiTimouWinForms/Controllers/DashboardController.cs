using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GMap.NET;
using gov.minahasa.sitimou.Helper;
using gov.minahasa.sitimou.Helper.Interfaces;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Tools;
using LiveCharts.Wpf.Charts.Base;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;

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
        private readonly CultureInfo _cultureInfo = new("id-ID");

        // Untuk chart
        private readonly List<double> _chartLaporY = new();
        private readonly List<string> _chartLaporX = new();
        private readonly List<double> _chartPanikY = new();
        private readonly List<string> _chartPanikX = new();

        // Map Stuff
        private readonly GMapProvider _mapProvider = GMapProviders.GoogleHybridMap;
        private GMarkerGoogle _mapMarker;
        private GMapOverlay _markerOverlay;
        private readonly double _mapZoom = 17;
        private double _gpsLat;
        private double _gpsLng;

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
        
        //
        // Counter data
        //

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
                            TotPanik = reader.GetInt32(6);

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

        //
        // Chart data
        //

        public void GetChartData(string tipeChart, Control chartContainer, CartesianChart chart)
        {
            // tiepCart: 1 = Mingguan, 2 = Tahunan
            using (var conn = GetDbConnection())
            {
                using (var cmd = new MySqlCommand("sp_dashboard_chart", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        conn.Open();

                        cmd.Parameters.AddWithValue("@p_jenis_chart", tipeChart);

                        using (var reader = cmd.ExecuteReader())
                        {
                            chartContainer.Visible = reader.HasRows;

                            if (!reader.HasRows) return;

                            switch (tipeChart)
                            {
                                case "1":
                                    _chartLaporX.Clear();
                                    _chartLaporY.Clear();
                                    break;
                                case "2":
                                    _chartPanikX.Clear();
                                    _chartPanikY.Clear();
                                    break;
                                default:
                                    return;
                            }

                            while (reader.Read())
                            {
                                switch (tipeChart)
                                {
                                    case "1":
                                        _chartLaporX.Add(reader.GetString(0));
                                        _chartLaporY.Add(reader.GetDouble(1));
                                        break;
                                    case "2":
                                        _chartPanikX.Add(reader.GetString(0));
                                        _chartPanikY.Add(reader.GetDouble(1));
                                        break;
                                }
                            }

                            switch (tipeChart)
                            {
                                case "1":
                                    GenChartLaporan(chart);
                                    break;
                                case "2":
                                    GenChartPanik(chart);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        DebugHelper.ShowError("OPD", @"OpdController", MethodBase.GetCurrentMethod()?.Name, e);
                    }
                }
            }
        }

        private void GenChartLaporan(CartesianChart chart)
        {
            // var values = new List<double> { 4, 6, 5, 2, 7, 5, 9 };
            // var tanggal = new List<string> {"23/01", "24/01", "25/01", "26/01", "27/01", "28/01", "29/01"};

            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();

            chart.Series = new SeriesCollection
            {

                new LineSeries
                {
                    Title = "LAPORAN",
                    Values = new ChartValues<double>(_chartLaporY), // new ChartValues<double>(_chartMingguY),
                    StrokeThickness = 2,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                    PointGeometrySize = 15,
                },
            };

            chart.AxisX.Add(new Axis
            {
                Title = "Tanggal",
                Labels = _chartLaporX,
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });

            chart.AxisY.Add(new Axis
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

        private void GenChartPanik(CartesianChart chart)
        {
            //var values = new List<double> { 7, 3, 9, 12, 17, 15, 19 };
            //var tanggal = new List<string> { "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul" };

            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();

            chart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "PANIK",
                    Values = new ChartValues<double>(_chartPanikY), // new ChartValues<double>(_chartTahunY),
                    StrokeThickness = 2,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                    PointGeometrySize = 15,
                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(35,235, 168, 59)),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(235, 168, 59)),

                },
            };

            chart.AxisX.Add(new Axis
            {   
                Title = "Tanggal",
                Labels = _chartPanikX, //new[] { "25/01", "26/01", "27/01", "28/01", "29/01" },
                Separator = new Separator
                {
                    Step = 1,
                    IsEnabled = true,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });

            chart.AxisY.Add(new Axis
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

        //
        // Chart data
        //

        public void GetMapData(string jenisChart, GMapControl map)
        {
            var sql = jenisChart == "1"
                ? "SELECT gps_lat, gps_lng FROM laporan WHERE flg = 'N' ORDER BY laporan_id DESC LIMIT 1"
                : "SELECT gps_lat, gps_lng FROM panik WHERE flg = 'N' ORDER BY laporan_id DESC LIMIT 1";

            using (var conn = GetDbConnection())
            {
                using (var cmd = new MySqlCommand(sql, conn) { CommandType = CommandType.Text })
                {
                    try
                    {
                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            map.Visible = reader.HasRows;
                            if (!reader.HasRows) return;

                            reader.Read();

                            _gpsLat = reader.GetDouble(0);
                            _gpsLng = reader.GetDouble(1);

                            ShowMapData(map);

                        }

                    }
                    catch (Exception e)
                    {

                        DebugHelper.ShowError(@"PEGAWAI", @"PegawaiController", MethodBase.GetCurrentMethod()?.Name, e);
                    }
                }
            }
        }

        private void ShowMapData(GMapControl map)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.MapProvider = _mapProvider;

            map.Position = new PointLatLng(_gpsLat, _gpsLng);
            _mapMarker = new GMarkerGoogle(map.Position, new Bitmap(Properties.Resources.map_marker_36))
            {
                Tag = "LOKASI LAPORAN",
                ToolTipMode = MarkerTooltipMode.Never,
                ToolTipText = "LOKASI LAPORAN",
            };
            _mapMarker.ToolTip.Stroke.Color = Color.FromArgb(0, 255, 255, 0);
            _mapMarker.ToolTip.Font = new Font("Arial", 12, FontStyle.Bold);
            _mapMarker.ToolTip.Fill = Brushes.LightGray;
            _mapMarker.ToolTip.Foreground = Brushes.BlueViolet;

            // Add marker ke peta
            _markerOverlay = new GMapOverlay("_markerOverlay");
            _markerOverlay.Markers.Add(_mapMarker);

            // Add ke map
            map.Overlays.Add(_markerOverlay);
            map.Zoom = _mapZoom;
            map.ShowCenter = false;
        }

        #endregion
    }

}
