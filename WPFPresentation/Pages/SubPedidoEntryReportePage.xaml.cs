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
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SubPedidoEntryReport.xaml
    /// </summary>
    public partial class SubPedidoEntryReportePage : UserControl
    {
        private SubPedidoEntryReporteViewModel _subPedidoEntryReporteViewModel;
        public SubPedidoEntryReportePage()
        {
            InitializeComponent();
            _subPedidoEntryReporteViewModel = new SubPedidoEntryReporteViewModel(DependencyResolver.Instance.FacadeProvider);
            DataContext = _subPedidoEntryReporteViewModel;

            PaginatorControlSubPedido.Paginator.ItemsPerPage = 50;
            PaginatorControlSubPedido.Paginator.SetPaginable(_subPedidoEntryReporteViewModel);

            _subPedidoEntryReporteViewModel.InitializeViewContent();


            FilterControlSubPedido.FilterControlViewModel.MensageToSendWhenApplay = ViewModelMessages.ApplayFilterInSubPedidoEntryReport;
            FilterControlSubPedido.FilterControlViewModel.MensageToSendWhenRemove = ViewModelMessages.ApplayFilterInSubPedidoEntryReport;


        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var item = DgSubPedidos.SelectedCells[0].Item;
            
            object v = item?.GetType().GetProperty("VentaId")?.GetValue(item, null);
            _subPedidoEntryReporteViewModel.ShowVentaDialog((int)v);

        }
    }
}
