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
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    class SubPedidoEntryDialogEditViewModel: BaseViewModel
    {
        #region Property
    
        protected decimal _abono;           
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

        public SubPedidoModel SubPedido { get; set; }

        public CommandModel AddSubPedidoEntryComman { get; set; }


        #endregion

        #region Ctor

        public SubPedidoEntryDialogEditViewModel(FacadeProvider facadeProvider, SubPedidoModel subPedido) : base(facadeProvider)
        {
            SubPedido = subPedido;
            AddSubPedidoEntryComman = new AddSubPedidoEntryeCommand(this);
        }


        #endregion


        #region Methods

        public void AddSubPedidoEntry()
        {
            var subPedioEntry = new SubPedidoEntryModel
            {
                Abono = Abono,
                SubPedidoId = SubPedido.SubPedidoId,
            };

            subPedioEntry = FacadeProvider.SubPedidoEntryProvider().Add(subPedioEntry);
            SubPedido.Add(subPedioEntry);

            Abono = 0;
        }
        
        public void RemoveSubPedidoEntry(SubPedidoEntryModel subPedidoEntry)
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
                    SubPedido.Remove(subPedidoEntry);
                    FacadeProvider.SubPedidoEntryProvider().Remove(subPedidoEntry);
                }
               
            }
            else
            {
                ModernDialog.ShowMessage("No tiene permiso para realizar esta operación", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
            }
            
        }

        #endregion


        #region Command

        private class AddSubPedidoEntryeCommand : CommandModel
        {
            private SubPedidoEntryDialogEditViewModel viewModel;

            public AddSubPedidoEntryeCommand(SubPedidoEntryDialogEditViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = viewModel.Abono > 0;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.AddSubPedidoEntry();
            }
        }

        #endregion
    }
}
