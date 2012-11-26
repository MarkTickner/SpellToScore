using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace TeacherSupportSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        int logInState; // 0 = logged out, 1 = logged in as a child, 2 = logged in as a teacher
        string loggedInUser; // the name of the user logged in, if any
        int loggedInUserID; // the ID of the user logged in, if any
        int loggedInUserGender; // 1 = male, 2 = female

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get int from session, if it exists
            if (Session["logInState"] == null)
            {
                // Session doesn't exist - user isn't logged in
                logInState = 0;
            }
            else
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

            // Get gender from session, if it exists
            if (Session["loggedInUserGender"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUserGender = (int)Session["loggedInUserGender"];
            }

            // Logged in or out specific actions
            if (logInState == 1)
            {
                // If logged in as a child
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a child.";

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;
            }
            else if (logInState == 2)
            {
                // If logged in as a teacher
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a teacher.";

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
            logInState = MyDBConnection.LogInUser(txtUsername.Text, txtPassword.Text);
            loggedInUser = MyDBConnection.GetName(logInState, txtUsername.Text);
            loggedInUserID = MyDBConnection.GetID(txtUsername.Text);
            loggedInUserGender = MyDBConnection.GetGender(logInState, loggedInUserID);

            // Store int in session
            Session["logInState"] = logInState;
            // Store name in session
            Session["loggedInUser"] = loggedInUser;
            // Store ID in session
            Session["loggedInUserID"] = loggedInUserID;
            // Store gender in session
            Session["loggedInUserGender"] = loggedInUserGender;

            if (logInState == 1)
            {
                // Successful log in as a child
                // Show message to user
                lblInfo.Text = "Success, " + loggedInUser + ", you are now logged in as a child.";

                // Enable log out fields
                btnLogOut.Enabled = true;

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;

                // Clear text from log in fields
                txtUsername.Text = "";
                txtPassword.Text = "";

                if (loggedInUserGender == 1)
                {
                    // If logged in user is male
                    // Change master page css to male colour scheme
                    this.Master.ChangeConfigurationElementClass(1);
                }
                else if (loggedInUserGender == 2)
                {
                    // If logged in user is female
                    // Change master page css to female colour scheme
                    this.Master.ChangeConfigurationElementClass(2);
                }
            }
            else if (logInState == 2)
            {
                // Successful log in as a teacher
                // Show message to user
                lblInfo.Text = "Success, " + loggedInUser + ", you are now logged in as a teacher.";

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
                // Show message to user
                lblInfo.Text = "Error logging in, check username and password.";
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            // Change 'logInState' int
            logInState = 0;
            loggedInUser = "";
            loggedInUserID = 0;

            // Store int in session
            Session["logInState"] = logInState;

            // Store name in session
            Session["loggedInUser"] = loggedInUser;

            // Store ID in session
            Session["loggedInUserID"] = loggedInUserID;

            // Show message to user
            lblInfo.Text = "You are now logged out.";

            // Enable log in fields
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogIn.Enabled = true;

            // Disable log out fields
            btnLogOut.Enabled = false;

            // Clear the 'loggedInUserGender' session
            Session["loggedInUserGender"] = 0;

            // Reset master page css colour scheme
            this.Master.ChangeConfigurationElementClass(3);
        }
    }
}