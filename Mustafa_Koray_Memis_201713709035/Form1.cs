using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mustafa_Koray_Memis_201713709035
{
    //tam ekranda görüntü bozulabilir kapattım eğer istenirse anchor ile ayarlanabilir..
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int SeritSayisi = 1;
        Random RND = new Random();
        int Yol = 0, Hiz = 70;
        class Random_Araba
        {
            public bool rnaraba = false;
            public PictureBox rncar;
            public bool sure = false;
        }
        Random_Araba[] rndAraba = new Random_Araba[2];
        void BringRandomCar(PictureBox pb) //Random arabalarımızı switch case ile çağırıyoruz
        {
            int rnd = RND.Next(0, 5);
            switch (rnd)
            {

                case 0:
                    pb.Image = Properties.Resources.car0;
                    break;
                case 1:
                    pb.Image = Properties.Resources.car1;
                    break;
                case 2:
                    pb.Image = Properties.Resources.car2;
                    break;
                case 3:
                    pb.Image = Properties.Resources.car3;
                    break;
                case 4:
                    pb.Image = Properties.Resources.car4;
                    break;
            }
            pb.SizeMode = PictureBoxSizeMode.StretchImage;//arabaları picture box içerisne streç modunda sığdırdık
        }
        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void AracYerine()
        {
            if (SeritSayisi == 1)//orta şerit
            {
                ferro.Location = new Point(296, 507);
            }
            else if (SeritSayisi == 0)//sol şerit
            {
                ferro.Location = new Point(75, 507);
            }
            else if (SeritSayisi == 2)//sağ şerit
            {
                ferro.Location = new Point(525, 507);
            }
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)//klavyeden sağ yön tusuna veya d tusuna basılırsa şerit değiştir
            {
                if (SeritSayisi < 2)
                    SeritSayisi++;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)//klavyeden sol yön tusuna veya a tusuna basılırsa şerit değiştir
            {
                if (SeritSayisi > 0)
                    SeritSayisi--;
            }
            AracYerine();
            if (e.KeyCode.ToString() == "Escape")
            //menuye girmek için kullanıcıdan escape komutu bekliyoruz eğer girilirse menuyu görünür yapıyorum.
            {
                button6.Enabled = false;
                button6.BackColor = Color.Red;
                button5.Enabled = false;
                button5.BackColor = Color.Red;
                MENU.Visible = true;
                timerRandomAraba.Enabled = false;
                timerSerit.Enabled = false;                                 
                button1.Enabled = true;
                button1.BackColor = Color.MediumBlue;
                axWindowsMediaPlayer1.Ctlcontrols.pause();//müziği durdurduk                              
                axWindowsMediaPlayer1.URL = @"muzik/menu.mp3"; //menu muziğimiz çalıyor                
            }            
            if (e.KeyCode == Keys.M)
            {                
                axWindowsMediaPlayer1.Ctlcontrols.pause();//müziği durdurduk
                pictureBox2.Image = Properties.Resources.volumeOff;//ses kapalı simgesini getirdik                
            }               
            else if(e.KeyCode == Keys.N)
            {                
                axWindowsMediaPlayer1.Ctlcontrols.play();//müziği baslattık
                pictureBox2.Image = Properties.Resources.volumeON;//ses acık simgesini getirdik
            }
            
        }        
        private void RandomMuzikEkle()
        {
            int Muzik = RND.Next(1, 4);//debug klasörüne eklediğimiz müzikleri rasgele çalıcak 3 adet eklediğimiz için 1-3 arası çaldırıyoruz.
            axWindowsMediaPlayer1.URL = @"muzik/sarki" + Muzik.ToString() + ".mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true; //HER ZAMAN OYUNUMUZU EKRANDA TUTUYORUZ           
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//kullanıcının uygulama boyutunu değiştirmesini istemediğim için form border style'ını fixliyorum
            for (var i = 0; i < rndAraba.Length; i++)
            {
                rndAraba[i] = new Random_Araba();
            }
            rndAraba[0].sure = true;
            timerRandomAraba.Enabled = false;
            timerSerit.Enabled = false;
            AracYerine();
            axWindowsMediaPlayer1.URL = @"muzik/menu.mp3"; //menu muziğimiz çalıyor                       
            labelMaxSkor.Text = Settings1.Default.MaxSkor.ToString();
            button5.Enabled = false;
            button5.BackColor = Color.Red;
            button1.Enabled = false;
            button1.BackColor = Color.Red;
        }

        bool Ses = true;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Ses == true)
            {
                Ses = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();//müziği durdurduk
                pictureBox2.Image = Properties.Resources.volumeOff;//ses kapalı simgesini getirdik
            }
            else if (Ses == false)
            {
                Ses = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();//müziği baslattık                
                pictureBox2.Image = Properties.Resources.volumeON;//ses acık simgesini getirdik
            }
        }

        private void timerRandomAraba_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rndAraba.Length; i++)
            {
                if (!rndAraba[i].rnaraba && rndAraba[i].sure)
                {
                    rndAraba[i].rncar = new PictureBox();
                    BringRandomCar(rndAraba[i].rncar);
                    rndAraba[i].rncar.Size = new Size(90, 150);
                    rndAraba[i].rncar.Top = -rndAraba[i].rncar.Height;

                    int sol_Yerles = RND.Next(0, 3); //random arabaları şeritlere yerleştiriyorum asıl arabamızla üst üste gelmeleri için aynı pixel değerlerini kullandım

                    if (sol_Yerles == 0)
                    {

                        rndAraba[i].rncar.Left = 75;

                    }
                    else if (sol_Yerles == 1)
                    {
                        rndAraba[i].rncar.Left = 296;
                    }

                    else if (sol_Yerles == 2)
                    {
                        rndAraba[i].rncar.Left = 525;
                    }


                    this.Controls.Add(rndAraba[i].rncar);
                    rndAraba[i].rnaraba = true;

                }
                else
                {
                    if (rndAraba[i].sure)
                    {
                        rndAraba[i].rncar.Top += 20;
                        if (rndAraba[i].rncar.Top >= 154)
                        {
                            for (int j = 0; j < rndAraba.Length; j++)
                            {
                                if (!rndAraba[j].sure)
                                {
                                    rndAraba[j].sure = true;
                                    break;
                                }
                            }
                        }
                        if (rndAraba[i].rncar.Top >= this.Height - 20)
                        {
                            rndAraba[i].rncar.Dispose();
                            rndAraba[i].rnaraba = false;
                            rndAraba[i].sure = false;
                        }
                    }
                }
                //Kaza kod kısmı
                if (rndAraba[i].sure)
                {
                    float mutlakX = Math.Abs((ferro.Left + (ferro.Width / 2)) - (rndAraba[i].rncar.Left + (rndAraba[i].rncar.Width / 2)));
                    float mutlakY = Math.Abs((ferro.Top + (ferro.Height / 2)) - (rndAraba[i].rncar.Top + (rndAraba[i].rncar.Height / 2)));
                    float FarkGenislik = (ferro.Width / 2) + (rndAraba[i].rncar.Width / 2);
                    float FarkYukseklik = (ferro.Height / 2) + (rndAraba[i].rncar.Height / 2);
                    if ((FarkGenislik > mutlakX) && (FarkYukseklik > mutlakY))//araclar birbirlerine temas ettiklerinde
                    {
                        timerRandomAraba.Enabled = false; //araba akısını durduruyorum
                        timerSerit.Enabled = false; // serit akısını durduruyorum
                        axWindowsMediaPlayer1.Ctlcontrols.stop(); // müzik duruyor
                        axWindowsMediaPlayer1.URL = @"muzik/kaza.mp3"; //kaza efektimizi çağırıyoruz
                        axWindowsMediaPlayer1.Ctlcontrols.play();// kaza efektimiz çalıyor                    
                        
                        if (Yol>Settings1.Default.MaxSkor)
                        {
                            MessageBox.Show("Yeni rekorunuz tebrikler!! ==>" + Yol.ToString() + "m"," ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Settings1.Default.MaxSkor = Yol;
                            Settings1.Default.Save();
                        }
                        DialogResult ddd = MessageBox.Show("Kaza yaptınız. Yeniden oynamak ister misiniz?", "Kaza", MessageBoxButtons.YesNo, MessageBoxIcon.Question);// kazadan sonra tercihi oyuncuya bırakıyoruz
                        if(ddd==DialogResult.Yes)//kullanıcıdan evet yanıtını alırsak random olarak çağırdığımız arabaları sıfırlayıp aracımızı default konumuna çekiyoruz
                        {
                            AracYerine();
                            ferro.Location = new Point(296, 507);
                            for (int j = 0; j <rndAraba.Length; j++)
                            {
                                rndAraba[j].rncar.Dispose();
                                rndAraba[j].rnaraba = false;
                                rndAraba[j].sure = false;
                            }
                            Yol = 0;//yol ve hız değerlerini baslangıcta atadığım default değerlere tekrar çekiyorum. Aynı sekilde level1 değerlerini tekrar girdim.
                            Hiz = 70;
                            rndAraba[0].sure = true;
                            timerRandomAraba.Enabled= true;
                            timerRandomAraba.Interval = 200;
                            timerSerit.Enabled = true;
                            timerSerit.Interval = 200;
                            //kazadan sonra müzik kesilip sadece kaza sesi çaldığı için bu kod satırını kullanıyorum
                            RandomMuzikEkle(); // random müzik komutunu çağırdım
                            axWindowsMediaPlayer1.Ctlcontrols.play(); //müziği baslattım
                            labelMaxSkor.Text = Settings1.Default.MaxSkor.ToString(); //max skoru Settings1e kayıt aldığımız maxskor veritabanına aldığı kayıtı ekrana yazdırıyorum
                        } 
                        
                        //hayır cevabı gelirse menüyü çağırıyoruz
                        else
                        {                            
                            MENU.Visible = true; //menü ekranını getiriyorum
                            button1.Enabled = false; //kaza yaptıktan sonra kaldığımız yerden devam etmemesi için buton1i deaktif ediyorum.
                            button1.BackColor = Color.Red;
                            button6.Enabled = false;
                            button6.BackColor = Color.Red;
                            button5.Enabled = true;
                            button5.BackColor = Color.MediumBlue;//butonun tıklanabilir olduğunu kullanıcıya mavi renk olarak yansıtıyorum
                            axWindowsMediaPlayer1.URL = @"muzik/menu.mp3"; //menu muziğimiz çalıyor                            
                        }                        
                    }
                }
            }
        }
            bool SeritHareketi = false;

        private void button1_Click(object sender, EventArgs e) //DEVAM ET BUTONU
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();//müziği baslattık
            MENU.Visible = false; //oyunu devam ettirdiğimiz için menüyü deaktif ediyorum
            MENU2.Visible = false;
            this.Focus();//form'a focus oluyor ama ne olur ne olmaz genede form'a focus attırıyorum
            timerRandomAraba.Enabled = true; //random araba ve serit akısını tekrar sağlıyorum
            timerSerit.Enabled = true;
            RandomMuzikEkle(); // random müzik komutunu çağırdım
            axWindowsMediaPlayer1.Ctlcontrols.play(); //müziği baslattım
        }

        private void button2_Click(object sender, EventArgs e)//ÇIKIŞ YAP BUTONU
        {
            DialogResult ccc = MessageBox.Show("Oyunu kapatmak istiyor musun?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//oyuncuyu oyundan çıkarmamak için elimden geleni yapıyorum.
            if (ccc == DialogResult.Yes) //evet derse oyunu kapattırıyorum
            {
                this.Close();
            }
            else //hayır derse menuyu tekrar ekrana getiriyorum
            {
                MENU.Visible = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {            
        }
        void LevelSistemi()//Hız-level sistemi. Gidilen yola göre interval düsürüp kullanıcıya hızlanma görünümü veriyorum.
        {
            //2.Level // hızlandıkça alınan yoluda arttırıyorum
            if (Yol>100&&Yol<300)
            {
                Yol+=1;
                Hiz = 90;
                timerSerit.Interval = 125;
                timerRandomAraba.Interval = 100;
            }
            //3.Level
            else if (Yol > 300 && Yol < 550)
            {
                Yol += 2;
                Hiz = 120;
                timerSerit.Interval = 100;
                timerRandomAraba.Interval = 80;
            }
            //4.Level
            else if (Yol > 550 && Yol < 1250)
            {
                Yol += 3;
                Hiz = 150;
                timerSerit.Interval = 80;
                timerRandomAraba.Interval = 30;
            }
            //5.Level
            else if (Yol > 1250)
            {
                Yol += 4;
                Hiz = 220;
                timerSerit.Interval = 50;
                timerRandomAraba.Interval = 10;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                 }

        private void button3_Click_1(object sender, EventArgs e)//AYARLAR BUTONU
        {
            MENU.Visible = false;
            MENU2.Visible = true;            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MENU.Visible = true;
            MENU2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e) //TEKRAR OYNA BUTONU
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();//müziği baslattık
            MENU.Visible = false;
            MENU2.Visible = false;
            AracYerine();
            ferro.Location = new Point(296, 507);
            for (int j = 0; j < rndAraba.Length; j++)
            {
                rndAraba[j].rncar.Dispose();
                rndAraba[j].rnaraba = false;
                rndAraba[j].sure = false;
            }
            Yol = 0;
            Hiz = 70;
            rndAraba[0].sure = true;
            timerRandomAraba.Enabled = true;
            timerRandomAraba.Interval = 200;
            timerSerit.Enabled = true;
            timerSerit.Interval = 200;
            //kazadan sonra müzik kesilip sadece kaza sesi çaldığı için bu kod satırını kullanıyorum
            RandomMuzikEkle();
            axWindowsMediaPlayer1.Ctlcontrols.play();
            labelMaxSkor.Text = Settings1.Default.MaxSkor.ToString();
            RandomMuzikEkle(); // random müzik komutunu çağırdım
            axWindowsMediaPlayer1.Ctlcontrols.play(); //müziği baslattım
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
           
            MENU.Visible = false;            
            AracYerine();
            ferro.Location = new Point(296, 507);            
            Yol = 0;
            Hiz = 70;
            rndAraba[0].sure = true;
            timerRandomAraba.Enabled = true;
            timerRandomAraba.Interval = 200;
            timerSerit.Enabled = true;
            timerSerit.Interval = 200;             
            axWindowsMediaPlayer1.Ctlcontrols.play();//müziği baslattık
            RandomMuzikEkle();
            MENU.Visible = false;
            if (Yol>0) //oyun devam ederken oyuncu yeni oyuna geçmek isterse 
            {                
                AracYerine();                
                Yol = 0;//yol ve hız değerlerini baslangıcta atadığım default değerlere tekrar çekiyorum. Aynı sekilde level1 değerlerini tekrar girdim.
                Hiz = 70;
                rndAraba[0].sure = false;
                timerRandomAraba.Enabled = false;
                timerRandomAraba.Interval = 200;
                timerSerit.Enabled = true;
                timerSerit.Interval = 200;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            
        }

        private void timerSerit_Tick(object sender, EventArgs e)
            {
                Yol++;
            LevelSistemi();
                if (SeritHareketi == false)
                {
                    for (int i = 1; i < 7; i++) // 6 adet şerit çizgimiz olduğu için hepsini döngüde toparlıyorum
                    {
                        this.Controls.Find("labelsolserit" + i.ToString(), true)[0].Top -= 25; //ilgili nesneyi bulduruyoruz ve yukarıya hareket ettiriyoruz
                        this.Controls.Find("labelsagserit" + i.ToString(), true)[0].Top -= 25;
                        SeritHareketi = true;
                    }
                }
                else
                {
                    for (int i = 1; i < 7; i++)
                    {
                        this.Controls.Find("labelsolserit" + i.ToString(), true)[0].Top += 25; //sonsuz döngü olusturduk ve seritlerimiz sadece yukarı asagi hareketi yaparak
                        this.Controls.Find("labelsagserit" + i.ToString(), true)[0].Top += 25; //aracımız ilerliyor imajı verdik
                        SeritHareketi = false;
                    }
                }
            labelYol.Text = Yol.ToString()+"m"; //oyunun alt kısmında yer alan yol labelına değeri çekiyorum
            labelHiz.Text = Hiz.ToString() + "m/s";//oyunun alt kısmında yer alan hız labelına değeri çekiyorum
        }
        }
}

    

