using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Utils
{
    public interface ISubject

    {
       void Attach(string propertyName, Action<object> observerFunc);
       void Notify(string propertyName, object objectAsValue);
    }
}
