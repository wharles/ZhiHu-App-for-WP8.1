using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace ZhiHuApp.ViewModels
{
    public class DefaultThreadPoolTimer
    {
        public delegate void TimerEventHandler();

        public static event TimerEventHandler UpdateUI;

        static void RaiseTimeCome()
        {
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        private static readonly TimeSpan period = TimeSpan.FromSeconds(5);

        private static ThreadPoolTimer PeriodicTimer = null;

        public static void Run()
        {
            RaiseTimeCome();
            PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                RaiseTimeCome();
            }, period);
        }

        public static void Stop()
        {
            if (PeriodicTimer != null)
            {
                PeriodicTimer.Cancel();
            }
        }
    }
}
