using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class TrabajadorEditDialogViewModel : BaseViewModel
    {

        #region Propertys

       
        public CommandModel UpdateTrabajadorComman { get; set; }

        private TrabajadorModel _trabajador;

        public TrabajadorModel Trabajador
        {
            get { return _trabajador; }
            set
            {
                if (_trabajador != value) _trabajador = value; OnPropertyChanged("Trabajador");
            }
        }

        public List<string> TipoAcceso { get; set; }

        public Action CloseAciAction { get; set; }

        #endregion

        #region Ctor

        public TrabajadorEditDialogViewModel(FacadeProvider facadeProvider, TrabajadorModel trabajador, Action close) : base(facadeProvider)
        {
            Trabajador = trabajador;
            CloseAciAction = close;

            UpdateTrabajadorComman = new UpdateTrabajadorCommand(this);
            TipoAcceso = new List<string>() { "Trabajador", "Administrado" };
        }

        #endregion

        #region Helpers

        private void UpdateTrabajador()
        {
            if (!Trabajador.IsValid())
            {
                ModernDialog.ShowMessage(Trabajador.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    Trabajador = FacadeProvider.TrabajadorProvider().Update(Trabajador);
                    ModernDialog.ShowMessage("El Trabajador ha sido actualizado con exito", "Opercion Existosa", MessageBoxButton.OK);
                    CloseAciAction();

                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.ToString(), "Error en la operacion", MessageBoxButton.OK);
                }
        }


        #endregion

        #region Command Region
   

        private class UpdateTrabajadorCommand : CommandModel
        {
            private TrabajadorEditDialogViewModel viewModel;

            public UpdateTrabajadorCommand(TrabajadorEditDialogViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.UpdateTrabajador();
            }

        }

        #endregion

    }
}
