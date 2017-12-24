using FirstFloor.ModernUI.Windows.Controls;
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
using WPFPresentation.Models;
using WPFPresentation.Pages.Content;
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ClienteEditDialogPage.xaml
    /// </summary>
    public partial class ClienteEditDialogPage : ModernDialog
    {
        private ClienteNewControlViewModel _clienteNewControlViewModel;
        public ClienteEditDialogPage(ClienteModel clienteModel)
        {
            InitializeComponent();

            _clienteNewControlViewModel = new ClienteNewControlViewModel(DependencyResolver.Instance.FacadeProvider, clienteModel , () => Close());

            UCClienteNew.Title.Text = "EDITAR CLIENTE";
            UCClienteNew.ButtonSaveCliente.Visibility = Visibility.Collapsed;
            UCClienteNew.DataContext = _clienteNewControlViewModel;

            OkButton.Content = "Aceptar";
            var updateCommand = _clienteNewControlViewModel.UpdateClienteComand;
            OkButton.Command = updateCommand.Command;
            OkButton.CommandBindings.Add(new CommandBinding(updateCommand.Command, updateCommand.OnExecute, updateCommand.OnCanExecute));
            CancelButton.Content = "Cancelar";
           
            
            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
