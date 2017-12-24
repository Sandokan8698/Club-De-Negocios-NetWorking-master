using System;
using Domain_Layer.Entities;

namespace WPFPresentation.Models
{
    public class CampanllaModel
    {
        public int CampanllaId { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaCierre { get; set; }

        public bool IsActive { get; set; }

        public int Numero { get; set; }
    }
}
