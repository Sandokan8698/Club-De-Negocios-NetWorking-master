using System.Collections.Generic;
using Data_Layer.Utils.FilterEngine;

namespace Data_Layer.Utils.ServiceFilter
{
    public abstract class FilterCreator
    {
        protected FilterEntitie FilterEntitie;

        public ICollection<AbstractSearch> FilterQuerys { get; protected set; }

        public FilterCreator(FilterEntitie filterEntitie)
        {
            FilterEntitie = filterEntitie;
            FilterQuerys = new List<AbstractSearch>();
        }

        public virtual ICollection<AbstractSearch> CreateFilterQuerys()
        {
            Build();
            return FilterQuerys;
        }

        protected abstract void Build();

    }
}
