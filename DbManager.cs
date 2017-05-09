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
        public string checkDb()
        {
            // Sökvägen till mappen dokument
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Neo";

            // Skapa en Neo mapp 
            System.IO.Directory.CreateDirectory(path);

            // Kontrollera om filen finns redan
            if (File.Exists(path + "\\db.mdf") == false)
            {
                File.WriteAllBytes(path + "\\db.mdf", Properties.Resources.db);
            }

            return path;
        }

        public DataTable executeDbQuery(String sql)
        {
            var dt = new DataTable();

            // Sökvägen till mappen dokument
            String path = checkDb();
            try
            {
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
                
            }
            catch (Exception)
            {
                throw;
            }

            // Skicka tillbaka objektet
            return dt;
        }

        public void iuQuery(string sqlQuery)
        {
            // Hämta sökvägen till db
            String path = checkDb();
            try
            {
                // Skapa en connection
                System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + path + "\\db.mdf;Integrated Security=True");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // Kör frågan
                cmd.CommandText = sqlQuery;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
