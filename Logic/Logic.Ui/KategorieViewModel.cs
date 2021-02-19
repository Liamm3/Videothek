using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Videothek.Logic.Ui {

    public class KategorieViewModel {

        public int Id {
            get => _id;
            set => _id = value;
        }

        public string Bezeichnung {
            get => _bezeichnung;
            set => _bezeichnung = value;
        }

        public ICommand OnAddCategory {
            get {
                if (_onAddCategory == null) {
                    _onAddCategory = new RelayCommand(() => {
                        var isSuccessful = db.AddCategory(Bezeichnung);
                        var r = new Result(isSuccessful, "OnAddItemInDialog");
                        Messenger.Default.Send<Result>(r);

                        if (isSuccessful) {
                            Bezeichnung = "";
                        }
                    });
                }

                return _onAddCategory;
            }
        }

        private int _id;
        private string _bezeichnung;
        private ICommand _onAddCategory;
        private DbAbfragen db = new DbAbfragen();
    }
}