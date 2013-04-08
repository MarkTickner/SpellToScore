using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpellToScore.Web
{
    public partial class DiscussionBoard : Page
    {
        User currentUser;

        int questionIDFromURL; // the questionID will be saved from the website URL here
        int editQuestionFromURL; // the questionID of the lesson to edit

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get logged in user
            if (UserLogin.LoggedInUser != null)
            {
                // Logged in
                currentUser = UserLogin.LoggedInUser;

                if (currentUser.UserType == 1)
                {
                    lblInfo.Text = currentUser.FirstName + " " + currentUser.Surname + ", you are logged in as a child.";
                    btnAskQuestion.Enabled = true;
                }
                else if (currentUser.UserType == 2)
                {
                    lblInfo.Text = currentUser.Title + " " + currentUser.Surname + ", you are logged in as a teacher.";
                    btnAskQuestion.Text = "Log in as a child to ask questions";
                }
            }
            else
            {
                // If not logged in
                lblInfo.Text = "You are not logged in.";

                // Hide the 'answerQuestion' div as user must be logged in to answer
                answerQuestion.Visible = false;
            }

            try
            {
                // Gets the 'questionID' from the URL if available, and passes it to the 'ShowEditQuestion' method
                editQuestionFromURL = int.Parse(Request.QueryString["editQuestionID"]);

                // Check to see if the page has posted back
                if (!Page.IsPostBack)
                {
                    // Page hasn't posted back
                    ShowEditQuestion(editQuestionFromURL);
                }
            }
            catch { }

            try
            {
                // Gets the 'questionID' from the URL if available, and passes it to the 'ShowQuestion' method
                questionIDFromURL = int.Parse(Request.QueryString["questionID"]);

                ShowQuestion(questionIDFromURL);
            }
            catch { }

            List<Question> questions = DatabaseWebService.ListAllQuestions();

            foreach (var question in questions)
            {
                TableRow row = new TableRow();

                // This button has the question title and uses the 'btnViewQuestion' event
                TableCell title = new TableCell();
                Button btnViewQuestion = new Button();
                btnViewQuestion.Text = question.Title;
                btnViewQuestion.CommandArgument = question.Id.ToString();
                btnViewQuestion.Click += new EventHandler(btnViewQuestion_Click);
                btnViewQuestion.CssClass += "hidden_btn";
                title.Controls.Add(btnViewQuestion);

                if (currentUser != null && currentUser.UserType == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a question and uses the 'btnEditQuestion' event
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = question.Id.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditQuestion_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    title.Controls.Add(btnEditLesson);

                    // This button is for deleting a question and uses the 'btnDeleteQuestion' event
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = question.Id.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteQuestion_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    title.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(title);

                TableCell asker = new TableCell();
                asker.Text = question.QuestionUser.FirstName + " " + question.QuestionUser.Surname;
                row.Cells.Add(asker);

                TableCell topic = new TableCell();
                topic.Text = question.QuestionLesson.LessonTopic.Name;
                row.Cells.Add(topic);

                TableCell date = new TableCell();
                date.Text = question.Date;
                row.Cells.Add(date);

                tblQuestions.Rows.Add(row);
            }
        }

        protected void btnViewQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoard.aspx?questionID=" + int.Parse(((Button)sender).CommandArgument));
        }

        public void ShowQuestion(int questionSelected)
        {
            // Show the 'selectedQuestion' div
            selectedQuestion.Visible = true;

            List<Question> showQuestion = DatabaseWebService.ShowQuestionByID(questionSelected);

            foreach (var question in showQuestion)
            {
                lblQuestionTitle.Text = question.Title;
                txtShowQuestionText.Text = question.Text;
                lblQuestionDate.Text = question.Date;
                lblQuestionChild.Text = question.QuestionUser.FirstName + " " + question.QuestionUser.Surname;
                lblQuestionTopic.Text = question.QuestionLesson.LessonTopic.Name;
                lblQuestionLesson.Text = question.QuestionLesson.Title;
            }

            List<Answer> answers = DatabaseWebService.ListAllAnswers(questionSelected);

            if (answers.Count == 0)
            {
                // There are no replies
                lblReplies.Text = "No Replies to selected question";
            }
            else
            {
                // There are replies
                lblReplies.Text = "Replies";

                foreach (var answer in answers)
                {
                    TableRow answerDetailsRow = new TableRow();

                    TableCell answerDetailsCell = new TableCell();
                    Label answerByText = new Label();
                    answerByText.Text = "Reply by ";
                    answerByText.Font.Bold = true;
                    answerDetailsCell.Controls.Add(answerByText);

                    Label answererName = new Label();
                    if (answer.Answerer.UserType == 1)
                    {
                        answererName.Text = answer.Answerer.FirstName + " " + answer.Answerer.Surname;
                    }
                    else
                    {
                        answererName.Text = answer.Answerer.Title + " " + answer.Answerer.Surname;
                    }
                    answerDetailsCell.Controls.Add(answererName);

                    Label answerDatePrefix = new Label();
                    answerDatePrefix.Text = " on ";
                    answerDatePrefix.Font.Bold = true;
                    answerDetailsCell.Controls.Add(answerDatePrefix);

                    Label answerDate = new Label();
                    answerDate.Text = answer.Date;
                    answerDetailsCell.Controls.Add(answerDate);

                    if (currentUser != null && currentUser.UserType == 2)
                    {
                        // If logged in user is a teacher
                        // This button is for deleting a lesson and uses the 'btnDeleteAnswer' event
                        Button btnDeleteAnswer = new Button();
                        btnDeleteAnswer.Text = "Delete";
                        btnDeleteAnswer.CommandArgument = answer.Id.ToString();
                        btnDeleteAnswer.Click += new EventHandler(btnDeleteAnswer_Click);
                        btnDeleteAnswer.CssClass += "hidden_delete_answer_btn";
                        answerDetailsCell.Controls.Add(btnDeleteAnswer);
                    }

                    answerDetailsRow.Cells.Add(answerDetailsCell);
                    answerDetailsRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    tblAnswers.Rows.Add(answerDetailsRow);

                    TableRow answerRow = new TableRow();
                    TableCell answerCell = new TableCell();
                    answerCell.Text = answer.Text;
                    answerRow.Cells.Add(answerCell);
                    tblAnswers.Rows.Add(answerRow);

                    TableRow dividerRow = new TableRow();
                    TableCell dividerCell = new TableCell();
                    dividerRow.Cells.Add(dividerCell);
                    tblAnswers.Rows.Add(dividerRow);
                }
            }
        }

        public void ShowEditQuestion(int questionSelected)
        {
            List<Question> showQuestion = DatabaseWebService.ShowQuestionByID(questionSelected);

            // Hide the 'allQuestions' div and show the 'editQuestion' div
            allQuestions.Visible = false;
            editQuestion.Visible = true;

            foreach (var question in showQuestion)
            {
                txtEditQuestionTitle.Text = question.Title;
                lblEditQuestionAsker.Text = question.QuestionUser.FirstName + " " + question.QuestionUser.Surname;
                lblEditQuestionDate.Text = question.Date;
                txtEditQuestionText.Text = question.Text;
            }
        }

        protected void btnAskQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoardAskQuestion.aspx");
        }

        protected void btnAddReply_Click(object sender, EventArgs e)
        {
            if (DatabaseWebService.SaveAnswer(questionIDFromURL, txtAddReplyTxt.Text, currentUser.UserType, UserLogin.LoggedInUser.Id) == true)
            {
                // Answer saved successfully
                Response.Redirect("DiscussionBoard.aspx?questionID=" + questionIDFromURL);
            }
        }

        protected void btnEditQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoard.aspx?editQuestionID=" + int.Parse(((Button)sender).CommandArgument));
        }

        protected void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (DatabaseWebService.DeleteQuestion(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                // Question deleted successfully
                Response.Redirect("DiscussionBoard.aspx");
            }
        }

        protected void btnDeleteAnswer_Click(object sender, EventArgs e)
        {
            if (DatabaseWebService.DeleteAnswer(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                // Answer deleted successfully
                Response.Redirect("DiscussionBoard.aspx?questionID=" + questionIDFromURL);
            }
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            if (DatabaseWebService.EditQuestion(editQuestionFromURL, txtEditQuestionTitle.Text, txtEditQuestionText.Text) == true)
            {
                // Question edited and save successfully
                Response.Redirect("DiscussionBoard.aspx");
            }
        }

        protected void btnShowAllQuestions_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiscussionBoard.aspx");
        }
    }
}