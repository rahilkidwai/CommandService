<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelpCategory.aspx.cs" Inherits="Command.Service.HelpCategory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h2><asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCategory" /></h2>
        Following are the commands available in this category
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCommands" />            
    </div>
</asp:Content>