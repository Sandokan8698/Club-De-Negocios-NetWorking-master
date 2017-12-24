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
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for TrabajadorEditDialogPage.xaml
    /// </summary>
    public partial class TrabajadorEditDialogPage : ModernDialog
    {
        private TrabajadorEditDialogViewModel _trabajadorNewViewModel;
        public TrabajadorEditDialogPage(TrabajadorModel trabajador)
        {
            InitializeComponent();

            _trabajadorNewViewModel = new TrabajadorEditDialogViewModel(DependencyResolver.Instance.FacadeProvider, trabajador, () => Close());

            UCTrabajadorEditPage.Tittle.Visibility = Visibility.Collapsed;
            UCTrabajadorEditPage.ButtonSaveTrabajador.Visibility = Visibility.Collapsed;
            UCTrabajadorEditPage.DataContext = _trabajadorNewViewModel;
            UCTrabajadorEditPage.ListTypoAccseso.SelectedItem = trabajador.Acceso;
            UCTrabajadorEditPage.ListTypoAccseso.SelectionChanged += (sender, args) =>
            {
                var combo = (ComboBox) sender;
                var selectedItem = combo.SelectedItem;
                _trabajadorNewViewModel.Trabajador.Acceso = (string)selectedItem;
            };

            Title = "EDIDTAR USUARIO";

            OkButton.Content = "Aceptar";

            CommandModel updateTrabajadorComman = _trabajadorNewViewModel.UpdateTrabajadorComman;

            OkButton.Command = updateTrabajadorComman.Command;
            OkButton.CommandBindings.Add(new CommandBinding(updateTrabajadorComman.Command, updateTrabajadorComman.OnExecute, updateTrabajadorComman.OnCanExecute));

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }



    }
}
