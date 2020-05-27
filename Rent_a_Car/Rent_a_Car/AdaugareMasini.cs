using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car
{
    public partial class AdaugareMasini : Form
    {
        public AdaugareMasini()
        {
            InitializeComponent();
        }
        string rezervare="Nerezervat";
        string predare="Nerezervat";


        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {
            string observare = textBox105.Text;
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string argdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(argdb);
            string marca = text_marca.Text;
            string model = text_model.Text;
            string motorizare = text_motorizare.Text;
            string clientid = "0";
            
            try
            {
                con.Open();
                String inserare = "Insert into masini(Marca,Model,Motorizare,rezervare,predare,clientid) " +"values('" + marca + "','" + model + "','" + motorizare + "','" + rezervare + "','" + predare + "','" + clientid + "')";
                 Console.WriteLine(inserare);
                // SqlConnection mySqlConnection = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(inserare, con);
                int row = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Date Adaugate cu succes!");
                this.Close();
            }
            catch (Exception es)
            { MessageBox.Show(es.Message); }

            functions f = new functions();

            if (pictureBox1.Image != null) {
                string path = @"" + cale + "\\Resources\\cars\\" + marca;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string format = null;
                if (ImageFormat.Jpeg.Equals(pictureBox1.Image.RawFormat))
                {
                    format = ".jpeg";
                }
                else if (ImageFormat.Png.Equals(pictureBox1.Image.RawFormat))
                {
                    format = ".png";
                }
                else if (".jpg".Equals(pictureBox1.Image.RawFormat))
                {
                    format = ".jpg";
                }
                
                File.Copy(pathimg.Text, Path.Combine(path, Path.GetFileName(marca + "-" + f.getidmasina() + format)), true);
            }
            else
            {
                MessageBox.Show("Nu a fost introdusa o imagine! Nu este o problema.");
            }
  
            if (!textBox105.Text.Equals("null"))
            {
                string bzbz = @"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + f.getidmasina() + ".txt";
                if (!File.Exists(bzbz)){
                    var da = File.Create(bzbz);
                    da.Close();
                    var tw = new StreamWriter(bzbz, true);
                    tw.WriteLine(""+ observare + "");
                    tw.Close();
                }
 
            }

            
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

        }

        private void AdaugareMasini_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";

            if(open.ShowDialog() == DialogResult.OK)
            {
                pathimg.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
