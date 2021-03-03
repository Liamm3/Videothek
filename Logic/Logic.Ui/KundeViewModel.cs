using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Videothek.Logic.Ui.Messages;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui {

    public class KundeViewModel : ViewModelBase {

        public int Id {
            get => kunde.Id;
            set {
                kunde.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Vorname {
            get => kunde.Vorname;
            set {
                kunde.Vorname = value;
                RaisePropertyChanged("Vorname");
            }
        }

        public string Nachname {
            get => kunde.Nachname;
            set {
                kunde.Nachname = value;
                RaisePropertyChanged("Nachname");
            }
        }

        public string Strasse {
            get => kunde.Strasse;
            set {
                kunde.Strasse = value;
                RaisePropertyChanged("Strasse");
            }
        }

        public string Hausnummer {
            get => kunde.Hausnummer;
            set {
                kunde.Hausnummer = value;
                RaisePropertyChanged("Hausnummer");
            }
        }

        public string Plz {
            get => kunde.Plz;
            set {
                kunde.Plz = value;
                RaisePropertyChanged("Plz");
            }
        }

        public string Ort {
            get => kunde.Ort;
            set {
                kunde.Ort = value;
                RaisePropertyChanged("Ort");
            }
        }

        public ICommand OnAddCustomer {
            get {
                if (_onAddCustomer == null) {
                    _onAddCustomer = new RelayCommand(() => {
                        var isSuccessful = db.Insert(kunde);
                        var r = new AddResultMessage(isSuccessful);
                        Messenger.Default.Send(r);

                        if (isSuccessful) {
                            Vorname = "";
                            Nachname = "";
                            Strasse = "";
                            Hausnummer = "";
                            Plz = "";
                            Ort = "";
                            Messenger.Default.Send(
                                new NotificationMessage(Notifications.REFRESH_CURRENT_TABLE)
                            );
                        }
                    });
                }

                return _onAddCustomer;
            }
        }

        private DbAbfragen db = new DbAbfragen();
        private Kunde kunde = new Kunde();
        private ICommand _onAddCustomer;
    }
}
