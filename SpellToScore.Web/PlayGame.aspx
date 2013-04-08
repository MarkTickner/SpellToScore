<%@ Page Title="Play Game" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PlayGame.aspx.cs" Inherits="SpellToScore.Web.PlayGame" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Play Game
    </h2>
    <p>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </p>
    <p>
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
            width="800" height="540">
            <param name="source" value="ClientBin/SpellToScore.xap" />
            <param name="onError" value="onSilverlightError" />
            <param name="background" value="white" />
            <param name="minRuntimeVersion" value="4.0.50401.0" />
            <param name="autoUpgrade" value="true" />
            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50401.0" style="text-decoration: none">
                <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight"
                    style="border-style: none" />
            </a>
        </object>
    </p>
</asp:Content>