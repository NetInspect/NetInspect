
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;


namespace NetInspectApp.ViewModels
{
    public partial class tracerouteScanViewModel : ObservableObject, INavigationAware
    {

        public ObservableCollection<string> Results { get; } = new ObservableCollection<string>();

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom()
        {
        }
    }
}

