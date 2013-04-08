using System;
using System.Web.UI;

namespace SpellToScore.Web
{
    public partial class HowToPlay : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get logged in user
            if (UserLogin.LoggedInUser != null)
            {
                // Logged in
                User currentUser = UserLogin.LoggedInUser;

                if (currentUser.UserType == 1)
                {
                    // Show message to user
                    lblInfo.Text = currentUser.FirstName + " " + currentUser.Surname + ", you are logged in as a child.";
                }
                else if (currentUser.UserType == 2)
                {
                    // Show message to user
                    lblInfo.Text = currentUser.Title + " " + currentUser.Surname + ", you are logged in as a teacher.";
                }
            }
            else
            {
                // If not logged in
                // Show message to user
                lblInfo.Text = "You are not logged in.";
            }
        }
    }
}