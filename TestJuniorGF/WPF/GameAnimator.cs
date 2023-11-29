using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TestJuniorGF
{
    public class GameAnimator
    {
        private const int AppScale = 25;
        private const int DestroySpeed = 200;
        private const int MoveSpeed = 100;
        
        public void DestroyAnimation(Image cell)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = cell.ActualWidth,
                To = cell.ActualWidth + AppScale,
                Duration = TimeSpan.FromMilliseconds(DestroySpeed)
            };
            var heightAnimation = new DoubleAnimation()
            {
                From = cell.ActualHeight,
                To = cell.ActualHeight + AppScale,
                Duration = TimeSpan.FromMilliseconds(DestroySpeed)
            };
            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(DestroySpeed)
            };
            cell.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            cell.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
            cell.BeginAnimation(FrameworkElement.HeightProperty, heightAnimation);
        }

        public void MoveAnimation(Image cell, Vector2 from, Vector2 to)
        {
            DoubleAnimation moveAnimation;
            if (from.X == to.X)
            {
                moveAnimation = new DoubleAnimation
                {
                    From = GameWindow.CanvasTop + GameWindow.CellSizePx * from.Y,
                    To = GameWindow.CanvasTop + GameWindow.CellSizePx * to.Y,
                    Duration = TimeSpan.FromMilliseconds(MoveSpeed)
                };
                cell.BeginAnimation(Canvas.TopProperty, moveAnimation);
            }
            else
            {
                moveAnimation = new DoubleAnimation
                {
                    From = GameWindow.CanvasLeft + GameWindow.CellSizePx * from.X,
                    To = GameWindow.CanvasLeft + GameWindow.CellSizePx * to.X,
                    Duration = TimeSpan.FromMilliseconds(MoveSpeed)
                };
                cell.BeginAnimation(Canvas.LeftProperty, moveAnimation);
            }
        }
    }
}