using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using gov.minahasa.sitimou.Helper;
using GMap.NET;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;


namespace gov.minahasa.sitimou.Views.Common
{
    public sealed partial class MapViewer : Form
    {
        #region === Constructor ===

        // Input
        public string JudulForm;
        public string MapName;
        // public string ToolTipTag;
        public string ToolTipText;
        public double GpsLatitude;
        public double GpsLongitude;
        public int PolygonColor = -9539676;
        public byte[] MarkerIcon = null;
        public string GpsTipe = null;
        public List<(double lat, double lng)> Polygons;

        // Class stuff
        private readonly NotifHelper _notifHelper = new();
        private readonly RegistryCore _registryCore = new();
        private readonly ImageHelper _imageHelper = new();

        private readonly FileHelper _fileHelper = new();
        // private readonly ConversionHelper _conversion = new ConversionHelper();

        // Map Stuff
        private GMapProvider _mapProvider;
        private GMarkerGoogle _mapMarker;
        private GMapOverlay _markerOverlay;
        private GMapOverlay _polygonsOverlay;
        private GMapPolygon _mapPolygon;
        private GMapRoute _mapRoute;
        private double _mapZoom;

        // Other sutff
        private readonly string _tempFolder = $"{Path.GetTempPath()}{Path.GetRandomFileName()}";
        private Image _image;
        private string _fileName;
        private readonly ToolTip _toolTip = new ToolTip();

        public MapViewer()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
            _registryCore.SubKey = "SOFTWARE\\Minahasa\\" + Application.ProductName + "\\maps";

            _toolTip.OwnerDraw = true;
            _toolTip.Draw += ToolTip_Draw;
            _toolTip.Popup += ToolTip_Popup;

        }

        #endregion

        #region === Functions and Procedures ===

        private void LoadMapSettings()
        {

            if (_registryCore.ValueCount() == 0)
            {
                _mapZoom = 15;
                _mapProvider = GMapProviders.GoogleHybridMap;
            }
            else
            {
                var providerIndex = _registryCore.Read("GMapProvider") == null
                    ? 4
                    : (int)_registryCore.Read("GMapProvider");


                _mapZoom = Convert.ToDouble(_registryCore.Read("GMapZoom"));
                _mapProvider = SetMapProvider(providerIndex);

                GComboJenis.SelectedIndex = providerIndex - 1;

            }
        }

        private static GMapProvider SetMapProvider(int mapIndex)
        {
            GMapProvider provider = mapIndex switch
            {
                1 => OpenStreetMapProvider.Instance,
                2 => ArcGIS_World_Street_MapProvider.Instance,
                3 => GoogleMapProvider.Instance,
                4 => GoogleHybridMapProvider.Instance,
                5 => GoogleTerrainMapProvider.Instance,
                _ => OpenStreetMapProvider.Instance
            };

            return provider;
        }

        private void LoadMap()
        {

            Debug.WriteLine($"GPS LAT: {GpsLatitude}");
            Debug.WriteLine($"GPS LNG: {GpsLongitude}");

            // Map Instance
            GMaps.Instance.Mode = AccessMode.ServerAndCache; //.ServerOnly;
            MapControl.MapProvider = _mapProvider;


            // Marker
            MapControl.Position = new PointLatLng(GpsLatitude, GpsLongitude);
            _mapMarker = new GMarkerGoogle(MapControl.Position,
            new Bitmap(MarkerIcon == null
                    ? Properties.Resources.map_marker_36
                    : _imageHelper.BytesToImage(MarkerIcon)))
            {
                Tag = ToolTipText,
                ToolTipMode = MarkerTooltipMode.Never,
                ToolTipText = ToolTipText
            };
            _mapMarker.ToolTip.Stroke.Color = Color.FromArgb(0, 255, 255, 0);
            _mapMarker.ToolTip.Font = new Font("Arial", 12, FontStyle.Bold);
            _mapMarker.ToolTip.Fill = Brushes.LightGray;
            _mapMarker.ToolTip.Foreground = Brushes.BlueViolet;

            // Add marker ke peta
            _markerOverlay = new GMapOverlay("_markerOverlay");
            _markerOverlay.Markers.Add(_mapMarker);

            // Polygons
            if (GpsTipe != "-" && GpsTipe != null)
            {
                var points = Polygons.Select(i => new PointLatLng(i.lat, i.lng)).ToList();

                switch (GpsTipe)
                {
                    // Area
                    case "1":
                        _polygonsOverlay = new GMapOverlay("_polygonsOverlay");
                        _mapPolygon = new GMapPolygon(points, MapName)
                        {
                            Fill = new SolidBrush(Color.FromArgb(50, Color.FromArgb(PolygonColor))),
                            Stroke = new Pen(Color.FromArgb(PolygonColor), 4)

                        };
                        _polygonsOverlay.Polygons.Add(_mapPolygon);

                        MapControl.Overlays.Add(_polygonsOverlay);

                        break;
                    // LIne
                    case "2":
                        _polygonsOverlay = new GMapOverlay("_polygonsOverlay");
                        _mapRoute = new GMapRoute(points, MapName)
                        {
                            Stroke = new Pen(Color.FromArgb(PolygonColor), 5)
                        };
                        _polygonsOverlay.Routes.Add(_mapRoute);

                        MapControl.Overlays.Add(_polygonsOverlay);

                        break;
                }
            }

            MapControl.Overlays.Add(_markerOverlay);
            MapControl.Zoom = _mapZoom;
            MapControl.DragButton = MouseButtons.Left;
            MapControl.ShowCenter = false;

        }

        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            var img = Image.FromFile(_fileName);

