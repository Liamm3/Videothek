using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
            var dict = new Dictionary<string, string>();
            dict.Add("Name", nachname);
            dict.Add("Vorname", vorname);
            dict.Add("Strasse", strasse);
            dict.Add("Hausnummer", hausnummer);
            dict.Add("PLZ", plz);
            dict.Add("Ort", ort);

            var isSucessful = GenericInsert(TableNames.Kunde, dict);

            return isSucessful;
        }

        public bool AddCategory(string bezeichnung) {
            var dict = new Dictionary<string, string>();
            dict.Add("Bezeichnung", bezeichnung);

            var isSucessful = GenericInsert(TableNames.Kategorie, dict);

            return isSucessful;
        }

        private bool GenericInsert(string table, Dictionary<string, string> columnNameValuePairs) {
            // TODO: Fixen
            conn.ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;Initial Catalog=Bibliothek;Integrated Security=SSPI;";

            foreach (var value in columnNameValuePairs.Values) {
                if (string.IsNullOrWhiteSpace(value)) {
                    return false;
                }
            }

            try {
                using (conn) {
                    conn.Open();
                    var query = GenerateInsertPreparedQuery(table, columnNameValuePairs.Keys);
                    var cmd = GenerateSqlCommand(conn, query, columnNameValuePairs);
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

        private string GenerateInsertPreparedQuery(
            string table,
            Dictionary<string, string>.KeyCollection columnNames
        ) {
            var columnList = columnNames.ToList();
            string columns = "";
            string values = "";
            foreach (var name in columnNames) {
                if (columnList.IndexOf(name) < columnList.Count - 1) {
                    columns += $"{name},";
                    values += $"@{name},";
                } else {
                    columns += name;
                    values += $"@{name}";
                }
            }

            return $"INSERT INTO {table} ({columns}) VALUES ({values})";
        }

        private SqlCommand GenerateSqlCommand(SqlConnection conn, string query,
                                              Dictionary<string, string> dict) {
            var cmd = new SqlCommand(query, conn);

            foreach (var kvp in dict) {
                cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value);
            }

            return cmd;
        }
    }
}
