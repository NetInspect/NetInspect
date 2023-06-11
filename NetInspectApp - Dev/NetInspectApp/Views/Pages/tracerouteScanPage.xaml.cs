using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

using NetInspectLib.Discovery;
using System.Threading.Tasks;
using NetInspectApp.ViewModels;
using NetInspectLib.Networking;
using NetInspectLib.Types;
using System;
using Microsoft.Extensions.Hosting;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for icmpPage.xaml
    /// </summary>
    public partial class tracerouteScanPage : INavigableView<ViewModels.tracerouteScanViewModel>
    {
        public ViewModels.tracerouteScanViewModel ViewModel
        {
            get;
        }

        public tracerouteScanPage(ViewModels.tracerouteScanViewModel viewModel)
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
            ViewModel.Results.Clear();
            string hostname = tcTextBox.Text;
            int maxHops = 30; 


            Traceroute traceroute = new Traceroute();
            bool success = await traceroute.DoTraceroute(hostname, maxHops);

            if (success)
            {
                foreach (Hop hop in traceroute)
                {
                    var row = new traceScanResult
                    {
                        hopNum = hop.number.ToString(),
                        hopAddress = hop.address.ToString(),
                        hopTime = hop.time.ToString()

                    };
                    ViewModel.Results.Add(row);

                }

            }
            else
            {
                var errorMessage = new traceScanResult
                {
                    hopNum = "Error",
                    hopAddress = "Failed to perform traceroute",
                    hopTime = ""
                };
                ViewModel.Results.Add(errorMessage);
            }
        }
    }
    public class traceScanResult
    {
        public string? hopNum { get; set; }
        public string hopAddress { get; set; }
        public string? hopTime { get; set; }
    }
}