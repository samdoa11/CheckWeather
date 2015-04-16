<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>AS.NETASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</h1>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <asp:Label ID="labAusgabe" runat="server"></asp:Label>
        </p>
    </div>

    <div class="row">
        
        
    </div>


</asp:Content>
