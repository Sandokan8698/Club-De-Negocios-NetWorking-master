using System;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content.ViewModels
{
    public class ClienteNewControlViewModel : BaseViewModel

    {

        #region Initializers 

        public ClienteNewControlViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            SaveClienteComand = new SaveClienteCommand(this);
            Cliente = new ClienteModel();
        }

        public ClienteNewControlViewModel(FacadeProvider facadeProvider, ClienteModel cliente, Action closAction):base(facadeProvider)
        {
            SaveClienteComand = new SaveClienteCommand(this);
            UpdateClienteComand = new UpdateClienteCommand(this);
            Cliente = cliente;
            CloseAction = closAction;
        }

       
        public override void UnloadViewContent()
        {

        }
        #endregion


        #region Property

        public CommandModel SaveClienteComand { private set; get; }
        public CommandModel UpdateClienteComand { private set; get; }

        private ClienteModel _clienteModel;

        public ClienteModel Cliente
        {
            get { return _clienteModel; }

            set { if (_clienteModel != value) { _clienteModel = value; OnPropertyChanged("Cliente"); } }
        }

        public Action CloseAction { get; set; }
        #endregion


        #region Helpers

        public void SaveCliente()
        {
            if (!Cliente.IsValid())
            {
                ModernDialog.ShowMessage(Cliente.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    var cliente = FacadeProvider.ClienteProvider().Add(Cliente);
                    Cliente = new ClienteModel();
                    Messenger.Instance.NotifyColleagues(ViewModelMessages.AddNewCliente, cliente);

                    ModernDialog.ShowMessage("El Cliente ha sido agregado con exito", "Opercion Existosa", MessageBoxButton.OK);
                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.Message, "Error en la operacion", MessageBoxButton.OK);
                }
           
        }


        public void UpdateCliente()
        {
            if (!Cliente.IsValid())
            {
                ModernDialog.ShowMessage(Cliente.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    Cliente = FacadeProvider.ClienteProvider().Update(Cliente);
                    ModernDialog.ShowMessage("El Cliente ha sido actualizado con exito", "Opercion Existosa", MessageBoxButton.OK);
                    CloseAction();
                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.Message, "Error en la operacion", MessageBoxButton.OK);
                }
        }

        #endregion

        #region Command Region

        private class SaveClienteCommand : CommandModel
        {
            private ClienteNewControlViewModel _newControlViewModel;

            public SaveClienteCommand(ClienteNewControlViewModel newControlViewModel)
            {
                this._newControlViewModel = newControlViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _newControlViewModel.SaveCliente();
            }

        }


        private class UpdateClienteCommand : CommandModel
        {
            private ClienteNewControlViewModel _newControlViewModel;

            public UpdateClienteCommand(ClienteNewControlViewModel newControlViewModel)
            {
                this._newControlViewModel = newControlViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _newControlViewModel.UpdateCliente();
            }

        }
    }

    #endregion
}

