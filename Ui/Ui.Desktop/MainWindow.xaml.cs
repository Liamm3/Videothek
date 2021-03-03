using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Videothek.Ui.Desktop {

    /// <summary>
    /// Das Hauptfenster für die Anwendung. Mit ContenControl wird der passende Inhalt angezeigt.
    /// </summary>
    public partial class MainWindow : Window {

        /// <summary>
        /// Konstruktor für das MainWindow.
        /// </summary>
        public MainWindow() {
            InitializeComponent();

            /**
             * Zeige Hauptfenster.xaml, wenn eine NotifactionMessage mit dem String "OnStartKlick"
             * empfangen wird.
             */
            Messenger
                .Default
                .Register(this, (NotificationMessage message) => {
                    if (message.Notification != null && message.Notification.Equals("OnStartKlick")) {
                        MainControl.Content = new HauptFenster();
                    }
                });
        }

        /// <summary>
        /// Wenn das MainWindow geladen wird, soll die StartSeite angezeigt werden.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e) =>
            MainControl.Content = new StartSeite();
    }
}
