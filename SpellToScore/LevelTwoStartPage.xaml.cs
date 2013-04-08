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
    public partial class LevelTwoStartPage : UserControl
    {
        private int levelOneScore;

        TextBlock completeTxt = new TextBlock();
        TextBlock instructionsTxt = new TextBlock();
        Button playBtn = new Button();

        MediaElement sound = new MediaElement();

        public LevelTwoStartPage(int levelOneScore)
        {
            // Save the level one score passed as a parameter
            this.levelOneScore = levelOneScore;

            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/level_one_complete.png", UriKind.Relative));
            LayoutRoot.Background = ib;

            sound.Source = new Uri("Sounds/LevelTwoStartPageSound.mp3", UriKind.Relative);
            sound.Volume = 1;
            sound.AutoPlay = true;
            LayoutRoot.Children.Add(sound);

            completeTxt.Text = "You scored " + levelOneScore + " in level one, now try the next level!";
            completeTxt.Width = 520;
            completeTxt.FontSize = 16;
            completeTxt.FontWeight = FontWeights.Bold;
            completeTxt.TextAlignment = TextAlignment.Center;
            completeTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(completeTxt, (LayoutRoot.Width / 2) - (completeTxt.Width / 2));
            Canvas.SetTop(completeTxt, 150);
            LayoutRoot.Children.Add(completeTxt);

            instructionsTxt.Text = "Level two uses the same controls, but this time you need to shoot as many even numbers as possible.";
            instructionsTxt.Width = 520;
            instructionsTxt.TextAlignment = TextAlignment.Center;
            instructionsTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(instructionsTxt, (LayoutRoot.Width / 2) - (instructionsTxt.Width / 2));
            Canvas.SetTop(instructionsTxt, 200);
            LayoutRoot.Children.Add(instructionsTxt);

            playBtn.Content = "Play";
            playBtn.Width = 250;
            playBtn.Height = 55;
            playBtn.FontSize = 26;
            playBtn.Click += new RoutedEventHandler(playBtn_Click);
            Canvas.SetLeft(playBtn, (LayoutRoot.Width / 2) - (playBtn.Width / 2));
            Canvas.SetTop(playBtn, 320);
            LayoutRoot.Children.Add(playBtn);
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new LevelTwo(levelOneScore));
        }
    }
}