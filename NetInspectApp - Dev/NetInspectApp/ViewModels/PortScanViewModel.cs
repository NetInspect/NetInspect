using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.icmpPage;
using static NetInspectApp.Views.Pages.PortScanPage;

namespace NetInspectApp.ViewModels
{
    public partial class PortScanViewModel : ObservableObject, INavigationAware
    {

        private ObservableCollection<PortScanResult> _results;

        public ObservableCollection<PortScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        public void OnNavigatedTo()
        {
            Results = new ObservableCollection<PortScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
