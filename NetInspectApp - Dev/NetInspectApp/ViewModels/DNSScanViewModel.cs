using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetInspectApp.Views.Pages;
using System.Collections.Generic;
using Wpf.Ui.Common.Interfaces;
using static NetInspectApp.Views.Pages.DNSScanPage;

namespace NetInspectApp.ViewModels
{
    public partial class DNSScanViewModel : ObservableObject, INavigationAware
    {
        private List<DnsResult> _results;

        public List<DnsResult> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        public void OnNavigatedTo()
        {
            Results = new List<DnsResult>();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
