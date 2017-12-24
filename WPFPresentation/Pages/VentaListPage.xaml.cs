using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using WPFPresentation.Converters;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for VentaListPage.xaml
    /// </summary>
    public partial class VentaListPage : UserControl
    {
        private VentaListViewModel _ventaListViewModel;

        public VentaListPage()
        {
            InitializeComponent();
            
            _ventaListViewModel = new VentaListViewModel(DependencyResolver.Instance.FacadeProvider);

            PaginatorControlVentas.Paginator.ItemsPerPage = 50;
            PaginatorControlVentas.Paginator.SetPaginable(_ventaListViewModel);

            _ventaListViewModel.InitializeViewContent();
            FilterControl.FilterControlViewModel.MensageToSendWhenApplay = ViewModelMessages.ApplayFilterInVentaList;
            FilterControl.FilterControlViewModel.MensageToSendWhenRemove = ViewModelMessages.RemoveFilterInVentalist;

            FilterControl.IdentificadorFilter.Visibility = Visibility.Collapsed;
            FilterControl.EmpresaFilter.Visibility = Visibility.Collapsed;

            this.DataContext = _ventaListViewModel;
        }

       
      
        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DgVentas.SelectedCells[0].Item;
            _ventaListViewModel.RemoveVenta((VentaModel)item);
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DgVentas.SelectedCells[0].Item;
            _ventaListViewModel.ShowVentaEditDialog((VentaModel)item);
        }

        private void ButtonVentaDetail_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DgVentas.SelectedCells[0].Item;
            _ventaListViewModel.ShowVentaEditDialog((VentaModel)item);
        }
    }
}
