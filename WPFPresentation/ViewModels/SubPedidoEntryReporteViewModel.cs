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
    public class SubPedidoEntryReporteViewModel : BaseViewModel, IPagiAble
    {

        #region Propertys

        private ObservableCollection<Object> _subPedidoEntrys;
        private FilterModel _filter;

        public ObservableCollection<Object> SubPedidoEntrys
        {
            get { return _subPedidoEntrys; }
            set
            {
                if (_subPedidoEntrys != value) _subPedidoEntrys = value; OnPropertyChanged("SubPedidoEntrys");
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
        
        public IPaginator Paginator { get; set; }


        #endregion


        #region Ctor
        public SubPedidoEntryReporteViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            //Registramos un oyente para escuchar por el filter apply
            Messenger.Instance.Register(ApplyFilter, ViewModelMessages.ApplayFilterInSubPedidoEntryReport);

            //Registramos un oyente parar escuchar para remover el filtro
            Messenger.Instance.Register(RemoveFilter, ViewModelMessages.RemoveFilterInSubPedidoEntryReport);

            _filter = new FilterModel();


        }


        public async override void InitializeViewContent()
        {
            //Comprobamos que no haya filtro aplicado sobre esta viewmodel
            //si lo hay entonces cargamos los supedidos filtrados

            IsLoading = true;

            if (Paginator != null)
            {
                SubPedidoEntrys = await Task.Run(() =>
                {
                    var subpedidoEntry = FacadeProvider.SubPedidoEntryProvider().PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter);

                    return subpedidoEntry;
                });

                await Task.Run(() =>
                {
                    Paginator.SetData();
                });

                await Task.Run(() =>
                {
                    var results = FacadeProvider.SubPedidoEntryProvider().ApplayFilter(_filter);
                  
                    Abono = 0;
                   

                    foreach (var subPedidoModel in results)
                    {                 
                        Abono += subPedidoModel.Abono;                  
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

        public int GetTotalItems()
        {
            return FacadeProvider.SubPedidoEntryProvider().ApplayFilter(_filter).Count();
        }

        public async void Paginate(int page, int pageSize)
        {
            IsLoading = true;

            SubPedidoEntrys = await Task.Run(() =>
            {
                return FacadeProvider.SubPedidoEntryProvider().PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter);
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

