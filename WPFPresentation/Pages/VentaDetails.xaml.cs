
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for VentaDetails.xaml
    /// </summary>
    public partial class VentaDetails : UserControl
    {
        private VentaDetailViewModel _ventaDetailViewModel;

        public VentaDetails(VentaDetailViewModel ventaDetailViewModel)
        {
            InitializeComponent();

            _ventaDetailViewModel = ventaDetailViewModel;
            DataContext = _ventaDetailViewModel;


            CommandModel addPedidoComand = _ventaDetailViewModel.AddPedidoComand;

            buttonSave.Command = addPedidoComand.Command;
            buttonSave.CommandBindings.Add(new CommandBinding(addPedidoComand.Command, addPedidoComand.OnExecute, addPedidoComand.OnCanExecute));

            
            CommandModel dailyReportPaymentomman = _ventaDetailViewModel.GetDailyReportPaymentomman;
            ButtonPrintSubReport.Command = dailyReportPaymentomman.Command;
            ButtonPrintSubReport.CommandBindings.Add(new CommandBinding(dailyReportPaymentomman.Command, dailyReportPaymentomman.OnExecute, dailyReportPaymentomman.OnCanExecute));


        }



        private void ButtonPrintVenta_OnClick(object sender, RoutedEventArgs e)
        {
            _ventaDetailViewModel.ShowVentPrintDialog();
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {

            var selectedCell = DgPedidos.SelectedCells[0];
            var cellInfo = selectedCell;

            var pedido = (PedidoModel)cellInfo.Item;
            _ventaDetailViewModel.RemovePedido(pedido);

        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedCell = DgPedidos.SelectedCells[0];
            var cellInfo = selectedCell;

            _ventaDetailViewModel.ShowSubPedidoEditDialog((PedidoModel)cellInfo.Item);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            AutoCompleteBoxEmpresa.Focus();
        }
    }
}
