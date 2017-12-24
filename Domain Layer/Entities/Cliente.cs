using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain_Layer.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Apellidos { get; set; }
       
        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(11)]
        [Required]
        [Index(IsUnique =  true)]
        public string NumeroDocumento { get; set; }

        [MaxLength(100)]
        public string Direccion { get; set; }

        [MaxLength(10)]
        public String Telefono { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Venta> Ventas { get; set; }


    }
}
