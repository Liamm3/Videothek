using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using Videothek.Logic.Ui.Messages;

namespace Videothek.Ui.Desktop {

    public partial class Artikel : UserControl {
        private ChildWindowPickData _childWindow;
        private bool _isChildWindowOpen = false;

        public Artikel() {
            InitializeComponent();

            Messenger
                .Default
                .Register(this, (OpenPickDataMessage message) => {
                    if (_childWindow == null && !_isChildWindowOpen) {
                        _childWindow = new ChildWindowPickData(message.Items);
                        _childWindow.Closed += (sender, args) => {
                            Cleanup();
                        };

                        _childWindow.Show();
                        _isChildWindowOpen = true;
                    }
                });

            Messenger
               .Default
               .Register(this, (ChooseItemMessage _) => {
                   _childWindow.Close();
                   Cleanup();
               });
        }

        private void Cleanup() {
            _childWindow = null;
            _isChildWindowOpen = false;
            Messenger.Default.Unregister<OpenPickDataMessage>(this);
        }
    }
}
