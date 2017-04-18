using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neo
{
    public partial class PersonForm : Form
    {
        private string selectedPersonId;
        private Boolean edit;
        public Manage manageWindow;

        // Skapa ett objekt för klassen
        DbManager dbOject = new DbManager();
        
        // Skapa objekt för klassen funkton
        functions funcObject = new functions();

        public PersonForm(string personidIn, Boolean editIn, Manage windowIn)
        {
            InitializeComponent();
            edit = editIn;
            selectedPersonId = personidIn;
            manageWindow = windowIn;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersonForm_FormClosing);

            // Hämnta info från db om det är
            // redigeringsknaopen som ledde hit
            if (edit == true)
            {
                // Lås person_id
                person_id.Enabled = false;

                // Hämta in data.
                var dt = dbOject.executeDbQuery("SELECT * FROM Children WHERE person_id='" + selectedPersonId + "'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Hämta objektet
                    DataRow dr = dt.Rows[i];

                    // Visa de olika attibuten i rätt kolumn
                    person_id.Text = dr["person_id"].ToString();
                    child_firstname.Text = dr["first_name"].ToString();
                    child_lastname.Text = dr["last_name"].ToString();
                    mother_firstname.Text = dr["mother_first_name"].ToString();
                    mother_lastname.Text = dr["mother_last_name"].ToString();
                    comments.Text = dr["comments"].ToString();

                    
                    // Konvertera och visa datum
                    string[] date = funcObject.splitDate(dr["planned_birthday"].ToString());
                    birthdayPicker.Value = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));

                    // Tolk
                    if(dr["interpreter"].ToString() == "0")
                    {
                        interpreter.Checked = false;
                    }
                    else
                    {
                        interpreter.Checked = true;
                    }

                    // titel
                    this.Text = dr["first_name"].ToString() + " " + dr["first_name"].ToString() + " (" + dr["person_id"].ToString() + ")";
                }
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if(edit == true)
            {
                updatePerson();
            }
            else
            {

            }

            // Stäng fönsret
            Close();
        }

        public void updatePerson()
        {
            // Tolk variabeln
            string interpreterValue = "0";
            if (interpreter.Checked == true)
            {
                interpreterValue = "1";
            }

            // Redigera data
            string sqlQuery = "UPDATE Children SET first_name = '" + child_firstname.Text
                + "', last_name = '" + child_lastname.Text
                + "', mother_first_name='" + mother_firstname.Text
                + "', mother_last_name='" + mother_lastname.Text
                + "', planned_birthday='" + birthdayPicker.Value.ToShortDateString().ToString()
                + "', interpreter='" + interpreterValue.ToString()
                + "', comments='" + comments.Text.ToString()
                + "' WHERE person_id = '" + selectedPersonId + "'; ";

            // Executaaaaa
            dbOject.iuQuery(sqlQuery);
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            manageWindow.PerformRefresh();
        }
    }
}
