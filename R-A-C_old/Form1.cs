using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_App_3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        string[] usernames = { "User1" };
        string[] passwords = { "pass1" };
        string[] adminuser = { "admin" };
        string[] adminpass = { "admin1" };
        private void button1_Click(object sender, EventArgs e)
        {
            if (usernames.Contains(textBox1.Text) && passwords.Contains(textBox2.Text) && Array.IndexOf(usernames, textBox1.Text) == Array.IndexOf(passwords, textBox2.Text))
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }

            else              

            if (adminuser.Contains(textBox1.Text) && adminpass.Contains(textBox2.Text) && Array.IndexOf(adminuser, textBox1.Text)== Array.IndexOf(adminpass, textBox2.Text))
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }

            else
                MessageBox.Show("The username and/or passaword is incorrect!");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
