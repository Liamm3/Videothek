using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel {

    /// <summary>
    /// ViewModel für HauptFenster.xaml.
    /// </summary>
    public class HauptFensterViewModel : ViewModelBase {

        #region Properties und Fields

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
        /// Property für den Namen der momentan ausgewählten Tabelle.
        /// </summary>
        public string NameOfSelectedTable {
            get => _nameOfSelectedTable;
            set {
                _nameOfSelectedTable = value;
                RaisePropertyChanged("NameOfSelectedTable");
            }
        }

        /// <summary>
        /// Property für den Command _onTableSelect. Holt die ausgewählte Tabelle
        /// aus der Datenbank.
        /// </summary>
        public ICommand OnTableSelect {
            get {
                if (_onTableSelect == null) {
                    _onTableSelect = new RelayCommand<string>(table => {
                        NameOfSelectedTable = table;
                        SelectedData = GetDataViewOfSelectedTable();
                    });
                }

                return _onTableSelect;
            }
        }

        /// <summary>
        /// Property für den Command _onAddItemToTable.
        /// </summary>
        public ICommand OnAddItemToTable {
            get {
                if (_onAddItemToTable == null) {
                    _onAddItemToTable = new RelayCommand(() => {
                        var message = new NotificationMessage(NameOfSelectedTable);
                        Messenger.Default.Send<NotificationMessage>(message);
                    });
                }

                return _onAddItemToTable;
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
        /// Field für den Namen der momentan ausgewählten Tabelle.
        /// </summary>
        private string _nameOfSelectedTable;

        /// <summary>
        /// Field für den Command zum fetchen der Daten aus der Datenbank.
        /// </summary>
        private ICommand _onTableSelect;

        /// <summary>
        /// Field für einen Command zum öffnen des Hinzufügen Dialogs.
        /// </summary>
        private ICommand _onAddItemToTable;

        #endregion Properties und Fields

        #region Methoden

        /// <summary>
        /// Holt alle Zeilen aus der ausgewählten Tabelle mit allen Spalten und gibt
        /// diese als DataView zurück.
        /// </summary>
        /// <returns>Alle Reihen mit allen Spalten der Tabelle als DataView.</returns>
        private DataView GetDataViewOfSelectedTable() {
            var dt = new DataTable();
            var adapter = new SqlDataAdapter();

            try {
                conn.Open();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {NameOfSelectedTable}",
                                                       conn);
                adapter.Fill(dt);
                return dt.DefaultView;
            } catch (System.Exception e) {
                conn.Close();
                throw e;
            } finally {
                conn.Close();
            }
        }

        #endregion Methoden
    }
}
