namespace WPFPresentation.Models.Provider
{
    public interface IFacadeProvider
    {
        CampanllaProvider CampanllaProvider();
        ClienteProvider ClienteProvider();
        PedidoProvider PedidoProvider();
        ProveedorProvider ProveedorProvider();
        SubPedidoEntryProvider SubPedidoEntryProvider();
        SubPedidoProvider SubPedidoProvider();
        TrabajadorProvider TrabajadorProvider();
        VentaProvider VentaProvider();
    }
}