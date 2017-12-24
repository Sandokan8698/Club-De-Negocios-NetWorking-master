using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content.ViewModels
{
    public class PaginatorControlViewModel : INotifyPropertyChanged, IPaginator
    {
        #region Propertys

        
        public event PropertyChangedEventHandler PropertyChanged;

        public CommandModel NextPCommand { get; set; }
        public CommandModel PreviusPCommand { get; set; }
        public CommandModel FirstPCommand { get; set; }
        public CommandModel LastPCommand { get; set; }

        public IPagiAble PagiAble { get; set; }

        private int _currentPageNumber;
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set { if (_currentPageNumber != value) _currentPageNumber = value; OnPropertyChanged("CurrentPageNumber");}
        }

        private int _totalPage;

        public int TotalPage
        {
            get { return _totalPage; }
            set { if (_totalPage != value) _totalPage = value; OnPropertyChanged("TotalPage"); }
        }

        private int _totalItems;

        public int ItemsPerPage { get; set; }
        public int ActualPage { get; set; }

        #endregion

        #region Ctor

        public PaginatorControlViewModel()
        {
            NextPCommand = new NextPageCommand(this);
            PreviusPCommand = new PreviusPageCommand(this);
            LastPCommand = new LastPageCommand(this);
            FirstPCommand = new FirstPageCommand(this);
        }

        #endregion


        #region Helper

        // raises the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public  void SetData()
        {
            try
            {
               _totalItems =  PagiAble.GetTotalItems();
                CurrentPageNumber += 1;
               TotalPage = (_totalItems / ItemsPerPage)+ 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Por favor espere un segundo hasta q se complete el proceso anterior");
            }
            
        }

        public void ResetData()
        {
            CurrentPageNumber = 0;
            ActualPage = 0;
        }



        public void SetPaginable(IPagiAble paginable)
        {
            PagiAble = paginable;
            paginable.SetPaginator(this);
        }

        public void MoveFirst()
        {
            
            ActualPage = 0;
            CurrentPageNumber = 1;
            PagiAble.Paginate(ActualPage,ItemsPerPage);
        }

        public void MoveLast()
        {
            
            ActualPage = (_totalPage - 1) * ItemsPerPage;
            CurrentPageNumber = _totalPage;
            PagiAble.Paginate(ActualPage, ItemsPerPage);
        }
      
        public void MoveForWard()
        {
            ActualPage = CurrentPageNumber * ItemsPerPage;
            CurrentPageNumber += 1;
            PagiAble.Paginate(ActualPage, ItemsPerPage);
        }

        public void MoveBackWard()
        {
           ActualPage -= ItemsPerPage;
           CurrentPageNumber -= 1;
           PagiAble.Paginate(ActualPage, ItemsPerPage);
        }

        #endregion


        #region Commnad
        private class FirstPageCommand : CommandModel
        {
            private PaginatorControlViewModel viewModel;

            public FirstPageCommand(PaginatorControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {

                e.CanExecute = viewModel.CurrentPageNumber <= viewModel.TotalPage && viewModel.CurrentPageNumber != 1 & !viewModel.PagiAble.IsLoading;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.MoveFirst();
            }
        }

        private class PreviusPageCommand : CommandModel
        {
            private PaginatorControlViewModel viewModel;

            public PreviusPageCommand(PaginatorControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = viewModel.CurrentPageNumber <= viewModel.TotalPage && viewModel.CurrentPageNumber != 1 & !viewModel.PagiAble.IsLoading;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {           
                viewModel.MoveBackWard();
                   
            }
        }

        private class NextPageCommand : CommandModel
        {
            private PaginatorControlViewModel viewModel;

            public NextPageCommand(PaginatorControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
               
                e.CanExecute = viewModel.CurrentPageNumber < viewModel.TotalPage & !viewModel.PagiAble.IsLoading; ;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.MoveForWard();
                               
            }
        }

        private class LastPageCommand : CommandModel
        {
            private PaginatorControlViewModel viewModel;

            public LastPageCommand(PaginatorControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = viewModel.CurrentPageNumber < viewModel.TotalPage & !viewModel.PagiAble.IsLoading; ;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.MoveLast();
            }
        }

        #endregion
    }
}
