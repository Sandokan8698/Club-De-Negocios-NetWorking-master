
using System.Windows;
using AutoMapper;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;
using WPFPresentation.Models;


namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Mapper.CreateMap<Venta, VentaModel>();
                

            Mapper.CreateMap<VentaModel, Venta>()
                .Ignore(c => c.Cliente).Ignore(d => d.Deuda).Ignore(c => c.Trabajador);

            Mapper.CreateMap<PedidoModel, Pedido>()
                //.ForMember(ps => ps.SubPedidos, pm => pm.MapFrom(p => p.Childrens ))
                .Ignore(p => p.Proveedor)
                .Ignore(p => p.Venta);

            Mapper.CreateMap<Pedido, PedidoModel>();

            Mapper.CreateMap<SubPedidoModel, SubPedido>();
            Mapper.CreateMap<SubPedido, SubPedidoModel>();

            Mapper.CreateMap<SubPedidoEntry, SubPedidoEntryModel>();
            Mapper.CreateMap<SubPedidoEntryModel, SubPedidoEntry>();

            Mapper.CreateMap<Cliente, ClienteModel>();
            Mapper.CreateMap<ClienteModel, Cliente>();

            Mapper.CreateMap<Campanlla, CampanllaModel>();
            Mapper.CreateMap<CampanllaModel, Campanlla>();

            Mapper.CreateMap<Proveedor, ProveedorModel>();
            Mapper.CreateMap<ProveedorModel, Proveedor>();

            Mapper.CreateMap<FilterModel, FilterEntitie>();

            Mapper.CreateMap<Trabajador, TrabajadorModel>();
            Mapper.CreateMap<TrabajadorModel, Trabajador>();
        }
       
    }
}
