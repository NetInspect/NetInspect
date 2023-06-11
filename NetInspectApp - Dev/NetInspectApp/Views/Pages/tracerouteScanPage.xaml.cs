using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

using NetInspectLib.Discovery;
using System.Threading.Tasks;
using NetInspectApp.ViewModels;

namespace NetInspectApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for icmpPage.xaml
    /// </summary>
    public partial class tracerouteScanPage : INavigableView<ViewModels.tracerouteScanViewModel>
    {
        public ViewModels.tracerouteScanViewModel ViewModel
        {
            get;
        }

        public tracerouteScanPage(ViewModels.tracerouteScanViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = ViewModel;
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