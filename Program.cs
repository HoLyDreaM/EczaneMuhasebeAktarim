using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BursaEczaKoopMuhasebeAktarim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        internal static ApplicationContext ac = new ApplicationContext();
        [STAThread]
        static void Main()
        {
            try
            {
                bool koruma;
                Mutex mt = new Mutex(true, "koru", out koruma);
                if (!koruma)
                {
                    MessageBox.Show("Mevcut Program Çalışmaktadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ac.MainForm = new LoginForm();
                Application.Run(ac);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
