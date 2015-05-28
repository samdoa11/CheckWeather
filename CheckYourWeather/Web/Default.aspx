<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>
<%@ Assembly Src="~/Classes/WetterList.cs"  %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
       <p>
            <asp:Button class="btn" ID="btnDownload" runat="server" OnClick="onDownloadFile" Text="Get Data from ZAMG" />
            <asp:Button class="btn" ID="btnAnzeige" runat="server" OnClick="onShowFile" Text="Show Data"  />
            <asp:Label ID="labAusgabe" runat="server"></asp:Label>

           <asp:Label ID="labChangeDat" runat="server"></asp:Label>
           
           <asp:Table CssClass="table" ID="tableZAMG" runat="server"></asp:Table>
        </p>
        
    </div>


</asp:Content>
