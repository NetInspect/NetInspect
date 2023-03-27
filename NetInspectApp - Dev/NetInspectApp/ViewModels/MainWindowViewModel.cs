using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace NetInspectApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "WPF UI - NetInspectApp";

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Home",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationItem()
                {
                    Content = "ICMP Scan",
                    PageTag = "icmp",
                    Icon = SymbolRegular.AppGeneric20,
                    PageType = typeof(Views.Pages.icmpPage)
                },
                         new NavigationItem()
                {
                    Content = "ARP Scan",
                    PageTag = "arp",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.arpPage)
                },
                new NavigationItem()
                {
                    Content = "Port Scan",
                    PageTag = "portscan",
                    Icon = SymbolRegular.GlanceDefault12,
                    PageType = typeof(Views.Pages.PortScanPage)
                },
                new NavigationItem()
                {
                    Content = "DNS - Lookup",
                    PageTag = "dnsscan",
                    Icon = SymbolRegular.GlanceDefault12,
                    PageType = typeof(Views.Pages.DNSScanPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            _isInitialized = true;
        }
    }
}
