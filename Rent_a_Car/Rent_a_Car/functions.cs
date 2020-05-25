using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_Car
{
    public class functions
    {
        public static String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static string argdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
        //Afisare genreala Detalii client
        public DataTable afisaredb(string identificator)
        {
            SqlConnection con = new SqlConnection(argdb);

            SqlCommand cmd = new SqlCommand("select * from " + identificator + "", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            //dataGridView1.DataSource = dt;
            return dt;
        }
        //
        public DataTable afisaredb_client()
        {
            SqlConnection con = new SqlConnection(argdb);

            SqlCommand cmd = new SqlCommand("select Id,nume,prenume,telefon from users", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            //dataGridView1.DataSource = dt;
            return dt;
        }

        public int insertRecord(string username, string password, string nume, string prenume, string CNP, string sex, string varsta, string adresa, string telefon, string ridicat, string returnat)
        {


            string connString = (argdb);
            SqlConnection mySqlConnection = new SqlConnection(connString);
            mySqlConnection.Open();

            var cmd = new SqlCommand("Insert into users(username,password,nume,prenume,CNP,sex,varsta,adresa,telefon,ridicat,returnat) values" +
                "('" + username + "','" + password + "','" + nume + "','" + prenume + "','" + CNP + "','" + sex + "','" + varsta + "','" + adresa + "','" + telefon + "'" +
                ",'" + ridicat + "','" + returnat + "')", mySqlConnection);
            int row = cmd.ExecuteNonQuery();


            return row;


        }

        public void UpdateRegistrationTable(int id, string username, string password, string nume, string prenume, string CNP, string sex, string varsta, string adresa, string telefon)
        {

            SqlConnection con = new SqlConnection(argdb);
            con.Open();
            SqlCommand cmd = new SqlCommand("update users set username=@UserName,password=@Password,nume=@nume, prenume=@prenume, CNP=@cnp, sex=@sex, varsta=@varsta, adresa=@adresa, telefon=@telefon where Id=@ID", con);
            Console.WriteLine(id + " " + username + " " + password + " " + " " + nume + " " + prenume + " " + CNP + " " + sex + " " + varsta + " " + adresa + " " + telefon); ;
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
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateCarRegistrationTable(int id, string marca, string model, string motorizare, string rezervare, string predare, string clientid)
        {

            SqlConnection con = new SqlConnection(argdb);
            con.Open();
            SqlCommand cmd = new SqlCommand("update masini set Marca=@marca,Model=@model,Motorizare=@motorizare, rezervare=@rezervare, predare=@predare, clientid=@clientid where idmasini=@ID", con);
         //   Console.WriteLine(id + " " + username + " " + password + " " + " " + nume + " " + prenume + " " + CNP + " " + sex + " " + varsta + " " + adresa + " " + telefon); ;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@marca", marca);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@motorizare", motorizare);
            cmd.Parameters.AddWithValue("@rezervare", rezervare);
            cmd.Parameters.AddWithValue("@predare", predare);
            cmd.Parameters.AddWithValue("@clientid", clientid);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateCarOnregistration(int id, string rezervare, string predare, string clientid)
        {

            SqlConnection con = new SqlConnection(argdb);
            con.Open();
            SqlCommand cmd = new SqlCommand("update masini set rezervare=@rezervare, predare=@predare, clientid=@clientid where idmasini=@ID", con);
            //   Console.WriteLine(id + " " + username + " " + password + " " + " " + nume + " " + prenume + " " + CNP + " " + sex + " " + varsta + " " + adresa + " " + telefon); ;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@rezervare", rezervare);
            cmd.Parameters.AddWithValue("@predare", predare);
            cmd.Parameters.AddWithValue("@clientid", clientid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public bool stergeutilizator(int id, string numedb, string db_id)
        {
            SqlConnection con = new SqlConnection(argdb);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from " + numedb + " where " + db_id + "=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public String numeUser(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select nume from users where username = '" + username + "'");
            command.Connection = conn;
            conn.Open();
            string value = (string)command.ExecuteScalar();
            conn.Close();
         //   Console.WriteLine(value);
            return value;
        }

        public String prenumeUser(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select prenume from users where username = '" + username + "'");
            command.Connection = conn;
            conn.Open();
            string value = (string)command.ExecuteScalar();
            conn.Close();
         //   Console.WriteLine(value);
            return value;
        }

        public String adresaUser(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select adresa from users where username = '" + username + "'");
            command.Connection = conn;
            conn.Open();
            string value = (string)command.ExecuteScalar();
            conn.Close();
          //  Console.WriteLine(value);
            return value;
        }

        public String rezervariUser(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select count (*) from masini where clientid = '" + getIDUser(username) + "'");
            command.Connection = conn;
            conn.Open();
            string value = (string)command.ExecuteScalar().ToString();
            conn.Close();
           // Console.WriteLine(value);
            return value;
        }

        public String getIDUser(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select id from users where username = '" + username + "'");
            command.Connection = conn;
            conn.Open();
            string value = (string)command.ExecuteScalar().ToString();
            conn.Close();
           // Console.WriteLine(value);
            return value;
        }

        //nefolosit
        public String numetest3(String username)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select nume,prenume,sex,adresa from users where username = '" + username + "'");
            command.Connection = conn;
            conn.Open();
            // string value = command.ExecuteScalar();
            /*conn.Close();
            Console.WriteLine(value);
            
*/
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{3}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            return "unu";
        }

        //este folosit pentru autentificare 
        public List<string> GetData()
        {
            List<string> lst = new List<string>();
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select username from users ");
            command.Connection = conn;
            conn.Open();
            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    lst.Add(Convert.ToString(dataReader["username"]));
                }
            }
            return lst;

        }
        //afisare informatii utilizator pentru modificare 
        public DataTable GetuserData(int id)
        {

            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select * from users where Id='" + id + "'");
            command.Connection = conn;
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            //dataGridView1.DataSource = dt;
            return dt;

        }
        public DataTable GetcarData(int id)
        {

            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select * from masini where idmasini='" + id + "'");
            command.Connection = conn;
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            //dataGridView1.DataSource = dt;
            return dt;

        }
        //afisare masini libere pentru inchiriere 
        public DataTable getfreecars(int n)
        {
            SqlConnection conn = new SqlConnection(argdb);
            SqlCommand command = new SqlCommand("select idmasini,Marca,Model,Motorizare,rezervare from masini where clientid = '" + n + "'");
            command.Connection = conn;
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            //dataGridView1.DataSource = dt;
            return dt;

        }


    }
}
