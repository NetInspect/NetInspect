using NetInspectLib.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {
        private TrafficAnalysis? traffic;

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
                traffic = new TrafficAnalysis(ComboBox.SelectedValue.ToString());
                traffic.TrafficUpdated += TrafficUpdated;
                traffic.Start();
            }
            else
            {
                traffic.Stop();
            }
        }

        private static void TrafficUpdated(object sender, TrafficEventArgs e)
        {
            //Console.Clear();
            //Console.WriteLine($"Bytes sent: {e.BytesSent}");
            //Console.WriteLine($"Bytes received: {e.BytesReceived}");
            //Console.WriteLine($"First 5 Active connections:");
            //foreach (var connection in e.ActiveConnections.Take(5))
            //{
            //    Console.WriteLine($"\tLocal Endpoint: {connection.LocalEndPoint}");
            //    Console.WriteLine($"\tRemote Endpoint: {connection.RemoteEndPoint}");
            //    Console.WriteLine($"\tState: {connection.State}");
            //    Console.WriteLine($"\tProcess: {connection.ProcessName}");
            //}
        }

        private void Connections_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}