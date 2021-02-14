using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using static Videothek.Logic.Ui.ViewModel.MainViewModel;

namespace Videothek.Ui.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger
                .Default
                .Register<PropertyChangedMessage<bool>>(this, (PropertyChangedMessage<bool> e) =>
                {
                    if (e.PropertyName.Equals("OnStartKlick") && e.NewValue)
                    {
                        MainControl.Content = new HauptFenster();
                    }
                });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainControl.Content = new StartSeite();
        }
    }
}
