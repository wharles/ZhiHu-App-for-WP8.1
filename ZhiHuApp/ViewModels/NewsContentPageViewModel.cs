using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using ZhiHuApp.Models;
using ZhiHuApp.Services;

namespace ZhiHuApp.ViewModels
{
    public class NewsContentPageViewModel : ViewModelBase
    {
        public NewsContentPageViewModel(string id)
        {
            this.LoadNewsContent(id);

            this.CommentCommand = new RelayCommand<string>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(new { Title = this.NewsContent.Title, Id = this.NewsContent.Id, Type = e }, "OnCommandClick"));
            });
            this.SettingCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("OnSettingButtonClick"));
            });
        }

        private NewsContent newsContent;

        public NewsContent NewsContent
        {
            get { return newsContent; }
            set
            {
                newsContent = value;
                RaisePropertyChanged(() => NewsContent);
            }
        }

        private StoryExtra storyExtra;

        public StoryExtra StoryExtra
        {
            get { return storyExtra; }
            set
            {
                storyExtra = value;
                RaisePropertyChanged(() => StoryExtra);
            }
        }

        private bool isActive = true;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }

        public RelayCommand<string> CommentCommand { set; get; }
        public RelayCommand ShareCommand { get; set; }
        public RelayCommand SettingCommand { get; set; }

        private async void LoadNewsContent(string id)
        {
            try
            {
                ICommonService<NewsContent> newsContentService = new CommonService<NewsContent>();
                var task1 = newsContentService.GetObjectAsync("4", "news", id);

                ICommonService<StoryExtra> storyExtraService = new CommonService<StoryExtra>();
                var task2 = storyExtraService.GetObjectAsync("4", "story-extra", id);

                await Task.WhenAll(task2, task1);

                NewsContent content = task1.Result;
                StoryExtra extra = task2.Result;

                if (content != null)
                {
                    this.StoryExtra = extra;
                    this.NewsContent = content;
                    var obj = new { Body = content.Body, CSS = content.Css[0], Image = content.Image, Title = content.Title, ImageSource = content.ImageSource, ShareUrl = content.ShareUrl };
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage(obj, "OnLoadCompleted"));
                    //Delay to destroy animation
                    await Task.Delay(500);
                    this.IsActive = false;
                }
                else
                {
                    MessageDialog msg = new MessageDialog(newsContentService.ExceptionsParameter, "提示");
                    await msg.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
