using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using WPFPresentation.Converters;
using WPFPresentation.Models;
using WPFPresentation.Reports.Models;

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for VentaPrintPage.xaml
    /// </summary>
    public partial class VentaPrintPage : ModernWindow
    {
        public VentaPrintPage()
        {
            InitializeComponent();
        }

        private void ReportViewer_OnRenderingComplete(object sender, RenderingCompleteEventArgs e)
        {

        }


        private void VentaPrintPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            

            VentaNumberConverter ventaNumberConverter = new VentaNumberConverter();
            var ventaNumber = ventaNumberConverter.Convert(VentaModels.VentaId, null, null, null);
            

           
            var ventaDataSource = new ReportDataSource("VentaDataSet",
                new List<VentaDataSetModel>()
                {
                    new VentaDataSetModel
                    {
                       Numero = ventaNumber.ToString(),
                       Fecha = VentaModels.Fecha.ToShortDateString(),
                       ClienteCedula = VentaModels.Cliente.NumeroDocumento,
                       ClienteNombre = VentaModels.Cliente.Nombre + " " + VentaModels.Cliente.Apellidos,
                       ClienteDireccion = VentaModels.Cliente.Direccion,
                       ClienteTelefono = VentaModels.Cliente.Telefono,
                       Trabajador = VentaModels.Trabajador.Nombre
                    }
                });

            List<PedidoDataSetModel> pedidoDataSet = new List<PedidoDataSetModel>();

            foreach (var ventaModelsPedido in VentaModels.Pedidos)
            {
                pedidoDataSet.Add(new PedidoDataSetModel
                {
                    ItemNumero = ventaModelsPedido.ItemNumero,
                    NombreProveedor = ventaModelsPedido.Proveedor.Nombre,
                    PrecioProveedor = ventaModelsPedido.PrecioProveedor,
                    Abono =  ventaModelsPedido.Abono,
                    Deuda = ventaModelsPedido.Deuda
                });
            }

            var pedidosDataSource = new ReportDataSource("PedidosDataset",pedidoDataSet);

            ReportViewer.ZoomMode = ZoomMode.PageWidth;
            ReportViewer.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\Reports\VentaReport.rdlc";
            ReportViewer.LocalReport.DataSources.Add(ventaDataSource);
            ReportViewer.LocalReport.DataSources.Add(pedidosDataSource);
            ReportViewer.RefreshReport();

        }

        public VentaModel VentaModels { get; set; }
    }
}
