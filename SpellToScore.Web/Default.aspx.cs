using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace SpellToScore.Web
{
    public partial class Default : Page
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

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;
            }
            else
            {
                // If not logged in
                lblInfo.Text = "You are not logged in.";

                // Disable log out fields
                btnLogOut.Enabled = false;
            }
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            // Find out if user is a child or teacher and log them in
            DatabaseWebService.LogInUser(txtUsername.Text, txtPassword.Text);

            if (Session["loggedInUser"] != null)
            {
                User currentUser = ((User)Session["loggedInUser"]);

                if (currentUser.UserType == 1)
                {
                    // Successful log in as a child
                    lblInfo.Text = "Success, " + currentUser.FirstName + ", you are now logged in as a child.";

                    if (currentUser.Gender == 1)
                    {
                        // If logged in user is male
                        // Change master page css to male colour scheme
                        this.Master.ChangePageColourOnGender(1);
                    }
                    else if (currentUser.Gender == 2)
                    {
                        // If logged in user is female
                        // Change master page css to female colour scheme
                        this.Master.ChangePageColourOnGender(2);
                    }
                }
                else if (currentUser.UserType == 2)
                {
                    // Successful log in as a teacher
                    lblInfo.Text = "Success, " + currentUser.Title + " " + currentUser.Surname + ", you are now logged in as a teacher.";
                }

                // Enable log out fields
                btnLogOut.Enabled = true;

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;

                // Clear text from log in fields
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                // Unsuccessful log in
                lblInfo.Text = "Error logging in, check username and password.";
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            // Null the 'loggedInChild' variable for when the game accesses it
            DatabaseWebService.LoggedInUser = null;

            // Clear variables
            UserLogin.LoggedInUser = null;

            // Store in session
            Session["loggedInUser"] = null;

            // Show message to user
            lblInfo.Text = "You are now logged out.";

            // Enable log in fields
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogIn.Enabled = true;

            // Disable log out fields
            btnLogOut.Enabled = false;

            // Reset master page css colour scheme
            this.Master.ChangePageColourOnGender(3);
        }
    }
}