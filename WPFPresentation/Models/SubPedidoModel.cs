using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Models
{
    public class SubPedidoModel: CompositeNotifiAbleModel<SubPedidoEntryModel>, ICalculationModel,ICloneable
    {
        #region Property
       
        private DateTime _fechaCreacion;
        private DateTime _fechaActualizacion;

       
        public int SubPedidoId { get; set; }
        public int PedidoId { get; set; }
        public PedidoModel Pedido { get; set; }

        public ObservableCollection<SubPedidoEntryModel> SubPedidosEntrys { get { return CompositeChildrens; } set { CompositeChildrens = value; OnPropertyChanged("SubPedidos"); } }

        public override decimal Abono { get { return SubPedidosEntrys.Sum(p => p.Abono); } set { } }

        public override decimal Deuda
        {
            get { return PrecioProveedor - Abono; }
            set {}
        }
        public override decimal PrecioProveedor
        {
            get { return _precioProveedor; }
            set
            {
                if (_precioProveedor != value)
                {
                    _precioProveedor = value;
                    OnPropertyChanged("PrecioProveedor");
                   
                    Notify("PrecioProveedor", value);
                }
            }
        }

        public DateTime FechaCreacion {
            get { return _fechaCreacion; }
            set
            {
                if (_fechaCreacion != value)
                {
                    _fechaCreacion = value; OnPropertyChanged("FechaCreacion");            
                }
            }
        }       
        public DateTime FechaActualizacion
        {
            get { return _fechaActualizacion; }
            set
            {
                if (_fechaActualizacion != value)
                {
                    _fechaActualizacion = value; OnPropertyChanged("FechaActualizacion");
                }
            }
        }
        public string Identificador { get; set; }
        
        #endregion

        #region Ctor
        public SubPedidoModel()
        {           
            FechaCreacion = DateTime.Now;
        }
        #endregion

        #region Helper Methods
        
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
