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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string tc, string sifre, string ogrtc)
        {
            InitializeComponent();
            Kimlik = tc;
            Sifre = sifre;
            OgrKimlik = ogrtc;
        }
        string OgrKimlik = "";
        string Kimlik = "";
        string Sifre = "";
        OleDbCommand Komut;
        OleDbDataReader Oku;
        Form1 F1 = new Form1();
        int Dakika = 0;
        int Saniye = 60;
        int D1, D2, DT = 0;
        private void Bilgiler()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Ogretmenler WHERE Tc='" + Kimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label5.Text = Oku["Adi"].ToString().ToUpper() + " " + Oku["Soyadi"].ToString().ToUpper();
                    label6.Text = Oku["Bolumu"].ToString().ToUpper();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void FormKucult() { this.Width = 750; this.Height = 353; } // 750; 353
        private void Form3_Load(object sender, EventArgs e)
        {
            Bilgiler();
            timer1.Start();
            Dakika = 10;
            FormKucult();
            label24.Location = new Point(374, 297);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Sinifi=@Sinifi AND Subesi=@Subesi",F1.Baglan);
                Komut.Parameters.AddWithValue("@Sinifi",comboBox1.Text);
                Komut.Parameters.AddWithValue("@Subesi",comboBox2.Text);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    listView1.Items.Add(Oku["OkulNo"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Adi"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Soyadi"].ToString());
                }
                F1.Baglan.Close();
                SinifSayi();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void SinifSayi()
        {
            F1.Baglan.Open();
            Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Sinifi=@Sinifi AND Subesi=@Subesi", F1.Baglan);
            Komut.Parameters.AddWithValue("@Sinifi", comboBox1.Text);
            Komut.Parameters.AddWithValue("@Subesi", comboBox2.Text);
            Oku = Komut.ExecuteReader();
            int S = 0;
            while (Oku.Read())
            {
                S++;
            }
            label25.Text = comboBox1.Text + "-" + comboBox2.Text + " Sınıfında toplam " + S + " öğrenci bunlunmaktadır.";
            F1.Baglan.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Notlar WHERE AdiSoyadi=@AdiSoyadi", F1.Baglan);
                Komut.Parameters.AddWithValue("@AdiSoyadi", comboBox5.Text);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox1.Text = Oku["Not1"].ToString();
                    textBox2.Text = Oku["Not2"].ToString();
                    textBox3.Text = Oku["Sozlu1"].ToString();
                    textBox4.Text = Oku["Sozlu2"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void KisiListele()
        {
            comboBox5.Items.Clear();
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Sinifi=@Sinifi AND Subesi=@Subesi", F1.Baglan);
                Komut.Parameters.AddWithValue("@Sinifi", comboBox3.Text);
                Komut.Parameters.AddWithValue("@Subesi", comboBox4.SelectedItem.ToString());
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    comboBox5.Items.Add(Oku["Adi"].ToString() + " " + Oku["Soyadi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            comboBox5.Text = "";
            KisiListele();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                DialogResult D = MessageBox.Show(comboBox5.Text + " Adlı öğrencinin notları düzenleniyor onaylıyormusunuz?", "Öğretmen Not Giriş", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    Komut = new OleDbCommand("UPDATE Notlar SET Not1=@Not1,Not2=@Not2,Sozlu1=@Sozlu1,Sozlu2=@Sozlu2 WHERE AdiSoyadi=@AdiSoyadi", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Not1", textBox1.Text);
                    Komut.Parameters.AddWithValue("@Not2", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Sozlu1", textBox3.Text);
                    Komut.Parameters.AddWithValue("@Sozlu2", textBox4.Text);
                    Komut.Parameters.AddWithValue("@AdiSoyadi", comboBox5.Text);
                    Komut.ExecuteNonQuery();
                    label12.Text = "İşlem başarılı.";
                    F1.Baglan.Close();
                }
                else
                {
                    label12.Text = "İşlem iptal edildi.";
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox2.Location = new Point(211, 111);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;

            Saniye = Saniye - 1;
            label13.Text = Convert.ToString(Saniye);
            label15.Text = Convert.ToString(Dakika - 1);
            if (Saniye == 0)
            {

                Dakika = Dakika - 1;
                label15.Text = Convert.ToString(Dakika);
                Saniye = 60;
            }

            if (label15.Text == "-1")
            {
                timer1.Stop();
                label15.Text = "00";
                label13.Text = "00";
                MessageBox.Show("10 dakika süreyle hiç bir işlem yapmadığınız için bağlantınız kesildi.Devam etmek için sisteme yeniden giriniz.", "Veli Bilgilendirme Sistemi Giriş");
                F1.Show();
                this.Close();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Location = new Point(284, 118);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox3.Location = new Point(211, 111);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                DialogResult D = MessageBox.Show("Devamsızlık bilgileri düzenleniyor onaylıyormusunuz?", "Öğretmen Devamsızlık", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    Komut = new OleDbCommand("UPDATE Devamsizlik SET Ozurlu=@Ozurlu,Ozursuz=@Ozursuz,Nobetci=@Nobetci WHERE OkulNo=@OkulNo", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Ozurlu", textBox5.Text);
                    Komut.Parameters.AddWithValue("@Ozursuz", textBox6.Text);
                    Komut.Parameters.AddWithValue("@Nobetci", textBox7.Text);
                    Komut.Parameters.AddWithValue("@OkulNo", textBox8.Text);
                    Komut.ExecuteNonQuery();
                    label23.Text = "İşlem başarılı.";
                    label22.Text = "";
                    DevamsizlikBilgi();
                }
                else
                {
                    label23.Text = "İşlem iptal edildi.";
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                DialogResult D = MessageBox.Show(comboBox5.Text + " Adlı öğrencinin notları girişi yapıyorsunuz onaylıyormusunuz?", "Öğretmen Not Giriş", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    Komut = new OleDbCommand("INSERT INTO Notlar(Tc,AdiSoyadi,Not1,Not2,Sozlu1,Sozlu2) VALUES(Tc=@Tc,AdiSoyadi=@AdiSoyadi,Not1=@Not1,Not2=@Not2,Sozlu1=@Sozlu1,Sozlu2=@Sozlu2)", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc", OgrKimlik);
                    Komut.Parameters.AddWithValue("@AdiSoyadi", comboBox5.Text);
                    Komut.Parameters.AddWithValue("@Not1", textBox1.Text);
                    Komut.Parameters.AddWithValue("@Not2", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Sozlu1", textBox3.Text);
                    Komut.Parameters.AddWithValue("@Sozlu2", textBox4.Text);
                    Komut.ExecuteNonQuery();
                    label12.Text = "İşlem başarılı.";
                }
                else
                {
                    label12.Text = "İşlem iptal edildi.";
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Devamsizlik WHERE OkulNo=@OkulNo", F1.Baglan);
                Komut.Parameters.AddWithValue("@OkulNo",textBox8.Text);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox5.Text = Oku["Ozurlu"].ToString();
                    textBox6.Text = Oku["Ozursuz"].ToString();
                    textBox7.Text = Oku["Nobetci"].ToString();
                    label19.Text = "Adı Soyadı: " + Oku["AdiSoyadi"].ToString();
                    label20.Text = "Okul Numarası: " + textBox8.Text;
                }
                F1.Baglan.Close();
                DevamsizlikBilgi();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void DevamsizlikBilgi()
        {
            D1 = int.Parse(textBox5.Text);
            D2 = int.Parse(textBox6.Text);
            DT = D1 + D2;
            label22.Text = "Sistemde toplam " + DT.ToString() + " gün devamsızlık bilgisi bulundu.";
        }
    }
}
