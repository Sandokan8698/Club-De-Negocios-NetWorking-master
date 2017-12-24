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
    public abstract class CompositeNotifiAbleModel<T>: NotifiAbleModel<T> where T : class, ICalculationModel, ISubject,  new()
    {
        

        #region Propertys


        protected ObservableCollection<T> CompositeChildrens{ get; set;}

        #endregion

        #region MyRegion

        public CompositeNotifiAbleModel() : base()
        {
            CompositeChildrens = new ObservableCollection<T>();
            CompositeChildrens.CollectionChanged += CompositeChildrensOnCollectionChanged;
        }

        #endregion


        #region Methods


        private void CompositeChildrensOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            var children = e.NewItems != null ? (T)e.NewItems[0] : null;

            if (children != null)
            {
                children.Attach("Abono", o => UpdateAbono(o));
                children.Attach("Deuda", o => UpdateDeuda(o));
                children.Attach("PrecioProveedor", o => UpdatePrecioProveedor(o));

            }

           UpdateAll(children);

        }

        public virtual T Add(T component)
        {
            CompositeChildrens.Add(component);
            CurrentChildren = new T();
            UpdateAll(component);
            return component;
        }
        public virtual T Remove(T component)
        {
            CompositeChildrens.Remove(component);
            UpdateAll(component);
            return component;
        }
        private void UpdateAll(T component)
        {
            if (component != null)
            {
                UpdateAbono(component.Abono);
                UpdatePrecioProveedor(component.PrecioProveedor);
               // UpdateDeuda(component.Deuda);
            }
           
        }

        #endregion

    }
}
