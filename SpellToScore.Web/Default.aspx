<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SpellToScore.Web.Default" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2 {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Welcome To Learn-On-Line!</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 215px" valign="top">
                <h2>Log In:</h2>
                <table style="width: 100%">
                    <tr>
                        <td>Username
                        </td>
                        <td style="width: 105px;">
                            <asp:TextBox ID="txtUsername" runat="server" Width="100px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Password
                        </td>
                        <td style="width: 105px;">
                            <asp:TextBox ID="txtPassword" runat="server" Width="100px" TextMode="Password">
                            </asp:TextBox>
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
                <h2>Welcome!</h2>
                <p>
                    Learn-On-Line (LOL) is a company that provides Maths homework support, tutoring
                    and testing services for children between 5 and 16 years of age all over the world.
                </p>
                <p>
                    You are able to view lessons that teachers have created as well as ask questions for the teacher to reply to. You are able to reply to questions by other children so that you can help each other out with your work.
                    We now have educational games!
                </p>
                <p>
                    To start, log in with the box on the left.
                </p>
                <h3>Test Users</h3>
                <table>
                    <tr>
                        <td class="style2">
                            <b>Name</b></td>
                        <td class="style2">
                            <b>Username</b></td>
                        <td class="style2">
                            <b>Password</b></td>
                    </tr>
                    <tr>
                        <td class="style2">Robert Jones</td>
                        <td class="style2">a</td>
                        <td class="style2">1</td>
                    </tr>
                    <tr>
                        <td class="style2">Sammy Woods</td>
                        <td class="style2">b</td>
                        <td class="style2">1</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
