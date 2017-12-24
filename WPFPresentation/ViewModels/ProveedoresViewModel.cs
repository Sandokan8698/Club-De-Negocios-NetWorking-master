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
    public class ProveedoresViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<ProveedorModel> _proveedores;

        public ObservableCollection<ProveedorModel> Proveedores
        {
            get { return _proveedores; }
            set
            {
                if (_proveedores != value) _proveedores = value; OnPropertyChanged("Proveedores");
            }
        }

        #endregion

        #region Ctor

        public ProveedoresViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            Proveedores = new ObservableCollection<ProveedorModel>();
            Messenger.Instance.Register(o => AddNewProveedor(o), ViewModelMessages.AddNewProveedor);
        }

        public async override void InitializeViewContent()
        {
            IsLoading = true;

            Proveedores = await Task.Run(() =>
            {
                return FacadeProvider.ProveedorProvider().GetAll();
            });

            IsLoading = false;
        }

        private void AddNewProveedor(object o)
        {
            Proveedores.Add((ProveedorModel)o);
        }

        public void ShowEditProveedorDialog(ProveedorModel proveedor)
        {
            ProveedorEditDialog proveedorEditDialog = new ProveedorEditDialog(proveedor);
            proveedorEditDialog.ShowDialog();
        }
        #endregion

    }
}
