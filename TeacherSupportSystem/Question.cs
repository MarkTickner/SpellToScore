using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Question
    {
        private int questionID;
        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        private string questionTitle;
        public string QuestionTitle
        {
            get { return questionTitle; }
            set { questionTitle = value; }
        }

        private string questionText;
        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }

        private string questionDate;
        public string QuestionDate
        {
            get { return questionDate; }
            set { questionDate = value; }
        }

        Child questionChild;
        public Child QuestionChild
        {
            get { return questionChild; }
            set { questionChild = value; }
        }

        Lesson questionLesson;
        public Lesson QuestionLesson
        {
            get { return questionLesson; }
            set { questionLesson = value; }
        }

        public Question(int questionID, string questionTitle, string questionDate, Child questionChild, Lesson questionLesson)
        {
            this.questionID = questionID;
            this.questionTitle = questionTitle;
            this.questionDate = questionDate;
            this.questionChild = questionChild;
            this.questionLesson = questionLesson;
        }

        public Question(int questionID, string questionTitle, string questionText, string questionDate, Child questionChild, Lesson questionLesson)
        {
            this.questionID = questionID;
            this.questionTitle = questionTitle;
            this.questionText = questionText;
            this.questionDate = questionDate;
            this.questionChild = questionChild;
            this.questionLesson = questionLesson;
        }
    }
}