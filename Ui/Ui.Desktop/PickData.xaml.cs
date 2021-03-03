using System.Collections.Generic;
using System.Windows.Controls;

namespace Videothek.Ui.Desktop {

    public partial class PickData : UserControl {

        public PickData(List<dynamic> data) {
            InitializeComponent();
            Grid.ItemsSource = data;
        }
    }
}
