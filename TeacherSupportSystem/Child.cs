using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Child
    {
        private int childID;
        public int ChildID
        {
            get { return childID; }
            set { childID = value; }
        }

        private int parentID;
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private string cldName;
        public string CldName
        {
            get { return cldName; }
            set { cldName = value; }
        }

        private string cldSurname;
        public string CldSurname
        {
            get { return cldSurname; }
            set { cldSurname = value; }
        }

        private string cldDOB;
        public string CldDOB
        {
            get { return cldDOB; }
            set { cldDOB = value; }
        }

        private string cldGender;
        public string CldGender
        {
            get { return cldGender; }
            set { cldGender = value; }
        }

        private string cldUsername;
        public string CldUsername
        {
            get { return cldUsername; }
            set { cldUsername = value; }
        }

        private string cldPassword;
        public string CldPassword
        {
            get { return cldPassword; }
            set { cldPassword = value; }
        }

        private int noOfLogins;
        public int NoOfLogins
        {
            get { return noOfLogins; }
            set { noOfLogins = value; }
        }

        public Child(int childID, string cldName, string cldSurname)
        {
            this.childID = childID;
            this.cldName = cldName;
            this.cldSurname = cldSurname;
        }
    }
}