using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpellToScore
{
    public class Seagull : ContentControl, IGameEntity
    {
        private int speed = 0;

        public Seagull()
        {
            Image seagullImage = new Image();
            seagullImage.Source = new BitmapImage(new Uri("Images/seagull.png", UriKind.Relative));
            seagullImage.Width = 75;
            seagullImage.Height = 84;
            this.Content = seagullImage;

            Random random = new Random();

            Canvas.SetLeft(this, 810);
            Canvas.SetTop(this, random.Next(50, 200));
            Canvas.SetZIndex(this, 3);
            speed = random.Next(3, 8);
        }

        public void Update(Canvas c)
        {
            Move(Direction.Left, c);

            // Remove seagull when it has moved off the canvas
            if (Canvas.GetLeft(this) < -c.Width)
            {
                c.Children.Remove(this);
            }
        }

        public void Move(Direction direction, Canvas canvas)
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) - speed);
        }
    }
}