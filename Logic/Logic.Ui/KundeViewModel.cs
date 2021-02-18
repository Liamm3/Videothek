using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Videothek.Logic.Ui {

    public class KundeViewModel : ViewModelBase {

        public KundeViewModel() {
            db = new DbAbfragen();
        }

        private DbAbfragen db;

        public int Id {
            get => _id;
            set {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Vorname {
            get => _vorname;
            set {
                _vorname = value;
                RaisePropertyChanged("Vorname");
            }
        }

        public string Nachname {
            get => _nachname;
            set {
                _nachname = value;
                RaisePropertyChanged("Nachname");
            }
        }

        public string Strasse {
            get => _strasse;
            set {
                _strasse = value;
                RaisePropertyChanged("Strasse");
            }
        }

        public string Hausnummer {
            get => _hausnummer;
            set {
                _hausnummer = value;
                RaisePropertyChanged("Hausnummer");
            }
        }

        public string Plz {
            get => _plz;
            set {
                _plz = value;
                RaisePropertyChanged("Plz");
            }
        }

        public string Ort {
            get => _ort;
            set {
                _ort = value;
                RaisePropertyChanged("Ort");
            }
        }

        public ICommand OnAddCustomer {
            get {
                if (_onAddCustomer == null) {
                    _onAddCustomer = new RelayCommand(() => {
                        var isSuccessful =
                            db.AddCustomer(Vorname, Nachname, Strasse, Hausnummer, Plz, Ort);

                        var r = new Result(isSuccessful, "OnAddItemInDialog");
                        Messenger.Default.Send<Result>(r);

                        if (isSuccessful) {
                            Vorname = "";
                            Nachname = "";
                            Strasse = "";
                            Hausnummer = "";
                            Plz = "";
                            Ort = "";
                        }
                    });
                }

                return _onAddCustomer;
            }
        }

        private int _id;
        private string _vorname;
        private string _nachname;
        private string _strasse;
        private string _hausnummer;
        private string _plz;
        private string _ort;
        private ICommand _onAddCustomer;
    }
}
