using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class CampanllaRepository : DomainContextRepository<Campanlla>, ICampanllaRepository
    {
        public CampanllaRepository(ClubNegociosNetworkingContext context) : base(context)
        {
        }
    }
}
