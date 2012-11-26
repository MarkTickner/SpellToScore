using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherSupportSystem
{
    public partial class DiscussionBoardAskQuestion : System.Web.UI.Page
    {
        int logInState; // 0 = logged out, 1 = logged in as a child, 2 = logged in as a teacher
        string loggedInUser; // the name of the user logged in, if any
        int loggedInUserID; // the ID of the user logged in, if any

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get int from session, if it exists
            if (Session["logInState"] != null)
            {
                // Session exists - user is logged in
                // Get int from session and assign to 'logInState'
                logInState = (int)Session["logInState"];
            }

            // Get name from session, if it exists
            if (Session["loggedInUser"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUser = (string)Session["loggedInUser"];
            }

            // Get ID from session, if it exists
            if (Session["loggedInUserID"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUserID = (int)Session["loggedInUserID"];
            }

            // Logged in or out specific actions
            if (logInState == 1)
            {
                // If logged in as a child
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a child.";
            }
            else if (logInState == 2)
            {
                // If logged in as a teacher
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a teacher.";
            }
            else
            {
                // If not logged in
                // Must be logged in, redirect to Discussion Board Page
                Response.Redirect("DiscussionBoard.aspx");

                // Show message to user
                lblInfo.Text = "You are not logged in.";
            }
        }

        // Save question to database
        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            // Save question to database using 'SaveQuestion' method
            if (MyDBConnection.SaveQuestion(loggedInUserID, txtQuestionTitle.Text, txtQuestionText.Text, int.Parse(dropQuestionLesson.SelectedValue)) == true)
            {
                // Show message to user
                lblConfirmation.Text = "Success, " + txtQuestionTitle.Text + " has been saved.";
            }
            else
            {
                // If question hasn't been saved
                lblConfirmation.Text = "Error saving " + txtQuestionTitle.Text + " to database, please try again.";
            }
        }

        // Redirects the browser to the 'DiscussionBoard' page
        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoard.aspx");
        }
    }
}