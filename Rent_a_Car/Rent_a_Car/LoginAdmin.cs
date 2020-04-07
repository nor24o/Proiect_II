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
    public partial class LoginAdmin : Form
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

        public LoginAdmin()
        {
            this.Icon = Properties.Resources.rencar;
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
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

        string[] adminuser = { "admin" };
        string[] adminpass = { "adminn" };

        private void button1_Click(object sender, EventArgs e)
        {
            while ((textBox1 != null) && (textBox2 != null))
            {
                if ((adminuser.Contains(textBox1.Text) && adminpass.Contains(textBox2.Text) && Array.IndexOf(adminuser, textBox1.Text) == Array.IndexOf(adminpass, textBox2.Text)))
                {
                    AdminCP admincp = new AdminCP();
                    admincp.Location = this.Location;
                    Hide();

                    admincp.ShowDialog();

                    break;

                }
                else
                {
                    MessageBox.Show("username sau passwrod gresit");
                    break;
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click_1(object sender, EventArgs e)
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
                Environment.Exit(0);
                //this.Close();
            }
        }

        private void LoginAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
