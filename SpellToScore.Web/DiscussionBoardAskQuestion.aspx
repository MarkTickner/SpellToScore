<%@ Page Title="Discussion Board - Ask Question" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiscussionBoardAskQuestion.aspx.cs" Inherits="SpellToScore.Web.DiscussionBoardAskQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .auto-style2
        {
            width: 100px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Discussion Board - Ask Question</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <p>
        To ask a teacher a question, fill out the following details and press &#39;Save Question&#39;.
    </p>
    <table style="width:100%">
        <tr>
            <td class="auto-style2">Question
        title:</td>
            <td>
                <asp:TextBox ID="txtQuestionTitle" runat="server" Width="350px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuestionTitle" ErrorMessage="Required" ForeColor="Red" ValidationGroup="group1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Question text:</td>
            <td>
                <asp:TextBox ID="txtQuestionText" runat="server" Width="700px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestionText" ErrorMessage="Required" ForeColor="Red" ValidationGroup="group1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Lesson:</td>
            <td>
                <asp:DropDownList ID="dropQuestionLesson" runat="server" DataSourceID="QuestionLessonDatasource" DataTextField="LessonTitle" DataValueField="LessonID">
                </asp:DropDownList>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dropQuestionLesson" ErrorMessage="Required" ForeColor="Red" ValidationGroup="group1"></asp:RequiredFieldValidator>
                <asp:AccessDataSource ID="QuestionLessonDatasource" runat="server" DataFile="~/App_Data/MainDatabase.accdb" SelectCommand="SELECT * FROM [Lesson] ORDER BY [LessonTitle]"></asp:AccessDataSource>
            </td>
        </tr>
    </table>
    <p>Topic will be completed automatically based on selected lesson.</p>
    <p>
        <asp:Button ID="btnSaveQuestion" runat="server" OnClick="btnSaveQuestion_Click"
            Text="Save Question" ValidationGroup="group1" /><asp:Button ID="btnShowAllLessons" runat="server" OnClick="btnShowAllLessons_Click" Text="Show All Questions" />
&nbsp;<asp:Label ID="lblConfirmation" runat="server"></asp:Label>
    </p>

</asp:Content>
