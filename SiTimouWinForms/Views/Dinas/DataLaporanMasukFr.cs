using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using gov.minahasa.sitimou.Controllers;
using gov.minahasa.sitimou.Helper;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;

namespace gov.minahasa.sitimou.Views.Dinas
{
    public sealed partial class DataLaporanMasukFr : Form
    {
        #region === Constructor ===

        private readonly LaporanController _controller = new();
        private readonly NotifHelper _notifHelper = new();

        private int? _idData;
        private string _jenisLaporan;
        private string _status;

        public DataLaporanMasukFr()
        {
            InitializeComponent();
            BackColor = Globals.PrimaryBgColor;
        }

        #endregion

        #region === Methode ===

        private void InitData()
        {
            _controller.GetDataFirstResponder(DataGGC, this);
            
            _ = DoTanggalJam();
        }

        private async Task DoTanggalJam()
        {
            // string ampm;

            while (true)
            {
                var tglJam = DateTime.Now;

                LabelJam.Text = tglJam.ToString("HH") + @":" + tglJam.ToString("mm");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        #endregion

        #region === FORM ===
        private void DataLaporanMasukOpdAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            InitData();
        }

        private void DataLaporanMasukOpdAdmin_Shown(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = false;
            ((MainForm)MdiParent).RemoveChildBorder();
        }

        private void DataLaporanMasukOpdAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)MdiParent).ShowPanel = true;
        }

        #endregion

        #region === Button Menu

        private void ButtonTeruskan_Click(object sender, EventArgs e)
        {
            if (_idData == null)
            {
                _notifHelper.MsgBoxWarning(@"Pilih data yang akan diselesaikan.");
                return;
            }

            var alamat = $"{DataGridHelper.GetCellValue<string>(DataGGC, "Alamat Pelapor")}, " +
                         $"{DataGridHelper.GetCellValue<string>(DataGGC, "Desa/Kelurahan")}, " +
                         $"{DataGridHelper.GetCellValue<string>(DataGGC, "Kecamatan")}";

            if (_jenisLaporan == "1") // Laporan
            {
                if (_status == "N")
                {
                    var win = new InfoLaporanFr
                    {
                        IdLaporan = _idData.Value,
                        IdDisposisi = DataGridHelper.GetCellValue<int>(DataGGC, "disposisi_id"),
                        IdPelapor = DataGridHelper.GetCellValue<int>(DataGGC, "pelapor_id"),
                        NamaPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Pelapor"),
                        NikPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "nik"),
                        NoTelp = DataGridHelper.GetCellValue<string>(DataGGC, "Nomor Telp"),
                        AlamatPelapor = alamat,
                        Status = _status,
                    };
                    win.ShowDialog();

                    if (win.IsDataSaved) _controller.GetDataFirstResponder(DataGGC, this); ;
                }
                else if(_status == "P")
                {
                    var win = new ProsesLaporanFr
                    {
                        IdLaporan = _idData.Value,
                        IdDisposisi = DataGridHelper.GetCellValue<int>(DataGGC, "disposisi_id"),
                        IdPelapor = DataGridHelper.GetCellValue<int>(DataGGC, "pelapor_id"),
                        NamaPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Pelapor"),
                        NikPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "nik"),
                        NoTelp = DataGridHelper.GetCellValue<string>(DataGGC, "Nomor Telp"),
                        AlamatPelapor = alamat,
                        Status = _status,
                    };
                    win.ShowDialog();

                    if (win.IsDataSaved) _controller.GetDataFirstResponder(DataGGC, this); ;
                }
            } 
            else if (_jenisLaporan == "2")
            {
                if (_status is "N" or "P")
                {
                    var win = new InfoPanikFr
                    {
                        IdLaporan = _idData.Value,
                        IdDisposisi = DataGridHelper.GetCellValue<int>(DataGGC, "disposisi_id"),
                        IdPelapor = DataGridHelper.GetCellValue<int>(DataGGC, "pelapor_id"),
                        NamaPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "Nama Pelapor"),
                        NikPelapor = DataGridHelper.GetCellValue<string>(DataGGC, "nik"),
                        NoTelp = DataGridHelper.GetCellValue<string>(DataGGC, "Nomor Telp"),
                        AlamatPelapor = alamat,
                        Status = _status,
                    };
                    win.ShowDialog();

                    if (win.IsDataSaved) _controller.GetDataFirstResponder(DataGGC, this); ;
                }
                else
                {
                    Console.WriteLine("UC");
                }
            }

            _idData = null;
        }

        
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            _controller.GetDataFirstResponder(DataGGC, this);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region === Data Grid ===

        private void DataGGC_TableControlCurrentCellActivating(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCurrentCellActivatingEventArgs e)
        {
            e.Inner.ColIndex = 0;
        }

        private void DataGGC_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            _idData = DataGridHelper.GetCellValue<int>(DataGGC, "laporan_id");
            _jenisLaporan = DataGridHelper.GetCellValue<string>(DataGGC, "jenis_laporan");
            _status = DataGridHelper.GetCellValue<string>(DataGGC, "flg");
        }

        private void DataGGC_TableControlCellDoubleClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            if (_idData == null) return;

            Console.WriteLine(_idData.Value);

            ButtonTambah.PerformClick();
        }

        #endregion


    }
}
