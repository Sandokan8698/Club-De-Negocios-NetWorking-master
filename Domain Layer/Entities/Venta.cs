using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Venta
    {
        public Venta()
        {
            this.Pedidos = new HashSet<Pedido>();
        }

        public int VentaId { get; set; }

        public int ClienteId { get; set; }
        public  Cliente Cliente { get; set; }

        public int TrabajadorId { get; set; }
        public virtual Trabajador Trabajador { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(20)]
        public string Serie { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }

        [NotMapped]
        public decimal Deuda
        {
            get { return Pedidos.Sum(p => p.Deuda); }
        }
    }
}
