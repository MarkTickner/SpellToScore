<%@ Page Title="Your Scores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="YourScores.aspx.cs" Inherits="SpellToScore.Web.YourScores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Your Scores
    </h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Table ID="userScoresTbl" runat="server" GridLines="Both" Width="920px" CellPadding="2"
            CellSpacing="1" BorderColor="#465767" Font-Bold="False">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF"
                    Font-Bold="True" Width="25%">Position</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF"
                    Font-Bold="True" Width="25%">Name</asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF"
                    Font-Bold="True" Width="25%">Score</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF"
                    Font-Bold="True" Width="25%">Date and Time</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server" BackColor="White">
            </asp:TableRow>
        </asp:Table>
    </p>
</asp:Content>