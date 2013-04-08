using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpellToScore.Web
{
    public partial class InteractiveLesson : Page
    {
        User currentUser;

        int editLessonFromURL; // the lessonID of the lesson to edit
        string filterTypeFromURL; // the type of filter to use, either topic or teacher
        int filterIDFromURL; // the ID of the type of filter to use, either for topic or teacher

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
                }
                else if (currentUser.UserType == 2)
                {
                    lblInfo.Text = currentUser.Title + " " + currentUser.Surname + ", you are logged in as a teacher.";
                }
            }
            else
            {
                // If not logged in
                lblInfo.Text = "You are not logged in.";
            }

            try
            {
                ShowLesson(int.Parse(Request.QueryString["lessonID"]));
            }
            catch { }

            try
            {
                editLessonFromURL = int.Parse(Request.QueryString["editLessonID"]);

                // Check to see if the page has posted back
                if (!Page.IsPostBack)
                {
                    // Page hasn't posted back
                    ShowEditLesson(editLessonFromURL);
                }
            }
            catch { }

            try
            {
                // Get value [0] from URL, which is the type of filter to use, either topic or teacher
                filterTypeFromURL = Request.QueryString[0];
                // Get value [1] from URL, which is the ID of the type of filter to use, either for topic or teacher
                filterIDFromURL = int.Parse(Request.QueryString[1]);

                if (filterTypeFromURL == "TopicID")
                {
                    // The type of filter to use is 'Topic'
                    lblFilter.Text = "Filtering lessons by Topic.";
                }
                else if (filterTypeFromURL == "TeacherID")
                {
                    // The type of filter to use is 'Teacher'
                    lblFilter.Text = "Filtering lessons by Teacher.";
                }

                CreateFilteredLessonsTable(filterTypeFromURL, filterIDFromURL);
            }
            catch
            {
                // The lesson filter URL parameters are null, so hide the filtered lessons table
                tblFilteredLessons.Visible = false;
            }

            CreateLessonsTable();
        }

        public void CreateLessonsTable()
        {
            List<Lesson> lessons = DatabaseWebService.ListAllLessons();

            foreach (var lesson in lessons)
            {
                TableRow row = new TableRow();

                // This button has the lesson name and uses the 'btnViewLesson' event
                TableCell title = new TableCell();
                Button btnViewLesson = new Button();
                btnViewLesson.Text = lesson.Title;
                btnViewLesson.CommandArgument = lesson.Id.ToString();
                btnViewLesson.Click += new EventHandler(btnViewLesson_Click);
                btnViewLesson.CssClass += "hidden_btn";
                title.Controls.Add(btnViewLesson);

                if (currentUser != null && currentUser.UserType == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a lesson and uses the 'btnEditLesson' event
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = lesson.Id.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditLesson_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    title.Controls.Add(btnEditLesson);

                    // This button is for deleting a lesson and uses the 'btnDeleteLesson' event
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = lesson.Id.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteLesson_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    title.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(title);

                TableCell teacher = new TableCell();
                teacher.Text = lesson.LessonTeacher.Title + " " + lesson.LessonTeacher.Surname;
                row.Cells.Add(teacher);

                TableCell topic = new TableCell();
                topic.Text = lesson.LessonTopic.Name;
                row.Cells.Add(topic);

                TableCell date = new TableCell();
                date.Text = lesson.Date;
                row.Cells.Add(date);

                tblLessons.Rows.Add(row);
            }
        }

        public void CreateFilteredLessonsTable(string filterType, int selectedValue)
        {
            // Show the filtered lesson table and hide the lesson table and 'filterLessons' div
            tblFilteredLessons.Visible = true;
            tblLessons.Visible = false;
            filterLessons.Visible = false;

            List<Lesson> filteredLessons = DatabaseWebService.ListAllLessonsByType(filterTypeFromURL, filterIDFromURL);

            foreach (var lesson in filteredLessons)
            {
                TableRow row = new TableRow();

                // This button has the lesson name and uses the 'btnViewLesson' event
                TableCell title = new TableCell();
                Button btnViewLesson = new Button();
                btnViewLesson.Text = lesson.Title;
                btnViewLesson.CommandArgument = lesson.Id.ToString();
                btnViewLesson.Click += new EventHandler(btnViewLesson_Click);
                btnViewLesson.CssClass += "hidden_btn";
                title.Controls.Add(btnViewLesson);

                if (currentUser != null && currentUser.UserType == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a lesson and uses the 'btnEditLesson' event
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = lesson.Id.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditLesson_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    title.Controls.Add(btnEditLesson);

                    // This button is for deleting a lesson and uses the 'btnDeleteLesson' event
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = lesson.Id.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteLesson_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    title.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(title);

                TableCell teacher = new TableCell();
                teacher.Text = lesson.LessonTeacher.Title + " " + lesson.LessonTeacher.Surname;
                row.Cells.Add(teacher);

                TableCell topic = new TableCell();
                topic.Text = lesson.LessonTopic.Name;
                row.Cells.Add(topic);

                TableCell date = new TableCell();
                date.Text = lesson.Date;
                row.Cells.Add(date);

                tblFilteredLessons.Rows.Add(row);
            }
        }

        public void ShowLesson(int lessonSelected)
        {
            List<Lesson> showLesson = DatabaseWebService.ShowLessonByID(lessonSelected);

            // Hide the 'allLessons' div and show the 'selectedLesson' div
            allLessons.Visible = false;
            selectedLesson.Visible = true;

            foreach (var lesson in showLesson)
            {
                lblLessonTitle.Text = lesson.Title;
                lblLessonTeacher.Text = lesson.LessonTeacher.Title + " " + lesson.LessonTeacher.Surname;
                lblLessonTopic.Text = lesson.LessonTopic.Name;
                lblLessonDate.Text = lesson.Date;
                txtLessonText.Text = lesson.Text;
                imgLessonImage.ImageUrl = lesson.LessonImage.Source;
            }
        }

        public void ShowEditLesson(int lessonSelected)
        {
            List<Lesson> showLesson = DatabaseWebService.ShowLessonByID(lessonSelected);

            // Hide the 'allLessons' dov and show the 'editLesson' div
            allLessons.Visible = false;
            editLesson.Visible = true;

            foreach (var lesson in showLesson)
            {
                txtEditLessonTitle.Text = lesson.Title;
                lblEditLessonTeacher.Text = lesson.LessonTeacher.Title + " " + lesson.LessonTeacher.Surname;
                lblEditLessonTopic.Text = lesson.LessonTopic.Name;
                lblEditLessonDate.Text = lesson.Date;
                txtEditLessonText.Text = lesson.Text;
            }
        }

        protected void btnViewLesson_Click(object sender, EventArgs e)
        {
            // Reload the interactive lesson page with the lesson details of the lesson to show as string queries
            Response.Redirect("InteractiveLesson.aspx?lessonID=" + int.Parse(((Button)sender).CommandArgument));
        }

        protected void btnEditLesson_Click(object sender, EventArgs e)
        {
            // Reload the interactive lesson page with the lesson details of the lesson to edit as string queries
            Response.Redirect("InteractiveLesson.aspx?editLessonID=" + int.Parse(((Button)sender).CommandArgument));
        }

        protected void btnDeleteLesson_Click(object sender, EventArgs e)
        {
            // Delete the selected lesson using the 'DeleteLesson' method
            if (DatabaseWebService.DeleteLesson(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                Response.Redirect("InteractiveLesson.aspx");
            }
        }

        protected void btnAddLesson_Click(object sender, EventArgs e)
        {
            Response.Redirect("InteractiveLessonAdd.aspx");
        }

        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            Response.Redirect("InteractiveLesson.aspx");
        }

        protected void btnSaveLesson_Click(object sender, EventArgs e)
        {
            // Save the edited lesson using the 'EditLesson' method
            if (DatabaseWebService.EditLesson(editLessonFromURL, txtEditLessonTitle.Text, txtEditLessonText.Text) == true)
            {
                Response.Redirect("InteractiveLesson.aspx");
            }
        }

        protected void dropFilterTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reload the interactive lesson page with filter lesson details as string queries
            Response.Redirect("InteractiveLesson.aspx?filterLessonType=TopicID&filterTopicID=" + int.Parse(dropFilterTopic.SelectedValue));
        }

        protected void dropFilterTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reload the interactive lesson page with filter lesson details as string queries
            Response.Redirect("InteractiveLesson.aspx?filterLessonType=TeacherID&filterTeacherID=" + int.Parse(dropFilterTeacher.SelectedValue));
        }
    }
}