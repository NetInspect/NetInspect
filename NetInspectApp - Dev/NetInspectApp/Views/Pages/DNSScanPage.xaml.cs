using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Interfaces;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DNSScanPage : INavigableView<ViewModels.PortScanViewModel>
    {
        public ViewModels.PortScanViewModel ViewModel
        {
            get;
        }

        public DNSScanPage(ViewModels.PortScanViewModel viewModel)
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

        public class DnsResult
        {
            public string? HostName { get; set; }
            public string? IpAddress { get; set; }
            public string? Type { get; set; }
            public int TTL { get; set; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DnsLookup lookup = new DnsLookup(HostTextBox2.Text);
            Task<bool> task = lookup.DoLookup();
            bool success = await task;
            if (success)
            {
                foreach (var record in lookup.records)
                {
                    var row = new DnsResult
                    {
                        HostName = record.Host.GetHostname(),
                        IpAddress = record.Host.GetIPAddress().ToString(),
                        Type = record.RecordType,
                        TTL = record.TTL,
                    };
                }
            }
        }
    }
}