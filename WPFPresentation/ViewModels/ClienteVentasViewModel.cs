using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class ClienteVentasViewModel : BaseViewModel
    {
        #region Propertys

        private ObservableCollection<VentaModel> _ventas;
        public ObservableCollection<VentaModel> Ventas
        {
            get { return _ventas; }
            set { if (_ventas != value) { _ventas = value; OnPropertyChanged("Ventas"); } }
        }

        private VentaModel _currentVenta;
        public VentaModel CurrVenta
        {
            get { return _currentVenta; }
            set { if (_currentVenta != value) { _currentVenta = value; OnPropertyChanged("CurrVenta"); } }
        }

        public ClienteModel Cliente { get; set; }

        private FilterModel _filter;

        #endregion


        #region Ctor
        public ClienteVentasViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
        }
        #endregion


        #region Methods  

        public override void InitializeViewContent()
        {
            //Escuchamos por el evento del cambio de item o sea el cambio en la 
            //seleccion de un cliente del listbox de clientes que ocurre en VentaListPage
            Messenger.Instance.Register(LoadContent, ViewModelMessages.ClienteItemsChange);

            
            //Escuchamos el evento mensage applyfilter lanzado desde el filter usercontro
            //que esta en la misma pagina em el page de este viewmodel en este caso 
            //ClienteListPage
            Messenger.Instance.Register(ApplyFilter, ViewModelMessages.ApplayFilterInClienteVenta);

            //Similar a lo anterior pero para removel los filtros
            Messenger.Instance.Register(RemoveFilter, ViewModelMessages.RemoveFilterInClienteVenta);

            

            base.InitializeViewContent();
        }


        /// <summary>
        /// Methodo que se ejecuta cuando la propiedad CurrentCliente del ViewModel 
        /// ClientesViewModel cambia
        /// </summary>
        /// <param name="param"></param>
        private void LoadContent(object param)
        {
            var paramList = (List<Object>)param;
            Cliente = (ClienteModel)paramList[1];
            LoadContent();
        }

        /// <summary>
        /// Methodo que actualiza las ventas para el cliente de la del este ViewModel
        /// </summary>
        private void LoadContent()
        {
            Ventas = _filter == null ? FacadeProvider.ClienteProvider().GetClientVentas(Cliente)
                : FacadeProvider.ClienteProvider().GetClientVentasFiltered(Cliente, _filter);
        }

        /// <summary>
        /// Lanzamos la Pagina que edita las venta seleccionada
        /// </summary>
        /// <param name="param"></param>
        public void ShowClienteVentaDetailPage(object param)
        {
            VentaDetailDialogPage ventaDetail = new VentaDetailDialogPage((VentaModel)param);
            ventaDetail.ShowDialog();
        }   

        private void ApplyFilter(object param)
        {
            _filter = (FilterModel) param;
            LoadContent();
           
        }

        private void RemoveFilter(object param)
        {
            _filter = null;
            LoadContent();
        }

        #endregion
    }

}
