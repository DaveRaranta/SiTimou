namespace gov.minahasa.sitimou.Views.Admin
{
    sealed partial class InputOpd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputOpd));
            this.LabelH1 = new System.Windows.Forms.Label();
            this.LabelCari = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextNamaSingkat = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboJenis = new System.Windows.Forms.ComboBox();
            this.TextEmail = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ButtonSimpan = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.TextNamaLengkap = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.TextGpsLat = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.TextGpsLng = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TextNamaSingkat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEmail)).BeginInit();
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
            this.LabelH1.Size = new System.Drawing.Size(118, 20);
            this.LabelH1.TabIndex = 17;
            this.LabelH1.Text = "Input Data Unit";
            // 
            // LabelCari
            // 
            this.LabelCari.AutoSize = true;
            this.LabelCari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCari.Location = new System.Drawing.Point(16, 70);
            this.LabelCari.Name = "LabelCari";
            this.LabelCari.Size = new System.Drawing.Size(57, 15);
            this.LabelCari.TabIndex = 19;
            this.LabelCari.Text = "Jenis Unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Nama Singkat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Nama Lengkap";
            // 
            // TextNamaSingkat
            // 
            this.TextNamaSingkat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextNamaSingkat.BeforeTouchSize = new System.Drawing.Size(169, 27);
            this.TextNamaSingkat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextNamaSingkat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextNamaSingkat.Location = new System.Drawing.Point(19, 143);
            this.TextNamaSingkat.Name = "TextNamaSingkat";
            this.TextNamaSingkat.Size = new System.Drawing.Size(351, 27);
            this.TextNamaSingkat.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "GPS Latitude";
            // 
            // ComboJenis
            // 
            this.ComboJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboJenis.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboJenis.FormattingEnabled = true;
            this.ComboJenis.Items.AddRange(new object[] {
            "DINAS / UNIT",
            "DESA / KELURAHAN"});
            this.ComboJenis.Location = new System.Drawing.Point(19, 88);
            this.ComboJenis.Name = "ComboJenis";
            this.ComboJenis.Size = new System.Drawing.Size(351, 28);
            this.ComboJenis.TabIndex = 0;
            // 
            // TextEmail
            // 
            this.TextEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextEmail.BeforeTouchSize = new System.Drawing.Size(169, 27);
            this.TextEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextEmail.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEmail.Location = new System.Drawing.Point(19, 310);
            this.TextEmail.Name = "TextEmail";
            this.TextEmail.Size = new System.Drawing.Size(351, 27);
            this.TextEmail.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 31;
            this.label4.Text = "Alamat Email";
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.FillColor = System.Drawing.Color.White;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(-8, 374);
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
            this.shapeContainer1.Size = new System.Drawing.Size(389, 438);
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
            this.ButtonSimpan.Location = new System.Drawing.Point(270, 388);
            this.ButtonSimpan.Name = "ButtonSimpan";
            this.ButtonSimpan.Size = new System.Drawing.Size(100, 38);
            this.ButtonSimpan.TabIndex = 6;
            this.ButtonSimpan.Text = "SIMPAN";
            this.ButtonSimpan.TextColor = System.Drawing.Color.White;
            this.ButtonSimpan.UseVisualStyleBackColor = false;
            this.ButtonSimpan.Click += new System.EventHandler(this.ButtonSimpan_Click);
            // 
            // TextNamaLengkap
            // 
            this.TextNamaLengkap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextNamaLengkap.BeforeTouchSize = new System.Drawing.Size(169, 27);
            this.TextNamaLengkap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextNamaLengkap.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextNamaLengkap.Location = new System.Drawing.Point(19, 198);
            this.TextNamaLengkap.Name = "TextNamaLengkap";
            this.TextNamaLengkap.Size = new System.Drawing.Size(351, 27);
            this.TextNamaLengkap.TabIndex = 2;
            // 
            // TextGpsLat
            // 
            this.TextGpsLat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextGpsLat.BeforeTouchSize = new System.Drawing.Size(169, 27);
            this.TextGpsLat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextGpsLat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGpsLat.Location = new System.Drawing.Point(19, 254);
            this.TextGpsLat.Name = "TextGpsLat";
            this.TextGpsLat.Size = new System.Drawing.Size(169, 27);
            this.TextGpsLat.TabIndex = 3;
            this.TextGpsLat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextGpsLat_KeyPress);
            // 
            // TextGpsLng
            // 
            this.TextGpsLng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextGpsLng.BeforeTouchSize = new System.Drawing.Size(169, 27);
            this.TextGpsLng.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextGpsLng.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGpsLng.Location = new System.Drawing.Point(201, 254);
            this.TextGpsLng.Name = "TextGpsLng";
            this.TextGpsLng.Size = new System.Drawing.Size(169, 27);
            this.TextGpsLng.TabIndex = 4;
            this.TextGpsLng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextGpsLng_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(198, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "GPS Longitude";
            // 
            // InputOpd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 438);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextGpsLng);
            this.Controls.Add(this.TextGpsLat);
            this.Controls.Add(this.TextNamaLengkap);
            this.Controls.Add(this.ButtonSimpan);
            this.Controls.Add(this.TextEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboJenis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextNamaSingkat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelCari);
            this.Controls.Add(this.LabelH1);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputOpd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Data Unit";
            this.Load += new System.EventHandler(this.InputOpd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TextNamaSingkat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextNamaLengkap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextGpsLng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelH1;
        private System.Windows.Forms.Label LabelCari;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextNamaSingkat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboJenis;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextEmail;
        private System.Windows.Forms.Label label4;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Helper.UI.RJControls.RjButton ButtonSimpan;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextNamaLengkap;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextGpsLat;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextGpsLng;
        private System.Windows.Forms.Label label5;
    }
}