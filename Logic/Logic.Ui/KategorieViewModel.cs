using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using Videothek.Logic.Ui.Messages;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui {

    public class KategorieViewModel : ViewModelBase {

        public int Id {
            get => kategorie.Id;
            set {
                kategorie.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Bezeichnung {
            get => kategorie.Bezeichnung;
            set {
                kategorie.Bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }

        public ICommand OnAddCategory {
            get {
                if (_onAddCategory == null) {
                    _onAddCategory = new RelayCommand(() => {
                        var isSuccessful = db.Insert(kategorie);
                        var r = new AddResultMessage(isSuccessful);
                        Messenger.Default.Send(r);

                        if (isSuccessful) {
                            Bezeichnung = "";
                            Messenger.Default.Send(
                                new NotificationMessage(Notifications.REFRESH_CURRENT_TABLE)
                            );
                        }
                    });
                }

                return _onAddCategory;
            }
        }

        private ICommand _onAddCategory;
        private Kategorie kategorie = new Kategorie();
        private DbAbfragen db = new DbAbfragen();
    }
}