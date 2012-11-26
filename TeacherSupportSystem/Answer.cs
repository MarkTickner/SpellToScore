using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Answer
    {
        private int answerID;
        public int AnswerID
        {
            get { return answerID; }
            set { answerID = value; }
        }

        Question answerQuestion;
        public Question AnswerQuestion
        {
            get { return answerQuestion; }
            set { answerQuestion = value; }
        }

        private string answerText;
        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }

        private string answerDate;
        public string AnswerDate
        {
            get { return answerDate; }
            set { answerDate = value; }
        }

        Child answerChild;
        public Child AnswerChild
        {
            get { return answerChild; }
            set { answerChild = value; }
        }

        Teacher answerTeacher;
        public Teacher AnswerTeacher
        {
            get { return answerTeacher; }
            set { answerTeacher = value; }
        }

        string currentPerson;
        public string CurrentPerson
        {
            get { return currentPerson; }
            set { currentPerson = value; }
        }

        public Answer(int answerID, Question answerQuestionID, string answerText, string answerDate, string currentPerson)
        {
            this.answerID = answerID;
            this.answerQuestion = answerQuestionID;
            this.answerText = answerText;
            this.answerDate = answerDate;
            this.currentPerson = currentPerson;
        }
    }
}