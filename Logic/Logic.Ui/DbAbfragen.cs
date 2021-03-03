using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui {

    public class DbAbfragen {

        private SqlConnection conn = new SqlConnection() {
            ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;Initial Catalog=Bibliothek;Integrated Security=SSPI;"
        };

        public bool Insert<T>(T record) where T : Record<T> {
            // TODO: Fixen
            conn.ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;Initial Catalog=Bibliothek;Integrated Security=SSPI;";
            var dict = record.ToDict();

            foreach (var value in dict.Values) {
                if (string.IsNullOrWhiteSpace(value)) {
                    return false;
                }
            }

            try {
                using (conn) {
                    conn.Open();
                    var query = GenerateInsertPreparedQuery(record.TABLE_NAME, dict.Keys);
                    var cmd = GenerateSqlCommand(conn, query, dict);
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

        public List<dynamic> GetAllByTable(string table) {
            switch (table) {
                case TableNames.Kunde:
                    return new List<dynamic>(GetAll<Kunde>());

                case TableNames.Kategorie:
                    return new List<dynamic>(GetAll<Kategorie>());

                case TableNames.Artikel:
                    return new List<dynamic>(GetAll<Artikel>());

                case TableNames.AusgelieheneArtikel:
                    return new List<dynamic>(GetAll<ArtikelAusgeliehen>());

                default:
                    return new List<dynamic>();
            }
        }

        private List<T> GetAll<T>() where T : Record<T>, new() {
            var t = new T();
            var dt = GetDataTableOf(t.TABLE_NAME);
            return t.FromDataTable(dt);
        }

        /// <summary>
        /// Holt alle Zeilen aus einer Tabelle mit allen Spalten und gibt
        /// diese als DataView zurück.
        /// </summary>
        /// <returns>Alle Reihen mit allen Spalten der Tabelle als DataView.</returns>
        private DataTable GetDataTableOf(string table) {
            var dt = new DataTable();
            var adapter = new SqlDataAdapter();

            try {
                conn.Open();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {table}", conn);
                adapter.Fill(dt);
                return dt;
            } catch (System.Exception e) {
                conn.Close();
                throw e;
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
