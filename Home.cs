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
    public partial class NeoHomePage : Form
    {
        // Skapa ett objekt för klassen
        DbManager dbOject = new DbManager();

        // Skapa objekt för klassen funkton
        functions funcObject = new functions();

        public NeoHomePage()
        {
            InitializeComponent();

            //
            countCallings();
        }

        public void countCallings()
        {
            int counter = 0;
            int time = 0;
            int notifications = 0;

            // 
            int doctor = 0;
            int physiotherapist = 0;
            int psychologist = 0;

            // Hämta in data.
            var dt = dbOject.executeDbQuery("SELECT * FROM Children");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Hämta objektet
                DataRow dr = dt.Rows[i];

                int status = new int();
                if (dr["status"].ToString() != "")
                {
                    status = int.Parse(dr["status"].ToString());
                }

                if (funcObject.countDays(dr["planned_birthday"].ToString(), DateTime.Now.ToString("yyyy-M-dd")) <= 35)
                {
                    if(status == 0 || status <= 2)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;
                    }
                    else if(status >= 3 || status <= 4)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;
                        psychologist = psychologist + 30;
                    }

                    counter++;
                }
                else
                {
                    if(status != 5)
                    {
                        notifications = notifications + 1;
                    }
                }
            }

            // Räkna ut tiden
            time = doctor + physiotherapist + psychologist;

            // Visa
            callingslabel.Text = counter.ToString();
            timeLabel.Text = time.ToString();
            physiotherapistLabel.Text = physiotherapist.ToString();
            notificationsLabel.Text = notifications.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manage ManageForm = new Manage("select * from Children", this);
            ManageForm.Show();
        }

        public void PerformRefresh()
        {
            Console.WriteLine("UPDATE");
            countCallings();
        }
    }
}
