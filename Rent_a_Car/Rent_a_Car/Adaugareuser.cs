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
    public partial class Adaugareuser : Form
    {
        functions fun = new functions();
        string idmasina = "";
        int atempt = 0;
        bool exista_utilizator = false;
        bool masinaselectata = false;
        string data_predare = "";
        string data_rezervare = "";
        List<string> lst = new List<string>();
        String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        public Adaugareuser()
        {
            InitializeComponent();
            //Preluam numele de utilizator pentru comparare intro lista
            foreach (var list in fun.GetData())
            {
                this.lst.Add(Convert.ToString(list));
            }
            //
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = fun.getfreecars(0);

            monthCalendar1.MinDate = DateTime.Now;
            monthCalendar2.MinDate = DateTime.Now;
        }


        private void cnp_TextChanged(object sender, EventArgs e)
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

        private void cautareuser()
        {

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
                //pentru a afisa grafic daca utilizatorul este deja inscris
                numeutilizator.BackColor = Color.Red;
                if ((atempt == 1) && (res == "1"))
                {
                    MessageBox.Show("Client Inregistrat");
                }
            }

            else
            {
                //pentru a afisa grafic daca utilizatorul este deja inscris
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
                        String inserare = "Insert into users(username,password,nume,prenume,CNP,sex,varsta,adresa,telefon) " +
                        "values('" + Username + "','" + Password + "','" + Nume + "','" + Prenume + "','" + CNP + "','" + Sex + "','"
                        + Varsta + "','" + Adresa + "','" + Telefon + "')";
                        var cmd = new SqlCommand(inserare, scn);
                        int row = cmd.ExecuteNonQuery();
                        scn.Close();
                        atempt = 1;
                        MessageBox.Show("Utilizator inserat cu succes!");
                    }
                    catch (Exception es)

                    { MessageBox.Show(es.Message); }

                    string id = fun.getIDUser(numeutilizator.Text);
                    fun.UpdateCarOnregistration(Int32.Parse(idmasina), data_rezervare, data_predare, id);
                }





            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (masinaselectata)
            {
                cautareuser();
            }
            else
            {
                MessageBox.Show("Nu ati selectat o masina/Nu avem masini libere ");
            }
        }



        private void Adaugareuser_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.masini' table. You can move, or remove it, as needed.
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.masinaselectata = true;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                idmasina = row.Cells[0].Value.ToString();


            };
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar2.MinDate = monthCalendar1.SelectionRange.Start;
            this.data_predare = monthCalendar2.SelectionRange.Start.ToShortDateString();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.data_rezervare = monthCalendar1.SelectionRange.Start.ToShortDateString();

        }

        private void numeutilizator_TextChanged(object sender, EventArgs e)
        {
            foreach (var list in lst)
            {
                if (list == numeutilizator.Text)
                {
                    this.exista_utilizator = true;
                    break;
                }
                else
                {
                    this.exista_utilizator = false;
                }
            }
            Console.WriteLine(exista_utilizator);
            if (exista_utilizator && numeutilizator.BackColor == Color.White)
            {
                numeutilizator.BackColor = Color.Red;
            }
            else
            {
                numeutilizator.BackColor = Color.White;
            }
        }
    }
}
