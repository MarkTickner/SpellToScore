using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Teacher
    {
        private int teacherID;
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        private string tchTitle;
        public string TchTitle
        {
            get { return tchTitle; }
            set { tchTitle = value; }
        }

        private string tchSurname;
        public string TchSurname
        {
            get { return tchSurname; }
            set { tchSurname = value; }
        }

        private string tchPassword;
        public string TchPassword
        {
            get { return tchPassword; }
            set { tchPassword = value; }
        }

        private int noOfLogins;
        public int NoOfLogins
        {
            get { return noOfLogins; }
            set { noOfLogins = value; }
        }

        public Teacher(int teacherID, string tchTitle, string tchSurname)
        {
            this.teacherID = teacherID;
            this.tchTitle = tchTitle;
            this.tchSurname = tchSurname;
        }
    }
}