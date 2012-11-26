using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherSupportSystem
{
    public class Topic
    {
        private int topicID;
        public int TopicID
        {
            get { return topicID; }
            set { topicID = value; }
        }

        private string topicName;
        public string TopicName
        {
            get { return topicName; }
            set { topicName = value; }
        }

        public Topic(int topicID, string topicName)
        {
            this.topicID = topicID;
            this.topicName = topicName;
        }
    }
}