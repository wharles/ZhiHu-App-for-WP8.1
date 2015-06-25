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
using ZhiHuAppHelper;

namespace ZhiHuApp.ViewModels
{
    public class SectionPageViewModel : ViewModelBase
    {
        private string _id;
        public SectionPageViewModel(string id)
        {
            _id = id;
            this.LoadSection();

            this.ItemClickCommand = new RelayCommand<Story>((e) =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(e, "OnItemClick"));
            });
            this.SettingCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("OnSettingButtonClick"));
            });
        }

        private Section section;

        public Section Section
        {
            get { return section; }
            set
            {
                section = value;
                RaisePropertyChanged(() => Section);
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


        private DateTime selectedDate = DateTime.Now;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                RaisePropertyChanged(() => SelectedDate);
                if (value < DateTime.Now)
                {
                    string timestamp = UnixTimestamp.ConvertToUnixTimestamp(value).ToString();
                    this.LoadSection(timestamp);
                }
            }
        }


        public RelayCommand<Story> ItemClickCommand { set; get; }
        public RelayCommand SettingCommand { get; set; }

        private void LoadSection()
        {
            Task.Run(async () =>
            {
                ICommonService<Section> sectionService = new CommonService<Section>();
                Section result = await sectionService.GetObjectAsync("3", "section", _id);
                await LoadAsync(sectionService, result);
            });
        }

        private void LoadSection(string timestamp)
        {
            Task.Run(async () =>
            {
                ICommonService<Section> sectionService = new CommonService<Section>();
                Section result = await sectionService.GetObjectAsync("3", "section", _id, "before", timestamp);
                await LoadAsync(sectionService, result);
            });
        }

        private async Task LoadAsync(ICommonService<Section> sectionService, Section result)
        {
            await DispatcherHelper.RunAsync(async () =>
            {
                if (result != null)
                {
                    this.Section = result;
                    this.IsActive = false;
                }
                else
                {
                    MessageDialog msg = new MessageDialog(sectionService.ExceptionsParameter);
                    await msg.ShowAsync();
                }
            });
        }
    }
}
