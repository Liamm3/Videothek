using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Windows.Input;
using Videothek.Logic.Ui.Messages;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui {

    public class ArtikelViewModel : ViewModelBase {

        public int Id {
            get => _artikel.Id;
            set {
                _artikel.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Bezeichnung {
            get => _artikel.Bezeichnung;
            set {
                _artikel.Bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }

        public int KategorieId {
            get => _artikel.KategorieId;
            set {
                _artikel.KategorieId = value;
                RaisePropertyChanged("KategorieId");
            }
        }

        public double Leihpreis {
            get => _artikel.Leihpreis;
            set {
                _artikel.Leihpreis = value;
                RaisePropertyChanged("Leihpreis");
            }
        }

        public int Menge {
            get => _artikel.Menge;
            set {
                _artikel.Menge = value;
                RaisePropertyChanged("Menge");
            }
        }

        public ICommand OnAddArticle {
            get {
                if (_onAddArticle == null) {
                    _onAddArticle = new RelayCommand(() => {
                        var isSuccessful = db.Insert(_artikel);
                        var m = new AddResultMessage(isSuccessful);
                        Messenger.Default.Send(m);

                        if (isSuccessful) {
                            Bezeichnung = "";
                            KategorieId = 0;
                            Leihpreis = 0;
                            Menge = 0;
                            Messenger.Default.Send(
                                new NotificationMessage(Notifications.REFRESH_CURRENT_TABLE)
                            );
                        }
                    });
                }

                return _onAddArticle;
            }
        }

        public ICommand OnCategorySelect {
            get {
                if (_onCategorySelect == null) {
                    _onCategorySelect = new RelayCommand(() => {
                        var categories = db.GetAllByTable(TableNames.Kategorie);
                        var m = new OpenPickDataMessage(categories);
                        Messenger.Default.Send(m);
                    });
                }

                return _onCategorySelect;
            }
        }

        private Artikel _artikel = new Artikel();
        private DbAbfragen db = new DbAbfragen();
        private ICommand _onAddArticle;
        private ICommand _onCategorySelect;

        public ArtikelViewModel() {
            Messenger.Default.Register(this, (ChooseItemMessage message) => {
                Id = message.Id;
                var isSuccessful = db.Insert(_artikel);
                var r = new AddResultMessage(isSuccessful);
                Messenger.Default.Send(r);

                if (isSuccessful) {
                    Id = 0;
                    KategorieId = 0;
                    Bezeichnung = "";
                    Leihpreis = 0;
                    Menge = 0;
                    Messenger.Default.Send(
                        new NotificationMessage(Notifications.REFRESH_CURRENT_TABLE)
                    );
                }
            });
        }
    }
}
