using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace ZhiHuApp.Behaviors
{
    [ContentProperty(Name = "Actions")]
    public class TimerTriggerBehavior : Behavior<DependencyObject>
    {

        private DispatcherTimer _timer = new DispatcherTimer();

        private int _millisecondPerTick = 1000;

        public int MilliSecondsPerTick
        {
            get { return _millisecondPerTick; }
            set
            {
                _millisecondPerTick = value;
                _timer.Interval = TimeSpan.FromMilliseconds(_millisecondPerTick);
            }
        }

        #region Actions Dependency Property

        /// <summary> 
        /// Actions collection 
        /// </summary> 
        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)base.GetValue(ActionsProperty);
                if (actions == null)
                {
                    actions = new ActionCollection();
                    base.SetValue(ActionsProperty, actions);
                }
                return actions;
            }
        }

        /// <summary> 
        /// Backing storage for Actions collection 
        /// </summary> 
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register("Actions",
                                        typeof(ActionCollection),
                                        typeof(SwipeTriggerBehavior),
                                        new PropertyMetadata(null));

        #endregion Actions Dependency Property

        protected void Execute(object sender, object parameter)
        {
            Interaction.ExecuteActions(sender, this.Actions, parameter);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            _timer.Tick += timer_Tick;
            if (this.IsEnabled)
            {
                _timer.Start();
            }
        }

        private void timer_Tick(object sender, object e)
        {
            this.Execute(this, null);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _timer.Stop();
            _timer.Tick -= timer_Tick;
        }

        #region IsEnabled Dependency Property

        /// <summary> 
        /// Get or Sets the IsEnabled dependency property.  
        /// </summary> 
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        /// <summary> 
        /// Identifies the IsEnabled dependency property. 
        /// This enables animation, styling, binding, etc...
        /// </summary> 
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled",
                    typeof(bool),
                    typeof(TimerTriggerBehavior),
                    new PropertyMetadata(true, OnIsEnabledPropertyChanged));

        /// <summary>
        /// IsEnabled changed handler. 
        /// </summary>
        /// <param name="d">TimerTriggerBehavior that changed its IsEnabled.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param> 
        private static void OnIsEnabledPropertyChanged(DependencyObject d,
                                DependencyPropertyChangedEventArgs e)
        {
            var source = d as TimerTriggerBehavior;
            if (source != null)
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    source._timer.Start();
                }
                else
                {
                    source._timer.Stop();
                }
            }
        }

        #endregion IsEnabled Dependency Property
    }
}
