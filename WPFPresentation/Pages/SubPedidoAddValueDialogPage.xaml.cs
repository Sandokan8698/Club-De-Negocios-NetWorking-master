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

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SubPedidoAddValueDialogPage.xaml
    /// </summary>
    public partial class SubPedidoAddValueDialogPage : ModernDialog
    {
        public SubPedidoAddValueDialogPage()
        {
            InitializeComponent();

            // define the dialog buttons
            OkButton.Content = "Aceptar";
            CancelButton.Content = "Cancelar";

            DataContext = this;
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }

        private void SubPedidoAddValueDialogPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxValor.Focus();
        }

        public decimal PrecioProveedor { get; set; }

    }
}
