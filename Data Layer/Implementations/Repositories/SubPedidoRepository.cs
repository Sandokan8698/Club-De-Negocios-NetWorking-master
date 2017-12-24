using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Utils.FilterEngine;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class SubPedidoRepository : DomainContextRepository<SubPedido>, ISubPedidoRepository
    {
        public SubPedidoRepository(ClubNegociosNetworkingContext  context) : base(context)
        {
        }

        public override void Remove(SubPedido entity)
        {
            var removeEntity = CnnContext.SubPedidoDbSet.First(sb => sb.SubPedidoId == entity.SubPedidoId);
            CnnContext.SubPedidoDbSet.Remove(removeEntity);
        }

        public override IEnumerable<SubPedido> GetAll()
        {
            
            return CnnContext.SubPedidoDbSet.Include(sb => sb.Pedido.Venta).Include(sb => sb.SubPedidosEntrys).ToList();
        }

        public IEnumerable<SubPedido> ApplayFilter(FilterEntitie filterEntitie)
        {
            try
            {
              
                var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.SubPedidoFilterCreator, filterEntitie);
                var filterQuerys = filterCreator.CreateFilterQuerys();
                return CnnContext.SubPedidoDbSet
                    .Include(sb => sb.SubPedidosEntrys)
                    .ApplySearchCriteria(filterQuerys)
                    .ToList();
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
                var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.SubPedidoFilterCreator, filterEntitie);
                var filterQuerys = filterCreator.CreateFilterQuerys();

              
                return CnnContext.SubPedidoDbSet
                    .Include(sb => sb.Pedido.Venta)
                    .Include(sb => sb.Pedido.Venta.Cliente)
                    .Include(sb => sb.Pedido.Venta.Trabajador)
                    .Include(sb => sb.Pedido.Proveedor)
                    .Include(sb => sb.SubPedidosEntrys)
                    .OrderByDescending(sb => sb.FechaCreacion)
                    .ApplySearchCriteria(filterQuerys)
                    .Skip(page)
                    .Take(pageSize)
                    .Select(sb => new {

                        FechaCreacion = sb.FechaCreacion,
                        ClienteNombre = sb.Pedido.Venta.Cliente.Nombre,
                        VentaId = sb.Pedido.VentaId,
                        ProveedorNombre = sb.Pedido.Proveedor.Nombre,
                        Identificador = sb.Identificador,
                        PrecioProveedor = sb.PrecioProveedor,
                        Abono = sb.SubPedidosEntrys.Sum(sbe => sbe.Abono),
                        Deuda = sb.PrecioProveedor - sb.SubPedidosEntrys.Sum(sbe => sbe.Abono)
                    });


                    
            }
            catch (Exception e)
            {
                throw e;

            }
            
        }
    }
}
