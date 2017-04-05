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

            DbManager dbOject = new DbManager();
            var dt = dbOject.executeDbQuery("select * from Children");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["person_id"].ToString());
                listitem.SubItems.Add(dr["first_name"].ToString());
                listitem.SubItems.Add(dr["last_name"].ToString());
                listitem.SubItems.Add(dr["mother_first_name"].ToString() + " " + dr["mother_last_name"].ToString());
                listitem.SubItems.Add(dr["planned_birthday"].ToString());
                listitem.SubItems.Add(dr["interpreter"].ToString());

                listView1.Items.Add(listitem);
            }
        }
    }
}
