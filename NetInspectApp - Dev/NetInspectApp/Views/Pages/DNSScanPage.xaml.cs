using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using System.Collections.Generic;
using DnsClient;
using DnsClient.Protocol;
using NetInspectLib.Types;


namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DNSScanPage : INavigableView<ViewModels.DNSScanViewModel>
    {
        public ViewModels.DNSScanViewModel ViewModel
        {
            get;
        }

        public DNSScanPage(ViewModels.DNSScanViewModel viewModel)
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
            public string DomainName { get; set; }
            public string RecordClass { get; set; }
            public string RecordType { get; set; }
            public string TimeToLive { get; set; }
            public string Data { get; set; }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string domain = HostTextBox1.Text;
            string dnsServer = HostTextBox2.Text;
            DnsLookup lookup = new DnsLookup(dnsServer);
            List<DnsResult> results = new List<DnsResult>();
            List<string> lookupResults = lookup.DoDNSLookup(domain);
            foreach (string result in lookupResults)
            {
                if (result.StartsWith("Query type: "))
                {
                    // Ignore the query type message
                    continue;
                }
                string[] parts = result.Split(' ');
                if (parts.Length > 4)
                {
                    DnsResult dnsResult = new DnsResult
                    {
                        DomainName = parts[0],
                        RecordClass = parts[1],
                        RecordType = parts[2],
                        TimeToLive = parts[3],
                        Data = parts[4]
                    };
                    results.Add(dnsResult);
                }
            }
            //ViewModel.Results = results;
        }

    }
}