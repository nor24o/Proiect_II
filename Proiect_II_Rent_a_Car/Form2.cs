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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            customizeDesing();
        }
        
        public void Form2_Load(object sender, EventArgs e)
        {

        }

         private void customizeDesing()
        {
            panelVmenu.Visible = false;
            panelCmenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelVmenu.Visible == true)
                panelVmenu.Visible = false;
            if(panelCmenu.Visible == true)
                panelCmenu.Visible = false;
        }

        private void showSubMenu(Panel Vmenu)
        {
            if (Vmenu.Visible == false)
            {
                Vmenu.Visible = true;
            }
            else
                Vmenu.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panelVmenu);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCmenu);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
    }
}
