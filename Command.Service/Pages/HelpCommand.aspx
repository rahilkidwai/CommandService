<%@ Page Title="Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelpCommand.aspx.cs" Inherits="Command.Service.HelpCommand" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h2><asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralCommand" /></h2>
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralDescription" /><br />
        <span class="text-muted">Type:&nbsp;<asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralType" /></span><br />
        <span class="text-muted">Assembly:&nbsp;<asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralAssembly" /></span>
        <h3>Parameters</h3>
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralParameters" />
        <h3>Returns</h3>
        <asp:Literal Mode="PassThrough" EnableViewState="false" runat="server" ID="uxLiteralReturn" />
    </div>
</asp:Content>