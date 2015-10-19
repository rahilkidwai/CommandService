<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Validate.aspx.cs" Inherits="Command.Service.Validate" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Validate</h1>
        <p class="text-primary">Validate the commands catalog. The commands listed (if any) are not available for execution.</p>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2><asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralHeader" /></h2>
        </div>       
    </div>
    <div class="container">
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralErrors" /> 
    </div>
</asp:Content>