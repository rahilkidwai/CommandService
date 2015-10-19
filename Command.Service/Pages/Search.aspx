<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Command.Service.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Search</h1>
        <p>Search for commands</p>
    </div>
    
   <div class="container">
       <form runat="server">
           <asp:TextBox EnableViewState="false" runat="server" ID="uxTextBoxSearch"  CssClass="form-control"/>
           <asp:Button EnableViewState="false" CssClass="btn btn-default" runat="server" ID="uxButtonSearch" OnClick="uxButtonSearch_Click"  Text="Search"/>
       </form>
    </div>
    <div class="container">
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralResult" /> 
    </div>
</asp:Content>