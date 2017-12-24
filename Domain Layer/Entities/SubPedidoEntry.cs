using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class SubPedidoEntry
    {
        public int SubPedidoEntryId { get; set; }

        public int SubPedidoId { get; set; }
        public SubPedido SubPedido { get; set; }

        public int? TrabajadorId { get; set; }
        public Trabajador Trabajador { get; set; }

        [Column(TypeName = "Money")]
        [Required]
        public decimal Abono { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
