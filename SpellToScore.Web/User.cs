namespace SpellToScore.Web
{
    public class User
    {
        protected int id;
        public int Id
        {
            get { return id; }
        }

        private int userType;
        public int UserType
        {
            get { return userType; }
        }

        protected string title;
        public string Title
        {
            get { return title; }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
        }

        protected string surname;
        public string Surname
        {
            get { return surname; }
        }

        private int gender;
        public int Gender
        {
            get { return gender; }
        }

        public User(int id, int userType, string title, string firstName, string surname, int gender)
        {
            this.id = id;
            this.userType = userType;
            this.title = title;
            this.firstName = firstName;
            this.surname = surname;
            this.gender = gender;
        }

    }
}