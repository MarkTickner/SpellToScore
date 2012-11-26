using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherSupportSystem
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        int loggedInUserGender; // 1 = male, 2 = female

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get gender from session, if it exists
            if (Session["loggedInUserGender"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUserGender = (int)Session["loggedInUserGender"];

                if (loggedInUserGender == 1)
                {
                    // If logged in user is male
                    // Change master page css to male colour scheme
                    ChangeConfigurationElementClass(1);
                }
                else if (loggedInUserGender == 2)
                {
                    // If logged in user is female
                    // Change master page css to female colour scheme
                    ChangeConfigurationElementClass(2);
                }
            }
        }

        // Method to change page colours depending on the gender of the child that is logged in
        public void ChangeConfigurationElementClass(int gender)
        {
            if (gender == 1)
            {
                // If logged in user is male
                // Change css to male colour scheme
                header.Attributes["class"] = "header_M";
                nav_bg.Attributes["class"] = "clear hideSkiplink_M";
            }
            else if (gender == 2)
            {
                // If logged in user is female
                // Change css to female colour scheme
                header.Attributes["class"] = "header_F";
                nav_bg.Attributes["class"] = "clear hideSkiplink_F";
            }
            else if (gender == 3)
            {
                // User is a teacher or logged out
                // Reset css colour scheme
                header.Attributes["class"] = "header";
                nav_bg.Attributes["class"] = "clear hideSkiplink";
            }
        }
    }
}
