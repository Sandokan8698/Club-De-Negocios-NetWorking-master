using System;
using System.Collections.Generic;
using Domain_Layer.Entities;

namespace WPFPresentation.Reports.Models
{
    class VentaDataSetModel
    {
        public List<PedidoDataSetModel> Pedidos { get; set; }
        public List<DailyPaymentDataSetModel> DailyPayments { get; set; }

        public string Numero { get; set; }

        public string Fecha { get; set; }

        public string Trabajador { get; set; }

        public string ClienteCedula { get; set; }

        public String ClienteNombre { get; set; }

        public string ClienteDireccion { get; set; }

        public string ClienteTelefono { get; set; }
    }
}
