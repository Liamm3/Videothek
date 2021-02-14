using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        private ICommand _onStartKlick;

        public ICommand OnStartKlick
        {
            get
            {
                if (_onStartKlick == null)
                {
                    _onStartKlick = new RelayCommand(() =>
                    {
                        var message = new NotificationMessage("OnStartKlick");
                        Messenger.Default.Send<NotificationMessage>(message);
                    });
                }

                return _onStartKlick;
            }
        }
    }
}
