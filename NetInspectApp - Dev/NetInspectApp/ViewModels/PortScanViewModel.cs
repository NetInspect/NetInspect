using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.DNSScanPage;

namespace NetInspectApp.ViewModels
{
    public partial class PortScanViewModel : ObservableObject, INavigationAware
    {
        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }
        private List<DnsResult> _results;

        public List<DnsResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }
    }
}
