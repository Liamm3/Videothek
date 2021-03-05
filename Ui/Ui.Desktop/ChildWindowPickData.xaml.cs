using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows;
using Videothek.Logic.Ui.Messages;

namespace Videothek.Ui.Desktop {

    public partial class ChildWindowPickData : Window {

        public ChildWindowPickData(List<dynamic> list) {
            InitializeComponent();
            PickDataController.Content = new PickData(list);
        }
    }
}
