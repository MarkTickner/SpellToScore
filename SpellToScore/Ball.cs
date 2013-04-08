using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SpellToScore
{
    public class Ball : ContentControl, IGameEntity
    {
        private int ballSpeed = 15;
        private int ballDirection; // 1 = right, 2 = left
        private int cannonAngle;

        private int ballSize = 25;
        public int BallSize
        {
            get { return ballSize; }
        }

        public Ball()
        {
        }

        public Ball(double left, double top, int ballDirection, int cannonAngle)
        {
            this.ballDirection = ballDirection;
            this.cannonAngle = cannonAngle;

            Ellipse cannonBall = new Ellipse();

            cannonBall.Height = ballSize;
            cannonBall.Width = ballSize;
            cannonBall.Fill = new SolidColorBrush(Colors.Gray);
            cannonBall.Stroke = new SolidColorBrush(Colors.Black);
            cannonBall.StrokeThickness = 2;

            this.Content = cannonBall;

            Canvas.SetLeft(this, left);
            Canvas.SetTop(this, top);
            Canvas.SetZIndex(this, 3);
        }

        public void Update(Canvas c)
        {
            if (ballDirection == 1)
            {
                Move(Direction.Right, c, cannonAngle);
            }
            else
            {
                Move(Direction.Left, c, cannonAngle);
            }

            if (Canvas.GetLeft(this) < -c.Width)
            {
                c.Children.Remove(this);
            }
        }

        // Method body required as it is specified in the interface
        public void Move(Direction direction, Canvas theCanvas)
        {
        }

        public void Move(Direction direction, Canvas theCanvas, double cannonAngle)
        {
            // If the cannon is facing right
            if (direction == Direction.Right)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) + ballSpeed);

                // Calculate the direction for the ball to move in based on the angle cannon
                if (cannonAngle >= -6)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) + ((cannonAngle / 2) - 3));
                }
                else if (cannonAngle >= -8)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) + ((cannonAngle / 2) - 2));
                }
                else if (cannonAngle >= -10)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) + ((cannonAngle / 2) - 1));
                }
                else if (cannonAngle >= -32)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) + ((cannonAngle / 2)));
                }
            }
            // If the cannon is facing left
            else
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) - ballSpeed);

                // Calculate the direction for the ball to move in based on the angle cannon
                if (cannonAngle >= -6)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) - ((cannonAngle / 2) + 3));
                }
                else if (cannonAngle >= -8)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) - ((cannonAngle / 2) + 2));
                }
                else if (cannonAngle >= -10)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) - ((cannonAngle / 2) + 1));
                }
                else if (cannonAngle >= -32)
                {
                    Canvas.SetTop(this, Canvas.GetTop(this) - ((cannonAngle / 2)));
                }
            }

            // Remove balls when they go off the canvas to free up memory
            // Left
            if (Canvas.GetLeft(this) <= -this.Width)
            {
                theCanvas.Children.Remove(this);
            }
            // Right
            else if (Canvas.GetLeft(this) >= theCanvas.Width + this.ballSize)
            {
                theCanvas.Children.Remove(this);
            }
            // Top
            else if (Canvas.GetTop(this) <= -this.ballSize)
            {
                theCanvas.Children.Remove(this);
            }
        }
    }
}