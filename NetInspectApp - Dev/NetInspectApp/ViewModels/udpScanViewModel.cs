using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;


namespace NetInspectApp.ViewModels
{
    public partial class udpScanViewModel : ObservableObject, INavigationAware
    {

        private ObservableCollection<UDPScanResult> _results;

        public ObservableCollection<UDPScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }
        public void OnNavigatedTo()
        {
            Results = new ObservableCollection<UDPScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
