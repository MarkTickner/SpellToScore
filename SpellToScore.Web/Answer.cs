namespace SpellToScore.Web
{
    public class Answer
    {
        private int id;
        public int Id
        {
            get { return id; }
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

        private User answerer;
        public User Answerer
        {
            get { return answerer; }
        }

        public Answer(int id, string text, string date, User answerer)
        {
            this.id = id;
            this.text = text;
            this.date = date;
            this.answerer = answerer;
        }
    }
}