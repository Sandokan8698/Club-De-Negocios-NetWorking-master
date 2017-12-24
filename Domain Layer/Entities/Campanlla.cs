using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Campanlla
    {
        public int CampanllaId { get; set; }

        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
       
        [Column(TypeName = "date")]
        [Required]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCierre { get; set; }

        public bool IsActive { get; set; }

        public int Numero { get; set; }

    }
}
