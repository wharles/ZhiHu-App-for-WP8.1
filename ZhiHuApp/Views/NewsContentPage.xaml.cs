using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZhiHuApp.ViewModels;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ZhiHuApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsContentPage : Page
    {
        private string shareUrl = "";
        private string title = "";

        public NewsContentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dynamic obj = e.Parameter;
            int id = Convert.ToInt32(obj.Id);
            this.DataContext = new NewsContentPageViewModel(id.ToString());
            //share
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
            //register message
            Messenger.Default.Register<NotificationMessage>(this, async (msg) =>
            {
                if (msg.Notification == "OnCommandClick")
                {
                    Frame.Navigate(typeof(CommentsPage), msg.Sender);
                }
                if (msg.Notification == "OnLoadCompleted")
                {
                    if (msg.Sender != null)
                    {
                        dynamic newsContent = msg.Sender;
                        try
                        {
                            await webView.InvokeScriptAsync("ShowBody", new string[] { newsContent.Body, newsContent.CSS,
                                newsContent.Image ?? "", newsContent.Title, newsContent.ImageSource ?? "" });
                        }
                        catch (Exception ex)
                        {
                            //when loading first time,there a error that no js name found.so do this to fix it.
                            webView.DOMContentLoaded += async(s, args) =>
                            {
                                await webView.InvokeScriptAsync("ShowBody", new string[] { newsContent.Body, newsContent.CSS,
                                newsContent.Image ?? "", newsContent.Title, newsContent.ImageSource ?? "" });
                            };
                            webView.Navigate(new Uri("ms-appx-web:///Assets/Htmls/NewContentPage.html"));
                        }

                        title = newsContent.Title;
                        shareUrl = newsContent.ShareUrl;
                    }
                }
                if (msg.Notification == "OnSettingButtonClick")
                {
                    Frame.Navigate(typeof(Views.SettingsPage));
                }
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            webView.Refresh();
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;

            Messenger.Default.Unregister<NotificationMessage>(this);
        }

        private async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Uri nvUri = new Uri(e.Value);
            await Windows.System.Launcher.LaunchUriAsync(nvUri);
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title = title;
            args.Request.Data.Properties.Description = "";
            args.Request.Data.SetText(shareUrl);
        }
    }
}
