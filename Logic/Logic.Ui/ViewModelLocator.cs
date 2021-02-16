using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Videothek.Logic.Ui.ViewModel {

    /// <summary>
    /// ViewModelLocator registriert ViewModel und kann dann diese in anderen Dateien zur
    /// Verfügung stellen.
    /// </summary>
    public class ViewModelLocator {

        /// <summary>
        /// Konstruktor für ViewModelLocator. Registriert alle ViewModels.
        /// </summary>
        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HauptFensterViewModel>();
        }

        /// <summary>
        /// Methode zum zurückgeben der Instanz des Typs "MainViewModel".
        /// </summary>
        /// <returns>Instanz von MainViewModel.</returns>
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Methode zum zurückgeben der Instanz des Typs "HauptFensterViewModel".
        /// </summary>
        /// <returns>Instanz von HauptFensterViewModel.</returns>
        public HauptFensterViewModel HauptFenster =>
            ServiceLocator.Current.GetInstance<HauptFensterViewModel>();
    }
}
