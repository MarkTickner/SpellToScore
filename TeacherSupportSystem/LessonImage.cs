using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class LessonImage
    {
        private int lessonImageID;
        public int LessonImageID
        {
            get { return lessonImageID; }
            set { lessonImageID = value; }
        }

        private string lessonImageSrc;
        public string LessonImageSrc
        {
            get { return lessonImageSrc; }
            set { lessonImageSrc = value; }
        }

        private string lessonImageDesc;
        public string LessonImageDesc
        {
            get { return lessonImageDesc; }
            set { lessonImageDesc = value; }
        }

        public LessonImage(int lessonImageID, string lessonImageSrc, string lessonImageDesc)
        {
            this.lessonImageID = lessonImageID;
            this.lessonImageSrc = lessonImageSrc;
            this.lessonImageDesc = lessonImageDesc;
        }
    }
}