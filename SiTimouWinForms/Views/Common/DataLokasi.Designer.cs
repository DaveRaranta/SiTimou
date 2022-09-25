namespace gov.minahasa.sitimou.Views.Common
{
    sealed partial class DataLokasi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataLokasi));
            this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.DataGGC = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.label3 = new System.Windows.Forms.Label();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmExportXls = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmExportPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.TextCari = new gov.minahasa.sitimou.Helper.UI.RJControls.RjTextBox();
            this.ButtonClose = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonMenu = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonRefresh = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGGC)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientPanel1.BorderColor = System.Drawing.Color.Silver;
            this.gradientPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPanel1.Controls.Add(this.DataGGC);
            this.gradientPanel1.Controls.Add(this.label3);
            this.gradientPanel1.Location = new System.Drawing.Point(12, 61);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(876, 527);
            this.gradientPanel1.TabIndex = 8;
            // 
            // DataGGC
            // 
            this.DataGGC.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.DataGGC.Appearance.ColumnHeaderCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Control);
            this.DataGGC.BackColor = System.Drawing.SystemColors.Window;
            this.DataGGC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGGC.ColorStyles = Syncfusion.Windows.Forms.ColorStyles.Office2016Colorful;
            this.DataGGC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGGC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGGC.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.DataGGC.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2016White;
            this.DataGGC.Location = new System.Drawing.Point(0, 0);
            this.DataGGC.Name = "DataGGC";
            this.DataGGC.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.White;
            this.DataGGC.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.DataGGC.ShowGroupDropArea = true;
            this.DataGGC.Size = new System.Drawing.Size(874, 525);
            this.DataGGC.TabIndex = 25;
            this.DataGGC.TableDescriptor.AllowNew = false;
            this.DataGGC.TableDescriptor.TableOptions.CaptionRowHeight = 32;
            this.DataGGC.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.DataGGC.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.DataGGC.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.DataGGC.TableOptions.SelectionTextColor = System.Drawing.Color.DimGray;
            this.DataGGC.TopLevelGroupOptions.ShowColumnHeaders = true;
            this.DataGGC.UseRightToLeftCompatibleTextBox = true;
            this.DataGGC.VersionInfo = "17.3460.0.26";
            this.DataGGC.Visible = false;
            this.DataGGC.TableControlCurrentCellActivating += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCurrentCellActivatingEventHandler(this.DataGGC_TableControlCurrentCellActivating);
            this.DataGGC.TableControlCellDoubleClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(this.DataGGC_TableControlCellDoubleClick);
            this.DataGGC.SelectedRecordsChanged += new Syncfusion.Grouping.SelectedRecordsChangedEventHandler(this.DataGGC_SelectedRecordsChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(3, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(868, 41);
            this.label3.TabIndex = 24;
            this.label3.Text = "Tidak ada data";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmExportXls,
            this.TsmExportPdf});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(171, 64);
            // 
            // TsmExportXls
            // 
            this.TsmExportXls.Image = ((System.Drawing.Image)(resources.GetObject("TsmExportXls.Image")));
            this.TsmExportXls.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsmExportXls.Name = "TsmExportXls";
            this.TsmExportXls.Size = new System.Drawing.Size(170, 30);
            this.TsmExportXls.Text = "Export ke Excel...";
            this.TsmExportXls.Click += new System.EventHandler(this.TsmExportXls_Click);
            // 
            // TsmExportPdf
            // 
            this.TsmExportPdf.Image = ((System.Drawing.Image)(resources.GetObject("TsmExportPdf.Image")));
            this.TsmExportPdf.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TsmExportPdf.Name = "TsmExportPdf";
            this.TsmExportPdf.Size = new System.Drawing.Size(170, 30);
            this.TsmExportPdf.Text = "Export ke PDF...";
            this.TsmExportPdf.Click += new System.EventHandler(this.TsmExportPdf_Click);
            // 
            // TextCari
            // 
            this.TextCari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextCari.BackColor = System.Drawing.SystemColors.Window;
            this.TextCari.BorderColor = System.Drawing.Color.Silver;
            this.TextCari.BorderFocusColor = System.Drawing.Color.DarkOrange;
            this.TextCari.BorderRadius = 5;
            this.TextCari.BorderSize = 1;
            this.TextCari.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.TextCari.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TextCari.Location = new System.Drawing.Point(598, 13);
            this.TextCari.Margin = new System.Windows.Forms.Padding(4);
            this.TextCari.Multiline = false;
            this.TextCari.Name = "TextCari";
            this.TextCari.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.TextCari.PasswordChar = false;
            this.TextCari.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.TextCari.PlaceholderText = "CARI DATA";
            this.TextCari.Size = new System.Drawing.Size(250, 34);
            this.TextCari.TabIndex = 6;
            this.TextCari.Texts = "";
            this.TextCari.UnderlinedStyle = false;
            this.TextCari._TextChanged += new System.EventHandler(this.TextCari__TextChanged);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.BackColor = System.Drawing.Color.White;
            this.ButtonClose.BackgroundColor = System.Drawing.Color.White;
            this.ButtonClose.BorderColor = System.Drawing.Color.Silver;
            this.ButtonClose.BorderRadius = 5;
            this.ButtonClose.BorderSize = 1;
            this.ButtonClose.FlatAppearance.BorderSize = 0;
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClose.ForeColor = System.Drawing.Color.White;
            this.ButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("ButtonClose.Image")));
            this.ButtonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonClose.Location = new System.Drawing.Point(855, 12);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(33, 36);
            this.ButtonClose.TabIndex = 5;
            this.ButtonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonClose.TextColor = System.Drawing.Color.White;
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonMenu
            // 
            this.ButtonMenu.BackColor = System.Drawing.Color.Gray;
            this.ButtonMenu.BackgroundColor = System.Drawing.Color.Gray;
            this.ButtonMenu.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonMenu.BorderRadius = 5;
            this.ButtonMenu.BorderSize = 0;
            this.ButtonMenu.FlatAppearance.BorderSize = 0;
            this.ButtonMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMenu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonMenu.ForeColor = System.Drawing.Color.White;
            this.ButtonMenu.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMenu.Image")));
            this.ButtonMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonMenu.Location = new System.Drawing.Point(117, 12);
            this.ButtonMenu.Name = "ButtonMenu";
            this.ButtonMenu.Size = new System.Drawing.Size(33, 36);
            this.ButtonMenu.TabIndex = 4;
            this.ButtonMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonMenu.TextColor = System.Drawing.Color.White;
            this.ButtonMenu.UseVisualStyleBackColor = false;
            this.ButtonMenu.Click += new System.EventHandler(this.ButtonMenu_Click);
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.BackColor = System.Drawing.Color.DarkOrchid;
            this.ButtonRefresh.BackgroundColor = System.Drawing.Color.DarkOrchid;
            this.ButtonRefresh.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonRefresh.BorderRadius = 5;
            this.ButtonRefresh.BorderSize = 0;
            this.ButtonRefresh.FlatAppearance.BorderSize = 0;
            this.ButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRefresh.ForeColor = System.Drawing.Color.White;
            this.ButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRefresh.Image")));
            this.ButtonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonRefresh.Location = new System.Drawing.Point(12, 12);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(99, 36);
            this.ButtonRefresh.TabIndex = 3;
            this.ButtonRefresh.Text = "REFRESH";
            this.ButtonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonRefresh.TextColor = System.Drawing.Color.White;
            this.ButtonRefresh.UseVisualStyleBackColor = false;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // DataLokasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.TextCari);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonMenu);
            this.Controls.Add(this.ButtonRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataLokasi";
            this.Text = "Data Lokasi Penting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataLokasiPentingAdmin_FormClosed);
            this.Load += new System.EventHandler(this.DataLokasiPentingAdmin_Load);
            this.Shown += new System.EventHandler(this.DataLokasiPentingAdmin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGGC)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Helper.UI.RJControls.RjButton ButtonRefresh;
        private Helper.UI.RJControls.RjButton ButtonMenu;
        private Helper.UI.RJControls.RjButton ButtonClose;
        private Helper.UI.RJControls.RjTextBox TextCari;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl DataGGC;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem TsmExportXls;
        private System.Windows.Forms.ToolStripMenuItem TsmExportPdf;
    }
}