﻿using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

using NetInspectLib.Scanning;
using System.Threading.Tasks;

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

        public class PortScanResult
        {
            public string? IpAddress { get; set; }
            public int PortNumber { get; set; }
            public string? Status { get; set; }
            public string? Other { get; set; }
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsDataGrid.Items.Clear();
            PortScan scaner = new PortScan();
            Task<bool> scan = scaner.DoPortScan(HostTextBox.Text, PortsTextBox.Text);
            bool success = await scan;
            if (success)
            {
                foreach (var host in scaner.results)
                {
                    foreach (var port in host.GetPorts())
                    {
                        var row = new PortScanResult
                        {
                            IpAddress = host.GetIPAddress().ToString(),
                            PortNumber = port.Number,
                            Status = "Open",
                            Other = port.Name
                        };
                        ResultsDataGrid.Items.Add(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}