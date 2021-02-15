using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Videothek.Ui.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger
                .Default
                .Register<NotificationMessage>(this, (NotificationMessage message) =>
                {
                    if (message.Notification.Equals("OnStartKlick"))
                    {
                        MainControl.Content = new HauptFenster();
                    }
                });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) =>
            MainControl.Content = new StartSeite();
    }
}
