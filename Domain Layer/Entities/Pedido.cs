using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain_Layer.Entities
{
    public class Pedido
    {
        public Pedido()
        {
            this.SubPedidos = new HashSet<SubPedido>();
        }

        public int PedidoId { get; set; }

        public int ItemNumero { get; set; }
       
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int VentaId { get; set; }
        public  Venta Venta { get; set; }   


        [NotMapped]
        public decimal PrecioProveedor { get { return SubPedidos.Sum(sp => sp.PrecioProveedor); }}
        [NotMapped]
        public decimal Abono { get { return SubPedidos.Sum(sp => sp.Abono); }}    
        [NotMapped]
        public decimal Deuda { get { return SubPedidos.Sum(sp => sp.Deuda); } }
        public ICollection<SubPedido> SubPedidos { get; set; }

    }
}
