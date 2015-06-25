using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZhiHuAppHelper;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ZhiHuApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = null;
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            UIHelper.HideSystemTray();

            if (localSettings.Values.ContainsKey("liveTile"))
            {
                if (!Convert.ToBoolean(localSettings.Values["liveTile"].ToString())) return;
            }
            BackgroundTaskHelper.RegisterBackgroundTask("BackgroundTask", "BackgroundTasksCX.BackgroundTask");
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            Messenger.Default.Register<NotificationMessage>(this, (msg) =>
            {
                if (msg.Notification == "OnItemClick")
                {
                    Frame.Navigate(typeof(Views.NewsContentPage), msg.Sender);
                }
                if (msg.Notification == "OnGridViewItemClick")
                {
                    Frame.Navigate(typeof(Views.ThemePage), msg.Sender);
                }
                if (msg.Notification == "OnGridViewSectionClick")
                {
                    Frame.Navigate(typeof(Views.SectionPage), msg.Sender);
                }
                if (msg.Notification == "OnSettingButtonClick")
                {
                    Frame.Navigate(typeof(Views.SettingsPage));
                }
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister<NotificationMessage>(this);
        }

        private void flipView_Loaded(object sender, RoutedEventArgs e)
        {
            FlipView flipView = sender as FlipView;
            if (timer == null)
            {
                timer = new DispatcherTimer();
            }
            timer.Interval = TimeSpan.FromSeconds(3.0);
            timer.Tick += ((s, args) =>
            {
                if (flipView.SelectedIndex < flipView.Items.Count - 1)
                    flipView.SelectedIndex++;
                else
                    flipView.SelectedIndex = 0;
            });
            timer.Start();
        }
    }
}
