using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
            //share
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                dynamic obj = e.Parameter;
                int id = Convert.ToInt32(obj.Id);
                this.DataContext = new NewsContentPageViewModel(id.ToString());
            }
            //register message
            Messenger.Default.Register<NotificationMessage>(this, (msg) =>
            {
                switch (msg.Notification)
                {
                    case "OnCommandClick":
                        Frame.Navigate(typeof(CommentsPage), msg.Sender);
                        break;
                    case "OnSettingButtonClick":
                        Frame.Navigate(typeof(Views.SettingsPage));
                        break;
                    case "OnLoadCompleted":
                        if (msg.Sender != null)
                        {
                            #region Initialize WebView
                            dynamic newsContent = msg.Sender;
                            StringBuilder sbHtml = new StringBuilder(10000);
                            sbHtml.Append("<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><meta name=\"viewport\" content=\"width=400,minimum-scale=0.5,maximum-scale=1.0,user-scalable=no, initial-scale=1.0\" /><title></title><script src=\"http://cdn.bootcss.com/jquery/2.1.4/jquery.min.js\"></script>");
                            sbHtml.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + newsContent.CSS + "\" />");
                            sbHtml.Append("<style type=\"text/css\"> #imgDiv { height: 300px;width: 400px; position: relative; }");
                            sbHtml.Append(".blackDiv { background-color: rgba(96, 96, 96,0.6); width: 400px; height: 100px; position: absolute; bottom: 0px; left: 0px;display: table; }");
                            sbHtml.Append("#title { font-size: 24px; display: table-cell; vertical-align: middle; color: #FFFFFF; padding-left: 10px; }");
                            sbHtml.Append("#copyRight { color: lightgray; font-size: 12px;  float: right; padding-right: 10px; } </style><script type=\"text/javascript\">");
                            string js = @"$(document).ready(function () {
                            $('div').remove('.img-place-holder');
                            $('a').each(function () {
                                var href = $(this).attr('href');
                                $(this).attr('href', '');
                                $(this).click(function (event) { event.preventDefault(); });
                                $(this).click(function () {
                                    window.external.notify(href);
                                });
                            });
                        });</script></head><body>";
                            sbHtml.Append(js);
                            if (!string.IsNullOrEmpty(newsContent.Image))
                            {
                                sbHtml.Append("<div id=\"imgDiv\" style=\"background-image:url('" + newsContent.Image + "');\"><div class=\"blackDiv\"><span id=\"title\">" + newsContent.Title + "<br /><span id=\'copyRight\'>" + newsContent.ImageSource + "</span></span></div></div>");
                            }
                            sbHtml.Append(newsContent.Body + "</body></html>");
                            webView.NavigateToString(sbHtml.ToString());
                            #endregion
                            title = newsContent.Title;
                            shareUrl = newsContent.ShareUrl;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
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
