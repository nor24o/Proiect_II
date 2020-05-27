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
        string id = "";
        functions fun = new functions();

        public modificamasina(int id)
        {
           
            InitializeComponent();
            this.ids = id;
            DataTable dt = new DataTable();
            dt.Merge(fun.GetcarData(id));
            foreach (DataRow row in dt.Rows)
            {
                object[] array = row.ItemArray;

                text_marca.Text = (array[1].ToString());
                text_model.Text = (array[2].ToString());
                text_motorizare.Text = (array[3].ToString());

                if((array[4].ToString()!= "Nerezervat")&&(array[5].ToString()!= "Nerezervat"))
                {
                    Console.WriteLine(array[4].ToString() + " " + array[5].ToString());
                    DateTime date = Convert.ToDateTime(array[4].ToString());
                    DateTime date2 = Convert.ToDateTime(array[5].ToString());
                    monthCalendar1.SetDate(date);
                    monthCalendar2.SetDate(date2);
                }

                textBox1.Text = (array[6].ToString());
            }
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = fun.afisaredb_client();

        }

        private void btn_salvare_masina_Click(object sender, EventArgs e)
        {
            if (id != "" && id.Length > 0)
            {
                Console.WriteLine(ids);
                fun.UpdateCarRegistrationTable(ids, text_marca.Text, text_model.Text, text_motorizare.Text, rez, pred, id);
                MessageBox.Show("Masina Actualizata");
            
        }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();


            };
        }
    }
}
