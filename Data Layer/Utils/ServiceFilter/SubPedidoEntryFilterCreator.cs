using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.FilterEngine;
using Domain_Layer.Entities;

namespace Data_Layer.Utils.ServiceFilter
{
    public class SubPedidoEntryFilterCreator : FilterCreator
    {
        public SubPedidoEntryFilterCreator(FilterEntitie filterEntitie) : base(filterEntitie)
        {
        }

        protected override void Build()
        {
            BuildClienteFilterQuery();
            BuildFechaFilterQuery(); 
            BuildVentaNumberFilterQuery();
            BuildProveedorFilterQuery();
            BuildIdentificadorQuery();
        }

        private void BuildClienteFilterQuery()
        {
            if (FilterEntitie.Cliente != null)
            {
                var criteria = new TextSearch
                {
                    Property = "SubPedido.Pedido.Venta.Cliente.Nombre",
                    TargetTypeName = typeof(SubPedidoEntry).AssemblyQualifiedName,
                    SearchTerm =  FilterEntitie.Cliente.Nombre,
                    Comparator = TextComparators.Equals

                };

                FilterQuerys.Add(criteria);
            }           
            
        }

        private void BuildFechaFilterQuery()
        {
            if (FilterEntitie.UsarFecha)
            {
                var criteria = new DateSearch
                {
                    Property = "FechaCreacion",
                    TargetTypeName = typeof(SubPedidoEntry).AssemblyQualifiedName,

                    SearchTerm = FilterEntitie.Desde,
                    SearchTerm2 = FilterEntitie.Hasta,
                    Comparator = DateComparators.InRange
                };


                FilterQuerys.Add(criteria);
            }
           
        }

        private void BuildVentaNumberFilterQuery()
        {
            if (FilterEntitie.VentaId > 0)
            {
                var criteria = new NumericSearch()
                {
                    Property = "SubPedido.Pedido.VentaId",
                    TargetTypeName = typeof(SubPedidoEntry).AssemblyQualifiedName,
                    SearchTerm = FilterEntitie.VentaId,
                    Comparator = NumericComparators.Equal

                };

                FilterQuerys.Add(criteria);
            }
            
        }

        private void BuildProveedorFilterQuery()
        {
            if (FilterEntitie.Proveedor != null)
            {
                var criteria = new TextSearch
                {
                    Property = "SubPedido.Pedido.Proveedor.Nombre",
                    TargetTypeName = typeof(SubPedidoEntry).AssemblyQualifiedName,
                    SearchTerm =  FilterEntitie.Proveedor.Nombre,
                    Comparator = TextComparators.Equals

                };

                FilterQuerys.Add(criteria);
            }

        }


        private void BuildIdentificadorQuery()
        {
            if (!string.IsNullOrEmpty(FilterEntitie.Identificador))
            {
                var criteria = new TextSearch
                {
                    Property = "SubPedido.Identificador",
                    TargetTypeName = typeof(SubPedidoEntry).AssemblyQualifiedName,
                    SearchTerm = FilterEntitie.Identificador,
                    Comparator = TextComparators.Equals

                };

                FilterQuerys.Add(criteria);
            }
        }

    }
}
