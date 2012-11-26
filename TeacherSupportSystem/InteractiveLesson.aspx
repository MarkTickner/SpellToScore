<%@ Page Title="Interactive Lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InteractiveLesson.aspx.cs" Inherits="TeacherSupportSystem.InteractiveLesson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>
        Interactive Lesson</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
<div id="allLessons" runat="server">


        <asp:Table ID="tblLessons" runat="server" GridLines="Both" 
    Width="920px" CellPadding="2" CellSpacing="1" 
        BorderColor="#465767" Font-Bold="False">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True">Lesson Title</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Teacher</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Topic</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Date</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" BackColor="White">
            </asp:TableRow>
        </asp:Table>
    <asp:Table ID="tblFilteredLessons" runat="server" GridLines="Both" 
    Width="920px" CellPadding="2" CellSpacing="1" 
        BorderColor="#465767" Font-Bold="False">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True">Lesson Title</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Teacher</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Topic</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Date</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" BackColor="White">
            </asp:TableRow>
        </asp:Table>
    
    <table style="width:100%">
        <tr>
            <td>
                <br /><div id="filterLessons" runat="server"><strong>Filter by:</strong> Topic: <asp:DropDownList ID="dropFilterTopic" runat="server" DataSourceID="filterTopicDataSource" DataTextField="TopicName" DataValueField="TopicID" onselectedindexchanged="dropFilterTopic_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><asp:AccessDataSource ID="FilterTopicDataSource" runat="server" DataFile="~/App_Data/MainDatabase.accdb" SelectCommand="SELECT * FROM [Topic] ORDER BY [TopicName]"></asp:AccessDataSource> <strong>or</strong> Teacher: <asp:DropDownList ID="dropFilterTeacher" runat="server" DataSourceID="FilterTeacherDataSource" DataTextField="TchSurname" DataValueField="TeacherID" onselectedindexchanged="dropFilterTeacher_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><asp:AccessDataSource ID="FilterTeacherDataSource" runat="server" DataFile="~/App_Data/MainDatabase.accdb" SelectCommand="SELECT [TeacherID], [TchTitle], [TchSurname] FROM [Teacher] ORDER BY [TchSurname]"></asp:AccessDataSource></div></td>
            <td align="right"><br /><asp:Label ID="lblFilter" runat="server">Showing all lessons.</asp:Label>&nbsp;<asp:Button 
                    ID="btnShowAllLessons1" runat="server" onclick="btnShowAllLessons_Click" 
                    Text="Show All Lessons" />&nbsp;<asp:Button ID="btnAddLesson" 
                    runat="server" onclick="btnAddLesson_Click" 
            Text="Add New Lesson" Visible="False" />
            </td>
        </tr>
    </table>

</div><div id="selectedLesson" runat="server" visible="false">

    <p><asp:Label ID="lblLessonTitle" runat="server" CssClass="style3" Font-Bold="True"></asp:Label><span class="style3"> by <asp:Label ID="lblLessonTeacher" runat="server" Font-Bold="True"></asp:Label></span>
    </p>
    <p><strong>Topic:</strong> <asp:Label ID="lblLessonTopic" runat="server"></asp:Label>, <strong>Date and Time:</strong> <asp:Label ID="lblLessonDate" runat="server"></asp:Label></p>
        <p>
        <asp:TextBox ID="txtLessonText" runat="server" Height="200px" TextMode="MultiLine" Width="710px" CssClass="hidden_txt" ReadOnly="true"></asp:TextBox>
            <asp:Image ID="imgLessonImage" runat="server" BorderWidth="1px" Height="200px" Width="200px" />
    </p>
    <p>
        <asp:Button ID="btnShowAllLessons2" runat="server" Text="Show All Lessons" 
            onclick="btnShowAllLessons_Click" />
    </p>
    </div>
    <div id="editLesson" runat="server" visible="false">

    <p><asp:TextBox ID="txtEditLessonTitle" 
            runat="server" Width="230px" Font-Bold="True" style="font-size: large"></asp:TextBox><span class="style3"> by <asp:Label ID="lblEditLessonTeacher" runat="server" 
            Font-Bold="True"></asp:Label><strong>*</strong></span></p>
    <p><strong>Topic:</strong> <asp:Label ID="lblEditLessonTopic" runat="server"></asp:Label>*, <strong>Date and Time:</strong> 
        <asp:Label ID="lblEditLessonDate" runat="server"></asp:Label>*</p>
        <p>* this information can&#39;t be edited.</p>
        <p>
        <asp:TextBox ID="txtEditLessonText" runat="server" Height="230px" 
                TextMode="MultiLine" Width="920px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnSaveLesson" runat="server" onclick="btnSaveLesson_Click" 
            Text="Save Lesson" />&nbsp;<asp:Button 
                    ID="btnShowAllLessons3" runat="server" onclick="btnShowAllLessons_Click" 
                    Text="Show All Lessons" />
    </p>
    </div>
        </asp:Content>
