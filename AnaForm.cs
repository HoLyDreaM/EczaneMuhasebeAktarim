using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BursaEczaKoopMuhasebeAktarim
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        public AnaForm anaFrm
        {
            get;
            set;
        }
        iniOku.iniOku iniOku = new iniOku.iniOku(Application.StartupPath + "\\LoginSettings.ini");
        DosyaOku inOku = new DosyaOku(Application.StartupPath + "\\Guncelleme.ini");
        iniOku.iniOku uzakIni;
        string Cs = Properties.Settings.Default.DbConn;
        SqlConnection con = new SqlConnection();
        public string sirketAdi;
        VersiyonKontrol versiyonKontrol = new VersiyonKontrol();

        public LoginForm FrmGiris;
        public AktarimForm FrmAktarim;

        private Thread tRemoteVersiyonCek;
        private Thread tRemoteClietVersiyon;

        #region Versiyon İşlemleri Classı

        private string _versiyon;
        public string Versiyon
        {
            get { return _versiyon; }
            set { _versiyon = value; }
        }

        private string _gelenVersion;
        public string GelenVersion
        {
            get { return _gelenVersion; }
            set { _gelenVersion = value; }
        }

        private string _aciklama;
        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        private string _dosyalar;
        public string Dosyalar
        {
            get { return _dosyalar; }
            set { _dosyalar = value; }
        }

        #endregion
        private void AnaForm_Load(object sender, EventArgs e)
        {
            #region Şirket Adı-Tarih Ve Adres Bilgi Tarafı

            lblSirket.Text = sirketAdi;
            lblKayanYazi.Text = "Editör Bilgi İşlem Elektronik San. ve Tic. Ltd. Şti.     Tel&&Faks : [224] 251 84 55      Web : www.editorgroup.net      E-mail : editor@editorgroup.net      Programmer : Mehmet ÖZDEMİR" +
                          "                                                                                                                                                        ";
            #endregion

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            CheckForIllegalCrossThreadCalls = false;

            bgForm frmacilis = new bgForm();
            frmacilis.MdiParent = this;
            frmacilis.Show();

            con = new SqlConnection(Cs);
            ribbonPageGroup3.Visible = false;

            versiyonKontrol.guncellemeBulundu += versiyonKontrol_guncellemeBulundu;
            versiyonKontrol.versiyonKontrol();
        }
        private void timerTarih_Tick(object sender, EventArgs e)
        {
            this.lblKayanYazi.Text = lblKayanYazi.Text.Substring(1) + lblKayanYazi.Text[0].ToString();
            lblTarih.Text = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
        }
        private void btnLoginAyari_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FrmGiris == null || FrmGiris.IsDisposed)
            {
                FrmGiris = new LoginForm();
                FrmGiris.kontrol = false;
                FrmGiris.Show();
            }
            else
            {
                FrmGiris.Activate();
            }
        }
        private void btnKayitAktar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(FrmAktarim==null||FrmAktarim.IsDisposed)
            {
                FrmAktarim = new AktarimForm();
                FrmAktarim.Owner = this;
                FrmAktarim.MdiParent = this;
                FrmAktarim.anaFrm = this;
                FrmAktarim.Cs = Cs.ToString();
                FrmAktarim.Show();
            }
            else
            {
                FrmAktarim.Activate();
            }
        }

        #region Güncelleme Kontrolü

        delegate void dlg_alert(string mesaj);

        private void ps_alert(string mesaj)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new dlg_alert(ps_alert), mesaj);
            }
            else
            {
                DevExpress.XtraBars.Alerter.AlertInfo ai = new DevExpress.XtraBars.Alerter.AlertInfo("Yeni Güncelleme!", mesaj);
                alertControl1.Show(this, ai);

            }
        }

        DataSet dsGuncelleme;

        private void ps_remoteVersiyonCek()
        {
            try
            {
                dsGuncelleme = new DataSet("guncelleme");
                dsGuncelleme.ReadXml("http://editorgroup.net/Programlar/EczaKoop/MuhasebeAktarim/version.xml");

                GelenVersion = dsGuncelleme.Tables[0].Rows[0]["Versiyon"].ToString();
                Aciklama = dsGuncelleme.Tables[0].Rows[0]["Aciklama"].ToString();
                Dosyalar = dsGuncelleme.Tables[0].Rows[0]["Dosya"].ToString();

                if (Versiyon != GelenVersion)
                {
                    ps_alert(Aciklama);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme kontrolü hatası:" + ex.Message.ToString());
            }
        }

        private void ps_threadremoteVersiyonCek()
        {
            tRemoteVersiyonCek = new Thread(new ThreadStart(ps_remoteVersiyonCek));
            tRemoteVersiyonCek.Start();
        }

        private void ps_clientVersiyonCek()
        {
            try
            {
                uzakIni = new global::iniOku.iniOku(iniOku.IniOku("Ayar", "IniYolu") + "@LoginSettings.ini");

                GelenVersion = uzakIni.IniOku("Ayar", "version");
                Aciklama = GelenVersion.ToString() + " Versiyonu Bulundu!";
                Dosyalar = "BursaEczaKoopMuhasebeAktarim.exe";


                if (Versiyon != GelenVersion && !string.IsNullOrEmpty(GelenVersion))
                {
                    ps_alert(Aciklama);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme kontrolü hatası:" + ex.Message.ToString());
            }
        }

        private void ps_threadtUzakVersiyon()
        {
            tRemoteClietVersiyon = new Thread(new ThreadStart(ps_threadtUzakVersiyon));
            tRemoteClietVersiyon.Start();
        }

        private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            if (MessageBox.Show(this, "Güncellemek İstediğinize Emin misiniz?", "Güncelleme!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                versiyonKontrol.guncellemePrograminiBaslat("ProgramGuncelleme");
            }
        }

        void versiyonKontrol_guncellemeBulundu(object sender, VersiyonKontrol.guncellemeBilgisi e)
        {
            this.Invoke(new EventHandler(delegate
            {
                DevExpress.XtraBars.Alerter.AlertInfo info = new DevExpress.XtraBars.Alerter.AlertInfo("Güncelleme Bulundu!", "\nYeni Versiyon :" + e.yeniVersiyon + "\nAçıklama :" + e.aciklama);
                alertControl1.Show(this, info);

                ribbonPageGroup3.Visible = true;

            }));
        }
        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show(this, "Güncellemek İstediğinize Emin misiniz?", "Güncelleme!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                versiyonKontrol.guncellemePrograminiBaslat("ProgramGuncelleme");
            }
        }

        #endregion
    }
}
