using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class VentaDetailViewModel: BaseVentaViewModel
    {
        #region Property

        public CommandModel UpdateVentaComand { get; private set; }
        public CommandModel GetDailyReportPaymentomman { get; private set; }

        #endregion

        #region Ctor

        public VentaDetailViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            UpdateVentaComand = new UpdateVentaCommand(this);
            GetDailyReportPaymentomman = new GetDailyReportPaymentommand(this);
        }

        #endregion

        #region Helpers

        private void UpdateVenta()
        {
            FacadeProvider.VentaProvider().Update(Venta);
        }

        private void GetDailyReportPayment()
        {
           var subPedidoEntryModels = FacadeProvider.SubPedidoEntryProvider().GetDailyReportPayment(Venta);

            DailyPaymentPrintPage dailyPaymentPrint = new DailyPaymentPrintPage(Venta,subPedidoEntryModels.ToList());
            dailyPaymentPrint.ShowDialog();
        }

        public override void AddPedido()
        {
            try
            {
                if (!ProveedorExist(Proveedor.ProveedorId))
                {
                    base.AddPedido();

                SubPedidoToAdd.Add(SubPedidoEntryToAdd);

                PedidoToAdd.Add(SubPedidoToAdd);

                PedidoToAdd.ItemNumero = GetItemNumero(Venta.Pedidos);

                PedidoToAdd = FacadeProvider.PedidoProvider().Add(PedidoToAdd);

                PedidoToAdd.Observers.Clear();

                Venta.Pedidos.Add(PedidoToAdd);
               
                InitPedidoToAddObjects();

                }
                else
                {
                    var dlg = new ModernDialog
                    {
                        Title = "Aviso",
                        Content = "El proveedor ya fue agregado",
                        MinHeight = 100
                    };
                    dlg.Buttons = new Button[] { dlg.OkButton };
                    dlg.ShowDialog();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public override void RemovePedido(object elemento)
        {
            if (Authenticator.Instance.IsAdming)
            {
                var dlg = new ModernDialog
                {
                    Title = "Aviso",
                    Content = "Este elemento se eliminara permanente, desea continuar ?"
                };
                dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
                dlg.ShowDialog();

                if (dlg.MessageBoxResult == MessageBoxResult.OK)
                {
                    var pedido = (PedidoModel) elemento;
                    Venta.Remove(pedido);
                    FacadeProvider.PedidoProvider().Remove(pedido);

                    //Actualizamos los numeros de los pedidos 
                    ActualizeItemNumero(Venta.Pedidos);

                    //Lo guardamos en la base de datos para que el cliente no 
                    //tenga que recordar guardarlos
                    Venta.Pedidos.ToList().ForEach(p => FacadeProvider.PedidoProvider().Update(p));
                }
            }
            else
            {
                ModernDialog.ShowMessage("No tiene permiso para realizar esta operación", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
            }
        }

        public void ShowSubPedidoEditDialog(PedidoModel pedido)
        {
            Venta.SelectedChildren = pedido;
            SubPedidoDialogEditPage subPedidoEditPage = new SubPedidoDialogEditPage(Venta);
            subPedidoEditPage.ShowDialog();

        }

        public void ShowVentPrintDialog()
        {
            VentaPrintPage ventaPrintPage = new VentaPrintPage();
            ventaPrintPage.VentaModels = Venta;
            ventaPrintPage.ShowDialog();
        }

        #endregion

        #region Command

        private class UpdateVentaCommand : CommandModel
        {
            private VentaDetailViewModel _viewModel;

            public UpdateVentaCommand(VentaDetailViewModel viewModel)
            {
                this._viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
               
                e.CanExecute = e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _viewModel.UpdateVenta();
            }
        }

        private class GetDailyReportPaymentommand : CommandModel
        {
            private VentaDetailViewModel _viewModel;

            public GetDailyReportPaymentommand(VentaDetailViewModel viewModel)
            {
                this._viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
               
                e.CanExecute = e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _viewModel.GetDailyReportPayment();
            }
        }

        #endregion
    }
}
