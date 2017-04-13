using System;
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
    }
}
