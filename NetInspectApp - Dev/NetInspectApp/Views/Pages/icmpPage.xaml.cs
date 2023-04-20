using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

using NetInspectLib.Discovery;
using System.Threading.Tasks;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for icmpPage.xaml
    /// </summary>
    public partial class icmpPage : INavigableView<ViewModels.PortScanViewModel>
    {
        public ViewModels.PortScanViewModel ViewModel
        {
            get;
        }

        public icmpPage(ViewModels.PortScanViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
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

        public class IcmpScanResult
        {
            public string IpAddress { get; set; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ICMPScan scaner = new ICMPScan(HostTextBox2.Text);
            Task<bool> scan = scaner.DoScan();
            bool success = await scan;
            if (success)
            {
                foreach (var host in scaner.results)
                {
                    var row = new IcmpScanResult
                    {
                        IpAddress = host.GetIPAddress().ToString(),
                    };
                    ResultsDataGrid.Items.Add(row);
                }
            }
        }
    }
}