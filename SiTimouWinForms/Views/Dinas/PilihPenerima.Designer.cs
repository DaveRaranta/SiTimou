namespace gov.minahasa.sitimou.Views.Dinas
{
    sealed partial class PilihPenerima
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PilihPenerima));
            this.LabelH1 = new System.Windows.Forms.Label();
            this.LabelCari = new System.Windows.Forms.Label();
            this.ComboPenerima = new System.Windows.Forms.ComboBox();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ButtonSimpan = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
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
            this.LabelH1.Text = "Pilih Penerima";
            // 
            // LabelCari
            // 
            this.LabelCari.AutoSize = true;
            this.LabelCari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCari.Location = new System.Drawing.Point(16, 70);
            this.LabelCari.Name = "LabelCari";
            this.LabelCari.Size = new System.Drawing.Size(86, 15);
            this.LabelCari.TabIndex = 19;
            this.LabelCari.Text = "Nama Pegawai";
            // 
            // ComboPenerima
            // 
            this.ComboPenerima.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboPenerima.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboPenerima.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboPenerima.FormattingEnabled = true;
            this.ComboPenerima.Location = new System.Drawing.Point(19, 88);
            this.ComboPenerima.Name = "ComboPenerima";
            this.ComboPenerima.Size = new System.Drawing.Size(371, 28);
            this.ComboPenerima.TabIndex = 0;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.FillColor = System.Drawing.Color.White;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(-8, 154);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(424, 70);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(409, 218);
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
            this.ButtonSimpan.Location = new System.Drawing.Point(290, 168);
            this.ButtonSimpan.Name = "ButtonSimpan";
            this.ButtonSimpan.Size = new System.Drawing.Size(100, 38);
            this.ButtonSimpan.TabIndex = 6;
            this.ButtonSimpan.Text = "PILIH";
            this.ButtonSimpan.TextColor = System.Drawing.Color.White;
            this.ButtonSimpan.UseVisualStyleBackColor = false;
            this.ButtonSimpan.Click += new System.EventHandler(this.ButtonSimpan_Click);
            // 
            // PilihPenerima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 218);
            this.Controls.Add(this.ButtonSimpan);
            this.Controls.Add(this.ComboPenerima);
            this.Controls.Add(this.LabelCari);
            this.Controls.Add(this.LabelH1);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PilihPenerima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pilih Penerima";
            this.Load += new System.EventHandler(this.InputDisposisiLaporan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelH1;
        private System.Windows.Forms.Label LabelCari;
        private System.Windows.Forms.ComboBox ComboPenerima;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Helper.UI.RJControls.RjButton ButtonSimpan;
    }
}