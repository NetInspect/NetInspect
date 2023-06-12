using NetInspectLib.Analysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using System.Windows;
using ScottPlot;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {
        private TrafficAnalysis? traffic;
        private List<double> TxGraphXData = new List<double>();
        private List<double> TxGraphYData = new List<double>();

        private List<double> RxGraphXData = new List<double>();
        private List<double> RxGraphYData = new List<double>();

        public ViewModels.DashboardViewModel ViewModel
        {
            get;
        }

        public DashboardPage(ViewModels.DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();

            var adapters = net_adapters();
            ComboBox.ItemsSource = adapters;
            ComboBox.SelectedIndex = 0;

            TxGraph.Plot.Title("Kilobytes Sent");
            RxGraph.Plot.Title("Kilobytes Received");

            TxGraph.Plot.Style(ScottPlot.Style.Gray1);
            RxGraph.Plot.Style(ScottPlot.Style.Gray1);

            TxGraph.Plot.XAxis.DateTimeFormat(true);
            RxGraph.Plot.XAxis.DateTimeFormat(true);
        }

        public IEnumerable<string> net_adapters()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                yield return nic.Name;
            }
            yield break;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var toggle = (Startbutton.Content.ToString() == "Start") ? true : false;
            var _ = (toggle) ? Startbutton.Content = "Stop" : Startbutton.Content = "Start";

            if (toggle)
            {
                ConnectionsDataGrid.Items.Clear();
                traffic = new TrafficAnalysis(ComboBox.SelectedValue.ToString());
                traffic.TrafficUpdated += TrafficUpdated;
                traffic.Start();
            }
            else
            {
                traffic.Stop();
            }
        }

        public void TrafficUpdated(object sender, TrafficEventArgs e)
        {
            TxGraphYData.Add(e.BytesSent / 1000);
            TxGraphXData.Add(DateTime.Now.ToOADate());

            RxGraphYData.Add(e.BytesReceived / 1000);
            RxGraphXData.Add(DateTime.Now.ToOADate());

            Application.Current.Dispatcher.Invoke(() =>
            {
                TxGraph.Plot.Clear();
                TxGraph.Plot.AddScatter(TxGraphXData.ToArray(), TxGraphYData.ToArray());
                TxGraph.Refresh();

                RxGraph.Plot.Clear();
                RxGraph.Plot.AddScatter(RxGraphXData.ToArray(), RxGraphYData.ToArray());
                RxGraph.Refresh();

                foreach (var c in e.ActiveConnections)
                {
                    var row = new Connection
                    {
                        LocalEndPoint = $"{c.LocalEndPoint.Address}:{c.LocalEndPoint.Port}",
                        RemoteEndPoint = $"{c.RemoteEndPoint.Address}:{c.RemoteEndPoint.Port}",
                        State = c.State.ToString(),
                        Process = c.ProcessName
                    };
                    ConnectionsDataGrid.Items.Add(row);
                }
            });
        }

        private void Connections_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        public class Connection
        {
            public string? LocalEndPoint { get; set; }
            public string? RemoteEndPoint { get; set; }
            public string? State { get; set; }
            public string? Process { get; set; }
        }
    }
}