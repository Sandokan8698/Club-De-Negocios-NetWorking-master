using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entities;

namespace WPFPresentation.Reports.Models
{
    class PedidoDataSetModel
    {
        public int ItemNumero { get; set; }

        public string NombreProveedor { get; set; }

        public decimal PrecioProveedor { get; set; }

        public decimal Abono { get; set; }

        public decimal Deuda { get; set; }
    }
}
