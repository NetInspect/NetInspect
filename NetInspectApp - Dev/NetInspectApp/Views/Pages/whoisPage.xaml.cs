using Wpf.Ui.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;
using NetInspectLib.Networking;
using System.Threading.Tasks;
using static NetInspectApp.Views.Pages.whoisPage;
using System.Net;
using System.Linq;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for arpPage.xaml
    /// </summary>
    public partial class whoisPage : INavigableView<ViewModels.whoisViewModel>
    {
        public ViewModels.whoisViewModel ViewModel
        {
            get;
        }

        public whoisPage(ViewModels.whoisViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Results.Clear();
            string query = queryTextBox.Text;
            Whois whois = new Whois();
            string resultsquery = whois.Lookup(query);
            var lines = resultsquery.Split('\n');
            resultsquery = string.Join("\n", lines.Skip(4)); // Skip the first 4 lines
            ViewModel.Results.Add(resultsquery);
        }


    }
}