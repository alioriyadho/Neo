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

        // DataTabellen som kommer in
        DataTable dataFromOtherView;

        public Manage(string sqlQueryIn, DataTable dataIn, NeoHomePage homeIn)
        {
            InitializeComponent();

            homeWindow = homeIn;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageForm_FormClosing);

            // lagra frågan i variabel
            sqlQuery = sqlQueryIn;

            if(dataIn != null)
            {
                dataFromOtherView = dataIn.Copy();
            }

            // init funktions klassen
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
            DataTable dt;
            if (sqlQuery != null)
            {
                dt = dbOject.executeDbQuery(sqlQuery);
            }
            else
            {
                dt = dataFromOtherView.Copy();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Hämta objektet
                DataRow dr = dt.Rows[i];

                // Visa de olika attibuten i rätt kolumn
                ListViewItem listitem = new ListViewItem(dr["person_id"].ToString().Insert(6, "-"));
                listitem.SubItems.Add(dr["first_name"].ToString());
                listitem.SubItems.Add(dr["last_name"].ToString());
                listitem.SubItems.Add(funcObject.translateStatusCode(int.Parse(dr["status"].ToString())));
                listitem.SubItems.Add(funcObject.getDateByStatus(funcObject.formatDate(dr["planned_birthday"].ToString(), "yyyy-MM-dd"), int.Parse(dr["status"].ToString())));
                listitem.SubItems.Add(funcObject.formatDate(dr["planned_birthday"].ToString(), "yyyy-MM-dd"));

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
                selectedPersonId = text.Replace("-", "");
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
                    // Hämta in data.
                    var dt = dbOject.executeDbQuery("SELECT * FROM Children WHERE person_id='" + selectedPersonId + "'");
                    if(dt.Rows.Count > 0)
                    {
                        deletePerson(selectedPersonId);
                    }
                    else
                    {
                        string personNr = selectedPersonId.Insert(6, "-");
                        dt = dbOject.executeDbQuery("SELECT * FROM Children WHERE person_id='" + personNr + "'");
                        if (dt.Rows.Count > 0)
                        {
                            deletePerson(personNr);
                        }
                    }
                }
            }
        }

        void deletePerson(string personId)
        {
            string sqlQuery = "DELETE FROM Children WHERE person_id = '" + personId + "'";
            dbOject.iuQuery(sqlQuery);
            selectedPersonId = null;
            PerformRefresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (selectedPersonId == null)
            {
                MessageBox.Show("Välj först ett barn från listan nedan.", "Fel");
            }
            else
            {
                // Kontroll
                Boolean control = false;

                // Hämta in data.
                var child = dbOject.executeDbQuery("select * from Children where person_id = '" + selectedPersonId + "'");
                if (child.Rows.Count > 0)
                {
                    control = true;
                }
                else
                {
                    string personNr = selectedPersonId.Insert(6, "-");
                    child = dbOject.executeDbQuery("select * from Children where person_id = '" + personNr + "'");
                    if (child.Rows.Count > 0)
                    {
                        control = true;
                    }
                }

                // Om det finns ett bra visa
                if(control == true)
                {
                    DataRow childDr = child.Rows[child.Rows.Count - 1];

                    // Skapa objekt för klassen funkton
                    functions funcObject = new functions();

                    MessageBox.Show(childDr["first_name"].ToString() + " " + childDr["last_name"].ToString()
                        + "\nStatus: " + funcObject.translateStatusCode(int.Parse(childDr["status"].ToString()))
                        + "\n\n2 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 61, "yyyy-MM-dd")
                        + "\n5-6 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 183, "yyyy-MM-dd")
                        + "\n10-12 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 365, "yyyy-MM-dd")
                        + "\n18-20 Månader: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 610, "yyyy-MM-dd")
                        + "\n2 år: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 730, "yyyy-MM-dd")
                        + "\n5 år: " + funcObject.getDateByStartDatePlusInterval(childDr["planned_birthday"].ToString(), 2013, "yyyy-MM-dd"), "Detaljer");
                }
            }
        }

        private void ManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            homeWindow.PerformRefresh();
        }
    }
}
