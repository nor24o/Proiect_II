using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_Car
{
   public class functions
    {
        public DataTable afisaredb(string identificator)
        {

            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30");
           
            SqlCommand cmd = new SqlCommand("select * from "+identificator+"", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            //dataGridView1.DataSource = dt;
            return dt;
        }

           public int insertRecord(string username,string password, string nume, string prenume, string CNP, string sex, string varsta, string adresa, string telefon, string ridicat, string returnat)
        {


            string connString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30");
            SqlConnection mySqlConnection = new SqlConnection(connString);
            mySqlConnection.Open();

            var cmd = new SqlCommand("Insert into users(username,password,nume,prenume,CNP,sex,varsta,adresa,telefon,ridicat,returnat) values" +
                "('" + username + "','" + password + "','" + nume + "','" + prenume + "','" + CNP + "','" + sex + "','" + varsta + "','" + adresa + "','" + telefon + "'" +
                ",'" + ridicat + "','" + returnat + "')", mySqlConnection);
            int row = cmd.ExecuteNonQuery();


            return row;


        }

        public void UpdateRegistrationTable(string id,string username,string password, string nume, string prenume, string CNP, string sex, string varsta, string adresa, string telefon, string ridicat, string returnat)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("update users set username=@UserName,password=@Password,nume=@nume, prenume=@prenume, CNP=@cnp, sex=@sex" +
                ", varsta=@varsta, adresa=@adresa, telefon=@telefon, ridicat=@ridicat, returnat=@returnat where Id=@ID", con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@nume", nume);
            cmd.Parameters.AddWithValue("@prenume", prenume);
            cmd.Parameters.AddWithValue("@cnp", CNP);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@varsta", varsta);
            cmd.Parameters.AddWithValue("@adresa", adresa);
            cmd.Parameters.AddWithValue("@telefon", telefon);
            cmd.Parameters.AddWithValue("@ridicat", ridicat);
            cmd.Parameters.AddWithValue("@returnat", returnat);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool stergeutilizator(int id,string numedb,string db_id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from " + numedb + " where "+db_id+"=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }



    }
}
