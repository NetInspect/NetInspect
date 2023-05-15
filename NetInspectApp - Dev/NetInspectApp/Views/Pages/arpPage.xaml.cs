using Wpf.Ui.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;
using NetInspectLib.Discovery;
using System.Threading.Tasks;
using static NetInspectApp.Views.Pages.icmpPage;
using System.Net;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for arpPage.xaml
    /// </summary>
    public partial class arpPage : INavigableView<ViewModels.arpViewModel>
    {
        public ViewModels.arpViewModel ViewModel
        {
            get;
        }

        public arpPage(ViewModels.arpViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
            DataContext = ViewModel;
        }

        public class ArpScanResult
        {
            public string? IpAddress { get; set; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Results.Clear();
            ARPScan scaner = new ARPScan();
            Task<bool> scan = scaner.DoARPScan(arpScanTexbox.Text);
            bool success = await scan;
            if (success)
            {
                foreach (var host in scaner.results)
                {
                    var arp = new ArpScanResult
                    {
                        IpAddress = host.IPAddress.ToString(),
                    };
                    ViewModel.Results.Add(arp);
                }
            }
        }
    }
}