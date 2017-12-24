using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Utils
{
    public sealed class InMemoryHelper
    {
        #region Data

        static readonly InMemoryHelper instance = new InMemoryHelper();
        private FacadeProvider _facadeProvider;
        
        #endregion

        #region Ctor

        //CTORs
        static InMemoryHelper()
        {

        }

        private InMemoryHelper()
        {
            _facadeProvider = DependencyResolver.Instance.FacadeProvider;

            Messenger.Instance.Register(o => ActualizeProveedorList(o), ViewModelMessages.AddNewProveedor);
            Messenger.Instance.Register(o => ActualizeClienteList(o), ViewModelMessages.AddNewCliente);
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static InMemoryHelper Instance
        {
            get { return instance; }
        }

        private ObservableCollection<ClienteModel> _clientes;

        public ObservableCollection<ClienteModel> Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = _facadeProvider.ClienteProvider().GetAll();
                }

                return _clientes;

            }

        }

        private ObservableCollection<ProveedorModel> _proveedores;

        public ObservableCollection<ProveedorModel> Proveedores
        {
            get
            {
                if (_proveedores == null)
                {
                    _proveedores = _facadeProvider.ProveedorProvider().GetAll();
                }

                return _proveedores;

            }

        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        /// <summary>
        /// Manejador del evento AddNewProveedor para cuando se agrege un nuevo proveedor se actualize la lista 
        /// de proveedores sobres los que se realiza la busqueda en este viewmodel
        /// </summary>
        /// <param name="o"></param>
        private void ActualizeProveedorList(object o)
        {
            Proveedores.Add((ProveedorModel)o);
        }

        private void ActualizeClienteList(object o)
        {
            Clientes.Add((ClienteModel)o);
        }
        #endregion
    }
}
