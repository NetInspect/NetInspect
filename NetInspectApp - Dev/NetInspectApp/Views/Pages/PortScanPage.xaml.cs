using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;

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
            public string IpAddress { get; set; }
            public int PortNumber { get; set; }
            public bool IsOpen { get; set; }
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the target IP address/range from the first text box
            string target = HostTextBox.Text;

            // Get the port range from the second text box
            string port = PortsTextBox.Text;

            // Scan for open ports
            var results = await PortScan.ScanAsync(target, port);

            // Clear the current items from the data grid
            ResultsDataGrid.Items.Clear();

            // Loop through each result and add it to the data grid
            foreach (var result in results)
            {
                var row = new PortScanResult
                {
                    IpAddress = result.Item1,
                    PortNumber = result.Item2,
               
                };

                ResultsDataGrid.Items.Add(row);
            }
        }


    }
}

