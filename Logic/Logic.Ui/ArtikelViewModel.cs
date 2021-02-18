using GalaSoft.MvvmLight;

namespace Videothek.Logic.Ui {

    public class ArtikelViewModel : ViewModelBase {

        public int Id {
            get => _id;
            set {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Bezeichnung {
            get => _bezeichnung;
            set {
                _bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }

        public string Kategorie {
            get => _kategorie;
            set {
                _kategorie = value;
                RaisePropertyChanged("Kategorie");
            }
        }

        public double Leihpreis {
            get => _leihpreis;
            set {
                _leihpreis = value;
                RaisePropertyChanged("Leihpreis");
            }
        }

        public int Menge {
            get => _menge;
            set {
                _menge = value;
                RaisePropertyChanged("Menge");
            }
        }

        private int _id;
        private string _bezeichnung;
        private string _kategorie;
        private double _leihpreis;
        private int _menge;
    }
}
