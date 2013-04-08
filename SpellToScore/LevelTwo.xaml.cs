using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpellToScore
{
    public partial class LevelTwo : UserControl
    {
        Cannon cannon;
        Image shipImg = new Image();
        Image bgImg = new Image();
        bool movingRight = true;
        Scoring scoring = new Scoring();
        bool runOnce = false;

        List<Ball> balls;
        private int cannonShootLimiter = 0;
        private int cannonShootDelay = 16;
        Ball ball = new Ball();

        Number number = new Number();
        List<Number> numbers;
        private int numberTimer = 200;
        private int numbersHitCount = 0;
        private int numberCollected;

        List<Cloud> clouds;
        private int cloudTimer = 0;

        List<Parrot> parrots;
        private int parrotTimer = 0;

        List<Seagull> seagulls;
        private int seagullTimer = 0;

        private int score;

        // The length of the level in seconds
        private int gameLength = 90;
        private int timeLeft;

        // Score boxes
        TextBlock scoreTxt = new TextBlock();
        TextBlock timerTxt = new TextBlock();

        MediaElement sound = new MediaElement();

        public LevelTwo(int levelOneScore)
        {
            // Save the level one score passed as a parameter
            this.score = levelOneScore;

            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            SolidColorBrush bgBrush = new SolidColorBrush();
            bgBrush.Color = Colors.Gray;
            LayoutRoot.Background = bgBrush;

            CanvasLayoutRoot.Width = 800;
            CanvasLayoutRoot.Height = 480;

            // Render the moving objects
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            SolidColorBrush bgCanvasBrush = new SolidColorBrush();
            bgCanvasBrush.Color = Colors.White;
            CanvasLayoutRoot.Background = bgCanvasBrush;

            bgImg.Source = new BitmapImage(new Uri("Images/background.png", UriKind.Relative));
            bgImg.Height = 480;
            bgImg.Width = 970;
            Canvas.SetLeft(bgImg, 0);
            Canvas.SetTop(bgImg, 0);
            CanvasLayoutRoot.Children.Add(bgImg);

            sound.Source = new Uri("Sounds/CannonSound.mp3", UriKind.Relative);
            sound.Volume = 1;
            sound.AutoPlay = false;
            LayoutRoot.Children.Add(sound);

            // Update amount of time left
            timeLeft = gameLength;

            numbers = new List<Number>();
            balls = new List<Ball>();
            clouds = new List<Cloud>();
            parrots = new List<Parrot>();
            seagulls = new List<Seagull>();

            shipImg.Source = new BitmapImage(new Uri("Images/ship_side.png", UriKind.Relative));
            shipImg.Height = 100;
            shipImg.Width = 800;
            Canvas.SetLeft(shipImg, 0);
            Canvas.SetTop(shipImg, (CanvasLayoutRoot.Height - shipImg.Height) + 10);
            Canvas.SetZIndex(shipImg, 4);
            CanvasLayoutRoot.Children.Add(shipImg);

            cannon = new Cannon();
            Canvas.SetLeft(cannon, 70);
            Canvas.SetTop(cannon, (CanvasLayoutRoot.Height - cannon.CannonHeight) - 5);
            Canvas.SetZIndex(cannon, 4);
            CanvasLayoutRoot.Children.Add(cannon);

            // Calls an event on load
            this.Loaded += new RoutedEventHandler(LevelTwo_Loaded);

            scoreTxt.Text = "Score: " + score;
            scoreTxt.Height = 20;
            scoreTxt.Width = 110;
            scoreTxt.FontWeight = FontWeights.Bold;
            TopMenu.Children.Add(scoreTxt);

            timerTxt.Text = "Time remaining: " + timeLeft;
            timerTxt.Height = 20;
            timerTxt.Width = 130;
            timerTxt.FontWeight = FontWeights.Bold;
            timerTxt.Loaded += StartTimer;
            TopMenu.Children.Add(timerTxt);
        }

        void LevelTwo_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            cannon.Focus();
        }

        public void AddBall(Ball b)
        {
            balls.Add(b);
            CanvasLayoutRoot.Children.Add(b);
        }

        public void RemoveBall(Ball b)
        {
            balls.Remove(b);
            CanvasLayoutRoot.Children.Remove(b);
        }

        public void AddNumber(Number n)
        {
            numbers.Add(n);
            CanvasLayoutRoot.Children.Add(n);
        }

        public void RemoveNumber(Number n)
        {
            numbers.Remove(n);
            CanvasLayoutRoot.Children.Remove(n);
        }

        public void AddCloud(Cloud c)
        {
            clouds.Add(c);
            CanvasLayoutRoot.Children.Add(c);
        }

        public void RemoveCloud(Cloud c)
        {
            clouds.Remove(c);
            CanvasLayoutRoot.Children.Remove(c);
        }

        public void AddParrot(Parrot p)
        {
            parrots.Add(p);
            CanvasLayoutRoot.Children.Add(p);
        }

        public void RemoveParrot(Parrot p)
        {
            parrots.Remove(p);
            CanvasLayoutRoot.Children.Remove(p);
        }

        public void AddSeagull(Seagull s)
        {
            seagulls.Add(s);
            CanvasLayoutRoot.Children.Add(s);
        }

        public void RemoveSeagull(Seagull s)
        {
            seagulls.Remove(s);
            CanvasLayoutRoot.Children.Remove(s);
        }

        public bool DetectCollision(ContentControl ctrl1, ContentControl ctrl2)
        {
            // Draw rectangles over the two objects
            Rect ctrl1Rect = new Rect(
                new Point(Convert.ToDouble(ctrl1.GetValue(Canvas.LeftProperty)),
                    Convert.ToDouble(ctrl1.GetValue(Canvas.TopProperty))),
                new Point((Convert.ToDouble(ctrl1.GetValue(Canvas.LeftProperty)) + ctrl1.ActualWidth),
                    (Convert.ToDouble(ctrl1.GetValue(Canvas.TopProperty)) + ctrl1.ActualHeight))
            );

            Rect ctrl2Rect = new Rect(
                new Point(Convert.ToDouble(ctrl2.GetValue(Canvas.LeftProperty)),
                    Convert.ToDouble(ctrl2.GetValue(Canvas.TopProperty))),
                new Point((Convert.ToDouble(ctrl2.GetValue(Canvas.LeftProperty)) + ctrl2.ActualWidth),
                    (Convert.ToDouble(ctrl2.GetValue(Canvas.TopProperty)) + ctrl2.ActualHeight))
            );

            // Check whether the rectangles are intersecting (therefore colliding)
            ctrl1Rect.Intersect(ctrl2Rect);

            // Return false if 'ctrl1Rect' is empty
            return !(ctrl1Rect == Rect.Empty);
        }

        // Event handler that controls the 'time remaining' text box
        public void StartTimer(object o, RoutedEventArgs sender)
        {
            DispatcherTimer myDispatcherTimer = new DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); // (days, hours, mins, secs, msecs) - 1 second
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }

        // Event handler that is raised every second while the DispatcherTimer is active
        public void Each_Tick(object o, EventArgs sender)
        {
            // Check if there is any time remaining
            if (timeLeft > 0)
            {
                timeLeft--;
                timerTxt.Text = "Time remaining: " + timeLeft;
            }
            else if (runOnce == false)
            {
                // No time remaining
                // Navigate the user to the save score page with 'score' as a parameter
                ((App)App.Current).Navigate(new SaveScore(score));

                // Change boolean so that the event handler isn't called again and the score isn't duplicated
                runOnce = true;
            }
        }

        // Event handler which renders the moving objects
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // Scrolling background
            // Move background right
            if (movingRight)
            {
                Canvas.SetLeft(bgImg, Canvas.GetLeft(bgImg) - 1);

                if (Canvas.GetLeft(bgImg) <= -165)
                {
                    movingRight = false;
                }
            }
            // Move background left
            else
            {
                Canvas.SetLeft(bgImg, Canvas.GetLeft(bgImg) + 1);

                if (Canvas.GetLeft(bgImg) > -5)
                {
                    movingRight = true;
                }
            }

            // Delays the cannon from being fired excessively
            if (cannonShootLimiter < cannonShootDelay)
            {
                cannonShootLimiter += 1;
            }

            // Calls the 'Update' method of all the balls
            for (int indexBall = 0; indexBall < balls.Count; indexBall++)
            {
                balls[indexBall].Update(CanvasLayoutRoot);

                // Nested loop which will check every ball against every number and detect collisions
                for (int indexOther = 0; indexOther < numbers.Count; indexOther++)
                {
                    try
                    {
                        // True is collision
                        if (DetectCollision(balls[indexBall], numbers[indexOther]))
                        {
                            // Increase the 'numberCount' int
                            numbersHitCount++;
                            numberCollected = int.Parse(numbers[indexOther].NumberValue);

                            // There has been a collision
                            // Remove the ball using its list index
                            RemoveBall(balls[indexBall]);

                            // Remove the number using its list index
                            RemoveNumber(numbers[indexOther]);

                            // Calculate the score for the number
                            score = score + scoring.NumberScore(numberCollected);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                // Nested loop which will check every ball against every parrot and detect collisions
                for (int indexParrot = 0; indexParrot < parrots.Count; indexParrot++)
                {
                    try
                    {
                        // True is collision
                        if (DetectCollision(balls[indexBall], parrots[indexParrot]))
                        {
                            // Increase the 'timeLeft' int
                            timeLeft = timeLeft + 10;

                            // There has been a collision
                            // Remove the ball using its list index
                            RemoveBall(balls[indexBall]);

                            // Remove the parrot using its list index
                            RemoveParrot(parrots[indexParrot]);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                // Nested loop which will check every ball against every seagull and detect collisions
                for (int indexSeagull = 0; indexSeagull < seagulls.Count; indexSeagull++)
                {
                    try
                    {
                        // True is collision
                        if (DetectCollision(balls[indexBall], seagulls[indexSeagull]))
                        {
                            // Decrease the 'timeLeft' int
                            timeLeft = timeLeft - 20;

                            // There has been a collision
                            // Remove the ball using its list index
                            RemoveBall(balls[indexBall]);

                            // Remove the seagull using its list index
                            RemoveSeagull(seagulls[indexSeagull]);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            // Add flying numbers
            numberTimer += 1;
            if (numberTimer > 100)
            {
                numberTimer = 0;
                AddNumber(new Number());
            }

            for (int index = 0; index < numbers.Count; index++)
            {
                numbers[index].Update(CanvasLayoutRoot);
            }

            // Add clouds
            cloudTimer += 1;
            if (cloudTimer > 400)
            {
                cloudTimer = 0;
                AddCloud(new Cloud());
            }

            for (int index = 0; index < clouds.Count; index++)
            {
                clouds[index].Update(CanvasLayoutRoot);
            }

            // Add parrots
            parrotTimer += 1;
            if (parrotTimer > 700)
            {
                parrotTimer = 0;
                AddParrot(new Parrot());
            }

            for (int index = 0; index < parrots.Count; index++)
            {
                parrots[index].Update(CanvasLayoutRoot);
            }

            // Add seagulls
            seagullTimer += 1;
            if (seagullTimer > 300)
            {
                seagullTimer = 0;
                AddSeagull(new Seagull());
            }

            for (int index = 0; index < seagulls.Count; index++)
            {
                seagulls[index].Update(CanvasLayoutRoot);
            }

            scoreTxt.Text = "Score: " + score;
            timerTxt.Text = "Time remaining: " + timeLeft;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Tilt cannon up
            if (e.Key == Key.Up)
            {
                this.cannon.Move(Direction.Up, CanvasLayoutRoot);
            }

            // Tilt cannon down
            else if (e.Key == Key.Down)
            {
                this.cannon.Move(Direction.Down, CanvasLayoutRoot);
            }

            // Move cannon left
            else if (e.Key == Key.Left)
            {
                this.cannon.Move(Direction.Left, CanvasLayoutRoot);
            }

            // Move cannon right
            else if (e.Key == Key.Right)
            {
                this.cannon.Move(Direction.Right, CanvasLayoutRoot);
            }

            // Shoot canon ball
            else if (e.Key == Key.Space && cannonShootLimiter >= cannonShootDelay)
            {
                // Cannon is pointing right
                if (cannon.CannonDirection == 1)
                {
                    AddBall(new Ball((Canvas.GetLeft(cannon)) + ((cannon.CannonHeight / 5) * 1), (Canvas.GetTop(cannon)) + ((cannon.CannonHeight / 3) * 1), cannon.CannonDirection, cannon.CannonAngle));
                }
                // Cannon is pointing left
                else
                {
                    AddBall(new Ball((Canvas.GetLeft(cannon)) + (((cannon.CannonHeight / 10) * 3)), (Canvas.GetTop(cannon)) + ((cannon.CannonHeight / 3) * 1), cannon.CannonDirection, cannon.CannonAngle));
                }

                // Stop the cannon sound if it is still playing
                sound.Stop();
                sound.Play();

                cannonShootLimiter = 0;
            }
        }
    }
}