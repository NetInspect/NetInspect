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
    public partial class icmpPage : INavigableView<ViewModels.icmpViewModel>
    {
        public ViewModels.icmpViewModel ViewModel
        {
            get;
        }

        public icmpPage(ViewModels.icmpViewModel viewModel)
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

        public class IcmpScanResult
        {
            public string IpAddress { get; set; }
            public string Status { get; set; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Results.Clear();
            ICMPScan scaner = new ICMPScan(icmpTextBox2.Text);
            Task<bool> scan = scaner.DoScan();
            bool success = await scan;
            if (success)
            {
                foreach (var host in scaner.results)
                {
                    var icmp = new IcmpScanResult
                    {
                        IpAddress = host.IPAdress.ToString(),
                        Status = "Success"
                    };
                    ViewModel.Results.Add(icmp);
                }
            }
            else
            {
                var icmp = new IcmpScanResult
                {
                    IpAddress = icmpTextBox2.Text,
                    Status = "Fail"
                };
                ViewModel.Results.Add(icmp);
            }
        }
    }
}