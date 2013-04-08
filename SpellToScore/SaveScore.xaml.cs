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
    public partial class SaveScore : UserControl
    {
        TextBlock nameLabelTxt = new TextBlock();
        TextBlock nameTxt = new TextBlock();
        Button saveScoreBtn = new Button();
        Button highScoresBtn = new Button();

        MediaElement sound = new MediaElement();

        int score;
        int childID;
        string childName;

        // Declare the web service reference
        DatabaseWebServiceReference.DatabaseWebServiceSoapClient serv = new DatabaseWebServiceReference.DatabaseWebServiceSoapClient();

        public SaveScore(int score)
        {
            this.score = score;

            InitializeComponent();

            LayoutRoot.Width = 800;
            LayoutRoot.Height = 540;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("Images/save_scores.png", UriKind.Relative));
            LayoutRoot.Background = ib;

            sound.Source = new Uri("Sounds/SaveScoreSound.mp3", UriKind.Relative);
            sound.Volume = 1;
            sound.AutoPlay = true;
            LayoutRoot.Children.Add(sound);

            nameLabelTxt.Text = "Loading...";
            nameLabelTxt.Width = 350;
            nameLabelTxt.FontSize = 16;
            nameLabelTxt.FontWeight = FontWeights.Bold;
            nameLabelTxt.TextAlignment = TextAlignment.Center;
            nameLabelTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(nameLabelTxt, (LayoutRoot.Width / 2) - (nameLabelTxt.Width / 2));
            Canvas.SetTop(nameLabelTxt, 150);
            LayoutRoot.Children.Add(nameLabelTxt);

            nameTxt.Width = 350;
            nameTxt.FontSize = 26;
            nameTxt.FontWeight = FontWeights.Bold;
            nameTxt.TextAlignment = TextAlignment.Center;
            nameTxt.TextWrapping = TextWrapping.Wrap;
            Canvas.SetLeft(nameTxt, (LayoutRoot.Width / 2) - (nameTxt.Width / 2));
            Canvas.SetTop(nameTxt, 235);

            saveScoreBtn.Content = "Save Score";
            saveScoreBtn.Width = 280;
            saveScoreBtn.Height = 55;
            saveScoreBtn.FontSize = 26;
            saveScoreBtn.Click += new RoutedEventHandler(saveScoreBtn_Click);
            Canvas.SetLeft(saveScoreBtn, ((LayoutRoot.Width / 2) - (saveScoreBtn.Width / 2)));
            Canvas.SetTop(saveScoreBtn, 320);

            highScoresBtn.Content = "High Scores";
            highScoresBtn.Width = 280;
            highScoresBtn.Height = 55;
            highScoresBtn.FontSize = 26;
            highScoresBtn.Click += new RoutedEventHandler(highScoresBtn_Click);
            Canvas.SetLeft(highScoresBtn, ((LayoutRoot.Width / 2) - (highScoresBtn.Width / 2)));
            Canvas.SetTop(highScoresBtn, 320);

            // Use the web service to get the ID of the logged in child
            serv.GetLoggedInUserIdCompleted += new EventHandler<DatabaseWebServiceReference.GetLoggedInUserIdCompletedEventArgs>(GetLoggedInUserIdCompleted);
            serv.GetLoggedInUserIdAsync();

            // Use the web service to get the name of the logged in child
            serv.GetLoggedInUserNameCompleted += new EventHandler<DatabaseWebServiceReference.GetLoggedInUserNameCompletedEventArgs>(GetLoggedInUserNameCompleted);
            serv.GetLoggedInUserNameAsync();
        }

        void GetLoggedInUserIdCompleted(object sender, DatabaseWebServiceReference.GetLoggedInUserIdCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != 0)
            {
                // Child is logged in
                // Save name to 'childID' variable
                childID = int.Parse(e.Result.ToString());
            }
        }

        void GetLoggedInUserNameCompleted(object sender, DatabaseWebServiceReference.GetLoggedInUserNameCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                // Child is logged in
                // Save name to 'childName' variable
                childName = e.Result.ToString();

                // Check score
                if (score <= 0 && childName != null)
                {
                    // Score of 0
                    nameLabelTxt.Text = "You scored " + score + " overall, better luck next time!";
                    nameTxt.Text = childName;
                    LayoutRoot.Children.Add(nameTxt);
                    LayoutRoot.Children.Add(highScoresBtn);
                }
                else if (score > 0 && childName != null)
                {
                    // Score above 0
                    nameLabelTxt.Text = "You scored " + score + " overall!";
                    nameTxt.Text = childName;
                    LayoutRoot.Children.Add(nameTxt);
                    LayoutRoot.Children.Add(saveScoreBtn);
                }
            }
            else
            {
                // Child not logged in
                // Check score
                if (score <= 0)
                {
                    // Score of 0
                    nameLabelTxt.Text = "You scored " + score + " overall, better luck next time!";
                    LayoutRoot.Children.Add(highScoresBtn);
                }
                else if (score > 0)
                {
                    // Score above 0
                    nameLabelTxt.Text = "You scored " + score + " overall! Next time log in to save your score.";
                    LayoutRoot.Children.Add(highScoresBtn);
                }
            }
        }

        private void highScoresBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).Navigate(new HighScores());
        }

        private void saveScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            // Use the web service to save the users score to the database
            serv.SaveScoreCompleted += new EventHandler<DatabaseWebServiceReference.SaveScoreCompletedEventArgs>(SaveScoreCompleted);
            serv.SaveScoreAsync(childID, score);
        }

        void SaveScoreCompleted(object sender, DatabaseWebServiceReference.SaveScoreCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result == true)
                {
                    // Save successful
                    ((App)App.Current).Navigate(new HighScores());
                }
                else
                {
                    // Save not successful
                    MessageBox.Show("Error saving score: " + nameTxt.Text + ", " + score + ". Please try again.");
                }
            }
        }
    }
}