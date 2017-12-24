using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Utils
{
    public sealed class NavigatorHelper
    {
        #region Data
        static readonly NavigatorHelper instance = new NavigatorHelper();
          
        #endregion

        #region Ctor

        //CTORs
        static NavigatorHelper()
        {

        }

        private NavigatorHelper()
        {
            
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static NavigatorHelper Instance
        {
            get { return instance; }
        }

        #endregion

        #region Public Methods

        public void Navigate(string url, FrameworkElement sender)
        {
            BBCodeBlock bs = new BBCodeBlock();
            try
            {
                bs.LinkNavigator.Navigate(new Uri(url, UriKind.Relative), sender);
            }
            catch (Exception error)
            {
                ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }

        public void GoHome(FrameworkElement sender)
        {
           Navigate("/Pages/Home.xaml", sender);
        }

        public bool ChechForPermisionToNavigate(string pageUri)
        {
            switch (pageUri)
            {
                case "/Pages/TrabajadoresPage.xaml":
                    if (!IsAdministrador())
                        return false;
                     break;
                  
            }

            return true;
        }

        private bool IsAdministrador()
        {
            return Authenticator.Instance.Trabajador.Acceso == "Administrador";
        }
        
        #endregion

        

    }
}
