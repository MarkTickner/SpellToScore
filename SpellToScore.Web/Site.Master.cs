using System;
using System.Web.UI;

namespace SpellToScore.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get user from session, if it exists
            if (Session["loggedInUser"] != null)
            {
                User currentUser = ((User)Session["loggedInUser"]);

                if (currentUser.UserType == 1)
                {
                    switch (currentUser.Gender)
                    {
                        case 1:
                            // Change master page css to male colour scheme
                            ChangePageColourOnGender(1);
                            break;
                        case 2:
                            // Change master page css to female colour scheme
                            ChangePageColourOnGender(2);
                            break;
                    }
                }
            }
        }

        public void ChangePageColourOnGender(int gender)
        {
            switch (gender)
            {
                case 1:
                    // Change css to male colour scheme
                    header.Attributes["class"] = "header_M";
                    nav_bg.Attributes["class"] = "clear hideSkiplink_M";
                    break;
                case 2:
                    // Change css to female colour scheme
                    header.Attributes["class"] = "header_F";
                    nav_bg.Attributes["class"] = "clear hideSkiplink_F";
                    break;
                case 3:
                    // Reset css colour scheme
                    header.Attributes["class"] = "header";
                    nav_bg.Attributes["class"] = "clear hideSkiplink";
                    break;
            }
        }
    }
}