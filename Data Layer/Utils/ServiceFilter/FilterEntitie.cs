using System;
using Domain_Layer.Entities;

namespace Data_Layer.Utils.ServiceFilter
{
    public class FilterEntitie
    {
        public Cliente Cliente { get; set; }

        public DateTime Desde { get; set; }

        public DateTime Hasta { get; set; }

        public int VentaId { get; set; }
        public Proveedor Proveedor { get; set; }

        public string Identificador { get; set; }

        public bool UsarFecha { get; set; }

    }
}
