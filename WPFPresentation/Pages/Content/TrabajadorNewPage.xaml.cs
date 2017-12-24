using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content
{
    /// <summary>
    /// Interaction logic for TrabajadorNewPage.xaml
    /// </summary>
    public partial class TrabajadorNewPage : UserControl
    {
        private TrabajadorNewViewModel _trabajadorNewViewModel;
        public TrabajadorNewPage()
        {
            InitializeComponent();
            _trabajadorNewViewModel = new TrabajadorNewViewModel(DependencyResolver.Instance.FacadeProvider);
            DataContext = _trabajadorNewViewModel;

            ListTypoAccseso.ItemsSource = _trabajadorNewViewModel.TipoAcceso;
            ListTypoAccseso.SelectedIndex = 0;

            CommandModel saveTrabajadorComman = _trabajadorNewViewModel.SaveTrabajadorComman;

            ButtonSaveTrabajador.Command = saveTrabajadorComman.Command;
            ButtonSaveTrabajador.CommandBindings.Add(new CommandBinding(saveTrabajadorComman.Command, saveTrabajadorComman.OnExecute, saveTrabajadorComman.OnCanExecute));
        }

        private void ListCliente_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedItem = comboBox.SelectedItem;

            _trabajadorNewViewModel.Trabajador.Acceso = (string) selectedItem;
        }
    }
}
