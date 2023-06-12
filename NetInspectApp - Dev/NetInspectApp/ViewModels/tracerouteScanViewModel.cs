
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;


namespace NetInspectApp.ViewModels
{
    public partial class tracerouteScanViewModel : ObservableObject, INavigationAware
    {

        private ObservableCollection<traceScanResult> _results;

        public ObservableCollection<traceScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }


        public void OnNavigatedTo()
        {
            Results = new ObservableCollection<traceScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}

