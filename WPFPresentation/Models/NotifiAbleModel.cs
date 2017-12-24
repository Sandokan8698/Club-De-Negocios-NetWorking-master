using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Utils;

namespace WPFPresentation.Models
{
    public abstract class NotifiAbleModel<T> : BaseModel, ISubject where T: new()
    {
        #region Property    

        protected decimal _abono;
        protected decimal _deuda;
        protected decimal _precioProveedor;
        private T _currentChildren;

        public T CurrentChildren
        {
            get { return _currentChildren; }
            set{  _currentChildren = value; OnPropertyChanged("CurrentChildren"); }
        }

        public T SelectedChildren { get; set; }

        public Dictionary<string, Action<object>> Observers { get; private set; }

        public abstract decimal Abono { get; set; }

        public abstract decimal Deuda { get; set; }

        public abstract decimal PrecioProveedor { get; set; }
        #endregion

        #region Ctor

        public NotifiAbleModel()
        {
                  
            CurrentChildren = new T();
            SelectedChildren = new T();
            Observers = new Dictionary<string, Action<object>>();
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
        protected virtual void UpdateAbono(object o)
        {
            OnPropertyChanged("Abono");
            Notify("Abono", o);
            UpdateDeuda(o);

        }
        protected virtual void UpdateDeuda(object o)
        {
            OnPropertyChanged("Deuda");
            Notify("Deuda", o);
        }
        protected virtual void UpdatePrecioProveedor(object o)
        {
            OnPropertyChanged("PrecioProveedor");
            Notify("PrecioProveedor", o);
            UpdateDeuda(o);
        }



        #endregion
    }
}
