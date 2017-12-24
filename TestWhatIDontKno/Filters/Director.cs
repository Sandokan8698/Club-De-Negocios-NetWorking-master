using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWhatIDontKno.Filters
{
    class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildClienteFilter();
            builder.BuildFechaFilter();
        }
    }
}
