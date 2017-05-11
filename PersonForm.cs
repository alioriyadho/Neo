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

                    if(dr["status"].ToString() != "" && dr["status"] != null)
                    {
                        if (funcObject.IsNumeric(dr["status"].ToString()))
                        {
                            statusBox.SelectedIndex = int.Parse(dr["status"].ToString());
                        }
                        else
                        {
                            statusBox.SelectedIndex = 0;
                        }
                    }

                    // Konvertera och visa datum
                    string[] date = funcObject.splitDate(dr["planned_birthday"].ToString());
                    if(date.Count() > 0)
                    {
                        birthdayPicker.Value = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                    }

                    // Tolk
                    if (dr["interpreter"].ToString() == "0")
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
            else
            {
                this.Text = "Lägg till ett barn";
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if (edit == true)
            {
                updatePerson();

                // Stäng fönsret
                Close();
            }
            else
            {
                // Kontroll personnymmret 
                if (person_id.Text.Length < 10 || funcObject.IsNumeric(person_id.Text) == false || person_id.Text.Length > 10)
                {
                    MessageBox.Show("Personnummeret är ogiltigt");
                }
                else if (child_firstname.Text.Length > 0 && child_lastname.Text.Length > 0)
                {
                    insertPerson();

                    // Stäng fönsret
                    Close();
                }
                else
                {
                    MessageBox.Show("Du måste fylla minst fylla i barnets för och efternamn");
                }
            }           
        }

        public void updatePerson()
        {
            // Tolk variabeln
            string interpreterValue = getInterpreterValue();

            // Redigera data
            string sqlQuery = "UPDATE Children SET first_name = '" + child_firstname.Text
                + "', last_name = '" + child_lastname.Text
                + "', mother_first_name='" + mother_firstname.Text
                + "', mother_last_name='" + mother_lastname.Text
                + "', planned_birthday='" + birthdayPicker.Value.ToShortDateString().ToString()
                + "', interpreter='" + interpreterValue.ToString()
                + "', comments='" + comments.Text.ToString()
                + "', status='" + statusBox.SelectedIndex.ToString()
                + "' WHERE person_id = '" + selectedPersonId + "'; ";

            // Executaaaaa
            dbOject.iuQuery(sqlQuery);
        }

        public void insertPerson()
        {
            string interpreterValue = getInterpreterValue();

            // Hämta in data.
            var dt = dbOject.executeDbQuery("SELECT * FROM Children WHERE person_id='" + person_id.Text + "'");
            if (dt.Rows.Count == 0)
            {
                // Skapa
                string sqlQuery = "INSERT INTO Children (person_id, first_name, last_name, mother_first_name, mother_last_name, planned_birthday, interpreter, comments, status)"
                                + "VALUES('" + person_id.Text + "', '" + child_firstname.Text + "', '" + child_lastname.Text + "', '" + mother_firstname.Text + "', '" + mother_lastname.Text + "', '" + birthdayPicker.Value.ToShortDateString() + "', '" + interpreterValue + "', '" + comments.Text + "', '" + statusBox.SelectedIndex.ToString() + "')";
                
                // Executaaaaa
                dbOject.iuQuery(sqlQuery);
            } 
            else
            {
                MessageBox.Show("Barnet finns redan inlagd i systemet.");
            }
        }

        public string getInterpreterValue()
        {
            string interpreterValue = "0";
            if (interpreter.Checked == true)
            {
                interpreterValue = "1";
            }
            return interpreterValue;
        }

        private void PersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            manageWindow.PerformRefresh();
        }
    }
}
