using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Threading;

namespace Rent_a_Car
{
    public partial class Client : Form
    {
        #region
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        #endregion

        public Client()
        {
            this.Icon = Properties.Resources.rencar;
            InitializeComponent();
            this.Text = "Rent A Car";
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            calendar_returnare.MinDate = DateTime.Now;
            calendar_ridicare.MinDate = DateTime.Now;

        }
        int r = (new Random()).Next(100, 1000);
        bool aceptEULA = false;
        int atempt = 0;
        bool res = false;
        int idmasina;
        bool masinaselectata = false;
        string pred = "";
        string rez = "";

        //interogheaza utilizatorul daca doreste sa inchida aplicatia 
        private void opresteaplicatia()
        {
            string message = "Doriti sa accesati pagina de Logare  ?";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons);

            if (result == DialogResult.Yes)
            {
                LoginForm loginform = new LoginForm();
                loginform.Location = this.Location;
                Hide();
                loginform.Show();

                // Closes the parent form.
                // Environment.Exit(0);
                //this.Close();
            }
        }

        #region //This function makes windows movable
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        #endregion

        List<string> lst = new List<string>();

        private void Client_Load(object sender, EventArgs e)
        {
            functions fun = new functions();

            foreach (var list in fun.GetData())
            {

                this.lst.Add(Convert.ToString(list));

            }


        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // functie revenire la forma precedenta
            LoginForm loginform = new LoginForm();
            loginform.StartPosition = FormStartPosition.CenterParent;
            Hide();
            loginform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Autocompletare varsta,sex , verifica conditi baza corectitudine cnp
            #region 
            while ((cnp.Text != null) && (cnp.Text.Length == 13))
            {
                string cnp = this.cnp.Text;
                var cnp_luna = Int32.Parse(cnp.Substring(3, 2));
                var cnp_zi = Int32.Parse(cnp.Substring(5, 2));
                if (((cnp_luna >= 1) && (cnp_luna <= 12)) && ((cnp_zi >= 1) && (cnp_zi <= 31)))
                {
                    var cnp_sex = Int32.Parse(cnp.Substring(0, 1));

                    switch (cnp_sex)
                    {
                        case 1:
                        case 5:
                            sex.Text = "M";
                            sex.BackColor = Color.Green;
                            break;
                        case 2:
                        case 6:
                            sex.Text = "F";
                            sex.BackColor = Color.Green;
                            break;
                        default:

                            break;

                    }

                    var currentDate = DateTime.Today;
                    var cnp_age = Int32.Parse(cnp.Substring(1, 2));

                    if ((cnp_sex == 1) || (cnp_sex == 2))
                    {
                        cnp_age += 1900;
                    }
                    else if ((cnp_sex == 5) || (cnp_sex == 6))
                    {
                        cnp_age += 2000;

                    }
                    var newage = currentDate.Year - cnp_age;

                    varsta.Text = newage.ToString();
                    varsta.BackColor = Color.Green;
                }
                else
                    MessageBox.Show("CNP Invalid");



                break;


            }

            #endregion
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }




        private void cautareuser_vechi()
        {

            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            string cautare_dupa = "select count (*) as cnt from users where username=@usr";
            SqlCommand scmd = new SqlCommand(cautare_dupa, scn);
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@usr", numeutilizator.Text);
            scn.Open();
            var res = scmd.ExecuteScalar().ToString();
            if (res == "1")
            {
                scn.Close();
                numeutilizator.BackColor = Color.Red;
                if ((atempt == 1) && (res == "1"))
                {
                    MessageBox.Show("Client Inregistrat");
                }
            }

            else
            {

                numeutilizator.BackColor = Color.White;

                string Username = numeutilizator.Text;
                string Password = parola.Text;
                string Nume = nume.Text;
                string Prenume = prenume.Text;
                string CNP = cnp.Text;
                string Sex = sex.Text;
                string Varsta = varsta.Text;
                string Adresa = adresa.Text;
                string Telefon = telefon.Text;
                if (this.atempt == 0)
                {
                    try

                    {

                        //  String connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30";


                        String inserare = "Insert into users(username,password,nume,prenume,CNP,sex,varsta,adresa,telefon) " +
                        "values('" + Username + "','" + Password + "','" + Nume + "','" + Prenume + "','" + CNP + "','" + Sex + "','"
                        + Varsta + "','" + Adresa + "','" + Telefon + "')";

                        /* SqlConnection mySqlConnection = new SqlConnection(connString);

                         mySqlConnection.Open();*/
                        var cmd = new SqlCommand(inserare, scn);
                        int row = cmd.ExecuteNonQuery();
                        scn.Close();
                        atempt = 1;
                        ;

                    }

                    catch (Exception es)

                    { MessageBox.Show(es.Message); }
                }




            }


        }

