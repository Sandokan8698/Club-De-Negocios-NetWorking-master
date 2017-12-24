using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Utils
{
    public sealed class Authenticator
    {
        #region Data
        static readonly Authenticator instance = new Authenticator();
        private volatile object locker = new object();
        private FacadeProvider _facadeProvider;
        
        #endregion

        #region Ctor

        //CTORs
        static Authenticator()
        {

        }

        private Authenticator()
        {
            _facadeProvider = DependencyResolver.Instance.FacadeProvider;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static Authenticator Instance
        {
            get { return instance; }
        }

        #endregion

        #region Public Methods

        public TrabajadorModel Authenticated(string username, string password)
        {
            var trabajador = _facadeProvider.TrabajadorProvider().Auhtenticate(username, password);
            if (trabajador != null)
            {
                IsAuthenticade = true;
                Trabajador = trabajador;
            }

            return trabajador;
        }

        #endregion

        #region Propertys

        public bool IsAuthenticade { get; set; }

        public TrabajadorModel Trabajador { get; set; }

        public bool IsAdming { get { return Trabajador.Acceso == "Administrador"; } }
        #endregion

    }
}
