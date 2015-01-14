<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="SAMLdecoder.Status" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        Status: <asp:Label runat="server" ID="AuthStatus"></asp:Label>
    </div>
    <div>
        Name: <asp:Label runat="server" ID="Name"></asp:Label>
    </div>
    <div>
        <asp:Button runat="server" ID="startSSO" OnClick="RunSso"/>
    </div>
</asp:Content>
