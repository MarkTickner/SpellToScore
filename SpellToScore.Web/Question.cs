namespace SpellToScore.Web
{
    public class Question
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

        private string title;
        public string Title
        {
            get { return title; }
        }

        private string text;
        public string Text
        {
            get { return text; }
        }

        private string date;
        public string Date
        {
            get { return date; }
        }

        User questionUser;
        public User QuestionUser
        {
            get { return questionUser; }
        }

        Lesson questionLesson;
        public Lesson QuestionLesson
        {
            get { return questionLesson; }
        }

        public Question(int id, string title, string text, string date, User questionUser, Lesson questionLesson)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.date = date;
            this.questionUser = questionUser;
            this.questionLesson = questionLesson;
        }
    }
}