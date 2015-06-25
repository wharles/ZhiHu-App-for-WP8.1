using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
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

        public RelayCommand<bool> ToggledCommand { get; set; }
    }
}
