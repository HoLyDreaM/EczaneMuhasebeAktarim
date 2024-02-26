using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BursaEczaKoopMuhasebeAktarim
{
    public partial class AktarimForm : Form
    {
        public AktarimForm()
        {
            InitializeComponent();
        }
        public AnaForm anaFrm
        {
            get;
            set;
        }

        Thread Kanal;
        SqlConnection Conn;
        SqlCommand cmd;
        DataTable dt;

        iniOku.iniOku Sirket = new iniOku.iniOku(Application.StartupPath + "\\LoginSettings.ini");
        public string Cs, CsKontrol;

        string HesapKodu1, HesapKodu2, HesapKodu3, HesapKodu4, HesapKodu5, Aciklama,
            GirenSaat, EvrakNo, FormBA, VergiHesapNo, KartUnvani, OncekiTarih;
        int IslemTarihi, FisNo, SiraNo, BorcAlacak, MaddeNo, GirenTarih,
            EvrakTarihi, IslemTipi, EvrakSayisi, FormYil, FormAy,
            SEQNo, ToplamKayit;
        decimal Tutar;
        double GirenTarihx;
        string[] TarihParcala;
        private void Aktarim_Load(object sender, EventArgs e)
        {
            Conn = new SqlConnection(Cs);
            prAktarim.Visible = false;
        }
        private void btnBelgeSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog File = new OpenFileDialog();
            File.Filter = "Excel Dosyası |*.xlsx| Excel Dosyası|*.xls";
            File.FilterIndex = 2;
            File.ShowDialog();

            txtYol.Text = File.FileName;
        }
        private void btnHazirla_Click(object sender, EventArgs e)
        {
            KayitHazirla();
        }
        private void KayitHazirla()
        {
            CheckForIllegalCrossThreadCalls = false;

            if (string.IsNullOrEmpty(txtYol.Text))
            {
                MessageBox.Show("Lütfen Dosya Yolunu Seçiniz.");
                return;
            }

            btnBelgeSec.Enabled = false;
            btnHazirla.Enabled = false;
            btnAktar.Enabled = false;
            txtFisNo.Enabled = false;

            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; " +
                        "Data Source=" + txtYol.Text + "; Extended Properties=Excel 12.0");
            baglan.Open();
            string sorgu = "select * from[BURSA ECZA KOOP$]";
            OleDbDataAdapter da = new OleDbDataAdapter(sorgu, baglan);
            baglan.Close();

            DataTable DtHam = new DataTable();
            dt = new DataTable();
            da.Fill(DtHam);

            dt.Columns.Add("Tarih");
            dt.Columns.Add("Fatura Numarası");
            dt.Columns.Add("%8 KDV Matrahı");
            dt.Columns.Add("%18 KDV Matrahı");
            dt.Columns.Add("%8 KDV");
            dt.Columns.Add("%18 KDV");
            dt.Columns.Add("Tutar");

            ToplamKayit = 0;

            foreach (DataRow dr in DtHam.Rows)
            {
                if (!string.IsNullOrEmpty(dr[1].ToString()))
                {
                    DataRow rowlar = dt.NewRow();
                    rowlar["Tarih"] = Convert.ToString(Convert.ToDateTime(dr[1]).ToString("dd.MM.yyyy"));
                    rowlar["Fatura Numarası"] = dr[2];
                    rowlar["%8 KDV Matrahı"] = dr[12];
                    rowlar["%18 KDV Matrahı"] = dr[13];
                    rowlar["%8 KDV"] = dr[17];
                    rowlar["%18 KDV"] = dr[18];
                    rowlar["Tutar"] = dr[19];
                    dt.Rows.Add(rowlar);

                    ToplamKayit++;
                }
            }

            //MessageBox.Show(Convert.ToString(ToplamKayit) + " Adet Kayıt Muhasebeye Aktarılması İçin Hazırlanmıştır.");

            btnBelgeSec.Enabled = true;
            btnHazirla.Enabled = true;
            btnAktar.Enabled = true;
            txtFisNo.Enabled = true;

            grdVeri.DataSource = dt;
        }
        private void btnAktar_Click(object sender, EventArgs e)
        {
            Kanal = new Thread(new ThreadStart(AktarimYap));
            Kanal.Priority = ThreadPriority.Highest;
            Kanal.Start();
        }
        private void AktarimYap()
        {
            CheckForIllegalCrossThreadCalls = false;

            if (string.IsNullOrEmpty(txtFisNo.Text))
            {
                MessageBox.Show("Lütfen Fiş Numarası Yazınız.");
                return;
            }

            btnBelgeSec.Enabled = false;
            btnHazirla.Enabled = false;
            btnAktar.Enabled = false;
            txtFisNo.Enabled = false;

            prAktarim.Visible = true;
            prAktarim.Properties.Minimum = 1;
            prAktarim.Properties.Maximum = 0;
            prAktarim.Properties.Maximum = dt.Rows.Count;
            prAktarim.Properties.Step = 1;

            int prograscontrol = 1;

            DateTime DtSaat = DateTime.Now;
            GirenSaat = Convert.ToString(DtSaat);
            int saatuzunluk = GirenSaat.ToString().Length;
            if (saatuzunluk == 18)
            {
                GirenSaat = GirenSaat.ToString().Substring(10, 8);
            }
            else if (saatuzunluk == 19)
            {
                GirenSaat = GirenSaat.ToString().Substring(11, 8);
            }
            GirenSaat = GirenSaat.ToString().Replace(":", "");

            if (GirenSaat.ToString().Length > 8)
            {
                GirenSaat = GirenSaat.ToString().Substring(0, 8);
            }

            DateTime obj = new DateTime();
            obj = DateTime.Now;
            string str;
            GirenTarihx = 0;
            GirenTarihx = obj.ToOADate();
            str = GirenTarihx.ToString().Substring(0, 5);
            GirenTarih = Convert.ToInt32(str);

            FisNo = Convert.ToInt32(txtFisNo.Text);
            MaddeNo = Convert.ToInt32(txtFisNo.Text);
            SiraNo = 1;
            ToplamKayit = 0;

            foreach (DataRow dr in dt.Rows)
            {
                Thread.Sleep(60);

                int EvrakKontrol = (int)this.queriesTableAdapter1.EvrakNumarasiKontrol(dr[1].ToString());

                if (EvrakKontrol < 1)
                {
                    EvrakSayisi = 1;

                    string TarihBakiyoruz = dr[0].ToString();
                    TarihParcala = TarihBakiyoruz.Split('.');

                    if (Convert.ToInt32(TarihParcala[0]) > 0 && Convert.ToInt32(TarihParcala[0]) <= 10)
                    {
                        TarihBakiyoruz = "10." + TarihParcala[1] + "." + TarihParcala[2];
                    }
                    else if (Convert.ToInt32(TarihParcala[0]) > 10 && Convert.ToInt32(TarihParcala[0]) <= 20)
                    {
                        TarihBakiyoruz = "20." + TarihParcala[1] + "." + TarihParcala[2];
                    }
                    else if (Convert.ToInt32(TarihParcala[0]) > 20 && Convert.ToInt32(TarihParcala[0]) <= 30)
                    {
                        TarihBakiyoruz = "30." + TarihParcala[1] + "." + TarihParcala[2];
                    }
                    else if (Convert.ToInt32(TarihParcala[0]) == 31)
                    {
                        TarihBakiyoruz = TarihParcala[0] + "." + TarihParcala[1] + "." + TarihParcala[2];
                    }

                    OncekiTarih = Convert.ToString(this.queriesTableAdapter1.SonIslemTarihi(FisNo));

                    double IslemTarihx;
                    DateTime objIslem = new DateTime();
                    objIslem = Convert.ToDateTime(TarihBakiyoruz);
                    string strIslem;
                    IslemTarihx = 0;
                    IslemTarihx = objIslem.ToOADate();
                    strIslem = IslemTarihx.ToString().Substring(0, 5);
                    IslemTarihi = Convert.ToInt32(strIslem);

                    if (OncekiTarih != TarihBakiyoruz)
                    {
                        FisNo++;

                        this.queriesTableAdapter1.MHS010Ekle(IslemTarihi, FisNo, "", MaddeNo, GirenTarih, GirenSaat);
                        
                        MaddeNo++;
                        SiraNo = 1;
                    }
                    if (Convert.ToDecimal(dr[2].ToString()) > 0)
                    {
                        HesapKodu1 = "153 01";
                        KartUnvani = this.queriesTableAdapter1.UnvanGetir(HesapKodu1);
                        VergiHesapNo = this.queriesTableAdapter1.VergiHesapNo(HesapKodu1);
                        FormBA = "A";
                        BorcAlacak = 0;
                        IslemTipi = 4;
                        EvrakNo = dr[1].ToString();
                        EvrakTarihi = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).ToOADate());
                        Aciklama = "F:" + dr[1].ToString() + " BURSA ECZA KOOP";
                        FormYil = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Year);
                        FormAy = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Month);
                        Tutar = Convert.ToDecimal(dr[2].ToString());

                        SEQNo = (int)this.queriesTableAdapter1.SEQNo();

                        this.queriesTableAdapter1.KayitEkle(HesapKodu1, IslemTarihi, FisNo, SiraNo, Aciklama, BorcAlacak,
                            Tutar, MaddeNo, GirenTarih, GirenSaat, EvrakNo, EvrakTarihi, IslemTipi,
                            FormBA, VergiHesapNo, EvrakSayisi, KartUnvani, FormYil, FormAy, SEQNo);

                        SiraNo++;
                        ToplamKayit++;

                        KayitGuncelle(HesapKodu1, Convert.ToString(FormAy), IslemTarihi, 0, GirenTarih, GirenSaat, Tutar, 0);
                    }
                    if (Convert.ToDecimal(dr[3].ToString()) > 0)
                    {
                        HesapKodu2 = "153 02";
                        KartUnvani = this.queriesTableAdapter1.UnvanGetir(HesapKodu2);
                        VergiHesapNo = this.queriesTableAdapter1.VergiHesapNo(HesapKodu2);
                        FormBA = "A";
                        BorcAlacak = 0;
                        IslemTipi = 4;
                        EvrakNo = dr[1].ToString();
                        EvrakTarihi = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).ToOADate());
                        Aciklama = "F:" + dr[1].ToString() + " BURSA ECZA KOOP";
                        FormYil = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Year);
                        FormAy = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Month);
                        Tutar = Convert.ToDecimal(dr[3].ToString());

                        SEQNo = (int)this.queriesTableAdapter1.SEQNo();

                        this.queriesTableAdapter1.KayitEkle(HesapKodu2, IslemTarihi, FisNo, SiraNo, Aciklama, BorcAlacak,
                            Tutar, MaddeNo, GirenTarih, GirenSaat, EvrakNo, EvrakTarihi, IslemTipi,
                            FormBA, VergiHesapNo, EvrakSayisi, KartUnvani, FormYil, FormAy, SEQNo);

                        SiraNo++;
                        ToplamKayit++;

                        KayitGuncelle(HesapKodu2, Convert.ToString(FormAy), IslemTarihi, 0, GirenTarih, GirenSaat, Tutar, 0);
                    }
                    if (Convert.ToDecimal(dr[4].ToString()) > 0)
                    {
                        HesapKodu3 = "191 01";
                        KartUnvani = "";
                        VergiHesapNo = "";
                        FormBA = "";
                        BorcAlacak = 0;
                        IslemTipi = 0;
                        EvrakNo = dr[1].ToString();
                        EvrakTarihi = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).ToOADate());
                        Aciklama = "F:" + dr[1].ToString() + " BURSA ECZA KOOP";
                        FormYil = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Year);
                        FormAy = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Month);
                        Tutar = Convert.ToDecimal(dr[4].ToString());

                        SEQNo = (int)this.queriesTableAdapter1.SEQNo();

                        this.queriesTableAdapter1.KayitEkle(HesapKodu3, IslemTarihi, FisNo, SiraNo, Aciklama, BorcAlacak,
                            Tutar, MaddeNo, GirenTarih, GirenSaat, EvrakNo, EvrakTarihi, IslemTipi,
                            FormBA, VergiHesapNo, EvrakSayisi, KartUnvani, FormYil, FormAy, SEQNo);

                        SiraNo++;
                        ToplamKayit++;

                        KayitGuncelle(HesapKodu3, Convert.ToString(FormAy), IslemTarihi, 0, GirenTarih, GirenSaat, Tutar, 0);
                    }
                    if (Convert.ToDecimal(dr[5].ToString()) > 0)
                    {
                        HesapKodu4 = "191 02";
                        KartUnvani = "";
                        VergiHesapNo = "";
                        FormBA = "";
                        BorcAlacak = 0;
                        IslemTipi = 0;
                        EvrakNo = dr[1].ToString();
                        EvrakTarihi = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).ToOADate());
                        Aciklama = "F:" + dr[1].ToString() + " BURSA ECZA KOOP";
                        FormYil = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Year);
                        FormAy = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Month);
                        Tutar = Convert.ToDecimal(dr[5].ToString());

                        SEQNo = (int)this.queriesTableAdapter1.SEQNo();

                        this.queriesTableAdapter1.KayitEkle(HesapKodu4, IslemTarihi, FisNo, SiraNo, Aciklama, BorcAlacak,
                            Tutar, MaddeNo, GirenTarih, GirenSaat, EvrakNo, EvrakTarihi, IslemTipi,
                            FormBA, VergiHesapNo, EvrakSayisi, KartUnvani, FormYil, FormAy, SEQNo);

                        SiraNo++;
                        ToplamKayit++;

                        KayitGuncelle(HesapKodu4, Convert.ToString(FormAy), IslemTarihi, 0, GirenTarih, GirenSaat, Tutar, 0);
                    }
                    if (Convert.ToDecimal(dr[6].ToString()) > 0)
                    {
                        HesapKodu5 = "320 B01";
                        KartUnvani = "";
                        VergiHesapNo = "";
                        FormBA = "";
                        BorcAlacak = 1;
                        IslemTipi = 0;
                        EvrakNo = dr[1].ToString();
                        EvrakTarihi = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).ToOADate());
                        Aciklama = "F:" + dr[1].ToString() + " BURSA ECZA KOOP";
                        FormYil = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Year);
                        FormAy = Convert.ToInt32(Convert.ToDateTime(dr[0].ToString()).Month);
                        Tutar = Convert.ToDecimal(dr[6].ToString());

                        SEQNo = (int)this.queriesTableAdapter1.SEQNo();

                        this.queriesTableAdapter1.KayitEkle(HesapKodu5, IslemTarihi, FisNo, SiraNo, Aciklama, BorcAlacak,
                                Tutar, MaddeNo, GirenTarih, GirenSaat, EvrakNo, EvrakTarihi, IslemTipi,
                                FormBA, VergiHesapNo, EvrakSayisi, KartUnvani, FormYil, FormAy, SEQNo);

                        SiraNo++;
                        ToplamKayit++;

                        KayitGuncelle(HesapKodu5, Convert.ToString(FormAy), 0, IslemTarihi, GirenTarih, GirenSaat, 0, Tutar);
                    }

                    prograscontrol++;
                    prAktarim.PerformStep();
                }                              
            }

            MessageBox.Show(Convert.ToString(ToplamKayit) + " Adet Kayıt Muhasebeye Aktarılmıştır.");
            
            prAktarim.Properties.Minimum = 1;
            prAktarim.Visible = false; 

            btnBelgeSec.Enabled = true;
            btnHazirla.Enabled = true;
            btnAktar.Enabled = true;
            txtFisNo.Enabled = true;

            dt.Rows.Clear();
            grdVeri.DataSource = null;
        }
        private void KayitGuncelle(string HesapKodu,string Ay,int BorcTarih,int AlacakTarih,int Tarih,
            string Saat,decimal Borc,decimal Alacak)
        {
            if(Conn.State==ConnectionState.Closed)
            {
                Conn.Open();
            }
            
            string sorgu = "";

            if (BorcTarih > 0)
            {
                sorgu = "UPDATE MHS003 SET " + Environment.NewLine +
                "MHS003_AylikBorcAlacakAylikBorc" + Ay + " = MHS003_AylikBorcAlacakAylikBorc" + Ay + "+" + Borc.ToString().Replace(',','.') + ", " + Environment.NewLine +
                "MHS003_SonBorcTarihi=" + BorcTarih + ", " + Environment.NewLine +
                "MHS003_DegistirenTarih=" + Tarih + ", " + Environment.NewLine +
                "MHS003_DegistirenSaat='" + Saat + "', " + Environment.NewLine +
                "MHS003_DegistirenKodu='Editor' " + Environment.NewLine +
                "WHERE MHS003_AltHesapKodu='" + HesapKodu + "'";
            }
            if (AlacakTarih > 0)
            {
                sorgu = "UPDATE MHS003 SET " + Environment.NewLine +
                "MHS003_AylikBorcAlacakAylikAlacak" + Ay + " = MHS003_AylikBorcAlacakAylikAlacak" + Ay + "+" + Alacak.ToString().Replace(',', '.') + ", " + Environment.NewLine +
                "MHS003_SonAlacakTarihi=" + AlacakTarih + ", " + Environment.NewLine +
                "MHS003_DegistirenTarih=" + Tarih + ", " + Environment.NewLine +
                "MHS003_DegistirenSaat='" + Saat + "', " + Environment.NewLine +
                "MHS003_DegistirenKodu='Editor' " + Environment.NewLine +
                "WHERE MHS003_AltHesapKodu='" + HesapKodu + "'";
            }

            cmd = new SqlCommand(sorgu, Conn);
            cmd.ExecuteNonQuery();
        }
        private void AktarimForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kanal != null)
            {
                Kanal.Abort();
            }
        }
    }
}
