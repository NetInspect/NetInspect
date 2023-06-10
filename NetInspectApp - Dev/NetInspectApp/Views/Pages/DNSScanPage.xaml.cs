using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using System.Collections.Generic;
using NetInspectLib.Networking;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DNSScanPage : INavigableView<ViewModels.DNSScanViewModel>
    {
        public ViewModels.DNSScanViewModel ViewModel { get; }

        public DNSScanPage(ViewModels.DNSScanViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }

        public class DnsResult
        {
            public string Name { get; set; }
            public int Type { get; set; }
            public int TTL { get; set; }
            public string Data { get; set; }
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
            DnsLookup dnsLookup = new DnsLookup();

            string queryType;
            if (ComboBox.SelectedItem == null)
            {
                queryType = "ANY";
            }
            else
            {
                switch (((ComboBoxItem)ComboBox.SelectedItem).Content.ToString())
                {
                    case "A":
                        queryType = "A";
                        break;
                    case "AAAA":
                        queryType = "AAAA";
                        break;
                    case "CNAME":
                        queryType = "CNAME";
                        break;
                    case "MX":
                        queryType = "MX";
                        break;
                    case "NS":
                        queryType = "NS";
                        break;
                    case "PTR":
                        queryType = "PTR";
                        break;
                    case "SOA":
                        queryType = "SOA";
                        break;
                    case "TXT":
                        queryType = "TXT";
                        break;
                    case "ANY":
                        queryType = "ANY";
                        break;
                    default:
                        queryType = "ANY";
                        break;
                }
            }

            List<DnsLookup.DnsRecord> dnsRecords = await dnsLookup.DoDNSLookup(HostTextBox1.Text, queryType);

            List<DnsResult> results = new List<DnsResult>();

            foreach (var dnsRecord in dnsRecords)
            {
                DnsResult result = new DnsResult
                {
                    Name = dnsRecord.Name,
                    Type = dnsRecord.Type,
                    TTL = dnsRecord.TTL,
                    Data = dnsRecord.Data
                };
                results.Add(result);
            }

            ViewModel.Results = results;
        }
    }
}
