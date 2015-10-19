<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelpCategory.aspx.cs" Inherits="Command.Service.HelpCategory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div>
            <h2><asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCategory" /></h2>
            Following are the commands available in this category
            <h3>Commands [Category: <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLIteralCategoryName" />]</h3>
            <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCommands" />            
        </div>
    </div>
</asp:Content>