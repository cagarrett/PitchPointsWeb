<%@ Page Title="Edit Competition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCompetition.aspx.cs" Inherits="PitchPointsWeb.Admin.EditCompetition" Async="true" %>

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
