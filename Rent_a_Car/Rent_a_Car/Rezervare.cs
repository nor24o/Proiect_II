using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.JScript;

namespace Rent_a_Car
{
    public partial class Rezervare : Form
    {
        String idmasina;
        string data_predare = "";
        string data_rezervare = "";
        

        public Rezervare(String id)
        {

            InitializeComponent();
            calendar_returnare.MinDate = DateTime.Now;
            calendar_ridicare.MinDate = DateTime.Now;
            textBox1.Text = id;

            
        }
        private void updatemasina()
        {
            SqlConnection scn = new SqlConnection();
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            Console.WriteLine(textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + " " + textBox5.Text);
            String updatemasina = "update masini set clientid='" + textBox1.Text + "',rezervare='" + textBox2.Text + "',predare='" + textBox3.Text + "' where idmasini='" + textBox4.Text + "' ";
            scn.Open();
            var cmd = new SqlCommand(updatemasina, scn);
            int row = cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Successfully");
            scn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            functions fun = new functions();
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.DataSource = fun.getfreecars(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" && textBox3.Text != "")
            {
                updatemasina();
                MessageBox.Show("Comanda plasata cu succes!");
                Hide();
            }
            else
            {
                MessageBox.Show("Completati campurile ridicare/predare!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                this.idmasina = row.Cells[0].Value.ToString();
                string marca = row.Cells[1].Value.ToString();
                textBox4.Text = this.idmasina;
                textBox5.Text = marca;
                if (row.Cells[1].Value.ToString() != "")
                {
                    try
                    {
                        button7.Visible = false;
                        if (!Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".jpeg").Equals(null))
                        {
                            pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".jpeg");
                        }
                        else if (!Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".png").Equals(null))
                        {
                            pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".png");
                        }
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                        if (File.Exists(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".txt"))
                        {
                            button7.Visible = true;
                        }
                    }
                    catch
                    {

                    }

                }
                else if (row.Cells[1].Value.ToString() != "")
                {
                    pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\Default.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }
        }

        private void calendar_ridicare_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.data_rezervare = calendar_ridicare.SelectionRange.Start.ToShortDateString();
            Console.WriteLine(calendar_ridicare.SelectionRange.Start.ToShortDateString());
            textBox2.Text = calendar_ridicare.SelectionRange.Start.ToShortDateString();
        }

        private void calendar_returnare_DateChanged(object sender, DateRangeEventArgs e)
        {
            calendar_returnare.MinDate = calendar_ridicare.SelectionRange.Start;
            this.data_predare = calendar_returnare.SelectionRange.Start.ToShortDateString();
            Console.WriteLine(calendar_returnare.SelectionRange.Start.ToShortDateString());
            textBox3.Text = calendar_returnare.SelectionRange.Start.ToShortDateString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string idmasina = textBox4.Text;
            string marca = textBox5.Text;
            string path = @"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".txt";

            string message = File.ReadAllText(path);
            string title = "Mentiuni ref. - " + marca + "/" + idmasina;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
        }
    }
}
