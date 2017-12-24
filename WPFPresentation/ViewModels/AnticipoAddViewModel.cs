using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPresentation.Models;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class AnticipoAddViewModel: BaseViewModel
    {
        #region Property

        public CommandModel SaveVentaComand { get; private set; }
        
        public ObservableCollection<ClienteModel> Clientes
        {
            get { return InMemoryHelper.Instance.Clientes; }
        }

        #endregion
    }
}
