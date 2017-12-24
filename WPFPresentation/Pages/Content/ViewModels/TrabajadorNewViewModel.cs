using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content.ViewModels
{
    public class TrabajadorNewViewModel : BaseViewModel
    {

        #region Propertys

        public CommandModel SaveTrabajadorComman { get; set; }
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


        #endregion

        #region Ctor

        public TrabajadorNewViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            Trabajador = new TrabajadorModel();
            SaveTrabajadorComman = new SaveTrabajadorCommand(this);
            UpdateTrabajadorComman = new UpdateTrabajadorCommand(this);
            TipoAcceso = new List<string>(){"Trabajador", "Administrador"};
        }

        #endregion

        #region Helpers

        private void SaveTrabajador()
        {
            if (!Trabajador.IsValid())
            {
                ModernDialog.ShowMessage(Trabajador.Error, "Error en la operacion", MessageBoxButton.OK);
            }
            else

                try
                {
                    var trabajador = FacadeProvider.TrabajadorProvider().Add(Trabajador);
                    
                    //Le decimos al newControlViewModel TrabajadoresViewModel que se ha creado un nuevo trabajador
                    //que haga lo que quiere con el
                    Messenger.Instance.NotifyColleagues(ViewModelMessages.AddNewTrabajador,trabajador);

                    Trabajador = new TrabajadorModel();
                    ModernDialog.ShowMessage("El Trabajador ha sido agregado con exito", "Opercion Existosa", MessageBoxButton.OK);

                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.ToString(), "Error en la operacion", MessageBoxButton.OK);
                }
           

        }

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
                    
                    Trabajador = new TrabajadorModel();
                    ModernDialog.ShowMessage("El Trabajador ha sido actualizado con exito", "Opercion Existosa", MessageBoxButton.OK);

                }
                catch (Exception exception)
                {
                    ModernDialog.ShowMessage(exception.ToString(), "Error en la operacion", MessageBoxButton.OK);
                }
        }


        #endregion

        #region Command Region

        private class SaveTrabajadorCommand : CommandModel
        {
            private TrabajadorNewViewModel viewModel;

            public SaveTrabajadorCommand(TrabajadorNewViewModel viewModel)
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
               viewModel.SaveTrabajador();
            }

        }

        private class UpdateTrabajadorCommand : CommandModel
        {
            private TrabajadorNewViewModel viewModel;

            public UpdateTrabajadorCommand(TrabajadorNewViewModel viewModel)
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

        
    }

    #endregion
}

