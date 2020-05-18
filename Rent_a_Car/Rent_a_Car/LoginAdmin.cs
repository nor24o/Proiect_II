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
            this.Text = "Rent A Car";
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

        public void autentificare()
        {
            if ((username != null) && (username.TextLength > 0) && ((password != null) && (password.TextLength > 0)))
            {
                String cale = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                SqlConnection scn = new SqlConnection();
                scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + cale + "\\database.mdf;Integrated Security=True;Connect Timeout=30";
                string cautare_dupa = "select count (*) as cnt from admins where nume=@usr and parola=@pass ";
                SqlCommand scmd = new SqlCommand(cautare_dupa, scn);
                scmd.Parameters.Clear();
                scmd.Parameters.AddWithValue("@usr", username.Text);
                scmd.Parameters.AddWithValue("@pass", password.Text);
                scn.Open();
                var res = scmd.ExecuteScalar().ToString();
                if (res == "1")
                {
                    scn.Close();
                    AdminCP admincp = new AdminCP();
                    admincp.Location = this.Location;
                    Hide();
                    admincp.ShowDialog();
                }

                else
                {
                    scn.Close();
                    MessageBox.Show("username sau passwrod gresit");
                }
            }
            else
            {
                MessageBox.Show("Introduceti Numele de Administrator si Parola ");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
             autentificare();

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

/*
            string message = "Doriti sa parasiti aceasta fereastra ?";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons);

            if (result == DialogResult.Yes)
            {
            */
                // Closes the parent form.
                LoginForm loginform = new LoginForm();
                loginform.StartPosition = FormStartPosition.CenterParent;
                Hide();
                loginform.Show();
                //this.Close();
          //  }
        }

        private void LoginAdmin_Load(object sender, EventArgs e)
        {

        }

        
    }
}
