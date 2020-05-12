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

namespace Rent_a_Car
{
    public partial class LoginForm : Form
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

        public LoginForm()
        {
            this.Icon = Properties.Resources.rencar;
            InitializeComponent();
            this.Text = "Rent A Car";
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));


     
        }




        //interogheaza utilizatorul daca doreste sa inchida aplicatia 
        public void opresteaprilcatia()
        {
            string message = "Sunteti sigur ca doriti sa parasiti aplicatia ?";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Afiseaza Mesagebox 

            result = MessageBox.Show(this, message, caption, buttons);

            if (result == DialogResult.Yes)
            {

                // Inchide aplicatia 

                Environment.Exit(0);
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



        public void autentificare()
        {
            if ((username != null)&&(username.TextLength > 0) && ((password != null) && (username.TextLength > 0)))
            {
                SqlConnection scn = new SqlConnection();
                scn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\database.mdf;Integrated Security=True;Connect Timeout=30";
                string cautare_dupa = "select count (*) as cnt from users where username=@usr and password=@pass ";
                SqlCommand scmd = new SqlCommand(cautare_dupa, scn);
                scmd.Parameters.Clear();
                scmd.Parameters.AddWithValue("@usr", username.Text);
                scmd.Parameters.AddWithValue("@pass", password.Text);
                scn.Open();
                var res = scmd.ExecuteScalar().ToString();
                if (res == "1")
                {
                    scn.Close();
                    Clientexistent clientexistent = new Clientexistent();
                    clientexistent.Location = this.Location;
                    Hide();
                    clientexistent.Show();
                }

                else
                {
                    scn.Close();
                    MessageBox.Show("username sau passwrod gresit");



                }
            }
            else
            {
                MessageBox.Show("Introduceti Numele de Utilizator si Parola ");
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {
            autentificare();           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Close();
            opresteaprilcatia();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginAdmin loginAdmin = new LoginAdmin();
            loginAdmin.Location = this.Location;
            Hide();

            loginAdmin.Show();
        }
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Client loginAdmin = new Client();
        //    loginAdmin.Location = this.Location;
        //    Hide();

        //    loginAdmin.Show();
        //}

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client secondform = new Client();
            Hide();
            secondform.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            AboutBox1 aboutWindow = new AboutBox1();
            aboutWindow.StartPosition = FormStartPosition.Manual;
            aboutWindow.Left = this.Location.X+50;
            aboutWindow.Top = this.Location.Y+80;
            // aboutWindow.SetDesktopLocation(500, 500);
            aboutWindow.ShowDialog();

            

        }

        private void LoginForm_Enter(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter key pressed");
            }
        }
    }
}
