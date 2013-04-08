using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.Services;

namespace SpellToScore.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DatabaseWebService : WebService
    {
        private static User _loggedInUser;
        public static User LoggedInUser
        {
            set { _loggedInUser = value; }
        }

        private static OleDbConnection GetConnection()
        {
            return new OleDbConnection(ConfigurationManager.ConnectionStrings["MainDatabase"].ConnectionString);
        }

        public static void LogInUser(string username, string password)
        {
            string sqlStatement = "SELECT * FROM [User] WHERE Username = '" + username + "' AND Password = '" + password + "'";

            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                if (dbDataReader != null && dbDataReader.HasRows)
                {
                    dbDataReader.Read();

                    User user = new User(
                        int.Parse(dbDataReader["UserId"].ToString()),
                        int.Parse(dbDataReader["UserType"].ToString()),
                        dbDataReader["UserTitle"].ToString(),
                        dbDataReader["UserFirstName"].ToString(),
                        dbDataReader["UserSurname"].ToString(),
                        int.Parse(dbDataReader["UserGender"].ToString())
                    );

                    HttpContext.Current.Session["loggedInUser"] = _loggedInUser = user;

                    string updateChildLogIns = "UPDATE [User] SET LoginCount = '" + (int.Parse(dbDataReader["LoginCount"].ToString()) + 1) + "' WHERE UserID = " + user.Id;
                    dbCommand = new OleDbCommand(updateChildLogIns, dbConnection);
                    dbCommand.ExecuteNonQuery();

                    dbDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        #region Find Methods

        private static User FindUser(List<User> users, int id)
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        private static Topic FindTopic(List<Topic> topics, int id)
        {
            foreach (var topic in topics)
            {
                if (topic.Id == id)
                {
                    return topic;
                }
            }
            return null;
        }

        private static Lesson FindLesson(List<Lesson> lessons, int id)
        {
            foreach (var lesson in lessons)
            {
                if (lesson.Id == id)
                {
                    return lesson;
                }
            }
            return null;
        }

        private static LessonImage FindLessonImage(List<LessonImage> lessonImages, int id)
        {
            foreach (var lessonImage in lessonImages)
            {
                if (lessonImage.Id == id)
                {
                    return lessonImage;
                }
            }
            return null;
        }

        #endregion

        #region List Methods

        private static List<User> ListUsers()
        {
            List<User> users = new List<User>();

            const string sqlStatement = "SELECT * FROM [User]";
            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();
                while (dbDataReader != null && dbDataReader.Read())
                {
                    users.Add(
                        new User(
                            int.Parse(dbDataReader["UserId"].ToString()),
                            int.Parse(dbDataReader["UserType"].ToString()),
                            dbDataReader["UserTitle"].ToString(),
                            dbDataReader["UserFirstName"].ToString(),
                            dbDataReader["UserSurname"].ToString(),
                            int.Parse(dbDataReader["UserGender"].ToString())
                        )
                    );
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Score> GetScores()
        {
            const string sqlStatement = "SELECT ID, ChildID AS UserID, Score, DateTime FROM SpellToScore_Scores ORDER BY Score DESC";
            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                int position = 0;
                List<User> users = ListUsers();
                List<Score> scores = new List<Score>();

                while (dbDataReader != null && dbDataReader.Read())
                {
                    position++;

                    User user = FindUser(users, int.Parse(dbDataReader["UserID"].ToString()));
                    scores.Add(new Score(position, user, int.Parse(dbDataReader["Score"].ToString()), dbDataReader["DateTime"].ToString()));
                }

                if (dbDataReader != null) dbDataReader.Close();
                return scores;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Score> GetScoresByUser(int userId)
        {
            string sqlStatement = "SELECT ID, ChildID AS UserID, Score, DateTime FROM SpellToScore_Scores WHERE ChildID = " + userId + " ORDER BY Score DESC";
            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                int position = 0;
                List<Score> scores = new List<Score>();
                List<User> users = ListUsers();

                while (dbDataReader != null && dbDataReader.Read())
                {
                    position++;

                    User user = FindUser(users, int.Parse(dbDataReader["UserID"].ToString()));
                    scores.Add(new Score(position, user, int.Parse(dbDataReader["Score"].ToString()), dbDataReader["DateTime"].ToString()));
                }

                if (dbDataReader != null) dbDataReader.Close();
                return scores;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Lesson> ListAllLessonsByType(string filterType, int ID)
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "SELECT * FROM Lesson WHERE " + filterType + " = " + ID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<Topic> topics = ListTopics();
            List<User> users = ListUsers();
            List<LessonImage> lessonImages = ListLessonImages();

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                while (dbDataReader != null && dbDataReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(dbDataReader["TopicID"].ToString()));
                    User currentTeacher = FindUser(users, int.Parse(dbDataReader["TeacherID"].ToString()));
                    LessonImage currentLessonImage = FindLessonImage(lessonImages, int.Parse(dbDataReader["LessonImageID"].ToString()));
                    lessons.Add(new Lesson(int.Parse(dbDataReader["LessonID"].ToString()), currentTeacher, dbDataReader["LessonTitle"].ToString(), dbDataReader["LessonText"].ToString(), dbDataReader["LessonDate"].ToString(), currentTopic, currentLessonImage));
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private static List<Topic> ListTopics()
        {
            List<Topic> topics = new List<Topic>();
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "SELECT TopicID, TopicName FROM Topic";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();
                while (dbDataReader != null && dbDataReader.Read())
                {
                    topics.Add(new Topic(int.Parse(dbDataReader["TopicID"].ToString()), dbDataReader["TopicName"].ToString()));
                }
                return topics;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Lesson> ListAllLessons()
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection dbConnection = GetConnection();

            const string sqlStatement = "SELECT * FROM Lesson";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<Topic> topics = ListTopics();
            List<User> users = ListUsers();
            List<LessonImage> lessonImages = ListLessonImages();

            try
            {
                dbConnection.Open();
                OleDbDataReader myReader = dbCommand.ExecuteReader();

                while (myReader != null && myReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(myReader["TopicID"].ToString()));
                    User currentTeacher = FindUser(users, int.Parse(myReader["TeacherID"].ToString()));
                    LessonImage currentLessonImage = FindLessonImage(lessonImages, int.Parse(myReader["LessonImageID"].ToString()));
                    lessons.Add(new Lesson(int.Parse(myReader["LessonID"].ToString()), currentTeacher, myReader["LessonTitle"].ToString(), myReader["LessonText"].ToString(), myReader["LessonDate"].ToString(), currentTopic, currentLessonImage));
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<LessonImage> ListLessonImages()
        {
            List<LessonImage> lessonImages = new List<LessonImage>();
            OleDbConnection dbConnection = GetConnection();

            const string sqlStatement = "SELECT * FROM LessonImage";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();
                while (dbDataReader != null && dbDataReader.Read())
                {
                    lessonImages.Add(new LessonImage(int.Parse(dbDataReader["LessonImageID"].ToString()), dbDataReader["ImageSrc"].ToString(), dbDataReader["Description"].ToString()));
                }
                return lessonImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Answer> ListAllAnswers(int questionSelected)
        {
            List<Answer> answers = new List<Answer>();
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "SELECT * FROM DiscussionAnswer WHERE DiscussionQID = " + questionSelected;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<Question> questions = ListAllQuestions();
            List<User> users = ListUsers();

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                while (dbDataReader != null && dbDataReader.Read())
                {
                    User answerer;

                    if (int.Parse(dbDataReader["ChildID"].ToString()) == 0)
                    {
                        answerer = FindUser(users, int.Parse(dbDataReader["TeacherID"].ToString()));
                    }
                    else
                    {
                        answerer = FindUser(users, int.Parse(dbDataReader["ChildID"].ToString()));
                    }

                    answers.Add(new Answer(int.Parse(dbDataReader["DiscussionAID"].ToString()), dbDataReader["AnswerText"].ToString(), dbDataReader["AnswerDate"].ToString(), answerer));
                }
                return answers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        #endregion

        public static bool SaveLesson(int teacherID, string lessonTitle, string lessonText, int topicID, int lessonImageID)
        {
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "INSERT INTO Lesson (TeacherID, LessonTitle, LessonText, LessonDate, TopicID, LessonImageID) VALUES (" + teacherID + ", '" + lessonTitle + "', '" + lessonText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', " + topicID + ", " + lessonImageID + ")";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Lesson> ShowLessonByID(int lessonID)
        {
            List<Lesson> lessons = new List<Lesson>();
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "SELECT * FROM Lesson WHERE LessonID = " + lessonID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<Topic> topics = ListTopics();
            List<User> users = ListUsers();
            List<LessonImage> lessonImages = ListLessonImages();

            try
            {
                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                while (dbDataReader != null && dbDataReader.Read())
                {
                    Topic currentTopic = FindTopic(topics, int.Parse(dbDataReader["TopicID"].ToString()));
                    User currentTeacher = FindUser(users, int.Parse(dbDataReader["TeacherID"].ToString()));
                    LessonImage currentLessonImage = FindLessonImage(lessonImages, int.Parse(dbDataReader["LessonImageID"].ToString()));
                    lessons.Add(new Lesson(int.Parse(dbDataReader["LessonID"].ToString()), currentTeacher, dbDataReader["LessonTitle"].ToString(), dbDataReader["LessonText"].ToString(), dbDataReader["LessonDate"].ToString(), currentTopic, currentLessonImage));
                }
                return lessons;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool DeleteLesson(int lessonID)
        {
            OleDbConnection dbConnection = GetConnection();
            string sqlStatement = "DELETE FROM Lesson WHERE LessonID = " + lessonID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool EditLesson(int lessonID, string newLessonTitle, string newLessonText)
        {
            OleDbConnection dbConnection = GetConnection();
            string sqlStatement = "UPDATE Lesson SET LessonTitle = '" + newLessonTitle + "', LessonText = '" + newLessonText + "' WHERE LessonID = " + lessonID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool SaveQuestion(int childID, string questionTitle, string questionText, int questionLesson)
        {
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "INSERT INTO DiscussionQuestion (QuestionTitle, QuestionText, QuestionDate, QuestionLessonID, ChildID) VALUES ('" + questionTitle + "', '" + questionText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', " + questionLesson + ", " + childID + ")";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Question> ListAllQuestions()
        {
            List<Question> questions = new List<Question>();
            OleDbConnection dbConnection = GetConnection();

            const string sqlStatement = "SELECT * FROM DiscussionQuestion ";
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<User> users = ListUsers();
            List<Lesson> lessons = ListAllLessons();

            try
            {
                dbConnection.Open();
                OleDbDataReader myReader = dbCommand.ExecuteReader();

                while (myReader != null && myReader.Read())
                {
                    User user = FindUser(users, int.Parse(myReader["ChildID"].ToString()));
                    Lesson currentLesson = FindLesson(lessons, int.Parse(myReader["QuestionLessonID"].ToString()));
                    questions.Add(new Question(int.Parse(myReader["DiscussionQID"].ToString()), myReader["QuestionTitle"].ToString(), myReader["QuestionText"].ToString(), myReader["QuestionDate"].ToString(), user, currentLesson));
                }
                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static List<Question> ShowQuestionByID(int questionID)
        {
            List<Question> questions = new List<Question>();
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement = "SELECT * FROM DiscussionQuestion WHERE DiscussionQID = " + questionID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<User> users = ListUsers();
            List<Lesson> lessons = ListAllLessons();

            try
            {
                dbConnection.Open();
                OleDbDataReader myReader = dbCommand.ExecuteReader();

                while (myReader != null && myReader.Read())
                {
                    User user = FindUser(users, int.Parse(myReader["ChildID"].ToString()));
                    Lesson currentLesson = FindLesson(lessons, int.Parse(myReader["QuestionLessonID"].ToString()));
                    questions.Add(new Question(int.Parse(myReader["DiscussionQID"].ToString()), myReader["QuestionTitle"].ToString(), myReader["QuestionText"].ToString(), myReader["QuestionDate"].ToString(), user, currentLesson));
                }
                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool SaveAnswer(int questionSelected, string answerText, int logInState, int loggedInUserID)
        {
            OleDbConnection dbConnection = GetConnection();

            string sqlStatement;

            if (logInState == 1)
            {
                // User logged in as a Child
                sqlStatement = "INSERT INTO DiscussionAnswer (DiscussionQID, AnswerText, AnswerDate, ChildID, TeacherID) VALUES ('" + questionSelected + "', '" + answerText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', '" + loggedInUserID + "', '0')";
            }
            else
            {
                // User logged in as a Teacher
                sqlStatement = "INSERT INTO DiscussionAnswer (DiscussionQID, AnswerText, AnswerDate, ChildID, TeacherID) VALUES ('" + questionSelected + "', '" + answerText + "', '" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + "', '0', '" + loggedInUserID + "')";
            }

            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool DeleteQuestion(int questionID)
        {
            OleDbConnection dbConnection = GetConnection();
            string sqlStatement = "DELETE FROM DiscussionQuestion WHERE DiscussionQID = " + questionID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool EditQuestion(int questionID, string newQuestionTitle, string newQuestionText)
        {
            OleDbConnection dbConnection = GetConnection();
            string sqlStatement = "UPDATE DiscussionQuestion SET QuestionTitle = '" + newQuestionTitle + "', QuestionText = '" + newQuestionText + "' WHERE DiscussionQID = " + questionID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool DeleteAnswer(int answerID)
        {
            OleDbConnection dbConnection = GetConnection();
            string sqlStatement = "DELETE FROM DiscussionAnswer WHERE DiscussionAID = " + answerID;
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);

                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        [WebMethod]
        public int GetLoggedInUserId()
        {
            if (_loggedInUser != null)
            {
                return _loggedInUser.Id;
            }

            return 0;
        }

        [WebMethod]
        public string GetLoggedInUserName()
        {
            if (_loggedInUser != null)
            {
                return _loggedInUser.FirstName + " " + _loggedInUser.Surname;
            }

            return null;
        }

        [WebMethod]
        public bool SaveScore(int userId, int score)
        {
            string sqlStatement = "INSERT INTO SpellToScore_Scores ([ChildID], [Score], [DateTime]) VALUES (" + userId + ", " + score + ", '" + DateTime.Now + "')";
            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
                dbCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        [WebMethod]
        public string LoadScores()
        {
            int position = 0;

            const string sqlStatement = "SELECT TOP 10 ChildID AS UserID, Score, [DateTime] FROM SpellToScore_Scores ORDER BY Score DESC";
            OleDbConnection dbConnection = GetConnection();
            OleDbCommand dbCommand = new OleDbCommand(sqlStatement, dbConnection);

            List<User> users = ListUsers();

            try
            {
                // Formats the heading string into neat columns
                string s = String.Format("{0,2} {1,-11} {2,-5} {3,-10} {4,-8}\n", "", "Name", "Score", "Date", "Time");
                s += String.Format("{0,2} {1,-13} {2,-3} {3,-10} {4,-8}\n", "--", "-------------", "---", "----------", "--------");

                dbConnection.Open();
                OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

                while (dbDataReader != null && (dbDataReader.Read() && position < 10))
                {
                    User user = FindUser(users, int.Parse(dbDataReader["UserID"].ToString()));

                    // Formats the string into neat columns
                    s += String.Format("{0,2} {1,-13} {2,-3} {3,-19}\n", ++position, user.FirstName + " " + user.Surname, dbDataReader["Score"], dbDataReader["DateTime"]);
                }

                if (dbDataReader != null) dbDataReader.Close();
                return s;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler, {0}", ex);
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
