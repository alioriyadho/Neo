using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

            // Sökvägen till mappen dokument
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Neo";

            // Skapa en Neo mapp 
            System.IO.Directory.CreateDirectory(path);

            // Kontrollera om filen finns redan
            if (File.Exists(path + "\\db.mdf") == false)
            {
                File.WriteAllBytes(path + "\\db.mdf", Properties.Resources.db);
            }

            // Öppna en koppling till DB 
            using (var cn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + path + "\\db.mdf;Integrated Security=True"))
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
