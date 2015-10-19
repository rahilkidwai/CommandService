<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Command.Service._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Commands API</h1>
        <p class="lead">Commands Api provides commands that provides CRUD operations for various data entities.</p>
        <p><a runat="server" href="~/help" class="btn btn-primary btn-large">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Categories</h2>
            <p>Commands are divided into several categories and each category provide one or more commands.</p>
            <p><a class="btn btn-default" runat="server" href="~/help">Learn more &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Sources</h2>
            <p>Commands are loaded dynamically from one or more sources (DLLs) as per defined configuration.</p>
            <p><a class="btn btn-default" runat="server" href="~/source">Learn more &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Validate</h2>
            <p>Validate the available commands.</p>
            <p><a class="btn btn-default" runat="server" href="~/validate">Learn more &raquo;</a></p>
        </div>
    </div>

</asp:Content>