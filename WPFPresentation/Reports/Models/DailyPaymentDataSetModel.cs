using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Reports.Models
{
    class DailyPaymentDataSetModel
    {
        public string Identificador { get; set; }

        public decimal Deuda { get; set; }

        public decimal Abono { get; set; }
    }
}
