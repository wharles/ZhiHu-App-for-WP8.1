using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace ZhiHuAppHelper
{
    public sealed class DownloadImage
    {
        private async static Task<StorageFile> SaveAsync(
              Uri fileUri,
              StorageFolder folder,
              string fileName)
        {
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var downloader = new BackgroundDownloader();
            var download = downloader.CreateDownload(
                fileUri,
                file);

            var res = await download.StartAsync();

            return file;
        }

        public static async void SaveImage(string url)
        {
            StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
            await SaveAsync(new Uri(url), applicationFolder, "splashimage.jpg");
        }
    }
}
