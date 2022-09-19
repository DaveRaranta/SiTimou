using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Syncfusion.ExcelToPdfConverter;
using Syncfusion.GridExcelConverter;
using Syncfusion.GroupingGridExcelConverter;
using Syncfusion.Pdf;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.XlsIO;

namespace gov.minahasa.sitimou.Helper
{
    internal class DataGridHelper
    {
        private static readonly NotifHelper Notif = new();

        public static void FormatTable(GridGroupingControl gridControl, bool newRow = false, string newRowText = "", Color? newRowColor = null)
        {
            gridControl.TableDescriptor.Name = string.Empty;
            gridControl.TableDescriptor.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridControl.AllowProportionalColumnSizing = true;
            gridControl.TopLevelGroupOptions.ShowCaption = false;
            gridControl.TableDescriptor.AllowNew = false;
            gridControl.TableDescriptor.TableOptions.ShowRowHeader = false;
            gridControl.Appearance.AnyCell.WrapText = false;
            gridControl.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            gridControl.Table.DefaultRecordRowHeight = 35;
            gridControl.TableOptions.SelectionTextColor = Color.Black;
            gridControl.TableOptions.SelectionBackColor = Color.LightSkyBlue;

            if (newRow)
            {
                var ggcFormatStatus = new GridConditionalFormatDescriptor();
                ggcFormatStatus.Appearance.AnyRecordFieldCell.TextColor = newRowColor ?? Color.RoyalBlue;
                ggcFormatStatus.Appearance.AnyRecordFieldCell.Font.Bold = true;
                ggcFormatStatus.Expression = newRowText;
                gridControl.TableDescriptor.ConditionalFormats.Add(ggcFormatStatus);
            }

        }

        public static void FormatTableDisposisiFr(GridGroupingControl gridControl)
        {
            gridControl.TableDescriptor.Name = string.Empty;
            gridControl.TableDescriptor.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridControl.AllowProportionalColumnSizing = true;
            gridControl.TopLevelGroupOptions.ShowCaption = false;
            gridControl.TableDescriptor.AllowNew = false;
            gridControl.TableDescriptor.TableOptions.ShowRowHeader = false;
            gridControl.Appearance.AnyCell.WrapText = false;
            gridControl.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            gridControl.Table.DefaultRecordRowHeight = 45;
            gridControl.TableOptions.SelectionTextColor = Color.Black;
            gridControl.TableOptions.SelectionBackColor = Color.LightSkyBlue;

            // Condition 1 (flg = 'N')
            var ggcFormatStatus1 = new GridConditionalFormatDescriptor();
            ggcFormatStatus1.Appearance.AnyRecordFieldCell.TextColor = Color.DodgerBlue;
            ggcFormatStatus1.Appearance.AnyRecordFieldCell.Font.Bold = true;
            ggcFormatStatus1.Expression = "[jenis_laporan] = '1'";
            gridControl.TableDescriptor.ConditionalFormats.Add(ggcFormatStatus1);

            var ggcFormatStatus2 = new GridConditionalFormatDescriptor();
            ggcFormatStatus2.Appearance.AnyRecordFieldCell.TextColor = Color.OrangeRed;
            ggcFormatStatus2.Appearance.AnyRecordFieldCell.Font.Bold = true;
            ggcFormatStatus2.Expression = "[jenis_laporan] = '2'";
            gridControl.TableDescriptor.ConditionalFormats.Add(ggcFormatStatus2);

        }


        public static void FormatPilih(GridGroupingControl gridControl)
        {
            gridControl.TableDescriptor.Name = string.Empty;
            gridControl.TableDescriptor.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridControl.AllowProportionalColumnSizing = true;
            gridControl.TopLevelGroupOptions.ShowCaption = false;
            gridControl.TableDescriptor.AllowNew = false;
            gridControl.TableDescriptor.TableOptions.ShowRowHeader = false;
            gridControl.Appearance.AnyCell.WrapText = false;
            gridControl.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            gridControl.Table.DefaultRecordRowHeight = 35;
            gridControl.TableOptions.SelectionTextColor = Color.Black;
            gridControl.TableOptions.SelectionBackColor = Color.LightSkyBlue;
            gridControl.TopLevelGroupOptions.ShowColumnHeaders = false;
            gridControl.TableControl.VScroll = true;
            gridControl.TableControl.VScrollBehavior = GridScrollbarMode.Enabled;
        }


