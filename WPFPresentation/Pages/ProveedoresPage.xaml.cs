
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ProveedoresPage.xaml
    /// </summary>
    public partial class ProveedoresPage : UserControl
    {
        private ProveedoresViewModel _proveedoresViewModel;
        public ProveedoresPage()
        {
            InitializeComponent();

            _proveedoresViewModel = new ProveedoresViewModel(DependencyResolver.Instance.FacadeProvider);
            _proveedoresViewModel.InitializeViewContent();
            DataContext = _proveedoresViewModel;
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DgProveedores.SelectedCells[0].Item;
            _proveedoresViewModel.ShowEditProveedorDialog((ProveedorModel)item);
        }
    }
}
