using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class DosyaOku
{
    public string path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
                string key, string def, StringBuilder retVal,
        int size, string filePath);

    public DosyaOku(string INIPath)
    {
        path = INIPath;
    }

    public void IniYaz(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, this.path);
    }

    public string IniOku(string Section, string Key)
    {
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(Section, Key, "", temp,
                                        255, this.path);

        return temp.ToString();
    }
}

class VersiyonKontrol
{
    protected string Guid = "218971C1-C8DE-4F00-981C-A4131299D166";

    DosyaOku iniOku = new DosyaOku(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini");

    System.Data.DataSet ds;
    private string link = "http://editorgroup.net/";

    private string _xmlYolu;
    public string xmlYolu
    {
        get { return _xmlYolu; }
        set { _xmlYolu = value; }
    }

    private string _mevcutVersiyon;
    public string mevcutVersiyon
    {
        get { return _mevcutVersiyon; }
        set { _mevcutVersiyon = value; }
    }

    private string _gelenVersiyon;
    public string gelenVersiyon
    {
        get { return _gelenVersiyon; }
        set { _gelenVersiyon = value; }
    }

    public int pcTipi { get; set; }
    public enum PcTipi
    {
        client = 0,
        server = 1
    }
    public VersiyonKontrol()
    {
        try
        {
            pcTipi = Convert.ToInt32(iniOku.IniOku("Ayar", "GuncellemeTuru"));
            mevcutVersiyon = iniOku.IniOku("Ayar", "version");
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            System.Windows.Forms.Application.Exit();
        }
    }

    /// <summary>
    /// Versiyon Kontrolü Yapar.Yeni güncelleme varsa bu eventı tetikler.
    /// </summary>
    public event EventHandler<guncellemeBilgisi> guncellemeBulundu;
    [STAThread]
    public void psGuncellemeBulundu(guncellemeBilgisi e)
    {
        EventHandler<guncellemeBilgisi> handler = guncellemeBulundu;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    private void _versiyonKontrol()
    {
        try
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini"))
            {
                string aciklama = "";
                string guncellemeTuru = "";

                iniOku = new DosyaOku(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini");
                guncellemeTuru = iniOku.IniOku("Ayar", "GuncellemeTuru");

                if (guncellemeTuru == "1")
                    xmlYolu = link + iniOku.IniOku("Ayar", "Ftp") + "version.xml";
                else
                    xmlYolu = iniOku.IniOku("Ayar", "ServerYolu") + @"temp/version.xml";


                ds = new System.Data.DataSet();
                ds.ReadXml(xmlYolu);

                gelenVersiyon = ds.Tables[0].Rows[0]["Versiyon"].ToString();
                aciklama = ds.Tables[0].Rows[0]["Aciklama"].ToString();

                if (gelenVersiyon != mevcutVersiyon)
                {
                    guncellemeBilgisi bilgi = new guncellemeBilgisi();
                    bilgi.yeniVersiyon = gelenVersiyon;
                    bilgi.aciklama = aciklama;
                    bilgi.eskiVersiyon = mevcutVersiyon;

                    psGuncellemeBulundu(bilgi);
                }
            }
            else
                System.Windows.Forms.MessageBox.Show(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini  Bulunamadı!");
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.InnerException.ToString());

        }

    }
    /// <summary>
    /// Versiyon Kontrol işlemini gerçekleştirir.
    /// </summary>

    public void versiyonKontrol()
    {
        System.Threading.Tasks.Task gorev = new System.Threading.Tasks.Task(() =>
        {
            _versiyonKontrol();
        });

        gorev.Start();
    }
    /// <summary>
    /// Güncelleme Programını çalıştırmak için kullanılır.
    /// </summary>
    /// <param name="Uzanti hariç program adi."></param>
    public void guncellemePrograminiBaslat(string programAdi)
    {
        try
        {
            int a = System.Diagnostics.Process.GetProcessesByName(programAdi).Count();

            if (a != 0)
                System.Windows.Forms.MessageBox.Show("Program Güncelleniyor!");
            else
                if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + programAdi + ".exe"))
                {
                    System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + programAdi + ".exe", Guid);
                }
                else
                    System.Windows.Forms.MessageBox.Show("Güncelleme Programı Bulunamadı!");
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.ToString());
        }
    }

    public class guncellemeBilgisi : EventArgs
    {
        public string yeniVersiyon;
        public string aciklama;
        public string eskiVersiyon;
    }
}

class Guncelleme
{
    DosyaOku DosyaRead;
    Kopyalama kopyala;

    System.Data.DataSet ds;
    private string link = "http://editorgroup.net/";
    private string indirilecekYer = System.Windows.Forms.Application.StartupPath + @"\temp\";

    private string _yeniVersiyon;
    public string YeniVersiyon
    {
        get { return _yeniVersiyon; }
        set { _yeniVersiyon = value; }
    }

