using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for TrabajadoresPage.xaml
    /// </summary>
    public partial class TrabajadoresPage : UserControl,IContent
    {
        private TrabajadoresViewModel _trabajadoresViewModel;
        public TrabajadoresPage()
        {
            InitializeComponent();
            _trabajadoresViewModel = new TrabajadoresViewModel(DependencyResolver.Instance.FacadeProvider);
            DataContext = _trabajadoresViewModel;
            _trabajadoresViewModel.InitializeViewContent();

        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
          
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
           
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
           
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            
            var selectedItem = DgTrabajadores.SelectedCells[0].Item;
            
            _trabajadoresViewModel.ShowEditTrabajadorDialog((TrabajadorModel)selectedItem);
            
        }


    }
}
