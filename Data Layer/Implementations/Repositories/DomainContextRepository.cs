using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Implementations.Repositories;

namespace Data_Layer.Implementations.Repositories
{
    public class DomainContextRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        public DomainContextRepository(ClubNegociosNetworkingContext context) : base(context)
        {
        }

        public ClubNegociosNetworkingContext CnnContext
        {
            get { return Context as ClubNegociosNetworkingContext; }
        }
    }
}
