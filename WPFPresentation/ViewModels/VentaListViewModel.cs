using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class VentaListViewModel : BaseViewModel, IPagiAble
    {
        #region Propertys

        private ObservableCollection<VentaModel> _ventas;

        public ObservableCollection<VentaModel> Ventas
        {
            get { return _ventas; }
            set
            {
                if (_ventas != value)
                {
                    _ventas = value;
                    OnPropertyChanged("Ventas");

                }
            }
        }

        /// <summary>
        /// Mantiene el valor del filtrado aplicado a esta colleccion de ventas en caso
        /// de que se halla realizado
        /// </summary>
        private FilterModel _filter;

        public IPaginator Paginator { get; set; }

        #endregion


        #region Ctor
        public VentaListViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            //Registramos un oyente para escuchar por el filter apply
            Messenger.Instance.Register(ApplyFilter, ViewModelMessages.ApplayFilterInVentaList);

            //Registramos un oyente parar escuchar para remover el filtro
            Messenger.Instance.Register(RemoveFilter, ViewModelMessages.RemoveFilterInVentalist);

            _filter = new FilterModel();
        }
        #endregion


        #region Methods

        public override async void InitializeViewContent()
        {
            IsLoading = true;

            if (Paginator != null)
            {
                Ventas = await Task.Run(() =>
                    FacadeProvider.VentaProvider()
                        .PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter));


                await Task.Run(() =>
                {
                    Paginator.SetData();
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

        public void ShowVentaEditDialog(VentaModel venta)
        {
            VentaDetailDialogPage ventaDetailDialogPage = new VentaDetailDialogPage(venta);
            ventaDetailDialogPage.ShowDialog();
        }

        public void RemoveVenta(VentaModel venta)
        {
            if (Authenticator.Instance.IsAdming)
            {
                try
                {
                    var dlg = new ModernDialog
                    {
                        Title = "Aviso",
                        Content = "Este elemento se eliminara permanente, desea continuar ?"
                    };
                    dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
                    dlg.ShowDialog();

                    if (dlg.MessageBoxResult == MessageBoxResult.OK)
                    {
                        FacadeProvider.VentaProvider().Remove(venta);
                        Ventas.Remove(venta);
                    }
                }
                catch (Exception e)
                {
                    ModernDialog.ShowMessage(e.Message, "Error en la Operación", MessageBoxButton.OK);
                }
            }
            else
            {
                ModernDialog.ShowMessage("No tiene permiso para realizar esta operación", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
            }

        }

        public int GetTotalItems()
        {
            return FacadeProvider.VentaProvider().Filter(_filter).Count;
        }

        public async void Paginate(int page, int pageSize)
        {
            IsLoading = true;

            Ventas = await Task.Run(() =>
                FacadeProvider.VentaProvider()
                    .PaginateFiltered(Paginator.ActualPage, Paginator.ItemsPerPage, _filter));

            IsLoading = false;
        }

        public void SetPaginator(IPaginator paginator)
        {
            Paginator = paginator;
        }


        #endregion

    }

}
