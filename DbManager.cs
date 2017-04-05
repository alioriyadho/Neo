using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo
{
    public class DbManager
    {
        public DataTable executeDbQuery(String sql)
        {
            var dt = new DataTable();

            // Öppna en koppling till DB 
            using (var cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\Neonatalvården\\Neo\\Neo\\bin\\Debug\\db.mdf;Integrated Security=True"))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();

                // Kör Queryn
                using (var reader = cmd.ExecuteReader())
                {
                    // Ladda över till objektet som kommer att skickas tillbaka
                    dt.Load(reader);
                }
            }

            // Skicka tillbaka objektet
            return dt;
        }
    }
}
