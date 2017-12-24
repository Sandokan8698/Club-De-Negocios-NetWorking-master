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
    /// Interaction logic for SubPedidoEntryDialogEditPage.xaml
    /// </summary>
    public partial class SubPedidoEntryDialogEditPage : ModernDialog
    {
        private SubPedidoEntryDialogEditViewModel _subPedidoEntryDialogEditViewModel;
        public SubPedidoEntryDialogEditPage(SubPedidoModel subPedido)
        {
            InitializeComponent();

            // define the dialog buttons
            OkButton.Content = "Aceptar";
            this.Buttons = new Button[] { this.OkButton };

            InitializeComponent();
            _subPedidoEntryDialogEditViewModel = new SubPedidoEntryDialogEditViewModel(DependencyResolver.Instance.FacadeProvider, subPedido);
            DataContext = _subPedidoEntryDialogEditViewModel;

            CommandModel addSubPedidoComman = _subPedidoEntryDialogEditViewModel.AddSubPedidoEntryComman;

            ButtonAddSubPedioEntry.Command = addSubPedidoComman.Command;
            ButtonAddSubPedioEntry.CommandParameter = this.DataContext;
            ButtonAddSubPedioEntry.CommandBindings.Add(new CommandBinding(addSubPedidoComman.Command, addSubPedidoComman.OnExecute, addSubPedidoComman.OnCanExecute));
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedCell = DgSubPedidosEntrys.SelectedCells[0];
            var cellInfo = selectedCell;
            
            _subPedidoEntryDialogEditViewModel.RemoveSubPedidoEntry((SubPedidoEntryModel)cellInfo.Item);
        }


        private void SubPedidoEntryDialogEditPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxAbono.Focus();
        }

        private void ButtonAddSubPedioEntry_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxAbono.Focus();
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }
    }
}
