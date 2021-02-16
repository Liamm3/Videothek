using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel {

    /// <summary>
    /// ViewModel für Main.xaml.
    /// </summary>
    public class MainViewModel : ViewModelBase {

        /// <summary>
        /// Property für einen Command, der eine NotificationMessage mit dem String "OnStartKlick"
        /// verschickt.
        /// </summary>
        public ICommand OnStartKlick {
            get {
                if (_onStartKlick == null) {
                    _onStartKlick = new RelayCommand(() => {
                        var message = new NotificationMessage("OnStartKlick");
                        Messenger.Default.Send<NotificationMessage>(message);
                    });
                }

                return _onStartKlick;
            }
        }

        /// <summary>
        /// Field für den Command OnStartKlick.
        /// </summary>
        private ICommand _onStartKlick;
    }
}
