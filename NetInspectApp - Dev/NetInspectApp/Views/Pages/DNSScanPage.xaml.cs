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
            List<DnsRecord> results = dnsLookup.DoDNSLookup(HostTextBox1.Text);
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