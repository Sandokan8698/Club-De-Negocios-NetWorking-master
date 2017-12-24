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
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ProveedorEditDialog.xaml
    /// </summary>
    public partial class ProveedorEditDialog : ModernDialog
    {
        private ProveedorNewViewModel _proveedorNewViewModel;
        public ProveedorEditDialog(ProveedorModel proveedor)
        {
            InitializeComponent();
            _proveedorNewViewModel = new ProveedorNewViewModel(DependencyResolver.Instance.FacadeProvider, proveedor, () => Close());

            UCProveedor.Title.Text = "EDITAR PROVEEDOR";
            UCProveedor.ButtonSaveProveedor.Visibility = Visibility.Collapsed;
            UCProveedor.DataContext = _proveedorNewViewModel;

            OkButton.Content = "Aceptar";
            var updateCommand = _proveedorNewViewModel.UpdateProveedorComand;
            OkButton.Command = updateCommand.Command;
            OkButton.CommandBindings.Add(new CommandBinding(updateCommand.Command, updateCommand.OnExecute, updateCommand.OnCanExecute));
            CancelButton.Content = "Cancelar";
            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
