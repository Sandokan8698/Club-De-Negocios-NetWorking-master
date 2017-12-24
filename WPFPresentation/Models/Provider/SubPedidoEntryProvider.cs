using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data_Layer.Implementations;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;


namespace WPFPresentation.Models.Provider
{
    public class SubPedidoEntryProvider : BaseProvider<SubPedidoEntryModel, SubPedidoEntry>
    {
        public SubPedidoEntryProvider(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.SubPedidoEntryRepository;
        }

        public ObservableCollection<SubPedidoEntryModel> GetDailyReportPayment(VentaModel venta)
        {
            using (UnitOfWork)
            {
                var ventaSource = Mapper.Map<VentaModel, Venta>(venta);
                var supedidosEntrys = UnitOfWork.SubPedidoEntryRepository.GetDailyReportPayment(ventaSource);
                return Mapper.Map<IEnumerable<SubPedidoEntry>, ObservableCollection<SubPedidoEntryModel>>(
                    supedidosEntrys);
            }
        }

        public ObservableCollection<SubPedidoEntryModel> ApplayFilter(FilterModel filterEntitie)
        {
            using (UnitOfWork)
            {
                var filter = Mapper.Map<FilterModel, FilterEntitie>(filterEntitie);
                var subPedidoEntrys = UnitOfWork.SubPedidoEntryRepository.ApplayFilter(filter);
                return Mapper.Map<IEnumerable<SubPedidoEntry>, ObservableCollection<SubPedidoEntryModel>>(subPedidoEntrys);
            }

        }

        public ObservableCollection<Object> PaginateFiltered(int page, int pageSize, FilterModel filterEntitie)
        {
            using (UnitOfWork)
            {
                var filter = Mapper.Map<FilterModel, FilterEntitie>(filterEntitie);
                var subPedidoEntrys = UnitOfWork.SubPedidoEntryRepository.PaginateFiltered(page, pageSize, filter);
                return Mapper.Map<IEnumerable<Object>, ObservableCollection<Object>>(subPedidoEntrys);
            }

        }
    }
}
