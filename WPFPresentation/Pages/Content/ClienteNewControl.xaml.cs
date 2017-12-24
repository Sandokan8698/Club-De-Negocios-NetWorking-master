using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content
{
    /// <summary>
    /// Interaction logic for ClienteNewControl.xaml
    /// </summary>
    public partial class ClienteNewControl : UserControl
    {
        public ClienteNewControl()
        {
            InitializeComponent();
            ClienteNewViewModel = new ClienteNewControlViewModel(DependencyResolver.Instance.FacadeProvider);
            ClienteNewViewModel.InitializeViewContent();

            //Bind the DataGrid to the customer data
            this.DataContext = ClienteNewViewModel;

            CommandModel addPedidoComand = ClienteNewViewModel.SaveClienteComand;

            ButtonSaveCliente.Command = addPedidoComand.Command;
            ButtonSaveCliente.CommandParameter = this.DataContext;
            ButtonSaveCliente.CommandBindings.Add(new CommandBinding(addPedidoComand.Command, addPedidoComand.OnExecute, addPedidoComand.OnCanExecute));
        }

        public ClienteNewControlViewModel ClienteNewViewModel { get; private set; }
    }
}
