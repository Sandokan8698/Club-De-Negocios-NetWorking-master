using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace Data_Layer.Abstract.Repositories
{
    public interface IVentaRepository: IRepository<Venta>
    {
        IEnumerable<Venta> GetClientVentas(Cliente cliente);

        IEnumerable<Venta> GetClienteVentasFiltered(Cliente cliente, FilterEntitie filterEntitie);

        IEnumerable<Venta> GetVentasFilters(FilterEntitie filterEntitie);

        IEnumerable<Venta> ApplyFilter(FilterEntitie filterEntitie, IQueryable<Venta> ventasQueryable);

        IEnumerable<Venta> PaginateFiltered(int page, int pageSize, FilterEntitie filterEntitie);

        Venta GetVentaWithDeuda(int clientId);
    }
}
