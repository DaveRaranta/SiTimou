namespace gov.minahasa.sitimou.Views.Admin
{
    sealed partial class InputKecamatan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputKecamatan));
            this.LabelH1 = new System.Windows.Forms.Label();
            this.LabelCari = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ButtonSimpan = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.TextNamaLengkap = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.TextGpsLat = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.TextGpsLng = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TextNamaLengkap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLng)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelH1
            // 
            this.LabelH1.AutoSize = true;
            this.LabelH1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelH1.ForeColor = System.Drawing.Color.DimGray;
            this.LabelH1.Location = new System.Drawing.Point(15, 15);
            this.LabelH1.Name = "LabelH1";
            this.LabelH1.Size = new System.Drawing.Size(129, 20);
            this.LabelH1.TabIndex = 17;
            this.LabelH1.Text = "Input Kecamatan";
            // 
            // LabelCari
            // 
            this.LabelCari.AutoSize = true;
            this.LabelCari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCari.Location = new System.Drawing.Point(16, 70);
            this.LabelCari.Name = "LabelCari";
            this.LabelCari.Size = new System.Drawing.Size(101, 15);
            this.LabelCari.TabIndex = 19;
            this.LabelCari.Text = "Nama Kecamatan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "GPS Latitude";
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.FillColor = System.Drawing.Color.White;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(-8, 208);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(404, 70);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(389, 272);
            this.shapeContainer1.TabIndex = 33;
            this.shapeContainer1.TabStop = false;
            // 
            // ButtonSimpan
            // 
            this.ButtonSimpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSimpan.BackColor = System.Drawing.Color.DarkCyan;
            this.ButtonSimpan.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.ButtonSimpan.BorderColor = System.Drawing.Color.OrangeRed;
            this.ButtonSimpan.BorderRadius = 7;
            this.ButtonSimpan.BorderSize = 0;
            this.ButtonSimpan.FlatAppearance.BorderSize = 0;
            this.ButtonSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSimpan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSimpan.ForeColor = System.Drawing.Color.White;
            this.ButtonSimpan.Location = new System.Drawing.Point(270, 222);
            this.ButtonSimpan.Name = "ButtonSimpan";
            this.ButtonSimpan.Size = new System.Drawing.Size(100, 38);
            this.ButtonSimpan.TabIndex = 3;
            this.ButtonSimpan.Text = "SIMPAN";
            this.ButtonSimpan.TextColor = System.Drawing.Color.White;
            this.ButtonSimpan.UseVisualStyleBackColor = false;
            this.ButtonSimpan.Click += new System.EventHandler(this.ButtonSimpan_Click);
            // 
            // TextNamaLengkap
            // 
            this.TextNamaLengkap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextNamaLengkap.BeforeTouchSize = new System.Drawing.Size(351, 27);
            this.TextNamaLengkap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextNamaLengkap.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextNamaLengkap.Location = new System.Drawing.Point(19, 88);
            this.TextNamaLengkap.Name = "TextNamaLengkap";
            this.TextNamaLengkap.Size = new System.Drawing.Size(351, 27);
            this.TextNamaLengkap.TabIndex = 0;
            // 
            // TextGpsLat
            // 
            this.TextGpsLat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextGpsLat.BeforeTouchSize = new System.Drawing.Size(351, 27);
            this.TextGpsLat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextGpsLat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGpsLat.Location = new System.Drawing.Point(19, 143);
            this.TextGpsLat.Name = "TextGpsLat";
            this.TextGpsLat.Size = new System.Drawing.Size(169, 27);
            this.TextGpsLat.TabIndex = 1;
            this.TextGpsLat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextGpsLat_KeyPress);
            // 
            // TextGpsLng
            // 
            this.TextGpsLng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextGpsLng.BeforeTouchSize = new System.Drawing.Size(351, 27);
            this.TextGpsLng.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextGpsLng.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGpsLng.Location = new System.Drawing.Point(201, 143);
            this.TextGpsLng.Name = "TextGpsLng";
            this.TextGpsLng.Size = new System.Drawing.Size(169, 27);
            this.TextGpsLng.TabIndex = 2;
            this.TextGpsLng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextGpsLng_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(198, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "GPS Longitude";
            // 
            // InputKecamatan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 272);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextGpsLng);
            this.Controls.Add(this.TextGpsLat);
            this.Controls.Add(this.TextNamaLengkap);
            this.Controls.Add(this.ButtonSimpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LabelCari);
            this.Controls.Add(this.LabelH1);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputKecamatan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Kecamatan";
            this.Load += new System.EventHandler(this.InputKecamatan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TextNamaLengkap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelH1;
        private System.Windows.Forms.Label LabelCari;
        private System.Windows.Forms.Label label3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Helper.UI.RJControls.RjButton ButtonSimpan;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextNamaLengkap;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextGpsLat;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextGpsLng;
        private System.Windows.Forms.Label label5;
    }
}