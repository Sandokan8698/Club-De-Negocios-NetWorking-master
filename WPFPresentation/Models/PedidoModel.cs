using System;
using System.Collections.ObjectModel;
using System.Linq;


namespace WPFPresentation.Models
{
    [Serializable]
    public class PedidoModel: CompositeNotifiAbleModel<SubPedidoModel>, ICalculationModel
    {

        #region Property

        private ProveedorModel _proveedor;
        private int _itemNumero;

        public int PedidoId { get; set; }
        public int VentaId { get; set; }
        public VentaModel Venta { get; set; }

        public int ItemNumero
        {
            get { return _itemNumero; }
            set { if (_itemNumero != value) { _itemNumero = value; OnPropertyChanged("ItemNumero"); } }
        }

        public ProveedorModel Proveedor
        {
            get { return _proveedor; }
            set { if (_proveedor != value) { _proveedor = value; OnPropertyChanged("Proveedor"); Notify("Proveedor", value); } }
        }

        public int ProveedorId { get; set; }

        public  ObservableCollection<SubPedidoModel> SubPedidos { get { return CompositeChildrens; } set { CompositeChildrens = value; OnPropertyChanged("SubPedidos"); } }
        public override decimal Abono { get { return SubPedidos.Sum(p => p.Abono); } set { } }
        public override decimal Deuda { get => PrecioProveedor - Abono; set { } }
        public override decimal PrecioProveedor { get => SubPedidos.Sum(p => p.PrecioProveedor); set { } }


        #endregion


    }
}
