using System.Web;

namespace SpellToScore.Web
{
    public static class UserLogin
    {
        private static User loggedInUser;
        public static User LoggedInUser
        {
            get
            {
                if (HttpContext.Current.Session["loggedInUser"] != null)
                {
                    // Session exists - user is logged in
                    return loggedInUser = (User)HttpContext.Current.Session["loggedInUser"];
                }
                else
                {
                    // Session doesn't exist - user isn't logged in
                    return loggedInUser = null;
                }
            }
            set
            {
                loggedInUser = value;
            }
        }
    }
}