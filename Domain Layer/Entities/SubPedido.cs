using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class SubPedido
    {
        public SubPedido()
        {
            SubPedidosEntrys = new HashSet<SubPedidoEntry>();
        }

        public int SubPedidoId { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [Index]
        [Required]
        public string Identificador { get; set; }


        [Column(TypeName = "date")]
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaActualizacion { get; set; }

        public ICollection<SubPedidoEntry> SubPedidosEntrys { get; set; }

        [Column(TypeName = "Money")]
        [Required]
        public decimal PrecioProveedor { get; set; }

        [NotMapped]
        public decimal Abono { get { return SubPedidosEntrys.Sum(spe => spe.Abono); } }

        [NotMapped]
        public decimal Deuda { get { return PrecioProveedor - Abono; } }
    }
}
