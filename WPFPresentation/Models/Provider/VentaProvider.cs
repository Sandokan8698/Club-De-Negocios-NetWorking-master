using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using AutoMapper;
using Data_Layer.Implementations;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;


namespace WPFPresentation.Models.Provider
{
    public class VentaProvider :BaseProvider<VentaModel,Venta>
    {
       

        public VentaProvider(UnitOfWork unitOfWork):base(unitOfWork)
        {
           
        }

        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.VentaRepository;
        }

        #region Base Method

        public override ObservableCollection<VentaModel> GetAll()
        {
            using (UnitOfWork)
            {
                var entitysSources = UnitOfWork.VentaRepository.GetAll();
                return Mapper.Map<IEnumerable<Venta>, ObservableCollection<VentaModel>>(entitysSources);
            }
        }

        public override VentaModel Add(VentaModel entity)
        {
           
            using (UnitOfWork)
            {
                var entitySource = Mapper.Map<VentaModel, Venta>(entity);
                UnitOfWork.VentaRepository.Add(entitySource); 
                UnitOfWork.Complete();

                entitySource = UnitOfWork.VentaRepository.Get(entitySource.VentaId);
                

                return Mapper.Map<Venta,VentaModel>(entitySource);
            }
          
        }


        public VentaModel FindVentaById(int ventaId)
        {
            using (UnitOfWork)
            {
                var venta = UnitOfWork.VentaRepository.Find(v => v.VentaId == ventaId).FirstOrDefault();
                return Mapper.Map<Venta, VentaModel>(venta);
            }
            
        }
        #endregion

        public VentaModel GetVentaWithDeuda(int clientId)
        {
            using (UnitOfWork)
            {
                var venta = UnitOfWork.VentaRepository.GetVentaWithDeuda(clientId);
                return Mapper.Map<Venta, VentaModel>(venta);
            }

           
        }

        public ObservableCollection<VentaModel> Filter(FilterModel filter)
        {
            using (UnitOfWork)
            {
                var filterEntitie = Mapper.Map<FilterModel, FilterEntitie>(filter);
                var ventas = UnitOfWork.VentaRepository.GetVentasFilters(filterEntitie);
                return Mapper.Map<IEnumerable<Venta>, ObservableCollection<VentaModel>>(ventas);
            }
        }

        public ObservableCollection<VentaModel> PaginateFiltered(int page, int pageSize, FilterModel filterEntitie)
        {
            using (UnitOfWork)
            {
                var filter = Mapper.Map<FilterModel, FilterEntitie>(filterEntitie);
                var ventas = UnitOfWork.VentaRepository.PaginateFiltered(page, pageSize, filter);
                return Mapper.Map<IEnumerable<Venta>, ObservableCollection<VentaModel>>(ventas);
            }

        }
    }
}
