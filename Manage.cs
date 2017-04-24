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
        private string sqlQuery;
        private string selectedPersonId;
        private functions funcObject;
        private NeoHomePage homeWindow;

        // Skapa ett objekt för klassen
        DbManager dbOject = new DbManager();

        public Manage(string sqlQueryIn, NeoHomePage homeIn)
        {
            InitializeComponent();

            homeWindow = homeIn;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageForm_FormClosing);

            // lagra frågan i variabel
            sqlQuery = sqlQueryIn;
            funcObject = new functions();

            // 
            this.Text = "Hantera";

            // Fyll i listvyn
            getDataFromDb();
        }

        public void getDataFromDb()
        {
            listView1.View = View.Details;
           
            // Hämta in data.
            var dt = dbOject.executeDbQuery(sqlQuery);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Hämta objektet
                DataRow dr = dt.Rows[i];

                // Visa de olika attibuten i rätt kolumn
                ListViewItem listitem = new ListViewItem(dr["person_id"].ToString());
                listitem.SubItems.Add(dr["first_name"].ToString());
                listitem.SubItems.Add(dr["last_name"].ToString());
                listitem.SubItems.Add(dr["mother_first_name"].ToString() + " " + dr["mother_last_name"].ToString());
                listitem.SubItems.Add(dr["planned_birthday"].ToString());

                // Visa Ja eller Nej
                if (dr["interpreter"].ToString() == "1")
                {
                    listitem.SubItems.Add("Ja");
                }
                else
                {
                    listitem.SubItems.Add("Nej");
                }
                

                // Lägg upp i listan
                listView1.Items.Add(listitem);
            }
        }

        // Hanterar val av person i listvyn
        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                // Ominit
                selectedPersonId = null;
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                // Lagra det markerade idet i en global variabel
                String text = listView1.Items[intselectedindex].Text;
                selectedPersonId = text;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (selectedPersonId == null)
            {
                MessageBox.Show("Välj först ett barn från listan nedan.", "Fel");
            }
            else
            {
                // Här ska vi visa fönstret där man kan ändra uppgifter om barnet
                PersonForm personForm = new PersonForm(selectedPersonId, true, this);
                selectedPersonId = null;
                personForm.Show();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Här ska vi visa ett tomt PersonForm 
            PersonForm personForm = new PersonForm(selectedPersonId, false, this);
            selectedPersonId = null;
            personForm.Show();
        }

        public void PerformRefresh()
        {
            Console.WriteLine("UPDATE");
            listView1.Items.Clear();
            getDataFromDb();
        }

        private void DeleteChild_Click(object sender, EventArgs e)
        {
            if (selectedPersonId == null)
            {
                MessageBox.Show("Välj först ett barn från listan nedan.", "Fel");
            }
            else
            {
                // Ta bort ett barn från DB
                DialogResult dialog = dialog = MessageBox.Show("Vill du verkligen ta bort " + selectedPersonId + "?", "Ta bort", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string sqlQuery = "DELETE FROM Children WHERE person_id = '" + selectedPersonId + "'";
                    dbOject.iuQuery(sqlQuery);
                    selectedPersonId = null;
                    PerformRefresh();
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (selectedPersonId == null)
            {
                MessageBox.Show("Välj först ett barn från listan nedan.", "Fel");
            }
            else
            {
                // Hämta detaljer info från DB
                var child = dbOject.executeDbQuery("select * from Children where person_id = " + selectedPersonId);
                DataRow childDr = child.Rows[child.Rows.Count - 1];

                // Skapa objekt för klassen funkton
                functions funcObject = new functions();

                MessageBox.Show(childDr["first_name"].ToString() + " " + childDr["last_name"].ToString()
                    + "\n\n2 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 61)
                    + "\n6 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 186)
                    + "\n12 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 365), "Detaljer");
                //
                // Här ska vi visa fönstret som visar detaljerad information om barnets
                //
            }
        }

        private void ManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            homeWindow.PerformRefresh();
        }
    }
}
