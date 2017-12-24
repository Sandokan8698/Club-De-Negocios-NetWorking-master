using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entities;

namespace Data_Layer.Abstract.Repositories
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        Cliente GetClienteByDocumento(string documento);
    }
}

