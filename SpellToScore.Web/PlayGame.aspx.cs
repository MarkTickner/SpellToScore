using System;
using System.Web.UI;

namespace SpellToScore.Web
{
    public partial class PlayGame : Page
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
                lblInfo.Text = "You are not logged in. To save your scores, log in on the homepage.";
            }
        }
    }
}