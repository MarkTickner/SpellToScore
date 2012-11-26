using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherSupportSystem
{
    public partial class DiscussionBoard : System.Web.UI.Page
    {
        int logInState; // 0 = logged out, 1 = logged in as a child, 2 = logged in as a teacher
        string loggedInUser; // the name of the user logged in, if any
        int loggedInUserID; // the ID of the user logged in, if any
        int questionIDFromURL; // the questionID will be saved from the website URL here
        int editQuestionFromURL; // the questionID of the lesson to edit

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Gets the 'questionID' from the URL if available, and passes it to the 'ShowEditQuestion' method
                editQuestionFromURL = int.Parse(Request.QueryString["editQuestionID"]);

                // Check to see if the page has posted back
                if (!Page.IsPostBack)
                {
                    // Page hasn't posted back
                    // Run the 'ShowEditQuestion' method with a parameter from the URL
                    ShowEditQuestion(editQuestionFromURL);
                }
            }
            catch { }

            // Get int from session, if it exists
            if (Session["logInState"] == null)
            {
                // Session doesn't exist - user isn't logged in
                logInState = 0;

                // Hide the 'answerQuestion' div as user must be logged in to answer
                answerQuestion.Visible = false;
            }
            else
            {
                // Session exists - user is logged in
                // Get int from session and assign to 'logInState'
                logInState = (int)Session["logInState"];
            }

            // Get name from session, if it exists
            if (Session["loggedInUser"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUser = (string)Session["loggedInUser"];
            }

            // Get ID from session, if it exists
            if (Session["loggedInUserID"] != null)
            {
                // Session exists - user is logged in
                // Get name from session and assign to 'loggedInUser'
                loggedInUserID = (int)Session["loggedInUserID"];
            }

            // Logged in or out specific actions
            if (logInState == 1)
            {
                // If logged in as a child
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a child.";

                // Enable the 'Ask Question' Button
                btnAskQuestion.Enabled = true;
            }
            else if (logInState == 2)
            {
                // If logged in as a teacher
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a teacher.";

                // Change the 'Ask Button' Text so it explains why teachers can't use it
                btnAskQuestion.Text = "Log in as a child to ask questions";
            }
            else
            {
                // If not logged in
                // Show message to user
                lblInfo.Text = "You are not logged in.";

                // Hide the 'answerQuestion' div
                answerQuestion.Visible = false;
            }

            try
            {
                // Gets the 'questionID' from the URL if available, and passes it to the 'ShowQuestion' method
                questionIDFromURL = int.Parse(Request.QueryString["questionID"]);

                // Run the 'ShowQuestion' method with a parameter from the URL
                ShowQuestion(questionIDFromURL);
            }
            catch { }

            // List all questions in a table
            List<Question> questions = MyDBConnection.ListAllQuestions();

            // Show each 'Question' object from the 'questions' list in a dynamically created table
            foreach (var question in questions)
            {
                TableRow row = new TableRow();

                // Question title
                TableCell cell1 = new TableCell();

                // This button has the question title and uses the 'btnViewQuestion' event
                // It is formatted using CSS to look like hyperlinked text
                Button btnViewQuestion = new Button();
                btnViewQuestion.Text = question.QuestionTitle;
                btnViewQuestion.CommandArgument = question.QuestionID.ToString();
                btnViewQuestion.Click += new EventHandler(btnViewQuestion_Click);
                btnViewQuestion.CssClass += "hidden_btn";
                cell1.Controls.Add(btnViewQuestion);

                if (logInState == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a question and uses the 'btnEditQuestion' event
                    // It is formatted using CSS to look like yellow hyperlinked text
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = question.QuestionID.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditQuestion_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    cell1.Controls.Add(btnEditLesson);

                    // This button is for deleting a question and uses the 'btnDeleteQuestion' event
                    // It is formatted using CSS to look like red hyperlinked text
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = question.QuestionID.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteQuestion_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    cell1.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(cell1);

                // Question asker child
                TableCell cell2 = new TableCell();
                cell2.Text = question.QuestionChild.CldName + " " + question.QuestionChild.CldSurname;
                row.Cells.Add(cell2);

                // Question topic
                TableCell cell3 = new TableCell();
                cell3.Text = question.QuestionLesson.LessonTopic.TopicName;
                row.Cells.Add(cell3);

                // Question date
                TableCell cell4 = new TableCell();
                cell4.Text = question.QuestionDate;
                row.Cells.Add(cell4);

                tblQuestions.Rows.Add(row);
            }
        }

        protected void btnViewQuestion_Click(object sender, EventArgs e)
        {
            // Reload the discussion board page with the question ID of the question as a query string
            Response.Redirect("DiscussionBoard.aspx?questionID=" + int.Parse(((Button)sender).CommandArgument));
        }

        // Method that displays the details of the selected question
        public void ShowQuestion(int questionSelected)
        {
            // Show the 'selectedQuestion' div
            selectedQuestion.Visible = true;

            // Create a list of 'Question' objects using the 'ShowQuestionByID' method
            List<Question> showQuestion = MyDBConnection.ShowQuestionByID(questionSelected);

            foreach (var question in showQuestion)
            {
                // Populate lables and text boxes with details from the selected question
                lblQuestionTitle.Text = question.QuestionTitle;
                txtShowQuestionText.Text = question.QuestionText;
                lblQuestionDate.Text = question.QuestionDate;
                lblQuestionChild.Text = question.QuestionChild.CldName + " " + question.QuestionChild.CldSurname;
                lblQuestionTopic.Text = question.QuestionLesson.LessonTopic.TopicName;
                lblQuestionLesson.Text = question.QuestionLesson.LessonTitle;
            }

            // Create a list of 'Answer' objects of the selected question using the 'ListAllAnswers' method
            List<Answer> answers = MyDBConnection.ListAllAnswers(questionSelected);

            if (answers.Count == 0)
            {
                // There are no replies
                // Show message to user
                lblReplies.Text = "No Replies to selected question";
            }
            else
            {
                // There are replies
                // Show message to user
                lblReplies.Text = "Replies";

                // Show answers in a dynamic table
                foreach (var answer in answers)
                {
                    TableRow row1 = new TableRow();

                    TableCell cell1 = new TableCell();
                    Label a = new Label();
                    a.Text = "Reply by ";
                    a.Font.Bold = true;
                    cell1.Controls.Add(a);

                    // Answer by
                    Label b = new Label();
                    b.Text = answer.CurrentPerson;
                    cell1.Controls.Add(b);

                    Label c = new Label();
                    c.Text = " on ";
                    c.Font.Bold = true;
                    cell1.Controls.Add(c);

                    // Answer date
                    Label d = new Label();
                    d.Text = answer.AnswerDate;
                    cell1.Controls.Add(d);

                    if (logInState == 2)
                    {
                        // If logged in user is a teacher
                        // This button is for deleting a lesson and uses the 'btnDeleteAnswer' event
                        // It is formatted using CSS to look like red hyperlinked text
                        Button btnDeleteAnswer = new Button();
                        btnDeleteAnswer.Text = "Delete";
                        btnDeleteAnswer.CommandArgument = answer.AnswerID.ToString();
                        btnDeleteAnswer.Click += new EventHandler(btnDeleteAnswer_Click);
                        btnDeleteAnswer.CssClass += "hidden_delete_answer_btn";
                        cell1.Controls.Add(btnDeleteAnswer);
                    }

                    row1.Cells.Add(cell1);
                    row1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    tblAnswers.Rows.Add(row1);

                    TableRow row2 = new TableRow();

                    // Answer text
                    TableCell cell2 = new TableCell();
                    cell2.Text = answer.AnswerText;
                    row2.Cells.Add(cell2);
                    tblAnswers.Rows.Add(row2);

                    TableRow row3 = new TableRow();
                    TableCell cell3 = new TableCell();
                    row3.Cells.Add(cell3);
                    tblAnswers.Rows.Add(row3);
                }
            }
        }

        // Method which shows the edit question div
        public void ShowEditQuestion(int questionSelected)
        {
            // Create a list of 'Question' objects using 'ShowQuestionByID' method
            List<Question> showQuestion = MyDBConnection.ShowQuestionByID(questionSelected);

            // Hide the 'allQuestions' div and show the 'editQuestion' div
            allQuestions.Visible = false;
            editQuestion.Visible = true;

            foreach (var question in showQuestion)
            {
                // Populate the labels and text boxes with details from the selected 'Question' object
                txtEditQuestionTitle.Text = question.QuestionTitle;
                lblEditQuestionAsker.Text = question.QuestionChild.CldName + " " + question.QuestionChild.CldSurname;
                lblEditQuestionDate.Text = question.QuestionDate;
                txtEditQuestionText.Text = question.QuestionText;
            }
        }

        protected void btnAskQuestion_Click(object sender, EventArgs e)
        {
            // Redirect the user to the 'DiscussionBoardAskQuestion.aspx' page
            Response.Redirect("DiscussionBoardAskQuestion.aspx");
        }

        protected void btnAddReply_Click(object sender, EventArgs e)
        {
            // Save the question answer to the database using the 'SaveAnswer' method
            if (MyDBConnection.SaveAnswer(questionIDFromURL, txtAddReplyTxt.Text, logInState, loggedInUserID) == true)
            {
                // Answer saved successfully
                // Reload page using the 'questionID' as a query string
                Response.Redirect("DiscussionBoard.aspx?questionID=" + questionIDFromURL);
            }
        }

        protected void btnEditQuestion_Click(object sender, EventArgs e)
        {
            // Reload the page with the 'editQuestionID' as a query string
            Response.Redirect("DiscussionBoard.aspx?editQuestionID=" + int.Parse(((Button)sender).CommandArgument));
        }

        protected void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            // Delete the selected question using the 'DeleteQuestion' method
            if (MyDBConnection.DeleteQuestion(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                // Question deleted successfully
                // Reload the page
                Response.Redirect("DiscussionBoard.aspx");
            }
        }

        protected void btnDeleteAnswer_Click(object sender, EventArgs e)
        {
            // Delete the selected question using the 'DeleteAnswer' method
            if (MyDBConnection.DeleteAnswer(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                // Answer deleted successfully
                // Reload page using the 'questionID' as a query string
                Response.Redirect("DiscussionBoard.aspx?questionID=" + questionIDFromURL);
            }
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            // Save the edited question using the 'EditQuestion' method
            if (MyDBConnection.EditQuestion(editQuestionFromURL, txtEditQuestionTitle.Text, txtEditQuestionText.Text) == true)
            {
                // Question edited and save successfully
                // Reload the page
                Response.Redirect("DiscussionBoard.aspx");
            }
        }

        protected void btnShowAllQuestions_Click(object sender, EventArgs e)
        {
            // Reload the current page, causing all questions to be displayed
            Response.Redirect("DiscussionBoard.aspx");
        }
    }
}