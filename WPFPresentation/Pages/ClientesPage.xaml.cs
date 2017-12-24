using System;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ClientesPage.xaml
    /// </summary>
    public partial class ClientesPage : UserControl, IContent
    {
        private readonly ClientesViewModel _clientesViewModel;
        public ClientesPage()
        {
            InitializeComponent();
            _clientesViewModel = new ClientesViewModel(DependencyResolver.Instance.FacadeProvider);
            _clientesViewModel.InitializeViewContent();
            DataContext = _clientesViewModel;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
           
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
          
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
           e.Cancel =! NavigatorHelper.Instance.ChechForPermisionToNavigate(e.Source.OriginalString);
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var item = Dgclientes.SelectedCells[0].Item;
            _clientesViewModel.ShowEditClienteDialog((ClienteModel) item);
        }


    }
}
