using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Nombre { get; set; }    

        [MaxLength(100)]
        public string Direccion { get; set; }

        [MaxLength(10)]
        public String Telefono { get; set; }

        [MaxLength(50)]
        public string NumeroCuenta { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string Url { get; set; }

        public  ICollection<Pedido> Pedidos { get; set; }

    }
}
