using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Entities
{
    public class Trabajador
    {
        public int TrabajadorId { get; set; }

        [MaxLength(20)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(40)]
        [Required]
        public string Apellidos { get; set; }


        [MaxLength(20)]
        [Required]
        public string Acceso { get; set; }

        [MaxLength(20)]
        [Required]
        public string Usuario { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }


    }
}
