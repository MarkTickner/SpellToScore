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
    public partial class HighScores : UserControl
    {
        TextBlock highScoresTxt = new TextBlock();
        Button backToMenuBtn = new Button();

        public HighScores()
        {
            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/high_scores.png", UriKind.Relative));
            LayoutRoot.Background = ib;

            highScoresTxt.Text = "Loading...";
            highScoresTxt.Width = 540;
            highScoresTxt.FontFamily = new FontFamily("Courier New");
            highScoresTxt.FontWeight = FontWeights.Bold;
            highScoresTxt.FontSize = 22;
            highScoresTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(highScoresTxt, (LayoutRoot.Width / 2) - (highScoresTxt.Width / 2));
            Canvas.SetTop(highScoresTxt, 125);
            LayoutRoot.Children.Add(highScoresTxt);

            backToMenuBtn.Content = "Back to Menu";
            backToMenuBtn.Width = 150;
            backToMenuBtn.Height = 35;
            backToMenuBtn.FontSize = 16;
            backToMenuBtn.Click += new RoutedEventHandler(backToMenuBtn_Click);
            Canvas.SetLeft(backToMenuBtn, (LayoutRoot.Width / 2) - (backToMenuBtn.Width / 2));
            Canvas.SetTop(backToMenuBtn, 480);
            LayoutRoot.Children.Add(backToMenuBtn);

            // Use the web service to load the high scores from the database
            DatabaseWebServiceReference.DatabaseWebServiceSoapClient serv = new DatabaseWebServiceReference.DatabaseWebServiceSoapClient();
            serv.LoadScoresCompleted += new EventHandler<DatabaseWebServiceReference.LoadScoresCompletedEventArgs>(LoadScoresCompleted);
            serv.LoadScoresAsync();
        }

        // Event handler for when the web service has completed
        void LoadScoresCompleted(object sender, DatabaseWebServiceReference.LoadScoresCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                highScoresTxt.Text = e.Result;
            }
            else
            {
                highScoresTxt.Text = "Error loading high scores, please try again.";
            }
        }

        private void backToMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new StartPage());
        }
    }
}