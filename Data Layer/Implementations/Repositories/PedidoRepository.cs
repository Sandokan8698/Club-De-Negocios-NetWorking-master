using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class PedidoRepository: DomainContextRepository<Pedido>, IPedidoRepositoy
    {
        public PedidoRepository(ClubNegociosNetworkingContext context):base(context)
        {
           
        }

        public override void Remove(Pedido entity)
        {
           
           var removeEntity = CnnContext.PedidoDbSet.First(sb => sb.PedidoId == entity.PedidoId);
            CnnContext.PedidoDbSet.Remove(removeEntity);         
            
        }

        public override Pedido Get(int id)
        {
            return CnnContext.PedidoDbSet
                .Include(p => p.Venta)
                .Include(p => p.Proveedor)
                .FirstOrDefault(p => p.PedidoId == id);
        }
    }
}
