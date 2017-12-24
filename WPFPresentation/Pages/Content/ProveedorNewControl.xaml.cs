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
    /// Interaction logic for ProveedorNewControl.xaml
    /// </summary>

    public partial class ProveedorNewControl : UserControl
    {
        private ProveedorNewViewModel _proveedorNewViewModel;
        public ProveedorNewControl()
        {
            InitializeComponent();

            _proveedorNewViewModel = new ProveedorNewViewModel(DependencyResolver.Instance.FacadeProvider);
            _proveedorNewViewModel.InitializeViewContent();

            //Bind the DataGrid to the customer data
            this.DataContext = _proveedorNewViewModel;

            CommandModel addPedidoComand = _proveedorNewViewModel.SaveProveedorComand;

            ButtonSaveProveedor.Command = addPedidoComand.Command;
            ButtonSaveProveedor.CommandParameter = this.DataContext;
            ButtonSaveProveedor.CommandBindings.Add(new CommandBinding(addPedidoComand.Command, addPedidoComand.OnExecute, addPedidoComand.OnCanExecute));
        }
    }
}
