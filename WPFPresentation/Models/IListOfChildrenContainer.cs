using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Models
{
    public interface IListOfChildrenContainer<T>
    {
        ObservableCollection<T> Childrens { get; set; }
    }
}
