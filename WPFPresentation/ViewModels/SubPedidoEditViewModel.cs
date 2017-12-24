using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class SubPedidoEditViewModel: BaseViewModel
    {
        #region Property

        private VentaModel _venta;
        protected decimal _abono;
        protected decimal _deuda;
        protected decimal _precioProveedor;

       
        public decimal Abono
        {
            get { return _abono; }
            set
            {
                if (_abono != value)
                {
                    _abono = value; OnPropertyChanged("Abono");
                  
                  
                }
            }
        }
        public decimal PrecioProveedor
        {
            get { return _precioProveedor; }
            set
            {
                if (_precioProveedor != value)
                {
                    _precioProveedor = value;
                    OnPropertyChanged("PrecioProveedor");
                 
                }
            }
        }
        public PedidoModel Pedido { get; set; }
        public IEnumerable<VentaModel> Ventas { get; set; }
        public CommandModel AddSubPedidoComman { get; set; }

        private string _identificador;
        public string Identificador {
            get { return _identificador; }
            set{ if (_identificador != value) _identificador = value; OnPropertyChanged("Identificador"); }
            
        }

        
        #endregion

        #region Ctor
        public SubPedidoEditViewModel(FacadeProvider facadeProvider, VentaModel venta) : base(facadeProvider)
        {
            _venta = venta;
            Pedido = venta.SelectedChildren;
         
            AddSubPedidoComman = new AddSubPedidoCommand(this);
            
        }

        public override void InitializeViewContent()
        {
            Identificador = UniqueKeyGenerator.GetUniqueKey();
        }

        #endregion

        #region Helpers


        public void AddSubPedido()
        {
            var subPedioEntry = new SubPedidoEntryModel
            {             
                Abono = Abono,              
            };

            var subPedido = new SubPedidoModel
            {
                PedidoId = Pedido.PedidoId,
                FechaCreacion = DateTime.Now,
                Identificador = Identificador,
                PrecioProveedor = PrecioProveedor,
            };

            subPedido.Add(subPedioEntry);
            
            subPedido =  FacadeProvider.SubPedidoProvider().Add(subPedido);

            //El subpedido cuando es creado y devuelto por automapper se agrega a la lista
            //del pedido al cual pertenece pero del que viene por la base de datos y no al que
            //se esta mostrando, por eso recive los oyennte de este pedido creado por automapper
            //por eso aqui se los quitamos y lo agregamos a su pedido
            subPedido.Observers.Clear();

            Pedido.Add(subPedido);

      
            PrecioProveedor = 0;
            Abono = 0;
            Identificador = UniqueKeyGenerator.GetUniqueKey();
            
        }

        public void RemoveSubPedido(SubPedidoModel subPedido)
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
                    Pedido.Remove(subPedido);
                    FacadeProvider.SubPedidoProvider().Remove(subPedido);
                }
            }
            else
            {
                ModernDialog.ShowMessage("No tiene permiso para realizar esta operación", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
            }
        }

        public void ShowSubPedidoEntryEditDialog(SubPedidoModel subPedidoModel)
        {
            SubPedidoEntryDialogEditPage subPedidoEntryDialog = new SubPedidoEntryDialogEditPage(subPedidoModel);
            subPedidoEntryDialog.ShowDialog();
        }

        #endregion

        #region Command

        private class AddSubPedidoCommand : CommandModel
        {
            private SubPedidoEditViewModel viewModel;

            public AddSubPedidoCommand(SubPedidoEditViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
               e.CanExecute = viewModel.PrecioProveedor > 0 & !string.IsNullOrEmpty(viewModel.Identificador);
               e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.AddSubPedido();
            }
        }

        #endregion

    }
}
