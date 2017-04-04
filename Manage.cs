using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neo
{
    public partial class Manage : Form
    {
        public Manage()
        {
            InitializeComponent();

            // Fyll i listvyn
            getDataFromDb();
        }

        public void getDataFromDb()
        {
            listView1.View = View.Details;

            string constring = "Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=H:/Neonatalvården/Neo/Neo/db.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter ada = new SqlDataAdapter("select * from Children", con);
            DataTable dt = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["person_id"].ToString());
                listitem.SubItems.Add(dr["first_name"].ToString());
                listitem.SubItems.Add(dr["first_name"].ToString());
                listitem.SubItems.Add(dr["first_name"].ToString());
                listView1.Items.Add(listitem);
            }
        }
    }
}
