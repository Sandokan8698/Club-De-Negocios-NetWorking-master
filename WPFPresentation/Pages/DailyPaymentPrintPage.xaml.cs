using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using WPFPresentation.Converters;
using WPFPresentation.Models;
using WPFPresentation.Reports.Models;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for DailyPaymentPrintPage.xaml
    /// </summary>
    public partial class DailyPaymentPrintPage : ModernWindow
    {
        public DailyPaymentPrintPage(VentaModel venta, List<SubPedidoEntryModel> subPedidoEntrys)
        {
            InitializeComponent();

            VentaNumberConverter ventaNumberConverter = new VentaNumberConverter();
            var ventaNumber = ventaNumberConverter.Convert(venta.VentaId, null, null, null);



            var ventaDataSource = new ReportDataSource("VentaDataSet",
                new List<VentaDataSetModel>()
                {
                    new VentaDataSetModel
                    {
                        Numero = ventaNumber.ToString(),
                        Fecha = venta.Fecha.ToShortDateString(),
                        ClienteCedula = venta.Cliente.NumeroDocumento,
                        ClienteNombre = venta.Cliente.Nombre,
                        ClienteDireccion = venta.Cliente.Direccion,
                        ClienteTelefono = venta.Cliente.Telefono,
                        Trabajador = venta.Trabajador.Nombre
                    }
                });


            List<DailyPaymentDataSetModel> dailyPaymentDataSet = new List<DailyPaymentDataSetModel>();

            foreach (var subPedidoEntryModel in subPedidoEntrys)
            {
                dailyPaymentDataSet.Add( new DailyPaymentDataSetModel
                {
                    Identificador = subPedidoEntryModel.SubPedido.Identificador,
                    Abono = subPedidoEntryModel.Abono,
                    Deuda = subPedidoEntryModel.SubPedido.Deuda
                });
            }
           
            var dailyPaymentDataSource = new ReportDataSource("DailyPaymentsDataSet", dailyPaymentDataSet);

            ReportViewer.ZoomMode = ZoomMode.PageWidth;
            ReportViewer.LocalReport.ReportPath =  Directory.GetCurrentDirectory() + @"\Reports\DailyPaymentReport.rdlc";
            ReportViewer.LocalReport.DataSources.Add(ventaDataSource);
            ReportViewer.LocalReport.DataSources.Add(dailyPaymentDataSource);
            ReportViewer.RefreshReport();
        }

      
    }
}
