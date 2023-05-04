using Wpf.Ui.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;

using NetInspectLib.Discovery;
using System.Threading.Tasks;
using static NetInspectApp.Views.Pages.whoisPage;
using System.Net;

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

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = queryTextBox.Text;
           // Whois whois = new Whois();
           // string result = whois.Lookup(query);
           // ResultsDataGrid.Items.Add(result);
        }
    }
}