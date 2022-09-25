namespace gov.minahasa.sitimou.Views.Common
{
    sealed partial class GantiPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GantiPassword));
            this.label3 = new System.Windows.Forms.Label();
            this.TextPwdKonfirm = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.TextPwdBaru = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.TextPwdLama = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.LabelH1 = new System.Windows.Forms.Label();
            this.ButtonSimpan = new gov.minahasa.sitimou.Helper.UI.RJControls.RjButton();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdKonfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdBaru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdLama)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(16, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Konfirmasi Password";
            // 
            // TextPwdKonfirm
            // 
            this.TextPwdKonfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPwdKonfirm.BeforeTouchSize = new System.Drawing.Size(392, 27);
            this.TextPwdKonfirm.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.TextPwdKonfirm.Location = new System.Drawing.Point(19, 213);
            this.TextPwdKonfirm.MaxLength = 100;
            this.TextPwdKonfirm.Name = "TextPwdKonfirm";
            this.TextPwdKonfirm.PasswordChar = '●';
            this.TextPwdKonfirm.Size = new System.Drawing.Size(392, 27);
            this.TextPwdKonfirm.TabIndex = 2;
            this.TextPwdKonfirm.TabStop = false;
            this.TextPwdKonfirm.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(16, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password Baru";
            // 
            // TextPwdBaru
            // 
            this.TextPwdBaru.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPwdBaru.BeforeTouchSize = new System.Drawing.Size(392, 27);
            this.TextPwdBaru.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.TextPwdBaru.Location = new System.Drawing.Point(19, 153);
            this.TextPwdBaru.MaxLength = 100;
            this.TextPwdBaru.Name = "TextPwdBaru";
            this.TextPwdBaru.PasswordChar = '●';
            this.TextPwdBaru.Size = new System.Drawing.Size(392, 27);
            this.TextPwdBaru.TabIndex = 1;
            this.TextPwdBaru.TabStop = false;
            this.TextPwdBaru.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(16, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Password Lama";
            // 
            // TextPwdLama
            // 
            this.TextPwdLama.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPwdLama.BeforeTouchSize = new System.Drawing.Size(392, 27);
            this.TextPwdLama.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.TextPwdLama.Location = new System.Drawing.Point(19, 93);
            this.TextPwdLama.MaxLength = 100;
            this.TextPwdLama.Name = "TextPwdLama";
            this.TextPwdLama.PasswordChar = '●';
            this.TextPwdLama.Size = new System.Drawing.Size(392, 27);
            this.TextPwdLama.TabIndex = 0;
            this.TextPwdLama.TabStop = false;
            this.TextPwdLama.UseSystemPasswordChar = true;
            // 
            // LabelH1
            // 
            this.LabelH1.AutoSize = true;
            this.LabelH1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelH1.ForeColor = System.Drawing.Color.DimGray;
            this.LabelH1.Location = new System.Drawing.Point(15, 15);
            this.LabelH1.Name = "LabelH1";
            this.LabelH1.Size = new System.Drawing.Size(146, 20);
            this.LabelH1.TabIndex = 21;
            this.LabelH1.Text = "Input Data Pegawai";
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
            this.ButtonSimpan.Location = new System.Drawing.Point(311, 290);
            this.ButtonSimpan.Name = "ButtonSimpan";
            this.ButtonSimpan.Size = new System.Drawing.Size(100, 38);
            this.ButtonSimpan.TabIndex = 3;
            this.ButtonSimpan.Text = "SIMPAN";
            this.ButtonSimpan.TextColor = System.Drawing.Color.White;
            this.ButtonSimpan.UseVisualStyleBackColor = false;
            this.ButtonSimpan.Click += new System.EventHandler(this.ButtonSimpan_Click);
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BorderColor = System.Drawing.Color.White;
            this.rectangleShape1.FillColor = System.Drawing.Color.White;
            this.rectangleShape1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.rectangleShape1.Location = new System.Drawing.Point(-3, 277);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(443, 70);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(434, 340);
            this.shapeContainer1.TabIndex = 23;
            this.shapeContainer1.TabStop = false;
            // 
            // GantiPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(434, 340);
            this.Controls.Add(this.ButtonSimpan);
            this.Controls.Add(this.LabelH1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextPwdKonfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextPwdBaru);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextPwdLama);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GantiPassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GantiPasswordForm";
            this.Load += new System.EventHandler(this.GantiPasswordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdKonfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdBaru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextPwdLama)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextPwdKonfirm;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextPwdBaru;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt TextPwdLama;
        private System.Windows.Forms.Label LabelH1;
        private Helper.UI.RJControls.RjButton ButtonSimpan;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
    }
}