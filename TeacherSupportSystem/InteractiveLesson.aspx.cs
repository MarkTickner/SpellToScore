using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherSupportSystem
{
    public partial class InteractiveLesson : System.Web.UI.Page
    {
        int logInState; // 0 = logged out, 1 = logged in as a child, 2 = logged in as a teacher
        string loggedInUser; // the name of the user logged in, if any
        int loggedInUserID; // the ID of the user logged in, if any
        int editLessonFromURL; // the lessonID of the lesson to edit
        string filterTypeFromURL; // the type of filter to use, either topic or teacher
        int filterIDFromURL; // the ID of the type of filter to use, either for topic or teacher

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Use the 'Show Lesson' method with the parameter from the URL 'lessonID'
                ShowLesson(int.Parse(Request.QueryString["lessonID"]));
            }
            catch { }

            try
            {
                // Gets the 'lessonID' from the URL if available, and passes it to the 'ShowEditLesson' method
                editLessonFromURL = int.Parse(Request.QueryString["editLessonID"]);

                // Check to see if the page has posted back
                if (!Page.IsPostBack)
                {
                    // Page hasn't posted back
                    // Use the 'Show Lesson' method with the parameter from the URL 'editLessonID'
                    ShowEditLesson(editLessonFromURL);
                }
            }
            catch { }

            // Get int from session, if it exists
            if (Session["logInState"] != null)
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
            }
            else if (logInState == 2)
            {
                // If logged in as a teacher
                // Show message to user
                lblInfo.Text = loggedInUser + ", you are logged in as a teacher.";

                // Show the 'Add Lesson' button
                btnAddLesson.Visible = true;
            }
            else
            {
                // If not logged in
                // Show message to user
                lblInfo.Text = "You are not logged in.";
            }

            try
            {
                // Get value [0] from URL, which is the type of filter to use, either topic or teacher
                filterTypeFromURL = Request.QueryString[0];
                // Get value [1] from URL, which is the ID of the type of filter to use, either for topic or teacher
                filterIDFromURL = int.Parse(Request.QueryString[1]);

                if (filterTypeFromURL == "TopicID")
                {
                    // The type of filter to use is 'Topic'
                    // Show message to user
                    lblFilter.Text = "Filtering lessons by Topic.";
                }
                else if (filterTypeFromURL == "TeacherID")
                {
                    // The type of filter to use is 'Teacher'
                    // Show message to user
                    lblFilter.Text = "Filtering lessons by Teacher.";
                }

                // Run the 'FilterLessons' method with the parameters taken from the URL
                FilterLessons(filterTypeFromURL, filterIDFromURL);
            }
            catch
            {
                // The lesson filter URL parameters are null, so hide the filtered lessons table
                tblFilteredLessons.Visible = false;
            }

            // List all lessons in a table using the 'ShowAllLessons' method
            ShowAllLessons();
        }

        // Method to show all the lessons from the database in a dynamically created table
        public void ShowAllLessons()
        {
            // Create a list of 'Lesson' objects using the 'ListAllLessons' method
            List<Lesson> lessons = MyDBConnection.ListAllLessons();

            // Show each 'Lesson' object from the 'lessons' list in a dynamically created table
            foreach (var lesson in lessons)
            {
                TableRow row = new TableRow();

                // Lesson title
                TableCell cell1 = new TableCell();

                // This button has the lesson name and uses the 'btnViewLesson' event
                // It is formatted using CSS to look like hyperlinked text
                Button btnViewLesson = new Button();
                btnViewLesson.Text = lesson.LessonTitle;
                btnViewLesson.CommandArgument = lesson.LessonID.ToString();
                btnViewLesson.Click += new EventHandler(btnViewLesson_Click);
                btnViewLesson.CssClass += "hidden_btn";
                cell1.Controls.Add(btnViewLesson);

                if (logInState == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a lesson and uses the 'btnEditLesson' event
                    // It is formatted using CSS to look like yellow hyperlinked text
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = lesson.LessonID.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditLesson_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    cell1.Controls.Add(btnEditLesson);

                    // This button is for deleting a lesson and uses the 'btnDeleteLesson' event
                    // It is formatted using CSS to look like red hyperlinked text
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = lesson.LessonID.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteLesson_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    cell1.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(cell1);

                // Lesson teacher
                TableCell cell2 = new TableCell();
                cell2.Text = lesson.LessonTeacher.TchTitle + " " + lesson.LessonTeacher.TchSurname;
                row.Cells.Add(cell2);

                // Lesson topic
                TableCell cell3 = new TableCell();
                cell3.Text = lesson.LessonTopic.TopicName;
                row.Cells.Add(cell3);

                // Lesson date
                TableCell cell4 = new TableCell();
                cell4.Text = lesson.LessonDate;
                row.Cells.Add(cell4);

                tblLessons.Rows.Add(row);
            }
        }

        public void FilterLessons(string filterType, int selectedValue)
        {
            // Show the filtered lesson table and hide the lesson table and 'filterLessons' div
            tblFilteredLessons.Visible = true;
            tblLessons.Visible = false;
            filterLessons.Visible = false;

            // Create a list of 'Lesson' objects using the 'ListAllLessonsByType' method with parameters from the URL
            List<Lesson> filteredLessons = MyDBConnection.ListAllLessonsByType(filterTypeFromURL, filterIDFromURL);

            // Show each 'Lesson' object from the 'filteredLessons' list in a dynamically created table
            foreach (var lesson in filteredLessons)
            {
                TableRow row = new TableRow();

                // Lesson title
                TableCell cell1 = new TableCell();

                // This button has the lesson name and uses the 'btnViewLesson' event
                // It is formatted using CSS to look like hyperlinked text
                Button btnViewLesson = new Button();
                btnViewLesson.Text = lesson.LessonTitle;
                btnViewLesson.CommandArgument = lesson.LessonID.ToString();
                btnViewLesson.Click += new EventHandler(btnViewLesson_Click);
                btnViewLesson.CssClass += "hidden_btn";
                cell1.Controls.Add(btnViewLesson);

                if (logInState == 2)
                {
                    // If logged in user is a teacher
                    // This button is for editing a lesson and uses the 'btnEditLesson' event
                    // It is formatted using CSS to look like yellow hyperlinked text
                    Button btnEditLesson = new Button();
                    btnEditLesson.Text = "Edit";
                    btnEditLesson.CommandArgument = lesson.LessonID.ToString();
                    btnEditLesson.Click += new EventHandler(btnEditLesson_Click);
                    btnEditLesson.CssClass += "hidden_edit_btn";
                    cell1.Controls.Add(btnEditLesson);

                    // This button is for deleting a lesson and uses the 'btnDeleteLesson' event
                    // It is formatted using CSS to look like red hyperlinked text
                    Button btnDeleteLesson = new Button();
                    btnDeleteLesson.Text = "Delete";
                    btnDeleteLesson.CommandArgument = lesson.LessonID.ToString();
                    btnDeleteLesson.Click += new EventHandler(btnDeleteLesson_Click);
                    btnDeleteLesson.CssClass += "hidden_delete_btn";
                    cell1.Controls.Add(btnDeleteLesson);
                }

                row.Cells.Add(cell1);

                // Lesson teacher
                TableCell cell2 = new TableCell();
                cell2.Text = lesson.LessonTeacher.TchTitle + " " + lesson.LessonTeacher.TchSurname;
                row.Cells.Add(cell2);

                // Lesson topic
                TableCell cell3 = new TableCell();
                cell3.Text = lesson.LessonTopic.TopicName;
                row.Cells.Add(cell3);

                // Lesson date
                TableCell cell4 = new TableCell();
                cell4.Text = lesson.LessonDate;
                row.Cells.Add(cell4);

                tblFilteredLessons.Rows.Add(row);
            }
        }

        // Method to show the selected lesson, using a parameter from the URL
        public void ShowLesson(int lessonSelected)
        {
            // Create a 'Lesson' object in a list using the 'ShowLessonByID' method
            List<Lesson> showLesson = MyDBConnection.ShowLessonByID(lessonSelected);

            // Hide the 'allLessons' dov and show the 'selectedLesson' div
            allLessons.Visible = false;
            selectedLesson.Visible = true; 
            
            foreach (var lesson in showLesson)
            {
                // Populate the lables, text boxes and image in the 'selectedLesson' div with the variables from the 'Lesson' object
                lblLessonTitle.Text = lesson.LessonTitle;
                lblLessonTeacher.Text = lesson.LessonTeacher.TchTitle + " " + lesson.LessonTeacher.TchSurname;
                lblLessonTopic.Text = lesson.LessonTopic.TopicName;
                lblLessonDate.Text = lesson.LessonDate;
                txtLessonText.Text = lesson.LessonText;
                imgLessonImage.ImageUrl = lesson.LessonImage.LessonImageSrc;
            }
        }

        // Method to show and edit the selected lesson, using a parameter from the URL
        public void ShowEditLesson(int lessonSelected)
        {
            // Create a 'Lesson' object in a list using the 'ShowLessonByID' method
            List<Lesson> showLesson = MyDBConnection.ShowLessonByID(lessonSelected);

            // Hide the 'allLessons' dov and show the 'editLesson' div
            allLessons.Visible = false;
            editLesson.Visible = true;

            foreach (var lesson in showLesson)
            {
                // Populate the lables and text boxes in the 'editLesson' div with the variables from the 'Lesson' object
                txtEditLessonTitle.Text = lesson.LessonTitle;
                lblEditLessonTeacher.Text = lesson.LessonTeacher.TchTitle + " " + lesson.LessonTeacher.TchSurname;
                lblEditLessonTopic.Text = lesson.LessonTopic.TopicName;
                lblEditLessonDate.Text = lesson.LessonDate;
                txtEditLessonText.Text = lesson.LessonText;
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
            if (MyDBConnection.DeleteLesson(int.Parse(((Button)sender).CommandArgument)) == true)
            {
                // Lesson deletion successful, reload the interactive lesson page
                Response.Redirect("InteractiveLesson.aspx");
            }
        }

        protected void btnAddLesson_Click(object sender, EventArgs e)
        {
            // Redirect the user to the 'InteractiveLessonAdd.aspx' page
            Response.Redirect("InteractiveLessonAdd.aspx");
        }

        protected void btnShowAllLessons_Click(object sender, EventArgs e)
        {
            // Reload the current page with no parameters, which will cause a list of all lessons to be shown
            Response.Redirect("InteractiveLesson.aspx");
        }

        protected void btnSaveLesson_Click(object sender, EventArgs e)
        {
            // Save the edited lesson using the 'EditLesson' method
            if (MyDBConnection.EditLesson(editLessonFromURL, txtEditLessonTitle.Text, txtEditLessonText.Text) == true)
            {
                // Lesson edit successful, reload the interactive lesson page
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
            Response.Redirect("InteractiveLesson.aspx?filterLessonType=TeacherID&filterTeacherID=" + int.Parse(dropFilterTopic.SelectedValue));
        }
    }
}