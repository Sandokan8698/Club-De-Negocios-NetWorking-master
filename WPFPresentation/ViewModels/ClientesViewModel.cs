using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {
        #region Property

        public CommandModel ShowEditClienteDialogComand { private set; get; }

        private ObservableCollection<ClienteModel> _clientes;
        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _clientes; }
            set
            {
                if (_clientes != value)
                {
                    _clientes = value; OnPropertyChanged("Clientes");
                    
                }
            }
        }

        private ClienteModel _currentCliente;
        public ClienteModel CurrentCliente
        {
            get { return _currentCliente; }
            set
            {
                if (_currentCliente != value)
                {
                    _currentCliente = value;

                    OnPropertyChanged("CurrentCliente");


                    List<Object> paramList = new List<Object>(){this,value};
                   
                    //Enviamos el mensage a la ClienteVentasViewmodel en este caso
                    //para que actualize los datos del  cliente que esta mostrando
                    Messenger.Instance.NotifyColleagues(ViewModelMessages.ClienteItemsChange, paramList);

                    //Enviamos un mensage al FilterControl que esta en la pagina donde se inicia 
                    //este vieModel en principio (Mira a ver si no cambia despues) ClienteLisPage
                    //para que actualize el estado de su propiedad cliente a la del CurrentCliente de este newControlViewModel
                    Messenger.Instance.NotifyColleagues(ViewModelMessages.InitializerFilterValue, new FilterModel{Cliente = value, Desde = DateTime.Now.AddMonths(-1), Hasta = DateTime.Now });

                    
                }
            }
        }

        #endregion

        #region Ctor

        public ClientesViewModel(IFacadeProvider facadeProvider) : base(facadeProvider)
        {
            ShowEditClienteDialogComand = new ShowEditClienteDialogCommand(this);
            Messenger.Instance.Register(o => AddCliente(o), ViewModelMessages.AddNewCliente);
        }

        #endregion


        #region Methods

        public override void InitializeViewContent()
        {
            Clientes = FacadeProvider.ClienteProvider().GetAll();
        }

        public void ShowEditClienteDialog(ClienteModel cliente)
        {
            ClienteEditDialogPage clienteEditDialog = new ClienteEditDialogPage(cliente);
            clienteEditDialog.ShowDialog();
        }

        private void AddCliente(object o)
        {
            Clientes.Add((ClienteModel)o);
        }

        #endregion


        #region Command Region

        private class ShowEditClienteDialogCommand : CommandModel
        {
            private ClientesViewModel viewModel;

            public ShowEditClienteDialogCommand(ClientesViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = viewModel.CurrentCliente != null;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
               ClienteEditDialogPage clienteEditDialogPage = new ClienteEditDialogPage(viewModel.CurrentCliente);

               clienteEditDialogPage.ShowDialog();
            }

        }
    }

    #endregion
}
