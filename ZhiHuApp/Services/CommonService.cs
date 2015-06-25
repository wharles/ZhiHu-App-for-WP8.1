using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiHuApp.Models;

namespace ZhiHuApp.Services
{
    public class CommonService<T> : ServiceBase, ICommonService<T> where T : new()
    {
        public string ExceptionsParameter { set; get; }

        public async Task<T> GetObjectAsync(params string[] parameter)
        {
            List<string> paras = new List<string>(parameter);
            string url = urls.GetUrl(paras);
            var result = await GetDataAsync<T>(url);
            if (result != null)
            {
                return result;
            }
            else
            {
                ExceptionsParameter = "未能获取到数据";
                return new T();
            }
        }
    }
}
