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

namespace Rent_a_Car
{
    public partial class AdaugareMasini : Form
    {
        public AdaugareMasini()
        {
            InitializeComponent();
        }
        string rezervare;
        string predare;
        bool rez = false;
        bool pred = false;
        public void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            Console.WriteLine("month1");
            this.rez = true;
        }
        public void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            Console.WriteLine("month2");
            this.pred = true;
        }

        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string argdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(argdb);

    





            string marca = text_marca.Text;
            string model = text_model.Text;
            string motorizare = text_motorizare.Text;
            if (rez && pred)
            {
                Console.WriteLine("month2 si month1");
                this.rezervare = monthCalendar1.SelectionRange.Start.ToShortDateString();
                this.predare = monthCalendar2.SelectionRange.Start.ToShortDateString();
            }
            else
            {
                Console.WriteLine("month2 nu month1");
                this.rezervare = "";
                this.predare = "";
            }
            string clientid = textBox1.Text;

            


            try

            {
                con.Open();
                String inserare = "Insert into masini(Marca,Model,Motorizare,rezervare,predare,clientid) " +"values('" + marca + "','" + model + "','" + motorizare + "','" + rezervare + "','" + predare + "','" + clientid + "')";
                 Console.WriteLine(inserare);
                // SqlConnection mySqlConnection = new SqlConnection(connString);


                SqlCommand cmd = new SqlCommand(inserare, con);
                int row = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Date inserate cu succes!");
                this.Close();
            }

            catch (Exception es)

            { MessageBox.Show(es.Message); }


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
        string imageLocation ="";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog()==DialogResult.OK)
            { 
                imageLocation = dialog.FileName.ToString();
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox1.ImageLocation = imageLocation;
                
            }
        }
    }
}
