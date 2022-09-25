namespace gov.minahasa.sitimou.Views.Common
{
    sealed partial class HtmlViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlViewer));
            this.LabelH1 = new System.Windows.Forms.Label();
            this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.HtmlUiViewer = new Syncfusion.Windows.Forms.HTMLUI.HTMLUIControl();
            this.LabelJudul = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HtmlUiViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelH1
            // 
            this.LabelH1.AutoSize = true;
            this.LabelH1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelH1.ForeColor = System.Drawing.Color.DimGray;
            this.LabelH1.Location = new System.Drawing.Point(15, 15);
            this.LabelH1.Name = "LabelH1";
            this.LabelH1.Size = new System.Drawing.Size(109, 20);
            this.LabelH1.TabIndex = 17;
            this.LabelH1.Text = "%Form_Title%";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientPanel1.BackColor = System.Drawing.Color.White;
            this.gradientPanel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.gradientPanel1.Controls.Add(this.HtmlUiViewer);
            this.gradientPanel1.Location = new System.Drawing.Point(19, 112);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(646, 426);
            this.gradientPanel1.TabIndex = 30;
            // 
            // HtmlUiViewer
            // 
            this.HtmlUiViewer.BackColor = System.Drawing.Color.White;
            this.HtmlUiViewer.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.HtmlUiViewer.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.HtmlUiViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HtmlUiViewer.DefaultFormat.BackgroundColor = System.Drawing.SystemColors.Control;
            this.HtmlUiViewer.DefaultFormat.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HtmlUiViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HtmlUiViewer.Location = new System.Drawing.Point(0, 0);
            this.HtmlUiViewer.Name = "HtmlUiViewer";
            this.HtmlUiViewer.ShowTitle = false;
            this.HtmlUiViewer.Size = new System.Drawing.Size(642, 422);
            this.HtmlUiViewer.SizeToFit = false;
            this.HtmlUiViewer.TabIndex = 1;
            this.HtmlUiViewer.Text = "<title></title><html/>";
            this.HtmlUiViewer.Title = "";
            // 
            // LabelJudul
            // 
            this.LabelJudul.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelJudul.AutoEllipsis = true;
            this.LabelJudul.BackColor = System.Drawing.Color.Transparent;
            this.LabelJudul.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelJudul.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelJudul.ForeColor = System.Drawing.Color.DimGray;
            this.LabelJudul.Location = new System.Drawing.Point(16, 70);
            this.LabelJudul.Name = "LabelJudul";
            this.LabelJudul.Size = new System.Drawing.Size(649, 28);
            this.LabelJudul.TabIndex = 58;
            this.LabelJudul.Text = "%Content_Title%";
            // 
            // HtmlViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.LabelJudul);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.LabelH1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HtmlViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Pegawai";
            this.Load += new System.EventHandler(this.InputPegawai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HtmlUiViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelH1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.HTMLUI.HTMLUIControl HtmlUiViewer;
        private System.Windows.Forms.Label LabelJudul;
    }
}