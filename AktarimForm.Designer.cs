namespace BursaEczaKoopMuhasebeAktarim
{
    partial class AktarimForm
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
            this.panelUst = new System.Windows.Forms.Panel();
            this.prAktarim = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnAktar = new System.Windows.Forms.Button();
            this.txtFisNo = new System.Windows.Forms.TextBox();
            this.txtYol = new System.Windows.Forms.TextBox();
            this.btnHazirla = new System.Windows.Forms.Button();
            this.lblFisNumarasi = new System.Windows.Forms.Label();
            this.lblBelgeYolu = new System.Windows.Forms.Label();
            this.btnBelgeSec = new System.Windows.Forms.Button();
            this.panelAlt = new System.Windows.Forms.Panel();
            this.grdVeri = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.queriesTableAdapter1 = new BursaEczaKoopMuhasebeAktarim.DsAktarimTableAdapters.QueriesTableAdapter();
            this.panelUst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prAktarim.Properties)).BeginInit();
            this.panelAlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVeri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelUst
            // 
            this.panelUst.Controls.Add(this.prAktarim);
            this.panelUst.Controls.Add(this.btnAktar);
            this.panelUst.Controls.Add(this.txtFisNo);
            this.panelUst.Controls.Add(this.txtYol);
            this.panelUst.Controls.Add(this.btnHazirla);
            this.panelUst.Controls.Add(this.lblFisNumarasi);
            this.panelUst.Controls.Add(this.lblBelgeYolu);
            this.panelUst.Controls.Add(this.btnBelgeSec);
            this.panelUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUst.Location = new System.Drawing.Point(0, 0);
            this.panelUst.Name = "panelUst";
            this.panelUst.Size = new System.Drawing.Size(904, 100);
            this.panelUst.TabIndex = 0;
            // 
            // prAktarim
            // 
            this.prAktarim.Location = new System.Drawing.Point(347, 46);
            this.prAktarim.Name = "prAktarim";
            this.prAktarim.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            this.prAktarim.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.prAktarim.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.prAktarim.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Green;
            this.prAktarim.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.prAktarim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.prAktarim.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Green;
            this.prAktarim.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.prAktarim.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.prAktarim.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Green;
            this.prAktarim.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.prAktarim.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.prAktarim.Properties.ShowTitle = true;
            this.prAktarim.Properties.StartColor = System.Drawing.Color.Green;
            this.prAktarim.Size = new System.Drawing.Size(298, 23);
            this.prAktarim.TabIndex = 21;
            // 
            // btnAktar
            // 
            this.btnAktar.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAktar.Image = global::BursaEczaKoopMuhasebeAktarim.Properties.Resources.Aktar;
            this.btnAktar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAktar.Location = new System.Drawing.Point(217, 46);
            this.btnAktar.Name = "btnAktar";
            this.btnAktar.Size = new System.Drawing.Size(124, 23);
            this.btnAktar.TabIndex = 20;
            this.btnAktar.Text = "Kayıtları Aktar";
            this.btnAktar.UseVisualStyleBackColor = true;
            this.btnAktar.Click += new System.EventHandler(this.btnAktar_Click);
            // 
            // txtFisNo
            // 
            this.txtFisNo.BackColor = System.Drawing.Color.Honeydew;
            this.txtFisNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFisNo.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFisNo.Location = new System.Drawing.Point(128, 47);
            this.txtFisNo.Name = "txtFisNo";
            this.txtFisNo.Size = new System.Drawing.Size(83, 21);
            this.txtFisNo.TabIndex = 19;
            // 
            // txtYol
            // 
            this.txtYol.BackColor = System.Drawing.Color.Honeydew;
            this.txtYol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYol.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtYol.Location = new System.Drawing.Point(128, 9);
            this.txtYol.Name = "txtYol";
            this.txtYol.ReadOnly = true;
            this.txtYol.Size = new System.Drawing.Size(352, 21);
            this.txtYol.TabIndex = 17;
            // 
            // btnHazirla
            // 
            this.btnHazirla.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHazirla.Image = global::BursaEczaKoopMuhasebeAktarim.Properties.Resources.Hazirla;
            this.btnHazirla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHazirla.Location = new System.Drawing.Point(563, 8);
            this.btnHazirla.Name = "btnHazirla";
            this.btnHazirla.Size = new System.Drawing.Size(82, 23);
            this.btnHazirla.TabIndex = 2;
            this.btnHazirla.Text = "Hazırla";
            this.btnHazirla.UseVisualStyleBackColor = true;
            this.btnHazirla.Click += new System.EventHandler(this.btnHazirla_Click);
            // 
            // lblFisNumarasi
            // 
            this.lblFisNumarasi.BackColor = System.Drawing.Color.Silver;
            this.lblFisNumarasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFisNumarasi.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFisNumarasi.Location = new System.Drawing.Point(11, 47);
            this.lblFisNumarasi.Name = "lblFisNumarasi";
            this.lblFisNumarasi.Size = new System.Drawing.Size(117, 21);
            this.lblFisNumarasi.TabIndex = 18;
            this.lblFisNumarasi.Text = "Fiş Numarası";
            this.lblFisNumarasi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBelgeYolu
            // 
            this.lblBelgeYolu.BackColor = System.Drawing.Color.Silver;
            this.lblBelgeYolu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBelgeYolu.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBelgeYolu.Location = new System.Drawing.Point(11, 9);
            this.lblBelgeYolu.Name = "lblBelgeYolu";
            this.lblBelgeYolu.Size = new System.Drawing.Size(117, 21);
            this.lblBelgeYolu.TabIndex = 18;
            this.lblBelgeYolu.Text = "Belge Yolu";
            this.lblBelgeYolu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBelgeSec
            // 
            this.btnBelgeSec.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBelgeSec.Image = global::BursaEczaKoopMuhasebeAktarim.Properties.Resources.BelgeSec;
            this.btnBelgeSec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBelgeSec.Location = new System.Drawing.Point(481, 8);
            this.btnBelgeSec.Name = "btnBelgeSec";
            this.btnBelgeSec.Size = new System.Drawing.Size(82, 23);
            this.btnBelgeSec.TabIndex = 1;
            this.btnBelgeSec.Text = "Belge Seç";
            this.btnBelgeSec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBelgeSec.UseVisualStyleBackColor = true;
            this.btnBelgeSec.Click += new System.EventHandler(this.btnBelgeSec_Click);
            // 
            // panelAlt
            // 
            this.panelAlt.Controls.Add(this.grdVeri);
            this.panelAlt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAlt.Location = new System.Drawing.Point(0, 100);
            this.panelAlt.Name = "panelAlt";
            this.panelAlt.Size = new System.Drawing.Size(904, 371);
            this.panelAlt.TabIndex = 1;
            // 
            // grdVeri
            // 
            this.grdVeri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVeri.Location = new System.Drawing.Point(0, 0);
            this.grdVeri.MainView = this.gridView1;
            this.grdVeri.Name = "grdVeri";
            this.grdVeri.Size = new System.Drawing.Size(904, 371);
            this.grdVeri.TabIndex = 0;
            this.grdVeri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.GridControl = this.grdVeri;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // AktarimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 471);
            this.Controls.Add(this.panelAlt);
            this.Controls.Add(this.panelUst);
            this.Name = "AktarimForm";
            this.Text = "Kayıt Aktarımı";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AktarimForm_FormClosing);
            this.Load += new System.EventHandler(this.Aktarim_Load);
            this.panelUst.ResumeLayout(false);
            this.panelUst.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prAktarim.Properties)).EndInit();
            this.panelAlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVeri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUst;
        private System.Windows.Forms.Panel panelAlt;
        private DevExpress.XtraGrid.GridControl grdVeri;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btnHazirla;
        private System.Windows.Forms.Button btnBelgeSec;
        private System.Windows.Forms.TextBox txtYol;
        private System.Windows.Forms.Label lblBelgeYolu;
        private System.Windows.Forms.Label lblFisNumarasi;
        private System.Windows.Forms.TextBox txtFisNo;
        private DsAktarimTableAdapters.QueriesTableAdapter queriesTableAdapter1;
        private System.Windows.Forms.Button btnAktar;
        private DevExpress.XtraEditors.ProgressBarControl prAktarim;
    }
}