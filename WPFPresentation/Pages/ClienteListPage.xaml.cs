using System.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ClienteListPage.xaml
    /// </summary>
    public partial class ClienteListPage : UserControl
    {
        private readonly ClientesViewModel _clientesViewModel;

        public ClienteListPage()
        {
            InitializeComponent();
            _clientesViewModel = new ClientesViewModel(DependencyResolver.Instance.FacadeProvider);
            LoadContent();

            CommandModel editClienteDialogComand = _clientesViewModel.ShowEditClienteDialogComand;

            //ButtonEditCliente.Command = editClienteDialogComand.Command;
            //ButtonEditCliente.CommandParameter = this.DataContext;
            //ButtonEditCliente.CommandBindings.Add(new CommandBinding(editClienteDialogComand.Command, editClienteDialogComand.OnExecute, editClienteDialogComand.OnCanExecute));

            ////El usuario no puede cambiar el cliente directamente en este filtro
            ////pero como el AutoComplete de los clientes es un poco complicado
            ////los remplazo con un textbox normal y escondo el autocomplete real
            ////y muestro el dumy textbox solo para que muestre el nombre del usuario
            //FilterControlClienteLis.ClienteAutoComplete.Visibility = Visibility.Collapsed;
            //FilterControlClienteLis.FakerAutoComplete.Visibility = Visibility.Visible;
            //FilterControlClienteLis.FakerAutoComplete.IsEnabled = false;


            //FilterControlClienteLis.FilterControlViewModel.MensageToSendWhenApplay =
            //    ViewModelMessages.ApplayFilterInClienteVenta;
            //FilterControlClienteLis.FilterControlViewModel.MensageToSendWhenRemove =
            //    ViewModelMessages.RemoveFilterInClienteVenta;

            //Escuchamos por el mensage que envia el filtercontrol para inahbilitar el listbox de cliente
            //y asi no se puede seleccionar mas ninguno esto al final lo voy a tener que cambiar
            Messenger.Instance.Register(DisableList, ViewModelMessages.ApplayFilterInClienteVenta);

            //Similar a lo anterior pero para removel los filtros
            Messenger.Instance.Register(EnableList, ViewModelMessages.RemoveFilterInClienteVenta);
        }

        private void LoadContent()
        {

            _clientesViewModel.InitializeViewContent();

            DataContext = _clientesViewModel;
            //ListCliente.ItemsSource = _clientesViewModel.Clientes;
            //ListCliente.SelectedItem = _clientesViewModel.Clientes[0];



        }

        private void ListCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedItem = listBox.SelectedItem;

            _clientesViewModel.CurrentCliente = (ClienteModel)selectedItem;
        }

        private void EnableList(object param)
        {
            //ListCliente.IsEnabled = true;
        }

        private void DisableList(object param)
        {
            //ListCliente.IsEnabled = false;
        }

      
       
    }
}
