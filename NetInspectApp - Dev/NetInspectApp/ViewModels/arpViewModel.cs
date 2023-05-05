using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.arpPage;

namespace NetInspectApp.ViewModels
{
    public partial class arpViewModel : ObservableObject, INavigationAware
    {
        private ObservableCollection<ArpScanResult> _results;

        public ObservableCollection<ArpScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }
        public void OnNavigatedTo()
        {
            Results = new ObservableCollection<ArpScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
