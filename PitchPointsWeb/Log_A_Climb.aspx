<%@ Page Title="Log_A_Climb" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log_A_Climb.aspx.cs" Inherits="PitchPointsWeb.Log_A_Climb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log A Climb</h2>

    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClimberID" CssClass="col-md-2 control-label">Climber ID</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ClimberID" CssClass="form-control" TextMode="Number" Width="75 px" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClimberID"
                    CssClass="text-danger" ErrorMessage="The climber ID field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Witness" CssClass="col-md-2 control-label">Witness</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Witness" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Witness"
                    CssClass="text-danger" ErrorMessage="The witness field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="RouteClimbed" CssClass="col-md-2 control-label">Route ID</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="RouteClimbed" CssClass="form-control" Style="width: 57px;" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="RouteClimbed"
                    CssClass="text-danger" ErrorMessage="The route climbed is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="numberOfFalls" CssClass="col-md-2 control-label">Falls</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="numberOfFalls" CssClass="form-control" Style="width: 57px;" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="numberOfFalls"
                    CssClass="text-danger" ErrorMessage="The number of falls is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