    private int _islemDevamEdiyormu = 0;
    public int islemDevamEdiyormu
    {
        get { return _islemDevamEdiyormu; }
    }

    public enum IslemDurumu
    {
        DevamEdiyor = 1,
        Bitti = 0
    }

    public int pcTipi { get; set; }
    public enum PcTipi
    {
        client = 0,
        server = 1
    }

    private string _xmlYolu;
    public string XmlYolu
    {
        get { return _xmlYolu; }
        set { _xmlYolu = value; }
    }

    private string _indirmeYolu;
    public string IndirmeYolu
    {
        get { return _indirmeYolu; }
        set { _indirmeYolu = value; }
    }

    private int indirilecekDosyaSayisi = 0;
    private int indirilecekIndex = 0;

    List<string> listIndirilecekler;

    public Guncelleme(PcTipi PcTipi)
    {
        pcTipi = (int)PcTipi;
    }

    private void ps_dosyaIndir(string _url, string _nereyeIndirilcek)
    {
        System.Net.WebClient client = new System.Net.WebClient();
        client.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
        client.DownloadFileAsync(new Uri(_url), _nereyeIndirilcek);
    }

    void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (indirilecekDosyaSayisi == indirilecekIndex)
        {
            _islemDevamEdiyormu = 0;
            psIndirmeBilgisi(new bilgi() { bilgi1 = "İndirme İşlemi Bitmiştir!" });

            kopyala = new Kopyalama(YeniVersiyon);
            kopyala.Kopyala();
        }
        else
        {
            string[] dizi = listIndirilecekler[indirilecekIndex].Split('\\');
            int diziUzunlugu = dizi.Length;

            string dosyaAdi = dizi[diziUzunlugu - 1];

            psIndirmeBilgisi(new bilgi() { bilgi1 = dosyaAdi + " İndiriliyor..." });
            ps_dosyaIndir(IndirmeYolu + dosyaAdi, indirilecekYer + dosyaAdi.ToString());
            indirilecekIndex++;
        }
    }

    public event System.Net.DownloadProgressChangedEventHandler client_DownloadProgressChanged;
    public event EventHandler<bilgi> indirmeBilgisi;

    public void psIndirmeBilgisi(bilgi e)
    {
        var handle = indirmeBilgisi;
        if (handle != null)
        {
            handle(this, e);
        }
    }

    /// <summary>
    /// İndirilecek Dosyaları ListBox şeklinde verir
    /// </summary>
    /// <param name="lb"></param>
    public void psIndirilecekdoslariGoster(System.Windows.Forms.ListBox lb)
    {
        foreach (var item in listIndirilecekler)
            lb.Items.Add(item);
    }

    /// <summary>
    /// Güncelleme İşlemini Tetikler; Xml'den okur ve indirecekler listesini oluşturur Ve indirme işlemini başlatır!
    /// </summary>
    public void Guncelle()
    {
        try
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini"))
            {
                _islemDevamEdiyormu = 1;

                string dosya = "";
                string guncellemeTuru = "";

                listIndirilecekler = new List<string>();

                if (!System.IO.Directory.Exists(dosya))
                    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\temp\");

                DosyaRead = new DosyaOku(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini");
                guncellemeTuru = DosyaRead.IniOku("Ayar", "GuncellemeTuru");

                if (guncellemeTuru == "1")
                {
                    XmlYolu = link + DosyaRead.IniOku("Ayar", "Ftp") + "version.xml";
                    IndirmeYolu = link + DosyaRead.IniOku("Ayar", "Ftp");
                }
                else
                {
                    XmlYolu = DosyaRead.IniOku("Ayar", "ServerYolu") + @"temp/version.xml";
                    IndirmeYolu = DosyaRead.IniOku("Ayar", "ServerYolu") + @"temp/";
                }

                ds = new System.Data.DataSet();
                ds.ReadXml(XmlYolu);

                dosya = ds.Tables[0].Rows[0]["Dosya"].ToString();
                YeniVersiyon = ds.Tables[0].Rows[0]["Versiyon"].ToString();

                listIndirilecekler.Add("version.xml");

                foreach (var item in dosya.Split(';'))
                    listIndirilecekler.Add(item);

                indirilecekDosyaSayisi = listIndirilecekler.Count;
                psIndirmeBilgisi(new bilgi() { bilgi1 = listIndirilecekler[indirilecekIndex] + " İndiriliyor..." });

                ps_dosyaIndir(IndirmeYolu + "version.xml", indirilecekYer + listIndirilecekler[0].ToString());
                indirilecekIndex++;

            }
            else
                System.Windows.Forms.MessageBox.Show(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini" + " Bulunamadı!");
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        }
    }

    /// <summary>
    /// Programı Yeniden Başlatmak İçin Kullanılacak
    /// </summary>
    /// <param name="Uzantı hariç program adi"></param>
    public void ProgramiBaslat(string programAdi)
    {
        try
        {
            if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + programAdi + ".exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + programAdi + ".exe");
            }
            else
                System.Windows.Forms.MessageBox.Show("Program Bulunamadı!");

        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.ToString());
        }
    }

    public class bilgi : EventArgs
    {
        public string bilgi1;
    }
}


