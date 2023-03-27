using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common.Interfaces;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for icmpPage.xaml
    /// </summary>
    public partial class icmpPage : INavigableView<ViewModels.PortScanViewModel>
    {
        public ViewModels.PortScanViewModel ViewModel
        {
            get;
        }

        public icmpPage(ViewModels.PortScanViewModel viewModel)
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


    }
}
