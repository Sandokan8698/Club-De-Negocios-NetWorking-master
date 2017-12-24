using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Utils.FilterEngine;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;
using RefactorThis.GraphDiff;

namespace Data_Layer.Implementations.Repositories
{
    public class VentaRepository : DomainContextRepository<Venta>, IVentaRepository
    {
        public VentaRepository(ClubNegociosNetworkingContext context) : base(context)
        {

        }

        public override void Remove(Venta entity)
        {
            var removeEntity = CnnContext.VentaDbSet.First(v => v.VentaId == entity.VentaId);
            CnnContext.VentaDbSet.Remove(removeEntity);
        }

        public override IEnumerable<Venta> GetAll()
        {
            var ventas = CnnContext.VentaDbSet
                .Include(v => v.Cliente)
                .Include(v => v.Trabajador)
                .Include(v => v.Pedidos)
                .Include("Pedidos.Proveedor")
                .Include("Pedidos.SubPedidos.SubPedidosEntrys").ToList();
            return ventas;
        }

        public override Venta Get(int id)
        {
            return CnnContext.VentaDbSet
                .Include(v => v.Trabajador)
                .Include(v => v.Cliente)
                .Include(v => v.Pedidos)
                .Include("Pedidos.Proveedor")
                .Include("Pedidos.SubPedidos.SubPedidosEntrys")
                .FirstOrDefault(v => v.VentaId == id);
        }

        public IEnumerable<Venta> GetClientVentas(Cliente cliente)
        {
            if (cliente != null)
            {
                return CnnContext.VentaDbSet.Include("Pedidos.SubPedidos").Where(v => v.ClienteId == cliente.ClienteId).ToList();
            }

            return new List<Venta>();
        }

        public Venta GetVentaWithDeuda(int clientId)
        {
            var ventas = GetAll().Where(v => v.ClienteId == clientId);
            return ventas.FirstOrDefault(v => v.Deuda > 0);
        }
 
        public IEnumerable<Venta> GetClienteVentasFiltered(Cliente cliente, FilterEntitie filterEntitie)
        {
            var clienteVentas = GetClientVentas(cliente);
            return ApplyFilter(filterEntitie, clienteVentas.AsQueryable());
        }

        public IEnumerable<Venta> GetVentasFilters(FilterEntitie filterEntitie)
        {
            return ApplyFilter(filterEntitie, GetAll().AsQueryable());
        }

        public IEnumerable<Venta> ApplyFilter(FilterEntitie filterEntitie, IQueryable<Venta> ventasQueryable)
        {
            var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.VentasFilterCreator, filterEntitie);
            var filterQuerys = filterCreator.CreateFilterQuerys();
            return ventasQueryable.ApplySearchCriteria(filterQuerys);
        }

        public IEnumerable<Venta> PaginateFiltered(int page, int pageSize, FilterEntitie filterEntitie)
        {
            try
            {
                var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.VentasFilterCreator, filterEntitie);
                var filterQuerys = filterCreator.CreateFilterQuerys();
                return CnnContext.VentaDbSet.OrderByDescending(v => v.Fecha)
                    .Include(v => v.Cliente)
                    .Include(v => v.Trabajador)
                    .Include(v => v.Pedidos)
                    .Include("Pedidos.Proveedor")
                    .Include("Pedidos.SubPedidos.SubPedidosEntrys")
                    .ApplySearchCriteria(filterQuerys).Skip(page).Take(pageSize).ToList();
            }
            catch (Exception e)
            {
                throw e;

            }
        }
    }
}