class Kopyalama : IDisposable
{
    System.Data.DataSet ds;
    DosyaOku iniOku = new DosyaOku(System.Windows.Forms.Application.StartupPath + @"\Guncelleme.ini");

    private string kaynak = System.Windows.Forms.Application.StartupPath + @"\temp\"; // + "dosya.uzanti"
    private string hedefYolu = System.Windows.Forms.Application.StartupPath + @"\";      // + "dosya.uzanti"
    private string dosyalar;
    private string ProgramAdi = "";
    private List<string> kopyalanacaklarinListesi;
    private string _yeniVersiyon;

    private int kopalacakSayisi = 0;
    private int kopyalanan = 0;

    public int kopyalamaDevamEdiyormu { get; set; }

    public Kopyalama(string versiyon)
    {
        _yeniVersiyon = versiyon;
    }

    /// <summary>
    /// Kopyalanacakların listesini oluşturur.Açık olan programları kapatır.Kopyalama işlemini yapar.Programı yeniden başlatır.
    /// </summary>
    public void Kopyala()
    {
        try
        {
            kopyalamaDevamEdiyormu = 1;
            ProgramAdi = iniOku.IniOku("Ayar", "ProgramAdi");

            if (System.IO.File.Exists(kaynak + @"version.xml"))
            {
                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(ProgramAdi))
                {
                    if (process.MainModule.ModuleName.Equals(ProgramAdi + ".exe"))
                    {
                        process.Kill();
                        System.Threading.Thread.Sleep(100);//Programı sonlandırmak için gereken zaman aşımı
                    }
                }

                ds = new System.Data.DataSet();
                ds.ReadXml(kaynak + @"version.xml");
                dosyalar = ds.Tables[0].Rows[0]["Dosya"].ToString();

                kopyalanacaklarinListesi = new List<string>();
                foreach (var item in dosyalar.Split(';'))
                {
                    kopyalanacaklarinListesi.Add(item);
                }

                kopalacakSayisi = kopyalanacaklarinListesi.Count;

                System.Threading.Tasks.Task gorev;

                foreach (var item in kopyalanacaklarinListesi)
                {
                    gorev = new System.Threading.Tasks.Task(() =>
                    {
                        kopyala(item);
                    });
                    gorev.Start();
                }
            }
            else
                System.Windows.Forms.MessageBox.Show(kaynak + @"version.xml  Bulunamadı!");
        }
        catch (Exception ex)
        {
            kopyalamaDevamEdiyormu = 0;
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        }
    }

    /// <summary>
    /// Listedeki sırası gelen dosyayı kopyalar. Tek bir dosya kopyalar.
    /// </summary>
    /// <param name="dosyaAdi"></param>
    private void kopyala(string dosyaAdi)
    {
        string[] dizi = dosyaAdi.Split('\\');
        int diziUzunlugu = dizi.Length;
        string dosyaYolu = "";

        dosyaAdi = dizi[diziUzunlugu - 1];
        if (diziUzunlugu > 1)
        {
            for (int i = 0; i < diziUzunlugu - 1; i++)
                dosyaYolu = dosyaYolu + dizi[i] + "\\";

            if (!System.IO.Directory.Exists(hedefYolu + dosyaYolu))
                System.IO.Directory.CreateDirectory(hedefYolu + dosyaYolu);
        }

        System.IO.File.Copy(kaynak + dosyaAdi, hedefYolu + dosyaYolu + dosyaAdi, true);
        kopyalanan++;

        if (kopyalanan == kopalacakSayisi)
        {
            kopyalamaDevamEdiyormu = 0;
            programiCalistir(ProgramAdi);
        }
    }

    /// <summary>
    /// Programı çalıştırır. 
    /// </summary>
    /// <param name="Uzanti hariç program adı"></param>
    private void programiCalistir(string programAdi)
    {
        try
        {
            iniOku.IniYaz("Ayar", "version", _yeniVersiyon);
            programAdi = System.Windows.Forms.Application.StartupPath + @"\" + programAdi + ".exe";
            System.Diagnostics.Process.Start(programAdi);
            this.Dispose();
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            Dispose();
        }
    }

    public event EventHandler<bilgi> dosyaKopyalandi;
    public void psDosyaKopyalandi(bilgi e)
    {
        var handle = dosyaKopyalandi;
        if (handle != null)
        {
            handle(this, e);
        }
    }

    public class bilgi : EventArgs
    {
        public string dosyaAdi;
    }

    public void Dispose()
    {
        System.Windows.Forms.Application.Exit();
    }
}