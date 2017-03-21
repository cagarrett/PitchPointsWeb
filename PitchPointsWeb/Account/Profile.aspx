<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="PitchPointsWeb.Account.Profile" Async="true" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <asp:Image ID="Image1" runat="server" />
    <h3></h3>
    <p>Use this area to provide additional information.</p>
</asp:Content>
