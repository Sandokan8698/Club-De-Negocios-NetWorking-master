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
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for VentaDetailDialogPage.xaml
    /// </summary>
    public partial class VentaDetailDialogPage : ModernDialog
    {
        private VentaDetailViewModel _ventaDetailViewModel;

        public VentaDetailDialogPage(VentaModel venta)
        {
            InitializeComponent();

            _ventaDetailViewModel = new VentaDetailViewModel(DependencyResolver.Instance.FacadeProvider);
            _ventaDetailViewModel.InitializeViewContent();
            _ventaDetailViewModel.Venta = venta;
           
            VentaDetails ventaDetails = new VentaDetails(_ventaDetailViewModel);

            GdContainer.Children.Add(ventaDetails);

            OkButton.Content = "Aceptar";
            
            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton };

            
        }

        
    }

}
