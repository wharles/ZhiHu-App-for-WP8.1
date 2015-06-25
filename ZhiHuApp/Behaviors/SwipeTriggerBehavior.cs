using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using ZhiHuApp.Common;

namespace ZhiHuApp.Behaviors
{
    [ContentProperty(Name = "Actions")]
    public class SwipeTriggerBehavior : Behavior<UIElement>
    {

        /// <summary>
        /// Get/Sets the direction of the Swipe gesture 
        /// </summary>
        public SwipeDirection Direction { get; set; }

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

            this.AssociatedObject.ManipulationMode =
                this.AssociatedObject.ManipulationMode |
                ManipulationModes.TranslateX |
                ManipulationModes.TranslateY;

            this.AssociatedObject.ManipulationCompleted += OnManipulationCompleted;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.ManipulationCompleted -= OnManipulationCompleted;
        }

        private void OnManipulationCompleted(object sender,
                                            ManipulationCompletedRoutedEventArgs e)
        {
            bool isRight = e.Velocities.Linear.X.Between(0.3, 100);
            bool isLeft = e.Velocities.Linear.X.Between(-100, -0.3);

            bool isUp = e.Velocities.Linear.Y.Between(-100, -0.3);
            bool isDown = e.Velocities.Linear.Y.Between(0.3, 100);

            switch (this.Direction)
            {
                case SwipeDirection.Left:
                    if (isLeft && !(isUp || isDown))
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.Right:
                    if (isRight && !(isUp || isDown))
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.Up:
                    if (isUp && !(isRight || isLeft))
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.Down:
                    if (isDown && !(isRight || isLeft))
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.LeftDown:
                    if (isLeft && isDown)
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.LeftUp:
                    if (isLeft && isUp)
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.RightDown:
                    if (isRight && isDown)
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                case SwipeDirection.RightUp:
                    if (isRight && isUp)
                    {
                        this.Execute(this.AssociatedObject, null);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public enum SwipeDirection
    {
        Left,
        Right,
        Up,
        Down,
        LeftDown,
        LeftUp,
        RightDown,
        RightUp,
    }
}
