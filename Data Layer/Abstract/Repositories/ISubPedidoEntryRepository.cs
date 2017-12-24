using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace Data_Layer.Abstract.Repositories
{
    public interface ISubPedidoEntryRepository: IRepository<SubPedidoEntry>
    {
        IEnumerable<SubPedidoEntry> GetDailyReportPayment(Venta venta);

        IEnumerable<SubPedidoEntry> ApplayFilter(FilterEntitie filterEntitie);

        IEnumerable<Object> PaginateFiltered(int page, int pageSize, FilterEntitie filterEntitie);

    }
}
