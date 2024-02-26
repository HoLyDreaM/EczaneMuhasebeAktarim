namespace BursaEczaKoopMuhasebeAktarim
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.oto = new System.Windows.Forms.CheckBox();
            this.btnBaglan = new System.Windows.Forms.Button();
            this.lblSirketAdi = new System.Windows.Forms.Label();
            this.txtSirket = new System.Windows.Forms.TextBox();
            this.lblServerAdi = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // oto
            // 
            this.oto.AutoSize = true;
            this.oto.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.oto.Location = new System.Drawing.Point(208, 262);
            this.oto.Name = "oto";
            this.oto.Size = new System.Drawing.Size(116, 18);
            this.oto.TabIndex = 62;
            this.oto.Text = "Otomatik Bağlan";
            this.oto.UseVisualStyleBackColor = true;
            // 
            // btnBaglan
            // 
            this.btnBaglan.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.btnBaglan.Location = new System.Drawing.Point(128, 233);
            this.btnBaglan.Name = "btnBaglan";
            this.btnBaglan.Size = new System.Drawing.Size(196, 23);
            this.btnBaglan.TabIndex = 63;
            this.btnBaglan.Text = "Bağlan";
            this.btnBaglan.UseVisualStyleBackColor = true;
            this.btnBaglan.Click += new System.EventHandler(this.btnBaglan_Click);
            // 
            // lblSirketAdi
            // 
            this.lblSirketAdi.BackColor = System.Drawing.Color.Silver;
            this.lblSirketAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSirketAdi.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSirketAdi.Location = new System.Drawing.Point(12, 194);
            this.lblSirketAdi.Name = "lblSirketAdi";
            this.lblSirketAdi.Size = new System.Drawing.Size(117, 20);
            this.lblSirketAdi.TabIndex = 68;
            this.lblSirketAdi.Text = "Şirket Adı";
            this.lblSirketAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSirket
            // 
            this.txtSirket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSirket.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.txtSirket.Location = new System.Drawing.Point(129, 194);
            this.txtSirket.Name = "txtSirket";
            this.txtSirket.Size = new System.Drawing.Size(196, 22);
            this.txtSirket.TabIndex = 56;
            // 
            // lblServerAdi
            // 
            this.lblServerAdi.BackColor = System.Drawing.Color.Silver;
            this.lblServerAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerAdi.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblServerAdi.Location = new System.Drawing.Point(12, 171);
            this.lblServerAdi.Name = "lblServerAdi";
            this.lblServerAdi.Size = new System.Drawing.Size(117, 20);
            this.lblServerAdi.TabIndex = 70;
            this.lblServerAdi.Text = "Server Adı";
            this.lblServerAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(162)));
            this.txtServer.Location = new System.Drawing.Point(129, 171);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(196, 22);
            this.txtServer.TabIndex = 55;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BursaEczaKoopMuhasebeAktarim.Properties.Resources.editorlogo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 71;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 300);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.oto);
            this.Controls.Add(this.btnBaglan);
            this.Controls.Add(this.lblSirketAdi);
            this.Controls.Add(this.txtSirket);
            this.Controls.Add(this.lblServerAdi);
            this.Controls.Add(this.txtServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Muhasebe Kayıt Aktarımı Giriş";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox oto;
        private System.Windows.Forms.Button btnBaglan;
        private System.Windows.Forms.Label lblSirketAdi;
        private System.Windows.Forms.TextBox txtSirket;
        private System.Windows.Forms.Label lblServerAdi;
        private System.Windows.Forms.TextBox txtServer;
    }
}

