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
    public class TrabajadoresViewModel : BaseViewModel
    {

        #region Propertys

        private ObservableCollection<TrabajadorModel> _trabajadores;

        public ObservableCollection<TrabajadorModel> Trabajadores
        {
            get { return _trabajadores; }
            set
            {
                if (_trabajadores != value) _trabajadores = value; OnPropertyChanged("Trabajadores");
            }
        }

       

        #endregion

        #region Ctor
        public TrabajadoresViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            Messenger.Instance.Register(o => AddTrabajador(o), ViewModelMessages.AddNewTrabajador);
        }

        public async override void InitializeViewContent()
        {
            IsLoading = true;

            Trabajadores = await Task<ObservableCollection<TrabajadorModel>>.Run(() =>
            {
                return FacadeProvider.TrabajadorProvider().GetAll();
            });

            IsLoading = false;
        }
        #endregion

        #region Helpers

        private void AddTrabajador(object o)
        {
            Trabajadores.Add((TrabajadorModel)o);
        }

        public void ShowEditTrabajadorDialog(TrabajadorModel trabajador)
        {
            TrabajadorEditDialogPage editDialogPage = new TrabajadorEditDialogPage(trabajador);
            editDialogPage.ShowDialog();
        }
        #endregion

    }
}
