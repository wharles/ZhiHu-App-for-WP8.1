using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiHuApp.ViewModels;
using ZhiHuApp.Services;
using ZhiHuApp.Models;

namespace ZhiHuApp.Locators
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ICommonService<StartImage>, CommonService<StartImage>>();
            SimpleIoc.Default.Register<ICommonService<HotNews>, CommonService<HotNews>>();
            SimpleIoc.Default.Register<ICommonService<Themes>, CommonService<Themes>>();
            SimpleIoc.Default.Register<ICommonService<LatestNews>, CommonService<LatestNews>>();
            SimpleIoc.Default.Register<ICommonService<Sections>, CommonService<Sections>>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
        }

        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public SettingsPageViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsPageViewModel>();
            }
        }
    }
}
