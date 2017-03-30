<%@ Page Title="Log Route For Climber" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogRouteForClimber.aspx.cs" Inherits="PitchPointsWeb.Admin.LogRouteForClimber" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>

    <div>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                An error has occurred.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
