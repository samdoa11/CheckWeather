<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>
<%@ Assembly Src="~/Classes/WetterList.cs"  %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <p>
            <asp:Button class="btn" ID="Button1" runat="server" OnClick="Button1_Click" Text="Get Data from ZAMG" />
            <asp:Label ID="labAusgabe" runat="server"></asp:Label>
            <asp:Label ID="labChangeDate" runat="server"></asp:Label>

            <asp:Table CssClass="table" ID="tableZAMG" runat="server"></asp:Table>
        </p>

    </div>


</asp:Content>
