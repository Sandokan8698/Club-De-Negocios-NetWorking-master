using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFPresentation.Pages.Content.ViewModels;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content
{
    /// <summary>
    /// Interaction logic for PaginatorControl.xaml
    /// </summary>
    public partial class PaginatorControl : UserControl
    {
        private  PaginatorControlViewModel _pagingControlViewModel;
        public IPaginator Paginator { get; set; }
        public PaginatorControl()
        {
            InitializeComponent();
            _pagingControlViewModel = new PaginatorControlViewModel();
            Paginator = _pagingControlViewModel;
            DataContext = _pagingControlViewModel;

            CommandModel nextPageCommand = _pagingControlViewModel.NextPCommand;

            ButtonNextPage.Command = nextPageCommand.Command;
            ButtonNextPage.CommandBindings.Add(new CommandBinding(nextPageCommand.Command, nextPageCommand.OnExecute, nextPageCommand.OnCanExecute));

            CommandModel previusPageCommand = _pagingControlViewModel.PreviusPCommand;

            ButtonPreviusPage.Command = previusPageCommand.Command;
            ButtonPreviusPage.CommandBindings.Add(new CommandBinding(previusPageCommand.Command, previusPageCommand.OnExecute, previusPageCommand.OnCanExecute));


            CommandModel lastPageCommand = _pagingControlViewModel.LastPCommand;

            ButtonLastPage.Command = lastPageCommand.Command;
            ButtonLastPage.CommandBindings.Add(new CommandBinding(lastPageCommand.Command, lastPageCommand.OnExecute, lastPageCommand.OnCanExecute));

            CommandModel firstPageCommand = _pagingControlViewModel.FirstPCommand;

            ButtonFirstPage.Command = firstPageCommand.Command;
            ButtonFirstPage.CommandBindings.Add(new CommandBinding(firstPageCommand.Command,firstPageCommand.OnExecute, firstPageCommand.OnCanExecute));
        }

        
      
    }
}
