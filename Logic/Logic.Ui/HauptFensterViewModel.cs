using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Data;
using System.Data.SqlClient;

namespace Videothek.Logic.Ui.ViewModel
{
    public class HauptFensterViewModel : ViewModelBase
    {
        private SqlConnection conn = new SqlConnection();

        private DataView selectedData;

        public DataView SelectedData
        {
            get => selectedData;
            set
            {
                selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        private ICommand _onTableSelect;

        public ICommand OnTableSelect
        {
            get
            {
                if (_onTableSelect == null)
                {
                    _onTableSelect = new RelayCommand<string>((table) =>
                    {
                        SelectedData = GetDataViewOf(table);
                    });
                }

                return _onTableSelect;
            }
        }

        public HauptFensterViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        private DataView GetDataViewOf(string table)
        {
            DataTable dt = new DataTable();
            conn.ConnectionString = "Data Source=W011076SYS\\SQLEXPRESS;" +
                                    "Initial Catalog=Bibliothek;" +
                                    "Integrated Security=SSPI;";
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                conn.Open();
                adapter.SelectCommand = new SqlCommand("SELECT * FROM " + table, conn);
                adapter.Fill(dt);
                return dt.DefaultView;
            }
            catch (System.Exception e)
            {
                conn.Close();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}