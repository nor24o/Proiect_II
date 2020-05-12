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
using System.Collections;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Rent_a_Car
{
    public partial class AdminCP : Form
    {
        #region realizeaza colturi rotunde
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

        public AdminCP()
        {
            this.Icon = Properties.Resources.rencar;
            InitializeComponent();
            this.Text = "Rent A Car";
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            

        }
        #region windows moving function 
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



        private void button4_Click(object sender, EventArgs e)
        {


            string message = "Sunteti sigur ca doriti sa parasiti aplicatia ?";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons);

            if (result == DialogResult.Yes)
            {

                // Closes the parent form.

                Application.Exit();
                //this.Close();
            }


        }

        private void AdminCP_Load(object sender, EventArgs e)
        {


            functions fun = new functions();
            dataGridView1.DataSource = fun.afisaredb("masini");
            dataGridView2.DataSource = fun.afisaredb("users");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            functions fun = new functions();
            dataGridView1.DataSource = fun.afisaredb("masini");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            functions fun = new functions();
            dataGridView2.DataSource = fun.afisaredb("users");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Adaugareuser newusers = new Adaugareuser();
            newusers.Location = this.Location;
            newusers.Show();
        }

        
        private void button7_Click(object sender, EventArgs e)
        {
            if (label2.Text != "")
            {
                functions fun = new functions();

                Console.WriteLine(label2.Text);

                fun.stergeutilizator(Int32.Parse(label2.Text), "users", "Id");

                dataGridView2.DataSource = fun.afisaredb("users");

            }
        
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                label2.Text = row.Cells[0].Value.ToString();
                
            };


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label3.Text != "")
            {
                
                functions fun = new functions();

                Console.WriteLine(label3.Text);

                fun.stergeutilizator(Int32.Parse(label3.Text), "masini", "Idmasini");

                dataGridView1.DataSource = fun.afisaredb("masini");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {       }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0)
                        {
                            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                            label3.Text = row.Cells[0].Value.ToString();

                        };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdaugareMasini newcar = new AdaugareMasini();
            newcar.Location = this.Location;
            newcar.Show();
        }
    }
}
