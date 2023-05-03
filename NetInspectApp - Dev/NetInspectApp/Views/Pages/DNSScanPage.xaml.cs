using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;
using System.Collections.Generic;
using DnsClient;
using DnsClient.Protocol;
using NetInspectLib.Types;
using static NetInspectApp.Views.Pages.DNSScanPage;


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
            DataContext = ViewModel;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DnsLookup dnsLookup = new DnsLookup(HostTextBox2.Text);

            QueryType queryType;
            if (ComboBox.SelectedItem == null)
            {
                queryType = QueryType.ANY;
            }
            else
            {
                switch (((ComboBoxItem)ComboBox.SelectedItem).Content.ToString())
                {
                    case "A":
                        queryType = QueryType.A;
                        break;
                    case "AAAA":
                        queryType = QueryType.AAAA;
                        break;
                    case "CNAME":
                        queryType = QueryType.CNAME;
                        break;
                    case "MX":
                        queryType = QueryType.MX;
                        break;
                    case "NS":
                        queryType = QueryType.NS;
                        break;
                    case "PTR":
                        queryType = QueryType.PTR;
                        break;
                    case "SOA":
                        queryType = QueryType.SOA;
                        break;
                    case "TXT":
                        queryType = QueryType.TXT;
                        break;
                    case "ANY":
                        queryType = QueryType.ANY;
                        break;
                    default:
                        queryType = QueryType.ANY;
                        break;
                }
            }

            List<DnsRecord> results = dnsLookup.DoDNSLookup(HostTextBox1.Text, queryType);
            ResultsDataGrid.ItemsSource = results;
        }




    }

    // No longer needed
    public class DnsResult
    {
        public string DomainName { get; set; }
        public string RecordClass { get; set; }
        public string RecordType { get; set; }
        public string TimeToLive { get; set; }
        public string Data { get; set; }
    }
}