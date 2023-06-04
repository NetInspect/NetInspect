using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using System.Windows.Shell;
using NetInspectLib.Scanning;
using System.Threading.Tasks;
using NetInspectLib.Types;
using System.Linq;
using System.Drawing;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class PortScanPage : INavigableView<ViewModels.PortScanViewModel>
    {

        public ViewModels.PortScanViewModel ViewModel
        {
            get;
        }

        public PortScanPage(ViewModels.PortScanViewModel viewModel)
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

        /////

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            ScanProgressBar.Value = 0;
            


            ViewModel.Results.Clear();
            PortScan scaner = new PortScan();

            for (int i = 0; i < 100; i++)
            {
                ViewModel.Progress = i;
               await Task.Delay(10);
            }

            Task<bool> scan = scaner.DoPortScan(HostTextBox.Text, PortsTextBox.Text);
            bool success = await scan;
            if (success)
            {
               ScanProgressBar.Visibility = Visibility.Visible;
               int totalPorts = scaner.Results.Sum(host => host.Ports.Count());
               int scannedPorts = 0;
                foreach (var host in scaner.Results)
                {
                    foreach (var port in host.Ports)
                    {
                        var row = new PortScanResult
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


    public class PortScanResult
    {
        public string? IpAddress { get; set; }
        public int PortNumber { get; set; }
        public string? Status { get; set; }
        public string? Other { get; set; }
    }
}