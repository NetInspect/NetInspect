using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

using NetInspectLib.Discovery;
using System.Threading.Tasks;
using NetInspectApp.ViewModels;
using NetInspectLib.Scanning;
using NetInspectLib.Types;
using System.Linq;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for icmpPage.xaml
    /// </summary>
    public partial class UdpPage : INavigableView<ViewModels.udpScanViewModel>
    {
        public ViewModels.udpScanViewModel ViewModel
        {
            get;
        }

        public UdpPage(ViewModels.udpScanViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
        }

        public class PlaceholderTextBox : TextBox
        {
            public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
                "PlaceholderText", typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(default(string)));

            public string PlaceholderText
            {
                get => (string)GetValue(PlaceholderTextProperty);
                set => SetValue(PlaceholderTextProperty, value);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Clear existing results
            ViewModel.Results.Clear();
            int scannedPorts = 0;

            UDPScan udpScan = new UDPScan();
            Task<bool> scan = udpScan.DoUDPScan(udpTextBox.Text, "1024");
            bool success = await scan;

            if (success)
            {
                ScanProgressBar.Visibility = Visibility.Visible;
                int totalPorts = udpScan.results.Sum(host => host.Ports.Count());

                foreach (Host host in udpScan.results)
                {
                    foreach (Port port in host.Ports)
                    {
                        var row = new UDPScanResult
                        {
                            IpAddress = host.IPAddress.ToString(),
                            PortNumber = port.Number,
                            Status = port.Status.ToString(),
                            Other = port.Name
                        };
                        ViewModel.Results.Add(row);

                        scannedPorts++;
                        ScanProgressBar.Value = scannedPorts * 100 / totalPorts;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Results");
            }

            ScanProgressBar.Visibility = Visibility.Hidden;
        }
    }
    public class UDPScanResult
    {
        public string? IpAddress { get; set; }
        public int PortNumber { get; set; }
        public string? Status { get; set; }
        public string? Other { get; set; }
    }
}