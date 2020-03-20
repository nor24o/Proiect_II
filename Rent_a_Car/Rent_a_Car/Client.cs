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
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
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
            Environment.Exit(0);
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
            while ((textBox3.Text != null) && (textBox3.Text.Length == 13))
            {
                
                string cnp= textBox3.Text;
                var currentDate = DateTime.Today;
                var cnp_age= Int32.Parse(cnp.Substring(1, 2));
                if (cnp_age >= 00)
                {
                    cnp_age += 1900;
                }
                else
                {
                    cnp_age += 2000;
                }
                var newage =  currentDate.Year - cnp_age;

                textBox4.Text = newage.ToString();
                


                var cnp_sex= Int32.Parse(cnp.Substring(0, 1));
                  switch(cnp_sex)
                {
                    case 1:
                        textBox6.Text = "Masculin";
                        break;
                    case 2:
                        textBox6.Text = "Feminin";
                        break;
                    default:
                        textBox6.Text = "CNP incomplet";
                        break;

                }
                break;




            }





        }
    }
}
