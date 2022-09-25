namespace gov.minahasa.sitimou.Views.Common
{
    sealed partial class MapViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewer));
            this.LabelH1 = new System.Windows.Forms.Label();
            this.MapControl = new GMap.NET.WindowsForms.GMapControl();
            this.gradientPanel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.ButtonPrint = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonSave = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonMaps = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonZoomOut = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.ButtonZoomIn = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.PanelJenisPeta = new System.Windows.Forms.Panel();
            this.ButtonClose = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.GComboJenis = new System.Windows.Forms.ComboBox();
            this.ButtonPilih = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).BeginInit();
            this.gradientPanel2.SuspendLayout();
            this.PanelJenisPeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelH1
            // 
            this.LabelH1.AutoSize = true;
            this.LabelH1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelH1.ForeColor = System.Drawing.Color.DimGray;
            this.LabelH1.Location = new System.Drawing.Point(15, 15);
            this.LabelH1.Name = "LabelH1";
            this.LabelH1.Size = new System.Drawing.Size(108, 20);
            this.LabelH1.TabIndex = 18;
            this.LabelH1.Text = "Tampilan Peta";
            // 
            // MapControl
            // 
            this.MapControl.Bearing = 0F;
            this.MapControl.CanDragMap = true;
            this.MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.MapControl.GrayScaleMode = false;
            this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MapControl.LevelsKeepInMemory = 5;
            this.MapControl.Location = new System.Drawing.Point(0, 0);
            this.MapControl.MarkersEnabled = true;
            this.MapControl.MaxZoom = 25;
            this.MapControl.MinZoom = 2;
            this.MapControl.MouseWheelZoomEnabled = true;
            this.MapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MapControl.Name = "MapControl";
            this.MapControl.NegativeMode = false;
            this.MapControl.PolygonsEnabled = true;
            this.MapControl.RetryLoadTile = 0;
            this.MapControl.RoutesEnabled = true;
            this.MapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MapControl.ShowTileGridLines = false;
            this.MapControl.Size = new System.Drawing.Size(843, 483);
            this.MapControl.TabIndex = 19;
            this.MapControl.Zoom = 0D;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientPanel2.BackColor = System.Drawing.Color.Transparent;
            this.gradientPanel2.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.gradientPanel2.BorderColor = System.Drawing.Color.LightGray;
            this.gradientPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientPanel2.Controls.Add(this.MapControl);
            this.gradientPanel2.Location = new System.Drawing.Point(19, 54);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(845, 485);
            this.gradientPanel2.TabIndex = 36;
            // 
            // ButtonPrint
            // 
            this.ButtonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPrint.BackColor = System.Drawing.Color.DarkOrchid;
            this.ButtonPrint.BackgroundColor = System.Drawing.Color.DarkOrchid;
            this.ButtonPrint.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonPrint.BorderRadius = 5;
            this.ButtonPrint.BorderSize = 0;
            this.ButtonPrint.FlatAppearance.BorderSize = 0;
            this.ButtonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPrint.ForeColor = System.Drawing.Color.White;
            this.ButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPrint.Image")));
            this.ButtonPrint.Location = new System.Drawing.Point(828, 10);
            this.ButtonPrint.Name = "ButtonPrint";
            this.ButtonPrint.Size = new System.Drawing.Size(36, 36);
            this.ButtonPrint.TabIndex = 20;
            this.ButtonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonPrint.TextColor = System.Drawing.Color.White;
            this.ButtonPrint.UseVisualStyleBackColor = false;
            this.ButtonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.ButtonSave.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.ButtonSave.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonSave.BorderRadius = 5;
            this.ButtonSave.BorderSize = 0;
            this.ButtonSave.FlatAppearance.BorderSize = 0;
            this.ButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSave.ForeColor = System.Drawing.Color.White;
            this.ButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSave.Image")));
            this.ButtonSave.Location = new System.Drawing.Point(786, 10);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(36, 36);
            this.ButtonSave.TabIndex = 37;
            this.ButtonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonSave.TextColor = System.Drawing.Color.White;
            this.ButtonSave.UseVisualStyleBackColor = false;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonMaps
            // 
            this.ButtonMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMaps.BackColor = System.Drawing.Color.Red;
            this.ButtonMaps.BackgroundColor = System.Drawing.Color.Red;
            this.ButtonMaps.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonMaps.BorderRadius = 5;
            this.ButtonMaps.BorderSize = 0;
            this.ButtonMaps.FlatAppearance.BorderSize = 0;
            this.ButtonMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMaps.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonMaps.ForeColor = System.Drawing.Color.White;
            this.ButtonMaps.Image = ((System.Drawing.Image)(resources.GetObject("ButtonMaps.Image")));
            this.ButtonMaps.Location = new System.Drawing.Point(744, 10);
            this.ButtonMaps.Name = "ButtonMaps";
            this.ButtonMaps.Size = new System.Drawing.Size(36, 36);
            this.ButtonMaps.TabIndex = 38;
            this.ButtonMaps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonMaps.TextColor = System.Drawing.Color.White;
            this.ButtonMaps.UseVisualStyleBackColor = false;
            this.ButtonMaps.Click += new System.EventHandler(this.ButtonMaps_Click);
            // 
            // ButtonZoomOut
            // 
            this.ButtonZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonZoomOut.BackColor = System.Drawing.Color.DarkOrange;
            this.ButtonZoomOut.BackgroundColor = System.Drawing.Color.DarkOrange;
            this.ButtonZoomOut.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonZoomOut.BorderRadius = 5;
            this.ButtonZoomOut.BorderSize = 0;
            this.ButtonZoomOut.FlatAppearance.BorderSize = 0;
            this.ButtonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonZoomOut.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonZoomOut.ForeColor = System.Drawing.Color.White;
            this.ButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ButtonZoomOut.Image")));
            this.ButtonZoomOut.Location = new System.Drawing.Point(702, 10);
            this.ButtonZoomOut.Name = "ButtonZoomOut";
            this.ButtonZoomOut.Size = new System.Drawing.Size(36, 36);
            this.ButtonZoomOut.TabIndex = 39;
            this.ButtonZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonZoomOut.TextColor = System.Drawing.Color.White;
            this.ButtonZoomOut.UseVisualStyleBackColor = false;
            this.ButtonZoomOut.Click += new System.EventHandler(this.ButtonZoomOut_Click);
            // 
            // ButtonZoomIn
            // 
            this.ButtonZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonZoomIn.BackColor = System.Drawing.Color.OrangeRed;
            this.ButtonZoomIn.BackgroundColor = System.Drawing.Color.OrangeRed;
            this.ButtonZoomIn.BorderColor = System.Drawing.Color.DarkOrchid;
            this.ButtonZoomIn.BorderRadius = 5;
            this.ButtonZoomIn.BorderSize = 0;
            this.ButtonZoomIn.FlatAppearance.BorderSize = 0;
            this.ButtonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonZoomIn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonZoomIn.ForeColor = System.Drawing.Color.White;
            this.ButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ButtonZoomIn.Image")));
            this.ButtonZoomIn.Location = new System.Drawing.Point(660, 10);
            this.ButtonZoomIn.Name = "ButtonZoomIn";
            this.ButtonZoomIn.Size = new System.Drawing.Size(36, 36);
            this.ButtonZoomIn.TabIndex = 40;
            this.ButtonZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonZoomIn.TextColor = System.Drawing.Color.White;
            this.ButtonZoomIn.UseVisualStyleBackColor = false;
            this.ButtonZoomIn.Click += new System.EventHandler(this.ButtonZoomIn_Click);
            // 
            // PanelJenisPeta
            // 
            this.PanelJenisPeta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PanelJenisPeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelJenisPeta.Controls.Add(this.ButtonClose);
            this.PanelJenisPeta.Controls.Add(this.GComboJenis);
            this.PanelJenisPeta.Controls.Add(this.ButtonPilih);
            this.PanelJenisPeta.Controls.Add(this.label2);
            this.PanelJenisPeta.Controls.Add(this.label1);
            this.PanelJenisPeta.Location = new System.Drawing.Point(272, 197);
            this.PanelJenisPeta.Name = "PanelJenisPeta";
            this.PanelJenisPeta.Size = new System.Drawing.Size(340, 198);
            this.PanelJenisPeta.TabIndex = 42;
            this.PanelJenisPeta.Visible = false;
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
            this.ButtonClose.Location = new System.Drawing.Point(185, 147);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(33, 38);
            this.ButtonClose.TabIndex = 26;
            this.ButtonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonClose.TextColor = System.Drawing.Color.White;
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // GComboJenis
            // 
            this.GComboJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GComboJenis.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GComboJenis.FormattingEnabled = true;
            this.GComboJenis.Items.AddRange(new object[] {
            "OpenStreetMap",
            "ArcGIS World Street Map",
            "Google Map",
            "Google Hybrid Map",
            "Google Terrain Map"});
            this.GComboJenis.Location = new System.Drawing.Point(12, 84);
            this.GComboJenis.Name = "GComboJenis";
            this.GComboJenis.Size = new System.Drawing.Size(316, 28);
            this.GComboJenis.TabIndex = 25;
            // 
            // ButtonPilih
            // 
            this.ButtonPilih.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPilih.BackColor = System.Drawing.Color.DarkCyan;
            this.ButtonPilih.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.ButtonPilih.BorderColor = System.Drawing.Color.OrangeRed;
            this.ButtonPilih.BorderRadius = 7;
            this.ButtonPilih.BorderSize = 0;
            this.ButtonPilih.FlatAppearance.BorderSize = 0;
            this.ButtonPilih.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPilih.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPilih.ForeColor = System.Drawing.Color.White;
            this.ButtonPilih.Location = new System.Drawing.Point(226, 145);
            this.ButtonPilih.Name = "ButtonPilih";
            this.ButtonPilih.Size = new System.Drawing.Size(100, 38);
            this.ButtonPilih.TabIndex = 24;
            this.ButtonPilih.Text = "PILIH";
            this.ButtonPilih.TextColor = System.Drawing.Color.White;
            this.ButtonPilih.UseVisualStyleBackColor = false;
            this.ButtonPilih.Click += new System.EventHandler(this.ButtonPilih_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Pilih jenis peta yang akan ditampilkan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "JENIS PETA";
            // 
            // MapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.PanelJenisPeta);
            this.Controls.Add(this.ButtonZoomIn);
            this.Controls.Add(this.ButtonZoomOut);
            this.Controls.Add(this.ButtonMaps);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.gradientPanel2);
            this.Controls.Add(this.ButtonPrint);
            this.Controls.Add(this.LabelH1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MapViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tampilan Peta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapViewer_FormClosed);
            this.Load += new System.EventHandler(this.MapViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).EndInit();
            this.gradientPanel2.ResumeLayout(false);
            this.PanelJenisPeta.ResumeLayout(false);
            this.PanelJenisPeta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelH1;
        private GMap.NET.WindowsForms.GMapControl MapControl;
        private Helper.UI.RJControls.RjButton ButtonPrint;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel2;
        private Helper.UI.RJControls.RjButton ButtonSave;
        private Helper.UI.RJControls.RjButton ButtonMaps;
        private Helper.UI.RJControls.RjButton ButtonZoomOut;
        private Helper.UI.RJControls.RjButton ButtonZoomIn;
        private System.Windows.Forms.Panel PanelJenisPeta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Helper.UI.RJControls.RjButton ButtonPilih;
        private System.Windows.Forms.ComboBox GComboJenis;
        private Helper.UI.RJControls.RjButton ButtonClose;
    }
}