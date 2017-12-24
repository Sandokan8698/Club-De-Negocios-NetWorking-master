using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Implementations;

namespace WPFPresentation.Models.Provider
{
    public class FacadeProvider : IFacadeProvider
    {
        private ClienteProvider _clienteProvider;
        private PedidoProvider _pedidoProvider;
        private ProveedorProvider _proveedorProvider;
        private VentaProvider _ventaProvider;
        private CampanllaProvider _campanllaProvider;
        private SubPedidoProvider _subPedidoProvider;
        private SubPedidoEntryProvider _subPedidoEntryProvider;
        private TrabajadorProvider _trabajadorProvider;

        public ClienteProvider ClienteProvider()
        {
            return new ClienteProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public PedidoProvider PedidoProvider()
        {
            return new PedidoProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public ProveedorProvider ProveedorProvider()
        {
            return new ProveedorProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public VentaProvider VentaProvider()
        {
            return new VentaProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public CampanllaProvider CampanllaProvider()
        {
            return new CampanllaProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public SubPedidoProvider SubPedidoProvider()
        {
            return new SubPedidoProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public SubPedidoEntryProvider SubPedidoEntryProvider()
        {
            return new SubPedidoEntryProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public TrabajadorProvider TrabajadorProvider()
        {
            return new TrabajadorProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }
    }
}
