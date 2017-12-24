using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Diagnostics;
using Domain_Layer;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Models
{
   
    // bbstract base class for business object models

    public abstract class BaseModel : BusinessObject, INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;

        // raises the PropertyChanged event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}


