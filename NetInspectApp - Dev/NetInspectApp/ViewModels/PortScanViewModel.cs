using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.Generic;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.PortScanPage;

namespace NetInspectApp.ViewModels
{
    public partial class PortScanViewModel : ObservableObject, INavigationAware
    {
        private List<PortScanResult> _results;

        public List<PortScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        public void OnNavigatedTo()
        {
            Results = new List<PortScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
