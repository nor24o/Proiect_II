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
        }

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

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            opresteaplicatia();
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
            while ((textBox3.Text != null) && (textBox3.Text.Length == 13))
            {
                string cnp= textBox3.Text;
                var cnp_luna = Int32.Parse(cnp.Substring(3, 2));
                var cnp_zi = Int32.Parse(cnp.Substring(5, 2));
                if (((cnp_luna >= 1) && (cnp_luna <= 12))&&((cnp_zi >= 1) && (cnp_zi <= 31)))
                {
                        var cnp_sex = Int32.Parse(cnp.Substring(0, 1));

                    switch (cnp_sex)
                    {
                        case 1:
                        case 5:
                            textBox6.Text = "Masculin";
                            textBox6.BackColor = Color.Green;
                            break;
                        case 2:
                        case 6:
                            textBox6.Text = "Feminin";
                            textBox6.BackColor = Color.Green;
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

                    textBox4.Text = newage.ToString();
                    textBox4.BackColor = Color.Green;
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
    }
}
