<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TeacherSupportSystem._Default" %>

<%@ MasterType  virtualPath="~/Site.master"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
            Home</h2>
    <table style="width:100%" cellpadding="0" cellspacing="0">
        <tr>
        <td colspan="3">

        <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
        </p>
            </td>
        </tr>
        <tr>
            <td style="width:215px" valign="top">
                <h2>
                    Log In:</h2>
                
                    <table style="width:100%">
                        <tr>
                            <td>
                                Username
                            </td>
                            <td style="width: 105px;">
                                <asp:TextBox ID="txtUsername" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Password
                            </td>
                            <td style="width: 105px;">
                                <asp:TextBox ID="txtPassword" runat="server" Width="100px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnLogIn" runat="server" OnClick="btnLogIn_Click" Text="Log In" Width="100px" />
                                &nbsp;<asp:Button ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" Text="Log Out"
                                    Width="100px" />
                            </td>
                        </tr>
                    </table>
                
            </td>
            <td style="width: 10px;"></td>
            <td valign="top">
                <h2>
                    Welcome To Learn-On-Line!</h2>
                <p>
                    Learn-On-Line (LOL) is a company that provides Maths homework support, tutoring and testing services for children between 5 and 16 years of age all over the world.</p>
                <p>
                    You will be able to view lessons that teachers have created as well as ask questions for the teacher to reply to. You are also able to reply to questions by other children so that you can help each other out with your work.</p>
                <p>
                    To start, log in with the box on the left.</p>
            </td>
        </tr>
    </table>
</asp:Content>
