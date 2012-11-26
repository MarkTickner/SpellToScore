using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace TeacherSupportSystem
{
    public class MyDBConnection
    {
        public static OleDbConnection GetConnection()
        {
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MainDatabase.accdb"; // Database path, |DataDirectory| points to the App_Data folder

            return new OleDbConnection(connString);
        }

        // Method that returns a list of 'Lesson' objects by 'TopicID' or 'TeacherID'
        public static List<Lesson> ListAllLessonsByType(string filterType, int ID)
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Lesson WHERE " + filterType + " = " + ID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Topic> topics = ListTopics();
            List<Teacher> teachers = ListTeachers();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(myReader["TopicID"].ToString()));
                    Teacher currentTeacher = FindTeacher(teachers, int.Parse(myReader["TeacherID"].ToString()));
                    Lesson l = new Lesson(int.Parse(myReader["LessonID"].ToString()), currentTeacher, myReader["LessonTitle"].ToString(), myReader["LessonDate"].ToString(), currentTopic);
                    lessons.Add(l);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }
        
        // Method that returns a list of 'Topic' objects
        public static List<Topic> ListTopics()
        {
            List<Topic> topics = new List<Topic>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT TopicID, TopicName FROM Topic";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Topic t = new Topic(int.Parse(myReader["TopicID"].ToString()), myReader["TopicName"].ToString());
                    topics.Add(t);
                }
                return topics;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Teacher' objects
        public static List<Teacher> ListTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT TeacherID, TchTitle, TchSurname FROM Teacher";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    teachers.Add(new Teacher(int.Parse(myReader["TeacherID"].ToString()), myReader["TchTitle"].ToString(), myReader["TchSurname"].ToString()));
                }
                return teachers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Lesson' objects
        public static List<Lesson> ListAllLessons()
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Lesson";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Topic> topics = ListTopics();
            List<Teacher> teachers = ListTeachers();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(myReader["TopicID"].ToString()));
                    Teacher currentTeacher = FindTeacher(teachers, int.Parse(myReader["TeacherID"].ToString()));
                    Lesson l = new Lesson(int.Parse(myReader["LessonID"].ToString()), currentTeacher, myReader["LessonTitle"].ToString(), myReader["LessonDate"].ToString(), currentTopic);
                    lessons.Add(l);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'LessonImage' objects
        public static List<LessonImage> ListLessonImages()
        {
            List<LessonImage> lessonImages = new List<LessonImage>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM LessonImage";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    LessonImage i = new LessonImage(int.Parse(myReader["LessonImageID"].ToString()), myReader["ImageSrc"].ToString(), myReader["Description"].ToString());
                    lessonImages.Add(i);
                }
                return lessonImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Topic' object by 'TopicID'
        private static Topic FindTopic(List<Topic> topics, int id)
        {
            foreach (var topic in topics)
            {
                if (topic.TopicID == id)
                {
                    return topic;
                }
            }
            return null;
        }

        // Method that returns a 'Teacher' object by 'TeacherID'
        private static Teacher FindTeacher(List<Teacher> teachers, int id)
        {
            foreach (var teacher in teachers)
            {
                if (teacher.TeacherID == id)
                {
                    return teacher;
                }
            }
            return null;
        }

        // Method that returns a 'Lesson' object by 'LessonID'
        private static Lesson FindLesson(List<Lesson> lessons, int id)
        {
            foreach (var lesson in lessons)
            {
                if (lesson.LessonID == id)
                {
                    return lesson;
                }
            }
            return null;
        }

        // Method that returns a 'lessonImage' object by 'lessonImageID'
        private static LessonImage FindLessonImage(List<LessonImage> lessonImages, int id)
        {
            foreach (var lessonImage in lessonImages)
            {
                if (lessonImage.LessonImageID == id)
                {
                    return lessonImage;
                }
            }
            return null;
        }

        // Method that checks user log in details and returns 'logInState'
        public static int LogInUser(string username, string password)
        {
            int count = 0;
            int userID = 0;

            // SQL query to send to database as a string
            string queryChild = "SELECT * FROM Child WHERE CldUsername = '" + username + "' AND CldPassword = '" + password + "'";
            string queryTeacher = "SELECT * FROM Teacher WHERE TchUsername = '" + username + "' AND TchPassword = '" + password + "'";

            OleDbConnection myConnection = GetConnection();
            OleDbCommand commandChild = new OleDbCommand(queryChild, myConnection);
            OleDbCommand commandTeacher = new OleDbCommand(queryTeacher, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader readerChild = commandChild.ExecuteReader();
                OleDbDataReader readerTeacher = commandTeacher.ExecuteReader();

                if (readerChild.HasRows == true)
                {
                    // User is found in 'Child' table
                    readerChild.Read();

                    try
                    {
                        count = int.Parse(readerChild["NoOfLogins"].ToString());
                    }
                    catch
                    {
                        count = 0;
                    }
                    
                    count++;
                    userID = int.Parse(readerChild["ChildID"].ToString());
                    
                    string updateChildLogIns = "UPDATE Child SET NoOfLogins = '" + count + "' WHERE ChildID = " + userID;
                    
                    commandChild = new OleDbCommand(updateChildLogIns, myConnection);
                    commandChild.ExecuteNonQuery();

                    return 1;
                }
                else if (readerTeacher.HasRows == true)
                {
                    // User is found in 'Teacher' database
                    readerTeacher.Read();

                    try
                    {
                        count = int.Parse(readerTeacher["NoOfLogins"].ToString());
                    }
                    catch
                    {
                        count = 0;
                    }

                    count++;
                    userID = int.Parse(readerTeacher["TeacherID"].ToString());

                    string myQuery1 = "UPDATE Teacher SET NoOfLogins = '" + count + "' WHERE TeacherID = " + userID;
                    
                    commandTeacher = new OleDbCommand(myQuery1, myConnection);
                    commandTeacher.ExecuteNonQuery();

                    return 2;
                }
                else
                {
                    // User isn't found in 'Child' or 'Teacher' table

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return 0;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns the name of the logged in user
        public static string GetName(int logInState, string username)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery;

            if (logInState == 1)
            {
                // 'logInState' 1 = child
                // SQL query to send to database as a string
                myQuery = "SELECT * FROM Child WHERE CldUsername = '" + username + "'";
            }
            else
            {
                // 'logInState' 2 = teacher
                // SQL query to send to database as a string
                myQuery = "SELECT * FROM Teacher WHERE TchUsername = '" + username + "'";
            }
            
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                myReader.Read();

                if (logInState == 1)
                {
                    // 'logInState' 1 = child
                    return myReader["CldName"].ToString() + " " + myReader["CldSurname"].ToString();
                }
                else
                {
                    // 'logInState' 2 = teacher
                    return myReader["TchTitle"].ToString() + " " + myReader["TchSurname"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns the gender of the logged in user
        public static int GetGender(int logInState, int loggedInUserID)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery;

            if (logInState == 1)
            {
                // 'logInState' 1 = child
                // SQL query to send to database as a string
                myQuery = "SELECT * FROM Child WHERE ChildID = " + loggedInUserID;

                OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

                try
                {
                    myConnection.Open();
                    OleDbDataReader myReader = myCommand.ExecuteReader();

                    myReader.Read();

                    return int.Parse(myReader["CldGender"].ToString());

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in DBHandler", ex);

                    return 0;
                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {
                return 0;
            }           
        }

        // Method that saves a new lesson to the database
        public static bool SaveLesson(int teacherID, string lessonTitle, string lessonText, int topicID, int lessonImageID)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "INSERT INTO Lesson (TeacherID, LessonTitle, LessonText, LessonDate, TopicID, LessonImageID) VALUES (" + teacherID + ", '" + lessonTitle + "', '" + lessonText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', " + topicID + ", " + lessonImageID + ")";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a users ID
        public static int GetID(string username)
        {
            OleDbConnection myConnection = GetConnection();

            string queryChild = "SELECT * FROM Child WHERE CldUsername = '" + username + "'";
            string queryTeacher = "SELECT * FROM Teacher WHERE TchUsername = '" + username + "'";

            OleDbCommand commandChild = new OleDbCommand(queryChild, myConnection);
            OleDbCommand commandTeacher = new OleDbCommand(queryTeacher, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader readerChild = commandChild.ExecuteReader();
                OleDbDataReader readerTeacher = commandTeacher.ExecuteReader();

                if (readerChild.HasRows == true)
                {
                    // Username found in 'Child' table
                    readerChild.Read();

                    return int.Parse(readerChild["ChildID"].ToString());
                }
                else if (readerTeacher.HasRows == true)
                {
                    // Username found in 'Teacher' table
                    readerTeacher.Read();

                    return int.Parse(readerTeacher["TeacherID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return 0;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Lesson' objects by 'LessonID'
        public static List<Lesson> ShowLessonByID(int lessonID)
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Lesson WHERE LessonID = " + lessonID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Topic> topics = ListTopics();
            List<Teacher> teachers = ListTeachers();
            List<LessonImage> lessonImages = ListLessonImages();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(myReader["TopicID"].ToString()));
                    Teacher currentTeacher = FindTeacher(teachers, int.Parse(myReader["TeacherID"].ToString()));
                    LessonImage currentLessonImage = FindLessonImage(lessonImages, int.Parse(myReader["LessonImageID"].ToString()));
                    
                    Lesson lessonDetail = new Lesson(int.Parse(myReader["LessonID"].ToString()), currentTeacher, myReader["LessonTitle"].ToString(), myReader["LessonText"].ToString(), myReader["LessonDate"].ToString(), currentTopic, currentLessonImage);
                    lessons.Add(lessonDetail);
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that deletes a lesson from the database
        public static bool DeleteLesson(int lessonID)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "DELETE FROM Lesson WHERE LessonID = " + lessonID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                // Lesson deleted
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                // Lesson not deleted
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that edits a lesson in the database
        public static bool EditLesson(int lessonID, string newLessonTitle, string newLessonText)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "UPDATE Lesson SET LessonTitle = '" + newLessonTitle + "', LessonText = '" + newLessonText + "' WHERE LessonID = " + lessonID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                // Lesson edited
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                // Lesson not edited
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that saves a question to the database
        public static bool SaveQuestion(int childID, string questionTitle, string questionText, int questionLesson)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "INSERT INTO DiscussionQuestion (QuestionTitle, QuestionText, QuestionDate, QuestionLessonID, ChildID) VALUES ('" + questionTitle + "', '" + questionText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', " + questionLesson + ", " + childID + ")";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Question' objects
        public static List<Question> ListAllQuestions()
        {
            List<Question> questions = new List<Question>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM DiscussionQuestion ";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Child> children = ListChildren();
            List<Lesson> lessons = ListAllLessons();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Child currentChild = FindChild(children, int.Parse(myReader["ChildID"].ToString()));
                    Lesson currentLesson = FindLesson(lessons, int.Parse(myReader["QuestionLessonID"].ToString()));
                    Question q = new Question(int.Parse(myReader["DiscussionQID"].ToString()), myReader["QuestionTitle"].ToString(), myReader["QuestionDate"].ToString(), currentChild, currentLesson);
                    questions.Add(q);
                }
                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Child' objects
        public static List<Child> ListChildren()
        {
            List<Child> children = new List<Child>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Child";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    children.Add(new Child(int.Parse(myReader["ChildID"].ToString()), myReader["CldName"].ToString(), myReader["CldSurname"].ToString()));
                }
                return children;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Child' object by 'ChildID'
        private static Child FindChild(List<Child> children, int id)
        {
            foreach (var child in children)
            {
                if (child.ChildID == id)
                {
                    return child;
                }
            }
            return null;
        }

        // Method that returns a list of 'Question' objects by 'QuestionID'
        public static List<Question> ShowQuestionByID(int questionID)
        {
            List<Question> questions = new List<Question>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM DiscussionQuestion WHERE DiscussionQID = " + questionID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Child> children = ListChildren();
            List<Lesson> lessons = ListAllLessons();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Child currentChild = FindChild(children, int.Parse(myReader["ChildID"].ToString()));
                    Lesson currentLesson = FindLesson(lessons, int.Parse(myReader["QuestionLessonID"].ToString()));
                    Question q = new Question(int.Parse(myReader["DiscussionQID"].ToString()), myReader["QuestionTitle"].ToString(), myReader["QuestionText"].ToString(), myReader["QuestionDate"].ToString(), currentChild, currentLesson);
                    questions.Add(q);
                }
                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Answer' objects
        public static List<Answer> ListAllAnswers(int questionSelected)
        {

            List<Answer> answers = new List<Answer>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM DiscussionAnswer WHERE DiscussionQID = " + questionSelected;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Question> questions = ListAllQuestions();
            List<Child> children = ListChildren();
            List<Teacher> teachers = ListTeachers();
            
            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Question currentQuestion = FindQuestion(questions, int.Parse(myReader["DiscussionQID"].ToString()));

                    Answer a;

                    if (int.Parse(myReader["ChildID"].ToString()) == 0)
                    {
                        Teacher currentTeacher = FindTeacher(teachers, int.Parse(myReader["TeacherID"].ToString()));
                        string currentTeacherStr = currentTeacher.TchTitle + " " + currentTeacher.TchSurname;
                        a = new Answer(int.Parse(myReader["DiscussionAID"].ToString()), currentQuestion, myReader["AnswerText"].ToString(), myReader["AnswerDate"].ToString(), currentTeacherStr);
                        
                    }
                    else
                    {
                        Child currentChild = FindChild(children, int.Parse(myReader["ChildID"].ToString()));
                        string currentChildStr = currentChild.CldName + " " + currentChild.CldSurname;
                        a = new Answer(int.Parse(myReader["DiscussionAID"].ToString()), currentQuestion, myReader["AnswerText"].ToString(), myReader["AnswerDate"].ToString(), currentChildStr);
                    }
                    
                    answers.Add(a);
                }
                return answers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Question' object by 'QuestionID'
        private static Question FindQuestion(List<Question> questions, int id)
        {
            foreach (var question in questions)
            {
                if (question.QuestionID == id)
                {
                    return question;
                }
            }
            return null;
        }

        // Method that saves an answer to the database
        public static bool SaveAnswer(int questionSelected, string answerText, int logInState, int loggedInUserID)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery;

            if (logInState == 1)
            {
                // User logged in as a Child
                myQuery = "INSERT INTO DiscussionAnswer (DiscussionQID, AnswerText, AnswerDate, ChildID, TeacherID) VALUES ('" + questionSelected + "', '" + answerText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', '" + loggedInUserID + "', '0')";
            }
            else
            {
                // User logged in as a Teacher
                // As form is only visible to logged in users we can assume that they are a Teacher
                myQuery = "INSERT INTO DiscussionAnswer (DiscussionQID, AnswerText, AnswerDate, ChildID, TeacherID) VALUES ('" + questionSelected + "', '" + answerText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', '0', '" + loggedInUserID + "')";
            }

            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that deletes a question from the database
        public static bool DeleteQuestion(int questionID)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "DELETE FROM DiscussionQuestion WHERE DiscussionQID = " + questionID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                // Lesson deleted
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                // Lesson not deleted
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that edits a question in the database
        public static bool EditQuestion(int questionID, string newQuestionTitle, string newQuestionText)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "UPDATE DiscussionQuestion SET QuestionTitle = '" + newQuestionTitle + "', QuestionText = '" + newQuestionText + "' WHERE DiscussionQID = " + questionID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                // Question edited
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                // Question not edited
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that deletes an answer from the database
        public static bool DeleteAnswer(int answerID)
        {
            OleDbConnection myConnection = GetConnection();
            string myQuery = "DELETE FROM DiscussionAnswer WHERE DiscussionAID = " + answerID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                // Answer deleted
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                // Answer not deleted
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}