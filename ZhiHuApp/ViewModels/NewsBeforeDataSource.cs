using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiHuApp.Models;
using ZhiHuApp.Services;

namespace ZhiHuApp.ViewModels
{
    public class NewsBeforeDataSource : DataSourceBase<Story>
    {
        private readonly ICommonService<LatestNews> _latestNesService;

        public NewsBeforeDataSource(ICommonService<LatestNews> latestNesService)
        {
            _latestNesService = latestNesService;
        }

        protected async override Task<IList<Story>> LoadItemsAsync()
        {
            string date = DateTime.Now.AddDays(-_currentPage).ToString("yyyyMMdd");
            var result = await _latestNesService.GetObjectAsync("4", "news", "before", date);

            return result.Stories == null ? null : result.Stories.ToList();
        }

        protected override void AddItems(IList<Story> items)
        {
            if (items != null)
            {
                foreach (var news in items)
                {
                    if (!this.Any(n => n.Id == news.Id))
                    {
                        this.Add(news);
                    }
                }
            }
        }
    }
}
