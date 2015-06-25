using GalaSoft.MvvmLight;
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
    public class CommentsPageViewModel : ViewModelBase
    {
        private readonly string _type;
        private readonly string _id;

        public CommentsPageViewModel(int id, string name, string type)
        {
            this._type = type;
            this._id = id.ToString();
            this.Name = name;
            this.LoadComments();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private Comments comments;

        public Comments Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                RaisePropertyChanged(() => Comments);
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

        private void LoadComments()
        {
            Task.Run(async () =>
            {
                ICommonService<Comments> newsContentService = new CommonService<Comments>();
                var result = await newsContentService.GetObjectAsync("4", "story", _id, _type);

                await DispatcherHelper.RunAsync(async () =>
                {
                    if (result != null)
                    {
                        this.Comments = result;
                        this.IsActive = false;
                    }
                    else
                    {
                        MessageDialog msg = new MessageDialog(newsContentService.ExceptionsParameter, "提示");
                        await msg.ShowAsync();
                    }
                });
            });
        }
    }
}
