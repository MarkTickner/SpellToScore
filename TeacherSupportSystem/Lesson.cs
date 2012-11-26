using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Lesson
    {
        private int lessonID;
        public int LessonID
        {
            get { return lessonID; }
            set { lessonID = value; }
        }

        Teacher lessonTeacher;
        public Teacher LessonTeacher
        {
            get { return lessonTeacher; }
            set { lessonTeacher = value; }
        }

        private string lessonTitle;
        public string LessonTitle
        {
            get { return lessonTitle; }
            set { lessonTitle = value; }
        }

        private string lessonText;
        public string LessonText
        {
            get { return lessonText; }
            set { lessonText = value; }
        }

        private string lessonDate;
        public string LessonDate
        {
            get { return lessonDate; }
            set { lessonDate = value; }
        }

        Topic lessonTopic;
        public Topic LessonTopic
        {
            get { return lessonTopic; }
            set { lessonTopic = value; }
        }

        LessonImage lessonImage;
        public LessonImage LessonImage
        {
            get { return lessonImage; }
            set { lessonImage = value; }
        }

        public Lesson(int lessonID, Teacher teacherID, string lessonTitle, string lessonDate, Topic topicID)
        {
            this.lessonID = lessonID;
            this.lessonTeacher = teacherID;
            this.lessonTitle = lessonTitle;
            this.lessonDate = lessonDate;
            this.lessonTopic = topicID;
        }

        public Lesson(int lessonID, Teacher teacherID, string lessonTitle, string lessonText, string lessonDate, Topic topicID, LessonImage lessonImage)
        {
            this.lessonID = lessonID;
            this.lessonTeacher = teacherID;
            this.lessonTitle = lessonTitle;
            this.lessonText = lessonText;
            this.lessonDate = lessonDate;
            this.lessonTopic = topicID;
            this.lessonImage = lessonImage;
        }
    }
}