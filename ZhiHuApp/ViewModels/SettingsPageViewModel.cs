using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using ZhiHuApp.Models;

namespace ZhiHuApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SettingsPageViewModel()
        {
            this.GetPackageInfo();
            this.ToggledCommand = new RelayCommand<bool>((b) =>
            {
                if (localSettings.Values.ContainsKey("liveTile"))
                {
                    localSettings.Values["liveTile"] = b;
                }
                else
                {
                    localSettings.Values.Add("liveTile", b);
                }
            });
            this.Index = LoadSetting();
            this.SelectionChangedCommand = new RelayCommand<int>((e) =>
            {
                SaveSetting(e);
            });
        }

        private void GetPackageInfo()
        {
            Windows.ApplicationModel.PackageId packageId = Windows.ApplicationModel.Package.Current.Id;

            this.PackageInfo.AppId = "ms-windows-store:reviewapp?appid=" + Windows.ApplicationModel.Store.CurrentApp.AppId;
            this.PackageInfo.Name = "知乎日报";
            this.PackageInfo.Version = packageId.Version.Major + "." + packageId.Version.Minor + "." + packageId.Version.Build + "." + packageId.Version.Revision;
            this.PackageInfo.Publisher = "Cherlies Wang";
        }

        private PackageInfo packageInfo = new PackageInfo();

        public PackageInfo PackageInfo
        {
            get { return packageInfo; }
            set
            {
                packageInfo = value;
                RaisePropertyChanged(() => PackageInfo);
            }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged(() => Index);
            }
        }


        private async void SaveSetting(int value)
        {
            if (localSettings.Values.ContainsKey("currentTheme"))
            {
                localSettings.Values.Remove("currentTheme");
            }

            localSettings.Values.Add("currentTheme", value == 1 ? "白" : "黑");
            MessageDialog msg = new MessageDialog("主题已更改，重启应用后生效!", "提示");
            await msg.ShowAsync();
        }

        private int LoadSetting()
        {
            if (localSettings.Values.ContainsKey("currentTheme") && (string)localSettings.Values["currentTheme"] == "白")
                return 1;
            else
                return 0;
        }

        public RelayCommand<bool> ToggledCommand { get; set; }
        public RelayCommand<int> SelectionChangedCommand { get; set; }
    }
}
