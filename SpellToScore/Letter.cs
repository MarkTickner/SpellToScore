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
    public class Letter : ContentControl, IGameEntity
    {
        private int speed = 0;
        private int letterSize = 30;

        private string letterValue;
        public string LetterValue
        {
            get { return letterValue; }
        }

        public Letter()
        {
            Random random = new Random();

            // Create a border layout with the skull image background
            Border skullBorder = new Border();
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/skull.png", UriKind.Relative));
            skullBorder.Background = ib;
            skullBorder.Width = letterSize;
            skullBorder.Height = letterSize;

            // Add a random letter to the border layout using the 'LetterGenerator' class
            LetterGenerator letters = new LetterGenerator();
            TextBlock lettersTxt = new TextBlock();
            lettersTxt.FontSize = 16;
            lettersTxt.Foreground = new SolidColorBrush(Colors.Black);
            lettersTxt.HorizontalAlignment = HorizontalAlignment.Center;
            lettersTxt.VerticalAlignment = VerticalAlignment.Top;
            letterValue = letters.GetRandomLetter();
            lettersTxt.Text = letterValue;
            skullBorder.Child = lettersTxt;

            // Add the border layout to the object
            this.Content = skullBorder;

            Canvas.SetLeft(this, 810);
            Canvas.SetTop(this, random.Next(50, 200));
            Canvas.SetZIndex(this, 3);
            speed = random.Next(1, 4);
        }

        public void Update(Canvas c)
        {
            Move(Direction.Left, c);

            // Remove letters when they go off the left of the canvas to free up memory
            if (Canvas.GetLeft(this) <= -this.ActualWidth)
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