            var newWidth = img.Width * 100 / img.HorizontalResolution;
            var newHeight = img.Height * 100 / img.VerticalResolution;

            var widthFactor = newWidth / e.PageBounds.Width;
            var heightFactor = newHeight / e.PageBounds.Height;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    /*newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;*/

                    newWidth /= widthFactor;
                    newHeight /= widthFactor;
                }
                else
                {
                    /*newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;*/

                    newWidth /= heightFactor;
                    newHeight /= heightFactor;
                }
            }
            //e.Graphics.DrawImage(i, 0, 0, (int)newWidth, (int)newHeight);
            e.Graphics.DrawImage(img,
                (e.PageBounds.Width - newWidth) / 2,
                (e.PageBounds.Height - newHeight) / 2,
                (int)newWidth,
                (int)newHeight);

        }

        #endregion

        private void MapViewer_Load(object sender, EventArgs e)
        {

            this.Text = JudulForm;
            LabelH1.Text = JudulForm;

            LoadMapSettings();
            LoadMap();
        }

        private void MapViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            _registryCore.Write("GMapZoom", MapControl.Zoom, RegistryValueKind.DWord);
        }

        #region  === Tooltip ===

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            using (var f = new Font("Arial", 11, FontStyle.Bold))
            {
                var text = TextRenderer.MeasureText(_toolTip.GetToolTip((e.AssociatedControl)), f);
                e.ToolTipSize = new Size(text.Width + 7, 32);
            }
        }

        private static void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            var g = e.Graphics;

            var b = new LinearGradientBrush(e.Bounds,
                Color.FromArgb(255, 117, 0, 97), Color.FromArgb(255, 239, 11, 81), 45f);

            g.FillRectangle(b, e.Bounds);

            g.DrawRectangle(new Pen(Brushes.Red, 1), new Rectangle(e.Bounds.X, e.Bounds.Y,
                e.Bounds.Width - 1, e.Bounds.Height - 1));

            g.DrawString(e.ToolTipText, new Font("Arial", 11, FontStyle.Bold), Brushes.White,
                new PointF(e.Bounds.X + 3, e.Bounds.Y + 7));

            b.Dispose();
        }


        #endregion

        #region === Button ===

        private void ButtonZoomIn_Click(object sender, EventArgs e)
        {
            if (MapControl.Zoom == 20) return;
            MapControl.Zoom += 1;
        }

        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            MapControl.Zoom -= 1;
        }

        private void ButtonMaps_Click(object sender, EventArgs e)
        {
            PanelJenisPeta.Visible = true;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Ambil Image dari peta
            _image = MapControl.ToImage();

            // Simpan
            var saveDialog = new SaveFileDialog()
            {
                Title = @"Simpan peta",
                Filter = @"Image file (*.jpg)|*.jpg",
                DefaultExt = "jpg",
                AddExtension = true
            };

            if (saveDialog.ShowDialog() == DialogResult.OK & saveDialog.CheckPathExists)
            {
                var byteImg = _imageHelper.ImageToByte(_image, ImageFormat.Jpeg);

                var result = _fileHelper.BytesToFile(byteImg, saveDialog.FileName);

                if (result)
                {
                    Process.Start(saveDialog.FileName);
                }
                else
                {
                    _notifHelper.MsgBoxWarning(@"Gagal simpan peta.");
                }

            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            // Temporary file
            var guidFile = Guid.NewGuid().ToString("N");
            _fileName = Path.Combine(_tempFolder, $"{guidFile}.jpg");
            if (!Directory.Exists(_tempFolder)) Directory.CreateDirectory(_tempFolder);

            // Export image
            _image = MapControl.ToImage();
            var byteImg = _imageHelper.ImageToByte(_image, ImageFormat.Jpeg);
            var result = _fileHelper.BytesToFile(byteImg, _fileName);

            if (result)
            {
                var printDialog = new PrintDialog();
                var printDocument = new PrintDocument();

                if (printDialog.ShowDialog() != DialogResult.OK) return;

                var margins = new Margins(12, 12, 12, 12);
                printDocument.DefaultPageSettings.Margins = margins;
                printDocument.DefaultPageSettings.Landscape = true;
                printDocument.PrintPage += PrintImage;
                printDocument.Print();
            }
            else
            {
                _notifHelper.MsgBoxWarning(@"Gagal cetak peta.");
            }
        }

        private void ButtonPilih_Click(object sender, EventArgs e)
        {
            if (GComboJenis.Text == string.Empty)
            {
                _notifHelper.MsgBoxWarning(@"Pilih peta terlebih dahulu.");
                return;
            }

            _mapProvider = SetMapProvider(GComboJenis.SelectedIndex + 1);
            MapControl.MapProvider = _mapProvider;

            _registryCore.Write("GMapProvider", GComboJenis.SelectedIndex + 1, RegistryValueKind.DWord);

            PanelJenisPeta.Visible = false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            PanelJenisPeta.Visible = false;
        }


        #endregion

        
    }
}
