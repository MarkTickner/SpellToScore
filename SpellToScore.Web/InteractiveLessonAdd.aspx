<%@ Page Title="Interactive Lesson - Add New Lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InteractiveLessonAdd.aspx.cs" Inherits="SpellToScore.Web.InteractiveLessonAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .auto-style2
        {
            width: 60px;
            height: 100px;
        }
        .auto-style3
        {
            width: 100px;
        }
        .auto-style4
        {
            width: 157px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Interactive Lesson 
        - Add New Lesson</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <p>
        To create a new lesson, fill out the following details and press &#39;Save Lesson&#39;.</p>
    <table class="style1">
        <tr>
            <td class="auto-style3">
                Lesson Title</td>
            <td style="width: 350px">
        <asp:TextBox ID="txtLessonTitle" runat="server" Width="280px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLessonText" ErrorMessage="Required" Font-Bold="False" ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style2">
                Topic</td>
            <td class="auto-style4">
        <asp:DropDownList ID="dropLessonTopic" runat="server" 
            DataSourceID="topicDataSource" DataTextField="TopicName" 
            DataValueField="TopicID">
        </asp:DropDownList>
        <asp:AccessDataSource ID="topicDataSource" runat="server" 
            DataFile="~/App_Data/MainDatabase.accdb" 
                    SelectCommand="SELECT * FROM [Topic] ORDER BY [TopicName]">
        </asp:AccessDataSource>
            </td>
            <td align="right">
        <asp:Button ID="btnSaveLesson" runat="server" onclick="btnSaveLesson_Click" 
            Text="Save Lesson" ValidationGroup="1" /> <asp:Button 
                    ID="btnShowAllLessons1" runat="server" onclick="btnShowAllLessons_Click" 
                    Text="Show All Lessons" />
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                Content</td>
            <td colspan="4">
        <asp:TextBox ID="txtLessonText" runat="server" Height="250px" 
            TextMode="MultiLine" Width="820px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLessonText" ErrorMessage="Required" ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                Image</td>
            <td colspan="4">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td>
                            <asp:Image ID="imgImage1" runat="server" BorderWidth="1px" Height="100px" Width="100px" />
                            <br />
                            <asp:RadioButton ID="rbImage1" runat="server" GroupName="images" />
                        </td>
                        <td>
                            <asp:Image ID="imgImage2" runat="server" BorderWidth="1px" Height="100px" Width="100px" />
                            <br />
                            <asp:RadioButton ID="rbImage2" runat="server" GroupName="images" />
                        </td>
                        <td>
                            <asp:Image ID="imgImage3" runat="server" BorderWidth="1px" Height="100px" Width="100px" />
                            <br />
                            <asp:RadioButton ID="rbImage3" runat="server" GroupName="images" />
                        </td>
                        <td>
                            <asp:Image ID="imgImage4" runat="server" BorderWidth="1px" Height="100px" Width="100px" />
                            <br />
                            <asp:RadioButton ID="rbImage4" runat="server" GroupName="images" />
                        </td>
                        <td>
                            <asp:Image ID="imgImage5" runat="server" BorderWidth="1px" Height="100px" Width="100px" />
                            <br />
                            <asp:RadioButton ID="rbImage5" runat="server" GroupName="images" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
        <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
    </p>
</asp:Content>
