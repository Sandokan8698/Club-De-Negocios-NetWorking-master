using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper;
using Data_Layer.Implementations;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace WPFPresentation.Models.Provider
{
    public class SubPedidoProvider : BaseProvider<SubPedidoModel, SubPedido>
    {
        public SubPedidoProvider(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.SubPedidoRepository;
        }

        public ObservableCollection<SubPedidoModel> ApplayFilter(FilterModel filterEntitie)
        {
            using (UnitOfWork)
            {
                var filter = Mapper.Map<FilterModel, FilterEntitie>(filterEntitie);
                var subPedidos = UnitOfWork.SubPedidoRepository.ApplayFilter(filter);
                return Mapper.Map<IEnumerable<SubPedido>, ObservableCollection<SubPedidoModel>>(subPedidos);
            }
            
        }

        public ObservableCollection<Object> PaginateFiltered(int page, int pageSize, FilterModel filterEntitie)
        {
            using (UnitOfWork)
            {
                var filter = Mapper.Map<FilterModel, FilterEntitie>(filterEntitie);
                var subPedidos = UnitOfWork.SubPedidoRepository.PaginateFiltered(page, pageSize, filter);
                return Mapper.Map<IEnumerable<Object>, ObservableCollection<Object>>(subPedidos);
            }
            
        }
    }
}
