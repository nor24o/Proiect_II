using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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

        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {
            string argdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\database.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(argdb);


            string marca = text_marca.Text;
            string model = text_model.Text;
            string disponibilitate = text_dispo.Text;
            string motorizare = text_motorizare.Text;


            try

            {
                con.Open();
                String inserare = "Insert into masini(Marca,Model,Disponibilitate,Motorizare) " +"values('" + marca + "','" + model + "','" + disponibilitate + "','" + motorizare + "')";
                 Console.WriteLine(inserare);
                // SqlConnection mySqlConnection = new SqlConnection(connString);


                SqlCommand cmd = new SqlCommand(inserare, con);
                int row = cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception es)

            { MessageBox.Show(es.Message); }


        }
    }
}
