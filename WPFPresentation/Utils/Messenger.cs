using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Utils
{
    /// <summary>
    /// Mensages disponible para eviar y recivir
    /// </summary>
    public enum ViewModelMessages
    {
        SetNavigation,
        ApplayFilterInVentaList,
        RemoveFilterInVentalist,
        ApplayFilterInClienteVenta,
        RemoveFilterInClienteVenta,
        ApplayFilterInSubPedidoReport,
        RemoveFilterInSubPedidoEntryReport,
        ApplayFilterInSubPedidoEntryReport,
        RemoveFilterInSubPedidoReport,
        AddNewTrabajador,
        AddNewCliente,
        AddNewProveedor,
        InitializerFilterValue,
        RemoveFilter,
        LoadVentaLinks,
        ClienteItemsChange,
        ShowVentaDialogDetail
    };

    public sealed class Messenger
    {
        #region Data
        static readonly Messenger instance = new Messenger();
        private volatile object locker = new object();

        private MultiDictionary<ViewModelMessages, Action<Object>> _internaList = new MultiDictionary<ViewModelMessages, Action<object>>();

        #endregion

        #region Ctor

        //CTORs
        static Messenger()
        {

        }

        private Messenger()
        {

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static Messenger Instance
        {
            get { return instance; }
        }

        #endregion

        #region Public Methods

        public void Register(Action<Object> callback, ViewModelMessages messages)
        {
            _internaList.AddValues(messages, callback);
        }

        public void Unregister(Action<Object> callback, ViewModelMessages messages)
        {
            _internaList.RemoveAllValue(messages,o => o.Equals(callback));
        }

        public void NotifyColleagues(ViewModelMessages message,
                       object args)
        {
            if (_internaList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (Action<object> callback in _internaList[message])
                    callback(args);
            }
        }

        #endregion
    }
}
