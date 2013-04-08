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
    public class Cannon : ContentControl, IGameEntity
    {
        private int cannonMoveVelocity = 5; // The speed the cannon moves at
        private int cannonAngleSpeed = 1; // The speed that the cannon angles at
        private int cannonAngleMax = 30; // Maximum upwards facing angle
        private int leftOrRight = 2; // 1 = left, 2 = right
        private int wheelAngle = 0; // The angle of the cannon wheel

        private int cannonDirection = 1; // 1 = pointing right, -1 = pointing left
        public int CannonDirection
        {
            get { return cannonDirection; }
        }

        private int cannonHeight = 135;
        public int CannonHeight
        {
            get { return cannonHeight; }
        }

        private int cannonAngle = 0; // Start angle, 0 = flat
        public int CannonAngle
        {
            get { return cannonAngle; }
        }

        Image wheelImg = new Image();
        Image cannonImg = new Image();

        public Cannon()
        {
            Canvas cannonCanvas = new Canvas();
            cannonCanvas.Width = 175;
            cannonCanvas.Height = cannonHeight;

            cannonImg.Source = new BitmapImage(new Uri("Images/cannon.png", UriKind.Relative));
            cannonImg.Width = 175;
            cannonImg.Height = cannonHeight;
            Canvas.SetLeft(cannonImg, 0);
            Canvas.SetTop(cannonImg, 0);
            cannonCanvas.Children.Add(cannonImg);

            wheelImg.Source = new BitmapImage(new Uri("Images/cannon_wheel.png", UriKind.Relative));
            wheelImg.Width = 95;
            wheelImg.Height = 88;
            Canvas.SetLeft(wheelImg, 8);
            Canvas.SetTop(wheelImg, 45);
            cannonCanvas.Children.Add(wheelImg);

            this.Content = cannonCanvas;
        }

        // Not used but needed as it is in the interface
        public void Update(Canvas theCanvas)
        {
        }

        public void MakeBubble(Canvas theCanvas, Ball b)
        {
            theCanvas.Children.Add(b);
        }

        // Method which moves the cannon depending on which direction key is pressed
        public void Move(Direction direction, Canvas canvas)
        {
            // Rotate cannon up
            if (direction == Direction.Up)
            {
                // If cannon is pointing left
                if (leftOrRight == 1)
                {
                    if (cannonAngle <= (+cannonAngleMax))
                    {
                        cannonAngle = cannonAngle + cannonAngleSpeed;
                        RotateFlipCannon();
                    }
                }
                // If cannon is pointing right
                if (leftOrRight == 2)
                {
                    if (cannonAngle >= (-cannonAngleMax))
                    {
                        cannonAngle = cannonAngle - cannonAngleSpeed;
                        RotateFlipCannon();
                    }
                }
            }

            // Rotate cannon down
            if (direction == Direction.Down)
            {
                // If cannon is pointing left
                if (leftOrRight == 1)
                {
                    if (cannonAngle >= 0)
                    {
                        cannonAngle = cannonAngle - cannonAngleSpeed;
                        RotateFlipCannon();
                    }
                }
                // If cannon is pointing left
                if (leftOrRight == 2)
                {
                    if (cannonAngle <= 0)
                    {
                        cannonAngle = cannonAngle + cannonAngleSpeed;
                        RotateFlipCannon();
                    }
                }
            }

            // Move cannon left
            if (direction == Direction.Left)
            {
                // Check to see if cannon is at far left of canvas already
                // Compensates for moved CenterX point
                if (Canvas.GetLeft(this) >= ((this.ActualWidth / 3) * 1) + 10)
                {
                    // Cannon not at far left
                    // If cannon isn't pointing left
                    if (leftOrRight != 1)
                    {
                        leftOrRight = 1;
                        cannonAngle = (-cannonAngle);
                    }

                    cannonDirection = -1;
                    RotateFlipCannon();

                    // Rotate the wheel
                    RotateWheel();

                    // Move left
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - cannonMoveVelocity);
                }
            }

            // Move cannon right
            if (direction == Direction.Right)
            {
                // Check to see if cannon is at far right of canvas already
                if (Canvas.GetLeft(this) <= ((canvas.ActualWidth - this.ActualWidth) - 10))
                {
                    // Cannon not at far right
                    // If cannon isn't pointing right
                    if (leftOrRight != 2)
                    {
                        leftOrRight = 2;
                        cannonAngle = (-cannonAngle);
                    }

                    cannonDirection = 1;
                    RotateFlipCannon();

                    // Rotate the wheel
                    RotateWheel();

                    // Move right
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + cannonMoveVelocity);
                }
            }
        }

        // Method which rotates and horizonally flips the cannon
        private void RotateFlipCannon()
        {
            cannonImg.RenderTransform = new CompositeTransform()
            {
                ScaleX = cannonDirection,
                CenterX = (cannonImg.Width / 3), // Puts the pivot point in centre of wheel
                CenterY = (cannonImg.Height / 3) * 2, // Puts the pivot point in centre of wheel
                Rotation = cannonAngle,
            };
        }

        private void RotateWheel()
        {
            if (cannonDirection == 1)
            {
                // Cannon is pointing right
                // Increment the wheel angle variable
                wheelAngle = wheelAngle + 5;
            }
            else if (cannonDirection == -1)
            {
                // Cannon is pointing left
                // Decrement the wheel angle variable
                wheelAngle = wheelAngle - 5;
            }

            wheelImg.RenderTransform = new CompositeTransform()
            {
                ScaleX = cannonDirection,
                CenterX = (wheelImg.Width / 2), // Puts the pivot point in centre of wheel
                CenterY = (wheelImg.Height / 2), // Puts the pivot point in centre of wheel
                Rotation = wheelAngle, // Rotates the wheel by the amount in the wheelAngle int
            };
        }
    }
}