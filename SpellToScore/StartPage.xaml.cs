using System;
using System.Collections.Generic;
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

namespace SpellToScore
{
    public partial class StartPage : UserControl
    {
        Button playBtn = new Button();
        Button howToPlayBtn = new Button();
        Button highScoresBtn = new Button();

        MediaElement sound = new MediaElement();

        public StartPage()
        {
            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/menu_screen.png", UriKind.Relative));
            LayoutRoot.Background = ib;

            sound.Source = new Uri("Sounds/StartPageSound.mp3", UriKind.Relative);
            sound.Volume = 1;
            sound.AutoPlay = false;
            sound.MediaOpened += new RoutedEventHandler(sound_MediaOpened);
            LayoutRoot.Children.Add(sound);

            playBtn.Content = "Play";
            playBtn.Width = 250;
            playBtn.Height = 55;
            playBtn.FontSize = 26;
            playBtn.Click += new RoutedEventHandler(playBtn_Click);
            Canvas.SetLeft(playBtn, ((LayoutRoot.Width - playBtn.Width) - 20));
            Canvas.SetTop(playBtn, 320);
            LayoutRoot.Children.Add(playBtn);

            highScoresBtn.Content = "High Scores";
            highScoresBtn.Width = 120;
            highScoresBtn.Height = 35;
            highScoresBtn.FontSize = 16;
            highScoresBtn.Click += new RoutedEventHandler(highScoresBtn_Click);
            Canvas.SetLeft(highScoresBtn, ((LayoutRoot.Width - highScoresBtn.Width) - 20));
            Canvas.SetTop(highScoresBtn, 400);
            LayoutRoot.Children.Add(highScoresBtn);

            howToPlayBtn.Content = "How To Play";
            howToPlayBtn.Width = 120;
            howToPlayBtn.Height = 35;
            howToPlayBtn.FontSize = 16;
            howToPlayBtn.Click += new RoutedEventHandler(howToPlayBtn_Click);
            Canvas.SetLeft(howToPlayBtn, ((Canvas.GetLeft(highScoresBtn) - howToPlayBtn.Width) - 10));
            Canvas.SetTop(howToPlayBtn, 400);
            LayoutRoot.Children.Add(howToPlayBtn);
        }

        // Event handler for when the sound file has loaded
        void sound_MediaOpened(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Play();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new LevelOne());
        }

        private void howToPlayBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new HowToPlay());
        }

        private void highScoresBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new HighScores());
        }
    }
}