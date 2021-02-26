using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using Videothek.Logic.Ui;
using Videothek.Logic.Ui.Messages;

namespace Videothek.Ui.Desktop {

    /// <summary>
    /// Das Hauptfenster dient als Bedienoberfläche für alle wichtigen Funktionen.
    /// </summary>
    public partial class HauptFenster : UserControl {
        private ChildWindow _childWindow = new ChildWindow();
        private bool _isChildWindowOpen = false;

        /// <summary>
        /// Wenn der Nutzer eine Tabelle ausgewählt hat und auf "Hinzufügen" drückt, soll
        /// das passende Fenster geladen werden.
        /// </summary>
        public HauptFenster() {
            InitializeComponent();
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

        /// <summary>
        /// Setze ChildWindow auf das passende UserControl an Hand des Namens.
        /// </summary>
        /// <param name="name">Der Name der Tabelle, deren Dialog aufgerufen werden soll.</param>
        private void SetChildWindowToMatchingUserControl(string name) {
            switch (name) {
                case TableNames.Artikel:
                    _childWindow.Content = new Artikel();
                    break;

                case TableNames.AusgelieheneArtikel:
                    _childWindow.Content = new Ausgeliehene_Artikel();
                    break;

                case TableNames.Kategorie:
                    _childWindow.Content = new Kategorie();
                    break;

                case TableNames.Kunde:
                    _childWindow.Content = new Kunde();
                    break;

                default:
                    _childWindow.Content = null;
                    break;
            }
        }
    }
}
