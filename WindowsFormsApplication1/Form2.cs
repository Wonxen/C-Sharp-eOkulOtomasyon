using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string tc, string no)
        {
            InitializeComponent();
            Kimlik = tc;
            Numara = no;
        }
        string Kimlik = "";
        string Numara = "";
        Form1 F1 = new Form1();
        OleDbCommand Komut;
        OleDbDataReader Oku;
        int Dakika = 0;
        int Saniye = 60;
        int Ortalama = 0;
        private void Bilgiler()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Tc='" + Kimlik + "'",F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label5.Text = Oku["Adi"].ToString().ToUpper() + " " + Oku["Soyadi"].ToString().ToUpper();
                    label6.Text = Oku["Sinifi"].ToString() + ".Sınıf";
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void Notlar()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Notlar WHERE Tc='" + Kimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label18.Text = Oku["Not1"].ToString();
                    label19.Text = Oku["Not2"].ToString();
                    label20.Text = Oku["Sozlu1"].ToString();
                    label21.Text = Oku["Sozlu2"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void OrtalamaBul()
        {
            try
            {
                Ortalama = (int.Parse(label18.Text) + int.Parse(label19.Text) + int.Parse(label20.Text) + int.Parse(label21.Text)) / 4;
                label27.Text = Ortalama.ToString();
            }
            catch 
            {
                ;
            }
        }
        private void FormKucult() { this.Width = 750; this.Height = 353; } // 750; 353
        private void Form2_Load(object sender, EventArgs e)
        {
            Notlar();
            Bilgiler();
            OrtalamaBul();
            FormKucult();
            timer1.Start();
            Dakika = 10;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;

            Saniye = Saniye - 1;
            label4.Text = Convert.ToString(Saniye);
            label2.Text = Convert.ToString(Dakika - 1);
            if (Saniye == 0)
            {

                Dakika = Dakika - 1;
                label2.Text = Convert.ToString(Dakika);
                Saniye = 60;
            }

            if (label2.Text == "-1")
            {
                timer1.Stop();
                label2.Text = "00";
                label4.Text = "00";
                MessageBox.Show("10 dakika süreyle hiç bir işlem yapmadığınız için bağlantınız kesildi.Devam etmek için sisteme yeniden giriniz.", "Veli Bilgilendirme Sistemi Giriş");
                F1.Show();
                this.Close();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Tc='" + Kimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    groupBox1.Text = Oku["Sinifi"].ToString() + ".SINIF Dönemlik Ders Notları";
                }
                groupBox1.Visible = true;
                groupBox1.Location = new Point(284, 118);
                label48.Visible = true;
                label48.Location = new Point(281, 211);
                label46.Text = "Öğrencinin Ders Notları";
                groupBox2.Visible = false;
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void DevamsizlikBul()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Devamsizlik WHERE Tc='" + Kimlik + "'",F1.Baglan);
                Oku = Komut.ExecuteReader();
                while(Oku.Read())
                {
                    label32.Text = Oku["Ozurlu"].ToString() + " gün.";
                    label33.Text = Oku["Ozursuz"].ToString() + " gün.";
                    label34.Text = Oku["Nobetci"].ToString() + " gün.";
                }
                F1.Baglan.Close();
            }
            catch
            {

            }
        }
        private void label8_Click(object sender, EventArgs e)
        {
            label48.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox2.Location = new Point(307, 119);
            DevamsizlikBul();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }
    }
}
