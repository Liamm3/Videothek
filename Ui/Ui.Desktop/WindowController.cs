using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using Videothek.Logic.Ui.Messages;

namespace Videothek.Ui.Desktop {

    public abstract class WindowController : UserControl {
        public ChildWindow ChildWindow { set => _childWindow.Content = value; }

        private ChildWindow _childWindow = new ChildWindow();
        private bool _isChildWindowOpen = false;

        public WindowController() : base() {
            Messenger
                .Default
                .Register(this, (NotificationMessage message) => {
                    if (!_isChildWindowOpen) {
                        _childWindow.Closed += (sender, args) => {
                            _isChildWindowOpen = false;
                            _childWindow = new ChildWindow();
                        };

                        SetChildWindowToMatchingUserControl(message.Notification);

                        if (_childWindow.Content != null) {
                            _childWindow.Show();
                            _isChildWindowOpen = true;
                        }
                    }
                });

            Messenger
               .Default
               .Register(this, (AddResultMessage m) => {
                   if (m.Success) {
                       _isChildWindowOpen = false;
                       _childWindow.Close();
                   } else {
                       MessageBox.Show("Es ist ein Fehler aufgetretreten. :/");
                   }
               });
        }

        protected abstract void SetChildWindowToMatchingUserControl(string name);
    }
}
