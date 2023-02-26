using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // process sınıfını ekliyorum

namespace Mustafa_Koray_Memis_201713709035
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string processName=Process.GetCurrentProcess().ProcessName;  //programın process adını alıyoruz
            Process[] processesByName = Process.GetProcessesByName(processName);//process sınıfının GetProcessesByName metoduyla isim ile ilgini processi çağırıyoruz
            if (processesByName.Length>1) //birden fazla process yani işlem yapılmak istenirse
            {
                MessageBox.Show("Birden fazla pencere açamazsınız."); //kullanıcıya bu mesajı gönderiyorum
                return; // return ile kapatıyorum  Application.Run komutu çalışmayacağı için program 2. kez açılmayacaktır.
            }


            Application.Run(new Form1());
        }
    }
}
