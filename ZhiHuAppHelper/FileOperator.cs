using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuAppHelper
{
    public class FileOperator
    {
        /// <summary>
        /// 验证库文件是否存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static async Task<bool> CheckFileAsync(string fileName)
        {
            bool fileIsExist = true;
            try
            {
                Windows.Storage.StorageFile sf = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            }
            catch
            {
                fileIsExist = false;
            }
            return fileIsExist;
        }
    }
}
