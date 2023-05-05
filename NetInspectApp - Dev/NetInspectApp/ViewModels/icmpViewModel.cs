using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.icmpPage;

namespace NetInspectApp.ViewModels
{
    public partial class icmpViewModel : ObservableObject, INavigationAware
    {
        private ObservableCollection<IcmpScanResult> _results;

        public ObservableCollection<IcmpScanResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }
        public void OnNavigatedTo()
        {
            Results = new ObservableCollection<IcmpScanResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
