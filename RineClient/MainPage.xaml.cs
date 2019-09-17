using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using RineClient.ViewModels;
using RineClient.Services;
using Windows.UI.Xaml.Navigation;
using RineClient.Models;
using RineClient.Common;
using Windows.ApplicationModel.Core;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace RineClient
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(RineTitleBar);

            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            ViewModel = ServiceLocator.Current.GetService<MainViewModel>();
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            LeftPaddingColumn.Width = new GridLength(sender.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
            Avatar.Margin = new Thickness(0, 0, sender.SystemOverlayRightInset, 0);
        }

        public MainViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeNavigation();
            
            ViewModel.LoadAsync((MainArgs)e.Parameter);
        }

        private void InitializeNavigation()
        {
            var navigationService = ServiceLocator.Current.GetService<INavigationService>();
            navigationService.Initialize(Frame);
        }
    }
}
