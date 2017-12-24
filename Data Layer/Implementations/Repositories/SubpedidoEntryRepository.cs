using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Utils.FilterEngine;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class SubpedidoEntryRepository : DomainContextRepository<SubPedidoEntry>, ISubPedidoEntryRepository
    {
        public SubpedidoEntryRepository(ClubNegociosNetworkingContext context) : base(context)
        {
            
        }

        public IEnumerable<SubPedidoEntry> GetDailyReportPayment(Venta venta)
        {
            return CnnContext.SubPedidoEntriesDbSet.Where(sbe => sbe.FechaCreacion == DateTime.Today && sbe.SubPedido.Pedido.VentaId == venta.VentaId && sbe.Abono > 0)
                .Include(sbe => sbe.SubPedido).AsNoTracking().ToList();
        }
        
        public override void Remove(SubPedidoEntry entity)
        {
            var removeEntity = CnnContext.SubPedidoEntriesDbSet.First(sbe => sbe.SubPedidoEntryId == entity.SubPedidoEntryId);
            CnnContext.SubPedidoEntriesDbSet.Remove(removeEntity);
        }


        public IEnumerable<SubPedidoEntry> ApplayFilter(FilterEntitie filterEntitie)
        {
            try
            {
                var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.SubPedidoEntryFilterCreator, filterEntitie);
                var filterQuerys = filterCreator.CreateFilterQuerys();
                return CnnContext.SubPedidoEntriesDbSet.ApplySearchCriteria(filterQuerys).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Object> PaginateFiltered(int page, int pageSize, FilterEntitie filterEntitie)
        {
            try
            {
                var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.SubPedidoEntryFilterCreator, filterEntitie);
                var filterQuerys = filterCreator.CreateFilterQuerys();
                return CnnContext.SubPedidoEntriesDbSet
                    .Include(sbe => sbe.SubPedido)
                    .Include(sbe => sbe.SubPedido.Pedido)
                    .Include(sbe => sbe.SubPedido.Pedido.Proveedor)
                    .Include(sbe => sbe.SubPedido.Pedido.Venta)
                    .Include(sbe => sbe.SubPedido.Pedido.Venta.Cliente)
                    .OrderByDescending(sb => sb.FechaCreacion)
                    .ApplySearchCriteria(filterQuerys).Skip(page)
                    .Take(pageSize).Select( sbe => new
                    {
                        FechaCreacion = sbe.FechaCreacion,
                        ClienteNombre = sbe.SubPedido.Pedido.Venta.Cliente.Nombre,
                        VentaId = sbe.SubPedido.Pedido.VentaId,
                        ProveedorNombre = sbe.SubPedido.Pedido.Proveedor.Nombre,
                        Identificador = sbe.SubPedido.Identificador,
                        Abono = sbe.Abono

                    });

            }
            catch (Exception e)
            {
                throw e;

            }
        }

    }
}
