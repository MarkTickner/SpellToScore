using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherSupportSystem
{
    public partial class InteractiveLessonAdd : System.Web.UI.Page
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
                // Children not allowed here, redirect to Interactive Lesson page
                Response.Redirect("InteractiveLesson.aspx");

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
                // Children not allowed here, redirect to Interactive Lesson page
                Response.Redirect("InteractiveLesson.aspx");

                // Show message to user
                lblInfo.Text = "You are not logged in.";
            }

            // Populate lesson images and descriptions
            List<LessonImage> showLessonImages = MyDBConnection.ListLessonImages();

            int count = 0;
            foreach (var image in showLessonImages)
            {
                count++;

                switch (count)
                {
                    case 0:
                        break;
                    case 1:
                        imgImage1.ImageUrl = image.LessonImageSrc;
                        rbImage1.Text = image.LessonImageDesc;
                        break;
                    case 2:
                        imgImage2.ImageUrl = image.LessonImageSrc;
                        rbImage2.Text = image.LessonImageDesc;
                        break;
                    case 3:
                        imgImage3.ImageUrl = image.LessonImageSrc;
                        rbImage3.Text = image.LessonImageDesc;
                        break;
                    case 4:
                        imgImage4.ImageUrl = image.LessonImageSrc;
                        rbImage4.Text = image.LessonImageDesc;
                        break;
                    case 5:
                        imgImage5.ImageUrl = image.LessonImageSrc;
                        rbImage5.Text = image.LessonImageDesc;
                        break;
                }
            }
        }

        // Button event to save all entered details to the 'Lesson' table in the database
        protected void btnSaveLesson_Click(object sender, EventArgs e)
        {
            int lessonImageID;
            if (rbImage1.Checked == true) lessonImageID = 1;
            else if (rbImage2.Checked == true) lessonImageID = 2;
            else if (rbImage3.Checked == true) lessonImageID = 3;
            else if (rbImage4.Checked == true) lessonImageID = 4;
            else if (rbImage5.Checked == true) lessonImageID = 5;
            else lessonImageID = 0;

            // Save lesson to database using 'SaveLesson' method
            if (MyDBConnection.SaveLesson(loggedInUserID, txtLessonTitle.Text, txtLessonText.Text, int.Parse(dropLessonTopic.SelectedValue), lessonImageID) == true)
            {
                // Show message to user
                lblConfirmation.Text = "Success, " + txtLessonTitle.Text + " has been saved.";

                // Clear text boxes
                txtLessonTitle.Text = "";
                txtLessonText.Text = "";
            }
            else
            {
                // If lesson hasn't been saved
                lblConfirmation.Text = "Error saving " + txtLessonTitle.Text + " to database, please try again.";
            }
        }

        // Button event to take the user to the 'Interactive Lesson' page
        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            Response.Redirect("InteractiveLesson.aspx");
        }
    }
}