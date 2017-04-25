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

        // Tider
        public int callings = 0;
        public int doctor = 0;
        public int time = 0;
        public int physiotherapist = 0;
        public int psychologist = 0;
        public int notifications = 0;
        public int interval = 35;

        public NeoHomePage()
        {
            InitializeComponent();

            //
            countCallings();
        }

        public void countCallings()
        {
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

                int countInterval = funcObject.countDays(dr["planned_birthday"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"));
                if (countInterval > 0 && countInterval <= interval)
                {
                    if (status == 0 || status <= 2)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;
                        callings++;
                    }
                    else if(status >= 3 || status <= 4)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;
                        psychologist = psychologist + 30;
                        callings++;
                    }
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
            callingslabel.Text = callings.ToString();
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
            callings = 0;
            doctor = 0;
            time = 0;
            physiotherapist = 0;
            psychologist = 0;
            notifications = 0;
            countCallings();
        }

        private void timeLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Läkare: " + doctor
                                + "\nSjukgymnast: " + physiotherapist
                                + "\nPsykolog: " + psychologist, "Detaljerade tider (Minuter)");
        }

        private void notificationsLabel_Click(object sender, EventArgs e)
        {
            string dateNow = DateTime.Now.ToString("yyyy-M-dd HH:mm");
            Manage ManageForm = new Manage("select * from Children WHERE status != 5 AND planned_birthday < '" + dateNow + "'", this);
            ManageForm.Show();
        }

        private void callingslabel_Click(object sender, EventArgs e)
        {
            string dateNow = DateTime.Now.ToString("yyyy-M-dd HH:mm");
            string calcDate = funcObject.getDateByStartDatePlusInterval(dateNow, interval);
            Manage ManageForm = new Manage("select * from Children WHERE planned_birthday BETWEEN '" + dateNow + "' AND '" + calcDate + "'", this);
            ManageForm.Show();
        }
    }
}
