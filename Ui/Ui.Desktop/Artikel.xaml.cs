﻿using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using Videothek.Logic.Ui;
using Videothek.Logic.Ui.Messages;

namespace Videothek.Ui.Desktop {

    public partial class Artikel : UserControl {
        private ChildWindowPickData _childWindow = new ChildWindowPickData();
        private bool _isChildWindowOpen = false;

        public Artikel() {
            InitializeComponent();

            Messenger
                .Default
                .Register(this, (NotificationMessage message) => {
                    if (!_isChildWindowOpen) {
                        _childWindow.Closed += (sender, args) => {
                            _isChildWindowOpen = false;
                            _childWindow = new ChildWindowPickData();
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

        private void SetChildWindowToMatchingUserControl(string name) {
            // implement
        }
    }
}
