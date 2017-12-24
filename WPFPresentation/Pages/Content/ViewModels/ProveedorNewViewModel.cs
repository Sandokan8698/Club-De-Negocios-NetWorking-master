using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content.ViewModels
{
    public class ProveedorNewViewModel : BaseViewModel
    {


        #region Initializers 
        public ProveedorNewViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            SaveProveedorComand = new SaveProveedorCommand(this);
            Proveedor = new ProveedorModel();
        }

        public ProveedorNewViewModel(FacadeProvider facadeProvider, ProveedorModel proveedor, Action closeAction) : base(facadeProvider)
        {
            SaveProveedorComand = new SaveProveedorCommand(this);
            UpdateProveedorComand = new UpdateProveedorCommand(this);
            Proveedor = proveedor;
            CloseAction = closeAction;
        }

        #endregion

        #region Property

        public CommandModel SaveProveedorComand { private set; get; }
        public CommandModel UpdateProveedorComand { private set; get; }

        private ProveedorModel _proveedorModel;

        public ProveedorModel Proveedor
        {
            get { return _proveedorModel; }

            set { if (_proveedorModel != value) { _proveedorModel = value; OnPropertyChanged("Proveedor"); } }
        }

        public Action CloseAction { get; private set; }


        #endregion


        #region Helpers

        public void SaveProveedor()
        {
            if (!Proveedor.IsValid())
            {
                ModernDialog.ShowMessage(Proveedor.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    var proveedor = FacadeProvider.ProveedorProvider().Add(Proveedor);
                    Messenger.Instance.NotifyColleagues(ViewModelMessages.AddNewProveedor, proveedor);
                    Proveedor = new ProveedorModel();
                    ModernDialog.ShowMessage("El proveedor ha sido agregado con exito", "Opercion Existosa", MessageBoxButton.OK);
                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.Message, "Error en la operacion", MessageBoxButton.OK);
                }
           
        }

        private void UpdateProveedor()
        {
            if (!Proveedor.IsValid())
            {
                ModernDialog.ShowMessage(Proveedor.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    Proveedor = FacadeProvider.ProveedorProvider().Update(Proveedor);
                    CloseAction();
                    ModernDialog.ShowMessage("El proveedor ha sido actualizado con exito", "Opercion Existosa", MessageBoxButton.OK);
                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.Message, "Error en la operacion", MessageBoxButton.OK);
                }
        }

        #endregion

        #region Command Region

        private class SaveProveedorCommand : CommandModel
        {
            private ProveedorNewViewModel _newViewModel;

            public SaveProveedorCommand(ProveedorNewViewModel newViewModel)
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
               _newViewModel.SaveProveedor();
            }

        }

        private class UpdateProveedorCommand : CommandModel
        {
            private ProveedorNewViewModel _newViewModel;

            public UpdateProveedorCommand(ProveedorNewViewModel newViewModel)
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
               _newViewModel.UpdateProveedor();
            }

        }
    }

    #endregion
}

