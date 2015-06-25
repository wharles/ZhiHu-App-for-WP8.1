using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace ZhiHuApp.Behaviors
{
    public abstract class Behavior<T> : DependencyObject, IBehavior
        where T : DependencyObject
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        public T AssociatedObject { get; set; }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == this.AssociatedObject ||
                DesignMode.DesignModeEnabled)
            {
                return;
            }

            this.AssociatedObject = (T)associatedObject;
            OnAttached();
        }

        public void Detach()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                OnDetaching();
            }
        }

        DependencyObject IBehavior.AssociatedObject
        {
            get { return this.AssociatedObject; }
        }
    }
}
