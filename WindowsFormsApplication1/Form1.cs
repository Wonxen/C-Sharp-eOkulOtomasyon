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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public OleDbConnection Baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        OleDbCommand Komut;
        OleDbDataReader Oku;
        Random R = new Random();
        bool Hata = false;
        ErrorProvider provider = new ErrorProvider();
        private void GuvenlikKodu()
        {
            label7.Text = R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString();
            label12.Text = R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GuvenlikKodu();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            GuvenlikKodu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void label16_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            GuvenlikKodu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                provider.SetError(textBox5, "Boş geçilmez.");
                Hata = true;
            }
            else
            {
                provider.Dispose();
                Hata = false;
            }
            if (Hata == false)
            {
                foreach (Control Kontrol in this.Controls)
                {
                    Kontrol.Enabled = false;
                }
                provider.Dispose();
                VeliGiris();
            }
        }
        private void VeliGiris()
        {
            try
            {
                if (int.Parse(textBox5.Text) == int.Parse(label12.Text))
                {
                    Baglan.Open();
                    Komut = new OleDbCommand("SELECT * FROM Ogrenciler WHERE Tc=@Tc AND OkulNo=@OkulNo", Baglan);
                    Komut.Parameters.AddWithValue("@Tc", textBox4.Text);
                    Komut.Parameters.AddWithValue("@OkulNo", textBox6.Text);
                    Oku = Komut.ExecuteReader();
                    if (Oku.Read())
                    {
                        Form2 F2 = new Form2(textBox4.Text, textBox6.Text);
                        F2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kimlik numaranız veya okul numaranız yanlış lütfen tekrar deneyin!", "Veli Bilgilendirme Sistemi Giriş");
                    }
                    Baglan.Close();
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void OgretmenGiris()
        {
            try
            {
                if (int.Parse(textBox1.Text) == int.Parse(label7.Text))
                {
                    if (textBox2.Text == "Admin" && textBox3.Text == "1234")
                    {
                        Form4 F4 = new Form4();
                        F4.Show();
                        this.Hide();
                    }
                    else
                    {
                        Baglan.Open();
                        Komut = new OleDbCommand("SELECT * FROM Ogretmenler WHERE Tc=@Tc AND Sifre=@Sifre", Baglan);
                        Komut.Parameters.AddWithValue("@Tc", textBox2.Text);
                        Komut.Parameters.AddWithValue("@Sifre", textBox3.Text);
                        Komut.ExecuteNonQuery();
                        Oku = Komut.ExecuteReader();
                        if (Oku.Read())
                        {
                            Form3 F3 = new Form3(textBox2.Text, textBox3.Text, textBox4.Text);
                            F3.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kimlik numaranız veya şifreniz yanlış lütfen tekrar deneyin!", "Sistem Giriş");
                        }
                        Baglan.Close();
                    }
                }
            }
            catch (Exception Hata)
            {
                Baglan.Close();
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                provider.SetError(textBox1, "Boş geçilmez.");
                Hata = true;
            }
            else
            {
                provider.Dispose();
                Hata = false;
            }
            if (Hata == false)
            {
                foreach (Control Kontrol in this.Controls)
                {
                    Kontrol.Enabled = false;
                }
                provider.Dispose();
                OgretmenGiris();
            }
        }
    }
}
