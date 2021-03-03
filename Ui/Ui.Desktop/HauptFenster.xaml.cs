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
        private ChildWindow _childWindow;
        private bool _isChildWindowOpen = false;

        /// <summary>
        /// Wenn der Nutzer eine Tabelle ausgewählt hat und auf "Hinzufügen" drückt, soll
        /// das passende Fenster geladen werden.
        /// </summary>
        public HauptFenster() {
            InitializeComponent();
            Messenger
                .Default
                .Register(this, (OpenChildWindowMessage message) => {
                    if (_childWindow == null && !_isChildWindowOpen) {
                        _childWindow = new ChildWindow();
                        _childWindow.Closed += (sender, args) => {
                            Cleanup();
                        };

                        SetChildWindowToMatchingUserControl(message.Notification);

                        _childWindow.Show();
                        _isChildWindowOpen = true;
                    }
                });

            Messenger
               .Default
               .Register(this, (AddResultMessage m) => {
                   if (m.Success) {
                       _childWindow.Close();
                       Cleanup();
                   } else {
                       MessageBox.Show("Es ist ein Fehler aufgetretreten. :/");
                   }
               });
        }

        private void Cleanup() {
            _isChildWindowOpen = false;
            _childWindow = null;
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
