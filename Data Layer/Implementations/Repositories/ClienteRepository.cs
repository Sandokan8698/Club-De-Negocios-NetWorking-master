using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    class ClienteRepository: DomainContextRepository<Cliente>, IClienteRepository

    {
        public ClienteRepository(ClubNegociosNetworkingContext context):base(context)
        {

        }

        public Cliente GetClienteByDocumento(string documento)
        {
            return this.Find(c => c.NumeroDocumento == documento).FirstOrDefault();
        }

        
    }
}
