using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.FilterEngine;
using Domain_Layer.Entities;

namespace TestWhatIDontKno.Filters
{
    public class VentasFitlerBuilder : Builder
    {
        public VentasFitlerBuilder()
        {
            AbstractSearches = new List<AbstractSearch>();
        }

        public override void BuildClienteFilter()
        {
            var criteria = new TextSearch();
            criteria.Property = "Cliente.Nombre";
            criteria.TargetTypeName = typeof(Venta).AssemblyQualifiedName;

            criteria.SearchTerm = "Denys Abdel";
            criteria.Comparator = TextComparators.Equals;

            AbstractSearches.Add(criteria);

        }

        public override void BuildFechaFilter()
        {
            var criteria = new DateSearch();
            criteria.Property = "Fecha";
            criteria.TargetTypeName = typeof(Venta).AssemblyQualifiedName;

            criteria.SearchTerm = new System.DateTime(2017, 10, 20);
            criteria.SearchTerm2 = null;
            criteria.Comparator = DateComparators.Equal;

            AbstractSearches.Add(criteria);
        }

        public override ICollection<AbstractSearch> GetResult()
        {
            return AbstractSearches;
        }
    }
}
