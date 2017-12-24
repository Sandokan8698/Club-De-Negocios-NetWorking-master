using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.FilterEngine;

namespace TestWhatIDontKno.Filters
{
    public abstract class Builder
    {
        protected ICollection<AbstractSearch> AbstractSearches { get; set; }
        public abstract void BuildClienteFilter();
        public abstract void BuildFechaFilter();
        public abstract ICollection<AbstractSearch> GetResult();
    }
}
