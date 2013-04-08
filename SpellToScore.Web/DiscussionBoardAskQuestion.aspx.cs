using System;
using System.Web.UI;

namespace SpellToScore.Web
{
    public partial class DiscussionBoardAskQuestion : Page
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
            }
            else
            {
                // If not logged in
                // Must be logged in, redirect to Discussion Board Page
                Response.Redirect("DiscussionBoard.aspx");
            }
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            if (DatabaseWebService.SaveQuestion(currentUser.Id, txtQuestionTitle.Text, txtQuestionText.Text, int.Parse(dropQuestionLesson.SelectedValue)) == true)
            {
                lblConfirmation.Text = "Success, " + txtQuestionTitle.Text + " has been saved.";
            }
            else
            {
                lblConfirmation.Text = "Error saving " + txtQuestionTitle.Text + " to database, please try again.";
            }
        }

        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoard.aspx");
        }
    }
}