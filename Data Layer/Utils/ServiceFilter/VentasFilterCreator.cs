using System;
using Data_Layer.Utils.FilterEngine;
using Domain_Layer.Entities;

namespace Data_Layer.Utils.ServiceFilter
{
    public class VentasFilterCreator : FilterCreator
    {
        public VentasFilterCreator(FilterEntitie filterEntitie) : base(filterEntitie)
        {
        }

        protected override void Build()
        {
            BuildClienteFilterQuery();
            BuildFechaFilterQuery();
            BuildVentaNumberFilterQuery();
        }

        private void BuildClienteFilterQuery()
        {
            if (FilterEntitie.Cliente != null)
            {
                var criteria = new TextSearch
                {
                    Property = "Cliente.Nombre",
                    TargetTypeName = typeof(Venta).AssemblyQualifiedName,
                    SearchTerm = FilterEntitie.Cliente.Nombre,
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
                    Property = "Fecha",
                    TargetTypeName = typeof(Venta).AssemblyQualifiedName,

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
                    Property = "VentaId",
                    TargetTypeName = typeof(Venta).AssemblyQualifiedName,
                    SearchTerm = FilterEntitie.VentaId,
                    Comparator = NumericComparators.Equal

                };

                FilterQuerys.Add(criteria);
            }

        }

    }
}
