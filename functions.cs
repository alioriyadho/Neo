﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo
{
    public class functions
    {
        public string getDateByStartDatePlusInterval(string startDate, int interval)
        {
            DateTime time = DateTime.Parse(startDate);
            Console.WriteLine(time);

            // Räkna ut nya datumet
            DateTime MyNewDateValue = time.AddDays(interval);

            return MyNewDateValue.ToString();
        }

        public string[] splitDate(string strDate)
        {
            string[] removeTime = strDate.Split(' ');
            string[] arrDate = removeTime[0].Split('-');
            return arrDate;
        }

        public string translateStatusCode(int code)
        {
            string statusVar = "";

            if(code == 0)
            {
                statusVar = "5-6 månaders kontroll";
            }

            if (code == 1)
            {
                statusVar = "10-12 månaders kontroll";
            }

            if (code == 2)
            {
                statusVar = "18-20 månaders kontroll";
            }

            if (code == 3)
            {
                statusVar = "2 års kontroll";
            }

            if (code == 4)
            {
                statusVar = "5 års kontroll";
            }

            return statusVar;
        }

        public int countDays(string to, string from)
        {
            DateTime d1 = DateTime.Parse(to);
            DateTime d2 = DateTime.Parse(from);
            return int.Parse((d2 - d1).TotalDays.ToString());
        }
    }
}
