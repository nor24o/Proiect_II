using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car
{
    public partial class modifuser : Form
    {
        int ids = 0;
        functions fun = new functions();
        public modifuser(int id)
        {
            InitializeComponent();

            this.ids = id;
            DataTable dt = new DataTable();
            dt.Merge(fun.GetuserData(id));
            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;

                textBox1.Text = (array[3].ToString());
                textBox2.Text = (array[4].ToString());
                textBox3.Text = (array[5].ToString());
                textBox4.Text = (array[6].ToString());
                textBox5.Text = (array[7].ToString());
                textBox6.Text = (array[8].ToString());
                textBox9.Text = (array[9].ToString());
                textBox7.Text = (array[1].ToString());
                textBox8.Text = (array[2].ToString());


            }


        }


        private void modifuser_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(ids);
            fun.UpdateRegistrationTable(ids, textBox7.Text, textBox8.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox9.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            while ((textBox3.Text != null) && (textBox3.Text.Length == 13))
            {
                string cnp = this.textBox3.Text;
                var cnp_luna = Int32.Parse(cnp.Substring(3, 2));
                var cnp_zi = Int32.Parse(cnp.Substring(5, 2));
                if (((cnp_luna >= 1) && (cnp_luna <= 12)) && ((cnp_zi >= 1) && (cnp_zi <= 31)))
                {
                    var cnp_sex = Int32.Parse(cnp.Substring(0, 1));

                    switch (cnp_sex)
                    {
                        case 1:
                        case 5:
                            textBox4.Text = "M";
                            textBox4.BackColor = Color.Green;
                            break;
                        case 2:
                        case 6:
                            textBox4.Text = "F";
                            textBox4.BackColor = Color.Green;
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

                    textBox5.Text = newage.ToString();
                    textBox5.BackColor = Color.Green;
                }
                else
                    MessageBox.Show("CNP Invalid");



                break;


            }
        }
    }
}
