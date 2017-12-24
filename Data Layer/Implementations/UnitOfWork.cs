using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Implementations.Repositories;

namespace Data_Layer.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClubNegociosNetworkingContext _context;

        public UnitOfWork(ClubNegociosNetworkingContext context)
        {
            _context = context;
            
            ProveedorRepository = new ProveedorRepository(_context);
            VentaRepository = new VentaRepository(_context);
            ClienteRepository = new ClienteRepository(_context);
            CampanllaRepository = new CampanllaRepository(_context);
            PedidoRepositoy = new PedidoRepository(_context);
            SubPedidoEntryRepository = new SubpedidoEntryRepository(_context);
            SubPedidoRepository = new SubPedidoRepository(_context);
            TrabajadorRepository = new TrabajadorRepository(_context);
          
        }


        public IProveedorRepository ProveedorRepository { get; private set; }
        public IVentaRepository VentaRepository { get; private set; }
        public IClienteRepository ClienteRepository { get; private set ; }
        public ICampanllaRepository CampanllaRepository { get; private set; }
        public IPedidoRepositoy PedidoRepositoy  { get; private set; }
        public ISubPedidoEntryRepository SubPedidoEntryRepository { get; private set; }
        public ISubPedidoRepository SubPedidoRepository { get; private set; }
       
        public ITrabajadorRepository TrabajadorRepository { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
