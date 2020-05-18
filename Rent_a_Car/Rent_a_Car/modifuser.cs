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
    }
}
