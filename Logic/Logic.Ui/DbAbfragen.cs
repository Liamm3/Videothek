using System.Data;
using System.Data.SqlClient;

namespace Videothek.Logic.Ui {

    public class DbAbfragen {
        private SqlConnection conn;

        public DbAbfragen() {
            conn = new SqlConnection("Data Source=W011076SYS\\SQLEXPRESS;Initial Catalog=Bibliothek;Integrated Security=SSPI;");
        }

        /// <summary>
        /// Holt alle Zeilen aus einer Tabelle mit allen Spalten und gibt
        /// diese als DataView zurück.
        /// </summary>
        /// <returns>Alle Reihen mit allen Spalten der Tabelle als DataView.</returns>
        public DataView GetDataViewOf(string table) {
            var dt = new DataTable();
            var adapter = new SqlDataAdapter();

            try {
                conn.Open();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {table}", conn);
                adapter.Fill(dt);
                return dt.DefaultView;
            } catch (System.Exception e) {
                conn.Close();
                throw e;
            } finally {
                conn.Close();
            }
        }

        public bool AddCustomer(string vorname, string nachname, string strasse,
                                      string hausnummer, string plz, string ort) {
            try {
                using (conn) {
                    conn.ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;Initial Catalog=Bibliothek;Integrated Security=SSPI;";
                    conn.Open();
                    var query = @"INSERT INTO Kunde(Name, Vorname, Strasse, Hausnummer, PLZ, Ort)
                                  VALUES (@Nachname, @Vorname, @Strasse, @Hausnummer, @PLZ, @Ort)";
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nachname", nachname);
                    cmd.Parameters.AddWithValue("@Vorname", vorname);
                    cmd.Parameters.AddWithValue("@Strasse", strasse);
                    cmd.Parameters.AddWithValue("@Hausnummer", hausnummer);
                    cmd.Parameters.AddWithValue("@PLZ", plz);
                    cmd.Parameters.AddWithValue("@Ort", ort);
                    // cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return true;
            } catch (System.Exception ex) {
                return false;
                throw ex;
            } finally {
                conn.Close();
            }
        }
    }
}
