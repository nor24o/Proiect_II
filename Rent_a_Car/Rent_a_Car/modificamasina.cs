using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent_a_Car
{
    public partial class modificamasina : Form
    {
        int ids = 0;
        string rez = "";
        string pred = "";
        functions fun = new functions();

        public modificamasina(int id)
        {
           
            InitializeComponent();
            monthCalendar2.MinDate = DateTime.Now;
            monthCalendar1.MinDate = DateTime.Now;
            this.ids = id;
            DataTable dt = new DataTable();
            dt.Merge(fun.GetcarData(id));
            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;

                text_marca.Text = (array[1].ToString());
                text_model.Text = (array[2].ToString());
                text_motorizare.Text = (array[3].ToString());
                DateTime example = Convert.ToDateTime(array[4]);
                monthCalendar1.SetDate(example);
               DateTime example2 = Convert.ToDateTime(array[5]);
                monthCalendar2.SetDate(example2);
                
                textBox1.Text = (array[6].ToString());
                


            }
        }

        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {

            Console.WriteLine(ids);
            fun.UpdateCarRegistrationTable(ids, text_marca.Text, text_model.Text, text_motorizare.Text, rez, pred, textBox1.Text);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.rez = monthCalendar1.SelectionRange.Start.ToShortDateString();
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar2.MinDate = monthCalendar1.SelectionRange.Start;
            this.pred = monthCalendar2.SelectionRange.Start.ToShortDateString();
        }
    }
}
