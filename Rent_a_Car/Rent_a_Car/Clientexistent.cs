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
using System.IO;

namespace Rent_a_Car
{
    public partial class Clientexistent : Form
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
        functions fun = new functions();
        string idmasina;
       
        public Clientexistent(string id)
        {
            this.Icon = Properties.Resources.rencar;
            InitializeComponent();
            this.Text = "Rent A Car";
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            /* afisare detalii utilizator in text field
*/          Console.WriteLine(id);
            
            textBox1.Text = fun.numeUser(id);
            textBox3.Text = fun.prenumeUser(id);
            textBox4.Text = fun.adresaUser(id);
            textBox5.Text = fun.rezervariUser(id);
            textBox8.Text = id;
            int id_user = Int32.Parse(fun.getIDUser(id));
            textBox7.Text = id_user.ToString(); // global id
            rezervariDataGridView.DataSource = fun.getfreecars(id_user);

          //  rezervariDataGridView.DataSource = fun.getfreecars(Int32.Parse(id));



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.rezervari' table. You can move, or remove it, as needed.

        }
        //interogheaza utilizatorul daca doreste sa inchida aplicatia 
        private void opresteaplicatia()
        {
            string message = "Sunteti sigur ca doriti sa parasiti pagina ?";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons);

            if (result == DialogResult.Yes)
            {

                // Closes the parent form.
                LoginForm loginForm= new LoginForm();
                loginForm.Location = this.Location;
                Hide();
                loginForm.ShowDialog();
            }
        }

        //Aceasta functie realizarea miscari ferestrei cu mouse
        #region
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
            opresteaplicatia();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void rezervariDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = rezervariDataGridView.Rows[e.RowIndex];
                this.idmasina = row.Cells[0].Value.ToString();
                string marca = row.Cells[1].Value.ToString();
                textBox6.Text = this.idmasina; // idmasina global
                textBox2.Text = marca; // marca global
                String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                if (rezervariDataGridView.CurrentCell != null)
                {
                        if (!Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".jpeg").Equals(null))
                        {
                            pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".jpeg");
                        }
                        else if (!Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".png").Equals(null))
                        {
                            pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\" + marca + "\\" + marca + "-" + this.idmasina + ".png");
                        }
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                else if (row.Cells[1].Value.ToString() != "")
                {
                    pictureBox2.Image = Image.FromFile(@"" + cale + "\\Resources\\cars\\Default.png");
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            opresteaplicatia();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rezervare rezerva = new Rezervare(textBox7.Text);
            rezerva.Show();
        }

        private void button5_Click(object sender, EventArgs e) // refresh forms
        {
            rezervariDataGridView.DataSource = fun.getfreecars(Int32.Parse(textBox7.Text));
            textBox5.Text = fun.rezervariUser(textBox8.Text);
        }
    }
}
