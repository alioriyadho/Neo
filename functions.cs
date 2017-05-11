using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo
{
    public class functions
    {
        public string getDateByStartDatePlusInterval(string startDate, int interval, string format)
        {
            DateTime time = DateTime.Parse(startDate);
            Console.WriteLine(time);

            // Räkna ut nya datumet
            DateTime MyNewDateValue = time.AddDays(interval);

            return String.Format("{0:"+ format + "}", MyNewDateValue);
        }

        public string formatDate(string startDate, string format)
        {
            DateTime time = DateTime.Parse(startDate);
            return String.Format("{0:" + format + "}", time);
        }

        public string[] splitDate(string strDate)
        {
            string[] removeTime = strDate.Split(' ');
            string[] arrDate = removeTime[0].Split('-');
            return arrDate;
        }

        public string translateStatusCode(int code)
        {
            string statusVar = "Klar";

            if (code == 0)
            {
                statusVar = "2 månaders kontroll";
            }

            if (code == 1)
            {
                statusVar = "5-6 månaders kontroll";
            }

            if (code == 2)
            {
                statusVar = "10-12 månaders kontroll";
            }

            if (code == 3)
            {
                statusVar = "18-20 månaders kontroll";
            }

            if (code == 4)
            {
                statusVar = "2 års kontroll";
            }

            if (code == 5)
            {
                statusVar = "5.5 års kontroll";
            }

            return statusVar;
        }

        public string getDateByStatus(string dateIn, int code)
        {
            string date = "";

            if (code == 0)
            {
                date = getDateByStartDatePlusInterval(dateIn, 61, "yyyy-MM-dd");
            }

            if (code == 1)
            {
                date = getDateByStartDatePlusInterval(dateIn, 183, "yyyy-MM-dd");
            }

            if (code == 2)
            {
                date = getDateByStartDatePlusInterval(dateIn, 365, "yyyy-MM-dd");
            }

            if (code == 3)
            {
                date = getDateByStartDatePlusInterval(dateIn, 610, "yyyy-MM-dd");
            }

            if (code == 4)
            {
                date = getDateByStartDatePlusInterval(dateIn, 730, "yyyy-MM-dd");
            }

            if (code == 5)
            {
                date = getDateByStartDatePlusInterval(dateIn, 2013, "yyyy-MM-dd");
            }

            return date;
        }

        public int countDays(string to, string from)
        {
            DateTime d1 = DateTime.Parse(to);
            DateTime d2 = DateTime.Parse(from);
            return int.Parse((d2 - d1).TotalDays.ToString());
        }

        public bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

    }
}
