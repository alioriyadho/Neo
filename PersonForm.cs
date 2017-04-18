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

        // Skapa ett objekt för klassen
        DbManager dbOject = new DbManager();
        
        // Skapa objekt för klassen funkton
        functions funcObject = new functions();

        public PersonForm(string personidIn, Boolean editIn)
        {
            InitializeComponent();
            edit = editIn;
            selectedPersonId = personidIn;

            // Hämnta info från db om det är
            // redigeringsknaopen som ledde hit
            if (edit == true)
            {
                // Hämta in data.
                var dt = dbOject.executeDbQuery("SELECT * FROM Children WHERE person_id='" + selectedPersonId + "'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Hämta objektet
                    DataRow dr = dt.Rows[i];

                    // Visa de olika attibuten i rätt kolumn
                    person_id.Text = dr["person_id"].ToString();
                    child_firstname.Text = dr["first_name"].ToString();
                    child_lastname.Text = dr["first_name"].ToString();
                    mother_firstname.Text = dr["mother_first_name"].ToString();
                    mother_lastname.Text = dr["mother_last_name"].ToString();
                    comments.Text = dr["comments"].ToString();

                    // Konvertera och visa datum
                    string[] date = funcObject.splitDate(dr["planned_birthday"].ToString());
                    birthdayPicker.Value = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                }
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {

        }
    }
}
