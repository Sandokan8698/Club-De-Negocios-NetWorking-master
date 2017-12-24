using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using Data_Layer.Implementations;
using Data_Layer.Abstract;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace WPFPresentation.Models.Provider
{
    public class ClienteProvider : BaseProvider<ClienteModel, Cliente>
    {

        public ClienteProvider(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.ClienteRepository;
        }

        public ClienteModel GetClienteByDocumento(string documento)
        {
            using (UnitOfWork)
            {
                var cliente = UnitOfWork.ClienteRepository.GetClienteByDocumento(documento);
                return Mapper.Map<Cliente, ClienteModel>(cliente);

            }
            
        }

        public ObservableCollection<VentaModel> GetClientVentas(ClienteModel  clienteModel)
        {
            using (UnitOfWork)
            {
                var cliente = Mapper.Map<ClienteModel, Cliente>(clienteModel);
                var ventas = UnitOfWork.VentaRepository.GetClientVentas(cliente);
               return Mapper.Map<IEnumerable<Venta>, ObservableCollection<VentaModel>>(ventas);
            }
            
        }


        public ObservableCollection<VentaModel> GetClientVentasFiltered(ClienteModel clienteModel, FilterModel filter)
        {
            using (UnitOfWork)
            {
                var cliente = Mapper.Map<ClienteModel, Cliente>(clienteModel);
                var filterEntitie = Mapper.Map<FilterModel, FilterEntitie>(filter);
                var ventas = UnitOfWork.VentaRepository.GetClienteVentasFiltered(cliente, filterEntitie);
                return Mapper.Map<IEnumerable<Venta>, ObservableCollection<VentaModel>>(ventas);
            }
        }

       
    }
}


