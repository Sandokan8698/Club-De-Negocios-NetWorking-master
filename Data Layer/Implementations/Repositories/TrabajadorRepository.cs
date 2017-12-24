using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class TrabajadorRepository : DomainContextRepository<Trabajador>, ITrabajadorRepository
    {
        public TrabajadorRepository(ClubNegociosNetworkingContext context) : base(context)
        {
        }

        public Trabajador Auhtenticate(string username, string password)
        {
          return  Find(t => t.Usuario == username && t.Password == password).FirstOrDefault();
        }
    }

}
