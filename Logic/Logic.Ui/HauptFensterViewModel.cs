using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel {

    public class HauptFensterViewModel : ViewModelBase {

        public DataView SelectedData {
            get => _selectedData;
            private set {
                _selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        public ICommand OnTableSelect {
            get {
                if (_onTableSelect == null) {
                    _onTableSelect = new RelayCommand<string>(table =>
                        SelectedData = GetDataViewOf(table)
                    );
                }

                return _onTableSelect;
            }
        }

        private readonly SqlConnection conn = new SqlConnection() {
            ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;" +
                               "Initial Catalog=Bibliothek;" +
                               "Integrated Security=SSPI;"
        };

        private DataView _selectedData;

        private ICommand _onTableSelect;

        private DataView GetDataViewOf(string table) {
            var dt = new DataTable();
            var adapter = new SqlDataAdapter();

            try {
                conn.Open();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {table}", conn);
                adapter.Fill(dt);
                return dt.DefaultView;
            } catch (System.Exception e) {
                conn.Close();
                throw e;
            } finally {
                conn.Close();
            }
        }
    }
}
