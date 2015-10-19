<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Command.Service.Help" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Help</h1>
        <p>Commands Api provides commands that provides CRUD operations for various data entities.</p>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>Categories</h2>
            <p class="text-primary"><asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCount" /></p>
            <p>Commands are divided into several categories and each category provide one or more commands. Following are various command categories available. Click on the specific category to see details.</p>
        </div>       
    </div>
    <div class="container">
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCategories" /> 
    </div>
</asp:Content>