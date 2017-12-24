
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;

namespace WPFPresentation.Pages.Content
{
    /// <summary>
    /// Interaction logic for FilterControl.xaml
    /// </summary>
    public partial class FilterControl : UserControl
    {
        public FilterControlViewModel FilterControlViewModel;

        public FilterControl()
        {
            InitializeComponent();

            FilterControlViewModel = new FilterControlViewModel(DependencyResolver.Instance.FacadeProvider);

            FilterControlViewModel.InitializeViewContent();
            this.DataContext = FilterControlViewModel;

            CommandModel applyFilter = FilterControlViewModel.ApplyFilter;

            ButtonApplyFilter.Command = applyFilter.Command;
            ButtonApplyFilter.CommandParameter = DataContext;
            ButtonApplyFilter.CommandBindings.Add(new CommandBinding(applyFilter.Command, applyFilter.OnExecute, applyFilter.OnCanExecute));

            CommandModel removeFilter = FilterControlViewModel.RemoveFilter;

            ButtonRemoveFilter.Command = removeFilter.Command;
            ButtonRemoveFilter.CommandParameter = this.DataContext;
            ButtonRemoveFilter.CommandBindings.Add(new CommandBinding(removeFilter.Command, removeFilter.OnExecute, removeFilter.OnCanExecute));

        }

      
    }
}
