﻿using System;
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
        public int interval = -35;
        string dateNow;

        DataTable manageCallingsData;
        DataTable manageNotificationsData;

        public NeoHomePage()
        {
            InitializeComponent();

            // Dagens datum
            dateNow = DateTime.Now.ToString("yyyy-M-dd");

            // Datum -35
            string[] date = funcObject.splitDate(funcObject.getDateByStartDatePlusInterval(dateNow, interval, "yyyy-MM-dd"));
            fromDatePicker.Value = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));

            // Räkna ut 
            countCallings();
        }

        public void countCallings()
        {
            // Hämta in data.
            var dt = dbOject.executeDbQuery("SELECT * FROM Children");
            manageCallingsData = dt.Clone();
            manageNotificationsData = dt.Clone();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Hämta objektet
                DataRow dr = dt.Rows[i];

                int status = new int();
                if (dr["status"].ToString() != "")
                {
                    status = int.Parse(dr["status"].ToString());
                }
                 
                int countInterval = (funcObject.countDays(funcObject.getDateByStatus(dr["planned_birthday"].ToString(), status), dateNow) * -1);

                Console.WriteLine("DATUM NU: " + dateNow);
                Console.WriteLine("INTERVALL: " + interval);
                Console.WriteLine("countInterval: " + countInterval);

                if (countInterval < 0 && countInterval >= interval)
                {
                    if (status == 0 || status <= 3)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;

                        // Lägg till
                        manageCallingsData.ImportRow(dr);

                        callings++;
                    }
                    else if(status >= 4 && status <= 5)
                    {
                        doctor = doctor + 30;
                        physiotherapist = physiotherapist + 30;
                        psychologist = psychologist + 30;

                        // Lägg till
                        manageCallingsData.ImportRow(dr);

                        callings++;
                    }
                }
                else
                {
                    if(status != 6)
                    {
                        if((countInterval+interval) <= interval)
                        {
                            notifications = notifications + 1;

                            // Lägg till
                            manageNotificationsData.ImportRow(dr);
                        }
                    }
                }
            }

            // Räkna ut tiden
            time = doctor + physiotherapist + psychologist;

            // Visa
            callingslabel.Text = callings.ToString();
            timeLabel.Text = doctor.ToString();
            physiotherapistLabel.Text = physiotherapist.ToString();
            notificationsLabel.Text = notifications.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manage ManageForm = new Manage("select * from Children", null, this);
            ManageForm.Show();
        }

        public void PerformRefresh()
        {
            Console.WriteLine("UPDATE");
            resetValues();
            countCallings();
        }

        public void resetValues()
        {
            callings = 0;
            doctor = 0;
            time = 0;
            physiotherapist = 0;
            psychologist = 0;
            notifications = 0;
        }

        private void timeLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Läkare: " + doctor
                                + "\nSjukgymnast: " + physiotherapist
                                + "\nPsykolog: " + psychologist, "Detaljerade tider (Minuter)");
        }

        // Den gula rutan
        private void notificationsLabel_Click(object sender, EventArgs e)
        {
            Manage ManageForm = new Manage(null, manageNotificationsData, this);
            ManageForm.Show();
        }

        // Den blåa rutan
        private void callingslabel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Antal dagar: " + funcObject.countDays(DateTime.Now.ToString("yyyy-MM-dd"), dateNow));

            string calcDate = funcObject.getDateByStartDatePlusInterval(dateNow, interval, "yyyy-MM-dd");
            Manage ManageForm = new Manage(null, manageCallingsData, this);
            ManageForm.Show();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            string to = fromDatePicker.Value.ToShortDateString().ToString();
            dateNow = toDatePicker.Value.ToShortDateString().ToString();

            // Räkna ut intervallen
            interval = funcObject.countDays(dateNow, to);

            // Återställ
            resetValues();
            
            // Uppdatera
            countCallings();
        }
    }
}
