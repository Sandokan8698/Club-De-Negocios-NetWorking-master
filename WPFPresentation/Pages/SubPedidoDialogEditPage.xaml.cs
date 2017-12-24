using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SubPedidoDialogEditPage.xaml
    /// </summary>
    public partial class SubPedidoDialogEditPage : ModernDialog
    {
        private SubPedidoEditViewModel _subPedidoEditViewModel;

        public SubPedidoDialogEditPage(VentaModel venta)
        {
            InitializeComponent();

            // define the dialog buttons
            OkButton.Content = "Aceptar";
            this.Buttons = new Button[] { this.OkButton };

            InitializeComponent();
            _subPedidoEditViewModel = new SubPedidoEditViewModel(DependencyResolver.Instance.FacadeProvider, venta);
            _subPedidoEditViewModel.InitializeViewContent();
            DataContext = _subPedidoEditViewModel;

            CommandModel addSubPedidoComman = _subPedidoEditViewModel.AddSubPedidoComman;

            ButtonAddSubPedio.Command = addSubPedidoComman.Command;
            ButtonAddSubPedio.CommandParameter = this.DataContext;
            ButtonAddSubPedio.CommandBindings.Add(new CommandBinding(addSubPedidoComman.Command, addSubPedidoComman.OnExecute, addSubPedidoComman.OnCanExecute));
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }


        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedCell = DgSubPedidos.SelectedCells[0];
            var cellInfo = selectedCell;

            _subPedidoEditViewModel.ShowSubPedidoEntryEditDialog((SubPedidoModel)cellInfo.Item);
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedCell = DgSubPedidos.SelectedCells[0];
            var cellInfo = selectedCell;

            var subPedido = (SubPedidoModel)cellInfo.Item;
            _subPedidoEditViewModel.RemoveSubPedido(subPedido);

        }


        private void SubPedidoDialogEditPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxPrecio.Focus();
        }

        private void ButtonAddSubPedio_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxPrecio.Focus();
        }

       
    }
}