        private void cautareuser()
        {
            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            if (res == true)
            {

                numeutilizator.BackColor = Color.Red;
                MessageBox.Show("Client Inregistrat");
                if ((atempt == 1) && (res == true))
                {

                }
            }

            else
            {

                string Username = numeutilizator.Text;
                string Password = parola.Text;
                string Nume = nume.Text;
                string Prenume = prenume.Text;
                string CNP = cnp.Text;
                string Sex = sex.Text;
                string Varsta = varsta.Text;
                string Adresa = adresa.Text;
                string Telefon = telefon.Text;
                if (this.atempt == 0)
                {
                    try

                    {

                        //  String connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xavier\Desktop\Proiect_II_Rent_a_car\Proiect_II\Rent_a_Car\Rent_a_Car\database.mdf;Integrated Security=True;Connect Timeout=30";


                        // scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
                        String inserare = "Insert into users(username,password,nume,prenume,CNP,sex,varsta,adresa,telefon) " +
                        "values('" + Username + "','" + Password + "','" + Nume + "','" + Prenume + "','" + CNP + "','" + Sex + "','"
                        + Varsta + "','" + Adresa + "','" + Telefon + "')";
                        scn.Open();
                        /* SqlConnection mySqlConnection = new SqlConnection(connString);
                        
                         mySqlConnection.Open();*/




                        /*
                        string updatemasina = "UPDATE masini SET clientid = '" + r + "',rezervare = '" + rez + "',predare = '" + pred + "' WHERE idmasini = '" + this.idmasina + "'; ";
                        Console.WriteLine(updatemasina);
                        var cmd2 = new SqlCommand(updatemasina, scn);
                        int row2= cmd2.ExecuteNonQuery();*/

                        var cmd = new SqlCommand(inserare, scn);

                        int row = cmd.ExecuteNonQuery();

                        scn.Close();
                        atempt = 1;
                        Thread.Sleep(50);
                        updatemasina();



                    }

                    catch (Exception es)

                    { MessageBox.Show(es.Message); }
                }




            }


        }


        private void updatemasina()
        {
            

            String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string argdb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(argdb);
            con.Open();
            SqlCommand cmd = new SqlCommand("update masini set clientid = @r, rezervare = @rez, predare = @pred where idmasini = @idmasina", con);
            Console.WriteLine("aproape  " + "cID:" + r + " r:" + rez + " pred: " + pred + " id:" + idmasina);

            cmd.Parameters.AddWithValue("@idmasina", idmasina);
            cmd.Parameters.AddWithValue("@r", r);
            cmd.Parameters.AddWithValue("@rez", rez);
            cmd.Parameters.AddWithValue("@pred", pred);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Successfully");
            con.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(idmasina);
            if (aceptEULA && masinaselectata)
            {
                cautareuser();
            }
            else
            {
                MessageBox.Show("Nu ati acceptat termenii si conditiile");
            }
        }

        private void sex_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void prenume_TextChanged(object sender, EventArgs e)
        {

        }

        private void nume_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            functions fun = new functions();
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.DataSource = fun.getfreecars();

        }

        private void numeutilizator_TextChanged(object sender, EventArgs e)
        {
            //  Console.WriteLine("test auto" + numeutilizator.Text);


            foreach (var list in lst)
            {
                if (list == numeutilizator.Text)
                {
                    // numeutilizator.BackColor = Color.Red;
                    //Console.WriteLine(list + "  " + numeutilizator.Text);
                    this.res = true;
                    break;
                }
                else
                {
                    this.res = false;
                }


            }
            Console.WriteLine(res);
            if (res && numeutilizator.BackColor == Color.White)
            {
                numeutilizator.BackColor = Color.Red;
            }
            else
            {
                numeutilizator.BackColor = Color.White;
            }

        }

        private void calendar_ridicare_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.rez = calendar_ridicare.SelectionRange.Start.ToShortDateString();
            Console.WriteLine(calendar_ridicare.SelectionRange.Start.ToShortDateString());
            label15.Text = calendar_ridicare.SelectionRange.Start.ToShortDateString();
        }

        private void calendar_returnare_DateChanged(object sender, DateRangeEventArgs e)
        {
            calendar_returnare.MinDate = calendar_ridicare.SelectionRange.Start;
            this.pred = calendar_returnare.SelectionRange.Start.ToShortDateString();
            Console.WriteLine(calendar_returnare.SelectionRange.Start.ToShortDateString());
            label16.Text = calendar_returnare.SelectionRange.Start.ToShortDateString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.aceptEULA = true;
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.idmasina = dataGridView1.CurrentCell.RowIndex;
            this.masinaselectata = true;
            Console.WriteLine("idmasina" + idmasina);

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
