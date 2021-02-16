using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel {

    /// <summary>
    /// ViewModel für HauptFenster.xaml.
    /// </summary>
    public class HauptFensterViewModel : ViewModelBase {

        /// <summary>
        /// Property für die ausgewählte Tabelle.
        /// </summary>
        public DataView SelectedData {
            get => _selectedData;
            private set {
                _selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        /// <summary>
        /// Property für den Command _onTableSelect. Holt die ausgewählte Tabelle
        /// aus der Datenbank.
        /// </summary>
        public ICommand OnTableSelect {
            get {
                if (_onTableSelect == null) {
                    _onTableSelect = new RelayCommand<string>(table =>
                        SelectedData = GetDataViewOf(table)
                    );
                }

                return _onTableSelect;
            }
        }

        /// <summary>
        /// Initialisiere eine Verbindung zur Datenbank.
        /// </summary>
        private readonly SqlConnection conn = new SqlConnection() {
            ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;" +
                               "Initial Catalog=Bibliothek;" +
                               "Integrated Security=SSPI;"
        };

        /// <summary>
        /// Field für die Daten aus der Datenbank.
        /// </summary>
        private DataView _selectedData;

        /// <summary>
        /// Field für den Command zum fetchen der Daten aus der Datenbank.
        /// </summary>
        private ICommand _onTableSelect;

        /// <summary>
        /// Holt alle Zeilen aus einer Tabelle mit allen Spalten und gibt diese als
        /// DataView zurück.
        /// </summary>
        /// <param name="table">Der Name der Tabelle, dessen Daten geholt werden sollen.</param>
        /// <returns>Alle Reihen mit allen Spalten der Tabelle als DataView.</returns>
        private DataView GetDataViewOf(string table) {
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
    }
}
