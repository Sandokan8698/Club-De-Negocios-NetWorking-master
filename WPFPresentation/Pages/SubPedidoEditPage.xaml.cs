using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;


namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SubPedidoEditPage.xaml
    /// </summary>
    public partial class SubPedidoEditPage : ModernWindow
    {
        private SubPedidoEditViewModel _subPedidoEditViewModel; 

        public SubPedidoEditPage(VentaModel venta)
        {
            InitializeComponent();
            _subPedidoEditViewModel = new SubPedidoEditViewModel(DependencyResolver.Instance.FacadeProvider,venta);
            DataContext = _subPedidoEditViewModel;

            CommandModel addSubPedidoComman = _subPedidoEditViewModel.AddSubPedidoComman;

            ButtonAddSubPedio.Command = addSubPedidoComman.Command;
            ButtonAddSubPedio.CommandParameter = this.DataContext;
            ButtonAddSubPedio.CommandBindings.Add(new CommandBinding(addSubPedidoComman.Command, addSubPedidoComman.OnExecute, addSubPedidoComman.OnCanExecute));
        }

       
        private void DgSubPedidos_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dg = (DataGrid)sender;


            if (dg.SelectedCells.Count > 0)
            {
                var selectedCell = dg.SelectedCells[0];
                var cellInfo = selectedCell;

                if (selectedCell.Column.Header == null)
                {
                    var subPedido = (SubPedidoModel)cellInfo.Item;
                    _subPedidoEditViewModel.RemoveSubPedido(subPedido);
                }
                else
                {
                    SubPedidoEntryDialogEditPage subPedidoEntryDialog = new SubPedidoEntryDialogEditPage();
                    subPedidoEntryDialog.ShowDialog();
                }

            }
        }

        private void SubPedidoEditPage_OnClosing(object sender, CancelEventArgs e)
        {
            if (_subPedidoEditViewModel.CheckForChanges())
            {             
                var dlg = new ModernDialog
                {
                    Title = "Información",
                    Content = "Los valores de esta orden cambiaron quiere guardar los datos"
                };
                dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
                dlg.ShowDialog();

                if (dlg.DialogResult == false)
                {
                    _subPedidoEditViewModel.RestoreValues();
                }
                else
                {
                    _subPedidoEditViewModel.UpdateSubPedidos();
                }
            }
        }
    }
}
