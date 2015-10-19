<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Source.aspx.cs" Inherits="Command.Service.Source" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="jumbotron">
        <h2>Source(s)</h2>
        <p class="text-primary">Commands are loaded dynamically from one or more sources (DLLs). The sources are listed in the configuration and on startup the application loads all the sources and traverses them for the available commands.</p>
    </div>
    <div class="row">
        <div class="col-md-12">
            <p class="lead">Following are the DLLs currently loaded:</p>
        </div>       
    </div>
    <div class="container">
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralSources" /> 
    </div>
</asp:Content>