using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using Videothek.Logic.Ui.Interfaces;
using Videothek.Logic.Ui.Messages;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui.ViewModel {

    /// <summary>
    /// ViewModel für HauptFenster.xaml.
    /// </summary>
    public class HauptFensterViewModel : ViewModelBase {

        /// <summary>
        /// Property für die ausgewählte Tabelle.
        /// </summary>
        public ObservableCollection<dynamic> SelectedData {
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
                        SelectedData = new ObservableCollection<dynamic>(db.GetAllByTable(table));
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
        /// Field für die Daten aus der Datenbank.
        /// </summary>
        private ObservableCollection<dynamic> _selectedData;

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

        private DbAbfragen db = new DbAbfragen();

        public HauptFensterViewModel() {
            Messenger.Default.Register(this, (NotificationMessage m) => {
                if (m.Notification.Equals("RefreshCurrentTable")) {
                    SelectedData = new ObservableCollection<dynamic>(
                        db.GetAllByTable(NameOfSelectedTable)
                    );
                }
            });
        }
    }
}
