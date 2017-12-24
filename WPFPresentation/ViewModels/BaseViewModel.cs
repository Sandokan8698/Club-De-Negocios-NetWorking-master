using System.ComponentModel;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.ViewModels
{
    
    // base class for ViewModels
    
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // raises the PropertyChanged event

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected IFacadeProvider FacadeProvider;

        protected BaseViewModel(IFacadeProvider facadeProvider)
        {
            FacadeProvider = facadeProvider;

        }

        public BaseViewModel()
        {
            
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;

                    OnPropertyChanged("IsLoading");
                }
            }
        }

        public bool HasContent { get; set; }
        public virtual void InitializeViewContent()
        {
            HasContent = true;
        }

        public virtual void UnloadViewContent()
        {
            HasContent = false;
        }
    }
}