        public static T GetCellValue<T>(GridGroupingControl grid, string column)
        {
            try
            {
                foreach (var selected in grid.Table.SelectedRecords)
                {
                    return (T)selected.Record.GetValue(column);
                }
            }
            catch
            {
                return default;
            }

            return default;

        }


        public static string ExportDataGridToXls(GridGroupingControl namaGrid, string ket = "Data")
        {

            //Dialog Box
            var simpanFileDialog = new SaveFileDialog
            {
                Title = @"Export ke Excel",
                Filter = @"Excel Files (*.xls)|*.xls",
                DefaultExt = "xls",
                AddExtension = true
            };

            try
            {
                if (simpanFileDialog.ShowDialog() == DialogResult.OK && simpanFileDialog.CheckPathExists)
                {

                    var toXls = new GroupingGridExcelConverterControl
                    {
                        ExportBorders = true,
                        ExportStyle = true

                    };

                    Application.UseWaitCursor = true;

                    toXls.GroupingGridToExcel(namaGrid, simpanFileDialog.FileName, ConverterOptions.Default);

                    // Simpan Log
                    new DebugHelper().SaveAppLog("EXPORT", "SUCCESS", $"[ExportToXls] Export {ket} ke file Excel berhasil.");

                    return simpanFileDialog.FileName;

                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Notif.MsgBoxWarning(@"Gagal export data ke Excel.");

                DebugHelper.ShowError("DataGrid", "DataGridHelper", MethodBase.GetCurrentMethod()?.Name, ex);

                return string.Empty;
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }

        public static string ExportDataGridToPdf(GridGroupingControl namaGrid, string ket = "Data")
        {
            //Dialog Box
            var simpanFileDialog = new SaveFileDialog
            {
                Title = @"Export ke PDF",
                Filter = @"PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true
            };


            try
            {
                if (simpanFileDialog.ShowDialog() == DialogResult.OK & simpanFileDialog.CheckPathExists)
                {

                    Application.UseWaitCursor = true;

                    //Export dulu ke XLS
                    var tempXlsFile = $"{Path.GetTempPath()}_mhdoc_export.xls";

                    //kill file jika apda
                    if (File.Exists(tempXlsFile)) File.Delete(tempXlsFile);

                    //Export file
                    var toXls = new GroupingGridExcelConverterControl { ExportBorders = true, ExportStyle = true };

                    toXls.GroupingGridToExcel(namaGrid, tempXlsFile, ConverterOptions.Default);

                    var excelEngine = new ExcelEngine();
                    IApplication application = excelEngine.Excel;
                    IWorkbook book = application.Workbooks.Open(tempXlsFile);
                    IWorksheet worksheet = book.Worksheets[0];
                    worksheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;

                    var toPdf = new ExcelToPdfConverter(worksheet);
                    var pdfDocument = new PdfDocument();
                    var pdfSettings = new ExcelToPdfConverterSettings
                    {
                        LayoutOptions = LayoutOptions.FitAllColumnsOnOnePage,
                        TemplateDocument = pdfDocument,
                        DisplayGridLines = GridLinesDisplayStyle.Invisible

                    };

                    pdfDocument = toPdf.Convert(pdfSettings);

                    pdfDocument.Save(simpanFileDialog.FileName);

                    new DebugHelper().SaveAppLog("EXPORT", "SUCCESS", $"[ExportToPdf] Export {ket} ke file PDF berhasil.");

                    return simpanFileDialog.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Notif.MsgBoxWarning(@"Gagal export data ke Pdf.");

                DebugHelper.ShowError("DataGrid", "DataGridHelper", MethodBase.GetCurrentMethod()?.Name, ex);

                return string.Empty;
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }
    }
}
