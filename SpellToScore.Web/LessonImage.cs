namespace SpellToScore.Web
{
    public class LessonImage
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

        private string source;
        public string Source
        {
            get { return source; }
        }

        private string description;
        public string Description
        {
            get { return description; }
        }

        public LessonImage(int id, string source, string description)
        {
            this.id = id;
            this.source = source;
            this.description = description;
        }
    }
}