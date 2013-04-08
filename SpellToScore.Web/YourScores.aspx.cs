using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SpellToScore.Web
{
    public partial class YourScores : System.Web.UI.Page
    {
        User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get logged in user
            if (UserLogin.LoggedInUser != null)
            {
                // Logged in
                currentUser = UserLogin.LoggedInUser;

                if (currentUser.UserType == 1)
                {
                    lblInfo.Text = currentUser.FirstName + " " + currentUser.Surname + ", you are logged in as a child.";
                }
                else if (currentUser.UserType == 2)
                {
                    lblInfo.Text = currentUser.Title + " " + currentUser.Surname + ", you are logged in as a teacher.";
                }

                CreateScoresTable();
            }
            else
            {
                // If not logged in
                lblInfo.Text = "You are not logged in. To view your scores, log in on the homepage.";
                userScoresTbl.Visible = false;
            }
        }

        public void CreateScoresTable()
        {
            List<Score> scores = DatabaseWebService.GetScoresByUser(currentUser.Id);

            try
            {
                foreach (var score in scores)
                {
                    TableRow row = new TableRow();

                    TableCell position = new TableCell();
                    position.Text = score.Position.ToString();
                    row.Cells.Add(position);

                    TableCell name = new TableCell();
                    name.Text = score.User.FirstName + " " + score.User.Surname;
                    row.Cells.Add(name);

                    TableCell playerScore = new TableCell();
                    playerScore.Text = score.PlayerScore.ToString();
                    row.Cells.Add(playerScore);

                    TableCell dateTime = new TableCell();
                    dateTime.Text = score.DateTime;
                    row.Cells.Add(dateTime);

                    userScoresTbl.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                // Error connecting to database
                TableRow row = new TableRow();
                TableCell errorMessage = new TableCell();
                errorMessage.Text = "Error loading scores, please try again.";
                errorMessage.ColumnSpan = 4;
                row.Cells.Add(errorMessage);
                userScoresTbl.Rows.Add(row);
            }
        }
    }
}