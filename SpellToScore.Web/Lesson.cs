namespace SpellToScore.Web
{
    public class Lesson
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

        User lessonTeacher;
        public User LessonTeacher
        {
            get { return lessonTeacher; }
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

        Topic lessonTopic;
        public Topic LessonTopic
        {
            get { return lessonTopic; }
        }

        LessonImage lessonImage;
        public LessonImage LessonImage
        {
            get { return lessonImage; }
        }

        public Lesson(int id, User lessonTeacher, string title, string text, string date, Topic lessonTopic, LessonImage lessonImage)
        {
            this.id = id;
            this.lessonTeacher = lessonTeacher;
            this.title = title;
            this.text = text;
            this.date = date;
            this.lessonTopic = lessonTopic;
            this.lessonImage = lessonImage;
        }
    }
}