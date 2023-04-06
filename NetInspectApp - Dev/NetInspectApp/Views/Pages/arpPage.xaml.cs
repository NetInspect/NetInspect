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
    public partial class arpPage : INavigableView<ViewModels.PortScanViewModel>
    {
        public ViewModels.PortScanViewModel ViewModel
        {
            get;
        }

        public arpPage(ViewModels.PortScanViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }

        public class ArpScanResult
        {
            public string IpAddress { get; set; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ARPScan scaner = new ARPScan(HostTextBox2.Text);
            Task<bool> scan = scaner.DoScan();
            bool success = await scan;
            if (success)
            {
                foreach (var host in scaner.results)
                {
                    var row = new ArpScanResult
                    {
                        IpAddress = host.GetIPAddress().ToString(),
                    };
                    ResultsDataGrid.Items.Add(row);
                }
            }
        }
    }
}