
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for VentaNewPage.xaml
    /// </summary>
    public partial class VentaNewPage : UserControl
    {
        private VentaNewViewModel _baseBaseVentaNewViewModel;


        public VentaNewPage()
        {


            InitializeComponent();

            _baseBaseVentaNewViewModel = new VentaNewViewModel(DependencyResolver.Instance.FacadeProvider);
            _baseBaseVentaNewViewModel.InitializeViewContent();

            //Bind the DataGrid to the customer data
            this.DataContext = _baseBaseVentaNewViewModel;

            CommandModel addPedidoComand = _baseBaseVentaNewViewModel.AddPedidoComand;

            buttonSave.Command = addPedidoComand.Command;
            buttonSave.CommandParameter = this.DataContext;
            buttonSave.CommandBindings.Add(new CommandBinding(addPedidoComand.Command, addPedidoComand.OnExecute, addPedidoComand.OnCanExecute));

            CommandModel saveVentaComand = _baseBaseVentaNewViewModel.SaveVentaComand;

            ButtonSaveVenta.Command = saveVentaComand.Command;
            ButtonSaveVenta.CommandParameter = this.DataContext;
            ButtonSaveVenta.CommandBindings.Add(new CommandBinding(saveVentaComand.Command, saveVentaComand.OnExecute, saveVentaComand.OnCanExecute));

            CommandModel addNewVentaCommand = _baseBaseVentaNewViewModel.AddNewVentaComman;

            ButtonNewVenta.Command = addNewVentaCommand.Command;
            ButtonNewVenta.CommandParameter = this.DataContext;
            ButtonNewVenta.CommandBindings.Add(new CommandBinding(addNewVentaCommand.Command, addNewVentaCommand.OnExecute, addNewVentaCommand.OnCanExecute));

        }


        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }
       

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _baseBaseVentaNewViewModel.ShowClienteDeudaDialog();
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedCell = DgPedidos.SelectedCells[0];

            var cellInfo = selectedCell;
            var pedido = (PedidoModel)cellInfo.Item;
            _baseBaseVentaNewViewModel.RemovePedido(pedido);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            AutoCompleteBoxEmpresa.Focus();
        }
    }
}
