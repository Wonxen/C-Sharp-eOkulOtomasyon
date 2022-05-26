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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbCommand Komut;
        OleDbDataReader Oku;
        OleDbDataAdapter Yaz;
        Form1 F1 = new Form1();
        Random R = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
        }
        private void OgrenciListele()
        {
            F1.Baglan.Open();
            Yaz = new OleDbDataAdapter("SELECT * FROM Ogrenciler",F1.Baglan);
            DataTable tablo = new DataTable();
            Yaz.Fill(tablo);
            dataGridView1.DataSource = tablo;
            F1.Baglan.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            OgrenciListele();
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show("Öğrenci silmek istediğinize eminmisiniz?", "ADMIN PANEL", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("DELETE * FROM Ogrenciler WHERE Tc=@Tc",F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    OgrenciListele();
                }
                else
                {
                    MessageBox.Show("İşlem iptal edili.","ADMIN PANEL");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }

        private void OkulNumarasiUret()
        {
            textBox2.Text = R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString();
        }
        private void OgrenciKayitTamam()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(textBox3.Text + " " + textBox4.Text + " isimli öğrenci kaydı ekleniyor oynarlıyormusunuz", "ADMIN PANEL", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("INSERT INTO Ogrenciler (TC,Adi,Soyadi,OkulNo,DogumTarihi,DogumYeri,Sinifi,Subesi) VALUES (Tc=@Tc,Adi=@Adi,Soyadi=@Soyadi,OkulNo=@OkulNo,DogumTarihi=@DogumTarihi,DogumYeri=@DogumYeri,Sinifi=@Sinifi,Subesi=@Subesi)", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc",textBox1.Text);
                    Komut.Parameters.AddWithValue("@Adi",textBox3.Text);
                    Komut.Parameters.AddWithValue("@Soyadi",textBox4.Text);
                    Komut.Parameters.AddWithValue("@OkulNo", textBox2.Text);
                    Komut.Parameters.AddWithValue("@DogumTarihi", textBox5.Text);
                    Komut.Parameters.AddWithValue("@DogumYeri", textBox6.Text);
                    Komut.Parameters.AddWithValue("@Sinifi", comboBox1.Text);
                    Komut.Parameters.AddWithValue("@Subesi", comboBox2.Text);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    OgrenciListele();
                    OgrenciKayitTamam();
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi.","ADMIN PANEL");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Sistemsel Hata: " + Hata);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel6.Location = new Point(364, 223);
            OkulNumarasiUret();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel3.Location = new Point(200, 99);
        }
    }
}
