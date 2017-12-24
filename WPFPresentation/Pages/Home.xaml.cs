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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using WPFPresentation.Models;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        private HomeViewModel _viewModel;
        
        public Home()
        {
            InitializeComponent();
            _viewModel = new HomeViewModel();
            DataContext = _viewModel;
        }

        private async void ButtonSaveCliente_Click(object sender, RoutedEventArgs e)
        {
           _viewModel.Login(this);
            TextContraseña.Password = "";
            TextUsuario.Text = "";
        }


        private void TextContraseña_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = ((PasswordBox) sender).Password;
        }
    }
}
