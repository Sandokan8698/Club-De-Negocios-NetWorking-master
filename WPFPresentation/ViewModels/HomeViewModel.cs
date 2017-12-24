using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        #region Propertys

        private string _username;

        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username != value) _username = value; OnPropertyChanged("UserName");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value) _password = value; OnPropertyChanged("Password");
            }
        }

        #endregion


        private Task<TrabajadorModel> Login(string user, string password)
        {
            return Task.Run(() =>
            {
                if (user == "Admin" && password == "admin")
                {
                    Authenticator.Instance.Trabajador = new TrabajadorModel { Nombre = "Admin", Acceso = "Administrador" };
                    return Authenticator.Instance.Trabajador;
                }
       
                  return  Authenticator.Instance.Authenticated(user, password);
                
                      
            });


        }


        public async void Login(FrameworkElement element)
        {
            if (Authenticator.Instance.IsAuthenticade)
            {
                ModernDialog.ShowMessage("Debe reiniciar la aplicación para logearse como otro usuario", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
                return;
            }

            IsLoading = true;

            var trabajador = await Login(UserName, Password);

            UserName = "";
            Password = "";

            IsLoading = false;



            if (trabajador != null)
            {
                Messenger.Instance.NotifyColleagues(ViewModelMessages.SetNavigation, null);
                NavigatorHelper.Instance.Navigate("/Pages/VentaNewPage.xaml", element);
            }
            else
            {
                ModernDialog.ShowMessage("La combinacion de usuario y contraseña no coinciden", "ERROR EN LA OPERACIÓN", MessageBoxButton.OK);
            }
        }


    }
}
