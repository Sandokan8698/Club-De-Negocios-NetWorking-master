using System;
using System.Collections.Generic;
using WPFPresentation.Utils;

namespace WPFPresentation.Models
{
    public class SubPedidoEntryModel : BaseModel, ICalculationModel, ISubject
    {   
        #region Property

        protected decimal _abono;
        protected decimal _deuda;
        protected decimal _precioProveedor;

        public int SubPedidoEntryId { get; set; }

        public Dictionary<string, Action<object>> Observers { get; private set; }
        public int SubPedidoId { get; set; }

        public  decimal Abono
        {
            get { return _abono; }
            set
            {
                if (_abono != value)
                {
                    _abono = value; OnPropertyChanged("Abono");                   
                    Notify("Abono", value);

                }
            }
        }

        public SubPedidoModel SubPedido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Deuda { get; set; }

        public decimal PrecioProveedor { get; set; }

        #endregion

        #region Ctor

        public SubPedidoEntryModel()
        {
            Observers = new Dictionary<string, Action<object>>();
            FechaCreacion = DateTime.Now;
        }

        #endregion

        #region Methods

  
        public void Attach(string propertyName, Action<object> observerFunc)
        {
            Observers.Add(propertyName, observerFunc);
        }

        public void Notify(string propertyName, object objectAsValue)
        {
            foreach (KeyValuePair<string, Action<object>> keyValuePair in Observers)
            {
                if (keyValuePair.Key == propertyName) { keyValuePair.Value(objectAsValue); }

            }
        }

        #endregion
    }
}
