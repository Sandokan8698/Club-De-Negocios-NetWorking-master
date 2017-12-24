
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Domain_Layer.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class VentaNewViewModel: BaseVentaViewModel
    {

        #region Property

        public CommandModel AddNewVentaComman { get; private set; }
        public CommandModel SaveVentaComand { get; private set; }

      
        public ObservableCollection<ClienteModel> Clientes  
        {
            get { return InMemoryHelper.Instance.Clientes; }      
        }

        #endregion

        public VentaNewViewModel(FacadeProvider facadeProvider):base(facadeProvider)
        {
            AddNewVentaComman = new AddNewVentaCommand(this);
            SaveVentaComand = new SaveVentaCommand(this);
            
            //Esto es para cuando se seleccione al cliente en el autocomplete
            //se ponga su inforacion acerca de si tiene deuda o no
            Venta.Attach("Cliente", s => SetClientByDocument(s));

        }

        #region Implementetion Command

        // implementation of the AddPedidoCommand
        private class AddNewVentaCommand : CommandModel
        {
            private VentaNewViewModel _newViewModel;

            public AddNewVentaCommand(VentaNewViewModel newViewModel)
            {
                this._newViewModel = newViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _newViewModel.AddNewVenta();
            }
        }

        private class SaveVentaCommand : CommandModel
        {
            private VentaNewViewModel _newViewModel;

            public SaveVentaCommand(VentaNewViewModel newViewModel)
            {
                this._newViewModel = newViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = _newViewModel.Venta.Pedidos.Count > 0;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
               _newViewModel.SaveVenta();
            }
        }
        #endregion

        #region Helpers

        private async void SaveVenta()
        {
            var venta =  FacadeProvider.VentaProvider().Add(Venta);

            InitPedidoToAddObjects();

            Venta = new VentaModel();
            Venta.Cliente = new ClienteModel();
            DeudaCliente = null;
            Venta.Attach("Cliente", s => SetClientByDocument(s));

            var ventaPrintPage = await Task.Factory.StartNew(() =>
            {
              return new VentaPrintPage();

            }, CancellationToken.None
                , TaskCreationOptions.None
                , TaskScheduler.FromCurrentSynchronizationContext());
             
            ventaPrintPage.VentaModels = venta;
            ventaPrintPage.ShowDialog();    

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

                    Venta.Add(PedidoToAdd);

                    ActualizeItemNumero(Venta.Pedidos);

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
                MessageBox.Show(e.Message);
            }
        }

        public override void RemovePedido(object elemento)
        {
            Venta.Remove((PedidoModel) elemento);
            ActualizeItemNumero(Venta.Pedidos);
        }

        public void AddNewVenta()
        {
            Venta = new VentaModel();
            Venta.Cliente = new ClienteModel();
            DeudaCliente = null;
            Venta.Attach("Cliente", s => SetClientByDocument(s));

            InitPedidoToAddObjects();
        }

        public void ShowClienteDeudaDialog()
        {
            if (DeudaCliente != null)
            {
                var DeudaVenta = FacadeProvider.VentaProvider().Get(DeudaCliente.VentaId);
                VentaDetailDialogPage ventaDetailViewModel = new VentaDetailDialogPage(DeudaVenta);
                ventaDetailViewModel.ShowDialog();
            }
           
        }

       
        /// <summary>
        /// Metodo que actualiza el cliente de esta venta lo hago asi
        /// para que los modelos permanescan libre del acceso a datos no se si esta bien
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async void SetClientByDocument(object cliente)
        {
            
            if (Venta.Cliente != null)
            {
                Venta.ClienteId = Venta.Cliente.ClienteId;
                SetClientDeuda(Venta.ClienteId);
            }
            else
            {
                DeudaCliente = null;
            }

            IsLoading = false;

        }

        public void SetClientDeuda(int id)
        {
            var cliente = FacadeProvider.ClienteProvider().Get(id);

            if (cliente != null)
            {
                var venta = FacadeProvider.VentaProvider().GetVentaWithDeuda(id);

                if (venta != null)
                {
                    DeudaCliente = new DeudaModel { Deuda = venta.Deuda, VentaId = venta.VentaId };
                }
                else
                {
                    DeudaCliente = null;
                }
            }
            else
            {
                //para que se borre la deuda del cliente que ya se habia buscado
                DeudaCliente = null;
            }


        }
        #endregion

    }
}
