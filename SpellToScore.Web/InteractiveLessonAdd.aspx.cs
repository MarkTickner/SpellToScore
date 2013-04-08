using System.Web.UI;
using System;
using System.Collections.Generic;

namespace SpellToScore.Web
{
    public partial class InteractiveLessonAdd : Page
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
                Response.Redirect("InteractiveLesson.aspx");
            }

            List<LessonImage> allLessonImages = DatabaseWebService.ListLessonImages();

            int count = 0;
            foreach (var image in allLessonImages)
            {
                count++;

                switch (count)
                {
                    case 0:
                        break;
                    case 1:
                        imgImage1.ImageUrl = image.Source;
                        rbImage1.Text = image.Description;
                        break;
                    case 2:
                        imgImage2.ImageUrl = image.Source;
                        rbImage2.Text = image.Description;
                        break;
                    case 3:
                        imgImage3.ImageUrl = image.Source;
                        rbImage3.Text = image.Description;
                        break;
                    case 4:
                        imgImage4.ImageUrl = image.Source;
                        rbImage4.Text = image.Description;
                        break;
                    case 5:
                        imgImage5.ImageUrl = image.Source;
                        rbImage5.Text = image.Description;
                        break;
                }
            }
        }

        protected void btnSaveLesson_Click(object sender, EventArgs e)
        {
            int lessonImageID;
            if (rbImage1.Checked == true) lessonImageID = 1;
            else if (rbImage2.Checked == true) lessonImageID = 2;
            else if (rbImage3.Checked == true) lessonImageID = 3;
            else if (rbImage4.Checked == true) lessonImageID = 4;
            else if (rbImage5.Checked == true) lessonImageID = 5;
            else lessonImageID = 0;

            if (DatabaseWebService.SaveLesson(currentUser.Id, txtLessonTitle.Text, txtLessonText.Text, int.Parse(dropLessonTopic.SelectedValue), lessonImageID) == true)
            {
                lblConfirmation.Text = "Success, " + txtLessonTitle.Text + " has been saved.";
                txtLessonTitle.Text = "";
                txtLessonText.Text = "";
            }
            else
            {
                // If lesson hasn't been saved
                lblConfirmation.Text = "Error saving " + txtLessonTitle.Text + " to database, please try again.";
            }
        }

        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            Response.Redirect("InteractiveLesson.aspx");
        }
    }
}