<%@ Page Title="Discussion Board" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DiscussionBoard.aspx.cs" Inherits="TeacherSupportSystem.DiscussionBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style2
        {
            width: 140px;
        }
        .auto-style3
        {
            font-size: 0.8em;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Discussion Board</h2>
    
        <table style="width:100%">
            <tr>
                <td>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </td>
                <td class="auto-style2" align="right">
                    <asp:Button ID="btnAskQuestion" runat="server" Text="Ask Question" Width="210px" OnClick="btnAskQuestion_Click" Enabled="False" />
                </td>
            </tr>
        </table>
    
    <div id="allQuestions" runat="server">
    <p>
        <asp:Table ID="tblQuestions" runat="server" GridLines="Both" 
    Width="920px" CellPadding="2" CellSpacing="1" 
        BorderColor="#465767" Font-Bold="False">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True">Question Title</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Child</asp:TableCell>
                <asp:TableCell runat="server" BackColor="#4B6C9E" Font-Bold="True" ForeColor="White">Topic</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Date</asp:TableCell>
            </asp:TableRow>
        </asp:Table></p>
    </div><div id="selectedQuestion" runat="server" visible="false">
    <p><asp:Label ID="lblQuestionTitle" runat="server" style="font-size: large; font-weight: 700"></asp:Label>
    </p>
    <p><strong>Asked by</strong> <asp:Label ID="lblQuestionChild" runat="server"></asp:Label> <strong>on</strong> <asp:Label ID="lblQuestionTopic" runat="server"></asp:Label> <strong>about lesson</strong> <asp:Label ID="lblQuestionLesson" runat="server"></asp:Label> <strong>on</strong> <asp:Label ID="lblQuestionDate" runat="server"></asp:Label>
    </p>
        <p>
        <asp:TextBox ID="txtShowQuestionText" runat="server" Height="50px" TextMode="MultiLine" Width="920px" CssClass="hidden_txt" ReadOnly="true"></asp:TextBox>
    </p>
        <p>
            <asp:Label ID="lblReplies" runat="server" Font-Bold="True" Font-Size="Large" CssClass="auto-style3"></asp:Label>
            </p>
        <asp:Table ID="tblAnswers" runat="server" GridLines="Both" 
    Width="920px" CellPadding="2" CellSpacing="1" 
        BorderColor="#465767" Font-Bold="False">
            <asp:TableRow ID="TableRow4" runat="server" BackColor="White">
            </asp:TableRow>
        </asp:Table>

        <div id="answerQuestion" runat="server">
    <p>Add Reply:</p>
    <p>
    <asp:TextBox ID="txtAddReplyTxt" runat="server" Height="50px" TextMode="MultiLine" Width="915px"></asp:TextBox> 

    </p>
        <p><asp:Button ID="btnAddReply" runat="server" Text="Save Reply" OnClick="btnAddReply_Click" ValidationGroup="addReply" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtAddReplyTxt" ForeColor="Red" ValidationGroup="addReply"></asp:RequiredFieldValidator>

    </p>
            </div></div>
    <div id="editQuestion" runat="server" visible="false">
        <p><asp:TextBox ID="txtEditQuestionTitle" 
            runat="server" Width="480px" Font-Bold="True" style="font-size: large"></asp:TextBox></p>
    <p><strong>Asked by:</strong> <asp:Label ID="lblEditQuestionAsker" runat="server"></asp:Label>*, <strong>on</strong> 
        <asp:Label ID="lblEditQuestionDate" runat="server"></asp:Label>*</p>
        <p>* this information can&#39;t be edited.</p>
        <p>
        <asp:TextBox ID="txtEditQuestionText" runat="server" Height="50px" 
                TextMode="MultiLine" Width="920px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnSaveQuestion" runat="server" onclick="btnSaveQuestion_Click" 
            Text="Save Question" />&nbsp;<asp:Button 
                    ID="btnShowAllQuestions" runat="server" onclick="btnShowAllQuestions_Click" 
                    Text="Show All Questions" />
    </p>
        </div>
    <div id="editAnswer" runat="server">
        </div>
    <br />
</asp:Content>
