using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Videothek.Logic.Ui.ViewModel {

    public class ViewModelLocator {

        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HauptFensterViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public HauptFensterViewModel HauptFenster =>
            ServiceLocator.Current.GetInstance<HauptFensterViewModel>();
    }
}
