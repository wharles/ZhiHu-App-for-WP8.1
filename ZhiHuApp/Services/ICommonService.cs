using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiHuApp.Models;

namespace ZhiHuApp.Services
{
    public interface ICommonService<T>
    {
        string ExceptionsParameter { set; get; }
        Task<T> GetObjectAsync(params string[] parameter);
    }
}
