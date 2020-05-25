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
        string rezervare="Nerezervat";
        string predare="Nerezervat";


        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {
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

    }
}
