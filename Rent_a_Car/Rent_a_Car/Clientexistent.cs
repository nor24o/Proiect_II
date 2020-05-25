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
            int  id_user = Int32.Parse(fun.getIDUser(id));
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



    }
}
