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
    public class ThemePageViewModel : ViewModelBase
    {
        private string _id;
        public ThemePageViewModel(string id)
        {
            _id = id;
            this.LoadTheme();

            this.ItemClickCommand = new RelayCommand<Story>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(e, "OnItemClick"));
            });
        }

        private Theme theme;

        public Theme Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                RaisePropertyChanged(() => Theme);
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

        public RelayCommand<Story> ItemClickCommand { set; get; }

        private void LoadTheme()
        {
            Task.Run(async () =>
            {
                ICommonService<Theme> themeService = new CommonService<Theme>();
                Theme result = await themeService.GetObjectAsync("4", "theme", _id);
                await DispatcherHelper.RunAsync(async() =>
                {
                    if (result != null)
                    {
                        this.Theme = new Theme();
                        this.Theme.Stories = result.Stories;
                        this.Theme.Name = result.Name;
                        this.Theme.Editors = result.Editors;
                        this.IsActive = false;
                    }
                    else
                    {
                        MessageDialog msg = new MessageDialog(themeService.ExceptionsParameter);
                        await msg.ShowAsync();
                    }
                });
            });
        }

    }
}
