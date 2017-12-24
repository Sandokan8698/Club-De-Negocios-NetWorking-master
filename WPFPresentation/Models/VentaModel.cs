using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Models
{
    public class VentaModel : CompositeNotifiAbleModel<PedidoModel>, ICalculationModel
    {


        #region Property

        
        private ClienteModel _cliente;

        public int TrabajadorId
        {
            get { return _trabajadorId; }
            set { if (_trabajadorId != value) { _trabajadorId = value; OnPropertyChanged("TrabajadorId"); } }
        }

        private int _trabajadorId;
        public TrabajadorModel Trabajador { get; set; }

        public int ClienteId { get; set; }

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set { if (_cliente != value) { _cliente = value; OnPropertyChanged("Cliente"); Notify("Cliente",value);} }
        }

       
        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha != value)
                {
                    _fecha = value;
                    OnPropertyChanged("Fecha");
                }
            }
        }

        public ObservableCollection<PedidoModel> Pedidos { get { return CompositeChildrens; } set { CompositeChildrens = value; OnPropertyChanged("Pedidos");} }
        public int VentaId { get; set; }
        public override decimal Abono { get { return Pedidos.Sum(p => p.Abono); } set {} }
        public override decimal Deuda { get => PrecioProveedor - Abono; set {} }
        public override decimal PrecioProveedor { get => Pedidos.Sum(p => p.PrecioProveedor); set{} }

        #endregion


        #region Ctor

        public VentaModel()
        {
            TrabajadorId = Authenticator.Instance.Trabajador.TrabajadorId;
            Fecha = DateTime.Now;
        }

        #endregion

        
    }
}
