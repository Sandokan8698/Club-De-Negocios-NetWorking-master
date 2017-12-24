using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class ProveedorRepository: DomainContextRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(ClubNegociosNetworkingContext context):base(context)
        {
            
        }
    }
}
