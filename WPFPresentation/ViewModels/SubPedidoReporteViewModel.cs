using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class SubPedidoReporteViewModel : BaseViewModel, IPagiAble
    {
        #region Propertys

        private ObservableCollection<Object> _subPedidos;

        private FilterModel _filter;

        public ObservableCollection<Object> SubPedidos
        {
            get { return _subPedidos; }
            set
            {
                if (_subPedidos != value) _subPedidos = value; OnPropertyChanged("SubPedidos");
            }
        }

        private decimal _precicoProveedor;
        public decimal PrecioProveedor
        {
            get { return _precicoProveedor; }
            set
            {
                if (_precicoProveedor != value) _precicoProveedor = value; OnPropertyChanged("PrecioProveedor");
            }
        }


        private decimal _abono;
        public decimal Abono
        {
            get { return _abono; }
            set
            {
                if (_abono != value) _abono = value; OnPropertyChanged("Abono");
            }
        }


        private decimal _deuda;
        public decimal Deuda
        {
            get { return _deuda; }
            set
            {
                if (_deuda != value) _deuda = value; OnPropertyChanged("Deuda");
            }
        }
        public IPaginator Paginator { get; set; }


        #endregion


        #region Ctor
        public SubPedidoReporteViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            //Registramos un oyente para escuchar por el filter apply
            Messenger.Instance.Register(ApplyFilter, ViewModelMessages.ApplayFilterInSubPedidoReport);

            //Registramos un oyente parar escuchar para remover el filtro
            Messenger.Instance.Register(RemoveFilter, ViewModelMessages.RemoveFilterInSubPedidoReport);

            _filter = new FilterModel();

            
        }


        public async override void InitializeViewContent()
        {
            //Comprobamos que no haya filtro aplicado sobre esta viewmodel
            //si lo hay entonces cargamos los supedidos filtrados

            IsLoading = true;

            if (Paginator != null)
            {
                SubPedidos = await Task.Run(() =>
                {
                    var subpedido =  FacadeProvider.SubPedidoProvider().PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter);

                    return subpedido;
                });

                await Task.Run(() =>
                {
                    Paginator.SetData();
                });

                await Task.Run(() =>
                {
                   var results =  FacadeProvider.SubPedidoProvider().ApplayFilter(_filter);

                    PrecioProveedor = 0;
                    Abono = 0;
                    Deuda = 0;

                    foreach (var subPedidoModel in results)
                    {
                        PrecioProveedor += subPedidoModel.PrecioProveedor;
                        Abono += subPedidoModel.Abono;
                        Deuda += subPedidoModel.Deuda;
                    }


                });
            }

            IsLoading = false;
        }


        private void ApplyFilter(object filterParam)
        {
            if (!IsLoading)
            {
                _filter = (FilterModel)filterParam;
                Paginator.ResetData();
                InitializeViewContent();
            }
            
        }

        private void RemoveFilter(object param)
        {
            _filter = new FilterModel();
            Paginator.ResetData();
            InitializeViewContent();
        }

        public int  GetTotalItems()
        {        
             return FacadeProvider.SubPedidoProvider().ApplayFilter(_filter).Count();
        }

        public async void Paginate(int page, int pageSize)
        {
            IsLoading = true;

            SubPedidos = await Task.Run(() =>
            {
                return FacadeProvider.SubPedidoProvider().PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter);
            });      

           IsLoading = false;
        }

        public void SetPaginator(IPaginator paginator)
        {
            Paginator = paginator;
        }

        public void ShowVentaDialog(int ventaId)
        {
            var venta = FacadeProvider.VentaProvider().Get(ventaId);
            VentaDetailDialogPage ventaDetailViewModel = new VentaDetailDialogPage(venta);
            ventaDetailViewModel.ShowDialog();
        }
    }
        #endregion

    
}
