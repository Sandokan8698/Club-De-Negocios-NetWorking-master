
using System;
using Data_Layer.Abstract.Repositories;

namespace Data_Layer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProveedorRepository ProveedorRepository { get; }
        IVentaRepository VentaRepository { get;  }
        IClienteRepository ClienteRepository { get; }
        ICampanllaRepository CampanllaRepository { get; }
        IPedidoRepositoy PedidoRepositoy { get; }
        ISubPedidoEntryRepository SubPedidoEntryRepository { get; }
        ISubPedidoRepository SubPedidoRepository { get;  }
        ITrabajadorRepository TrabajadorRepository { get;  }
       
        int Complete();
    }
}