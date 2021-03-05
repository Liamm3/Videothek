using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows.Input;
using Videothek.Logic.Ui.Messages;
using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui {

    public class PickDataViewModel : ViewModelBase {

        public dynamic SelectedItem {
            get => _selectedItem;
            set {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ICommand OnChooseItem {
            get {
                if (_onChooseItem == null) {
                    _onChooseItem = new RelayCommand(() => {
                        var m = new ChooseItemMessage(SelectedItem.TABLE_NAME, SelectedItem.Id);
                        Messenger.Default.Send(m);
                        // send object to artikelviewmodel.cs
                        // schliesse fenster wenn erfolgreich
                    });
                }

                return _onChooseItem;
            }
        }

        private dynamic _selectedItem;
        private ICommand _onChooseItem;
    }
}
