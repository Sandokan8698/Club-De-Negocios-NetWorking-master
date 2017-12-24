using System.Windows.Controls;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;


namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ClienteVentasPage.xaml
    /// </summary>
    public partial class ClienteVentasPage : UserControl
    {
        public ClienteVentasViewModel ClienteVentaViewModel { get; private set; }

        public ClienteVentasPage()
        {
            InitializeComponent();
            
            ClienteVentaViewModel = new ClienteVentasViewModel(DependencyResolver.Instance.FacadeProvider);
           
            LoadContent();
        }


        private void LoadContent()
        {
            ClienteVentaViewModel.InitializeViewContent();
            DataContext = ClienteVentaViewModel;
        }

        private void DgVentas_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dg = (DataGrid)sender;

            if (dg.SelectedCells.Count > 0)
            {
                var selectedCell = dg.SelectedCells[0];

                var cellInfo = selectedCell.Item;

                ClienteVentaViewModel.ShowClienteVentaDetailPage(cellInfo);
                
            }
        }

       
    }
}
