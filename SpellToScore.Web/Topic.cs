namespace SpellToScore.Web
{
    public class Topic
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        public Topic(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}