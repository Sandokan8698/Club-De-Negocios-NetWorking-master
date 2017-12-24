using System;
using FirstFloor.ModernUI.Presentation;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        
        private LinkGroupCollection menuLinkGroups;

        public MainWindowViewModel(LinkGroupCollection menuLinkGroups)
        {
            this.menuLinkGroups = menuLinkGroups;

            Messenger.Instance.Register(o => SetNavigation(),ViewModelMessages.SetNavigation);

        }

        public LinkGroupCollection Groups
        {
            get { return this.menuLinkGroups; }
        }


        private void SetNavigation()
        {
            

            var ventasGroup = new LinkGroup { DisplayName = "ventas" };
            ventasGroup.Links.Add(new Link { DisplayName = "nueva", Source = new Uri("/Pages/VentaNewPage.xaml",UriKind.Relative) });
            ventasGroup.Links.Add(new Link { DisplayName = "ventas", Source = new Uri("/Pages/VentaListPage.xaml", UriKind.Relative) });


            var clienteGroup = new LinkGroup { DisplayName = "clientes" };
            clienteGroup.Links.Add(new Link { DisplayName = "clientes", Source = new Uri("/Pages/ClientesPage.xaml", UriKind.Relative) });
            clienteGroup.Links.Add(new Link { DisplayName = "anticipo", Source = new Uri("/Pages/AnticipoAddPage.xaml", UriKind.Relative) });

            var proveedorGroup = new LinkGroup { DisplayName = "empresas" };
            proveedorGroup.Links.Add(new Link { DisplayName = "empresas", Source = new Uri("/Pages/ProveedoresPage.xaml", UriKind.Relative) });

           

            this.menuLinkGroups.Add(ventasGroup);
            this.menuLinkGroups.Add(clienteGroup);
            this.menuLinkGroups.Add(proveedorGroup);


            if (Authenticator.Instance.IsAdming)
            {
                var trabajadoresGroup = new LinkGroup { DisplayName = "usuarios" };
                trabajadoresGroup.Links.Add(new Link { DisplayName = "usuarios", Source = new Uri("/Pages/TrabajadoresPage.xaml", UriKind.Relative) });

                var reportesGroup = new LinkGroup { DisplayName = "reportes " };
                reportesGroup.Links.Add(new Link { DisplayName = "reportes pedido", Source = new Uri("/Pages/SubPedidoReportePage.xaml", UriKind.Relative) });
                reportesGroup.Links.Add(new Link { DisplayName = "reportes abono", Source = new Uri("/Pages/SubPedidoEntryReportePage.xaml", UriKind.Relative) });

                this.menuLinkGroups.Add(trabajadoresGroup);
                this.menuLinkGroups.Add(reportesGroup);
            }
            
        }
    }
}
