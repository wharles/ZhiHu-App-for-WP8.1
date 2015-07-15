using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiHuApp.Services;
using ZhiHuApp.Models;
using GalaSoft.MvvmLight.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ZhiHuAppHelper;

namespace ZhiHuApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICommonService<StartImage> _startImageService;
        private readonly ICommonService<HotNews> _hotNewsService;
        private readonly ICommonService<Themes> _themesService;
        private readonly ICommonService<LatestNews> _latestNewsService;
        private readonly ICommonService<Sections> _sectionsService;

        public MainPageViewModel(ICommonService<StartImage> startImageService, ICommonService<LatestNews> latestNewsService,
            ICommonService<HotNews> hotNewsService, ICommonService<Themes> themesService, ICommonService<Sections> sectionsService)
        {
            _startImageService = startImageService;
            _hotNewsService = hotNewsService;
            _themesService = themesService;
            _latestNewsService = latestNewsService;
            _sectionsService = sectionsService;

            this.LoadMainSource();
            this.NewsDS = new NewsBeforeDataSource(_latestNewsService);

            this.ItemClickCommand = new RelayCommand<object>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(e, "OnItemClick"));
            });
            this.GridViewItemClickCommand = new RelayCommand<Others>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(e.Id, "OnGridViewItemClick"));
            });
            this.SectionClickCommand = new RelayCommand<Datum>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(e.Id, "OnGridViewSectionClick"));
            });
            this.RefreshCommand = new RelayCommand(() =>
            {
                //Refresh the data
                this.LoadMainSource();
                this.NewsDS = new NewsBeforeDataSource(_latestNewsService);
            });
            this.SettingCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("OnSettingButtonClick"));
            });
        }

        private HotNews hotNews;

        public HotNews HotNews
        {
            get { return hotNews; }
            set
            {
                hotNews = value;
                RaisePropertyChanged(() => HotNews);
            }
        }

        private Sections sections;

        public Sections Sections
        {
            get { return sections; }
            set
            {
                sections = value;
                RaisePropertyChanged(() => Sections);
            }
        }

        private Themes themes;

        public Themes Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                RaisePropertyChanged(() => Themes);
            }
        }

        private LatestNews latestNews;

        public LatestNews LatestNews
        {
            get { return latestNews; }
            set
            {
                latestNews = value;
                RaisePropertyChanged(() => LatestNews);
            }
        }

        // if progress is complete property 
        private bool isCompleted = false;

        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                RaisePropertyChanged(() => IsCompleted);
            }
        }

        private NewsBeforeDataSource newsDS;

        public NewsBeforeDataSource NewsDS
        {
            get { return newsDS; }
            set
            {
                newsDS = value;
                RaisePropertyChanged(() => NewsDS);
            }
        }

        //Event to Command
        public RelayCommand<object> ItemClickCommand { set; get; }
        public RelayCommand<Others> GridViewItemClickCommand { get; set; }
        public RelayCommand<Datum> SectionClickCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand SettingCommand { get; set; }

        /// <summary>
        /// Load all data
        /// </summary>
        private async void LoadMainSource()
        {
            try
            {
                var _startImage = _startImageService.GetObjectAsync("4", "start-image", "1080*1776");
                var themes = _themesService.GetObjectAsync("4", "themes");
                var latest = _latestNewsService.GetObjectAsync("4", "news", "latest");
                var hot = _hotNewsService.GetObjectAsync("3", "news", "hot");
                var _section = _sectionsService.GetObjectAsync("3", "sections");
                //await when all task finish
                await Task.WhenAll(_startImage, themes, latest, hot, _section);
                DownloadImage.SaveImage(_startImage.Result.Img);

                if (themes != null && latest != null && hot != null && _startImage != null)
                {
                    this.HotNews = hot.Result;
                    this.Themes = themes.Result;
                    this.LatestNews = latest.Result;
                    this.Sections = _section.Result;
                    this.IsCompleted = true;
                }
                else
                {
                    MessageDialog msg = new MessageDialog(_startImageService.ExceptionsParameter, "提示");
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
