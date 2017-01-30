<%@ Page Title="Log_A_Climb" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log_A_Climb.aspx.cs" Inherits="PitchPointsWeb.Log_A_Climb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log A Climb</h2>

    <div class="form-horizontal">
            <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Witness" CssClass="col-md-2 control-label">Witness</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Witness" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Witness"
                    CssClass="text-danger" ErrorMessage="The subject field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="RouteClimbed" CssClass="col-md-2 control-label">Route ID</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="RouteClimbed" CssClass="form-control" style="width:50px;" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="RouteClimbed"
                    CssClass="text-danger" ErrorMessage="The route climbed is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FallsList" CssClass="col-md-2 control-label">Falls </asp:Label>
            <div class="col-md-10">
                <asp:RadioButtonList ID="FallsList" runat="server" RepeatDirection="Horizontal" Width="117px">
                    <asp:ListItem>0</asp:ListItem> 
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem> 
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem> 
                    <asp:ListItem>5</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="PlusFiveFalls" CssClass="form-control" style="width:50px; display: inline-block;" runat="server" ></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <br />
                <br />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <br />
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
        </div>
    </div>
    </div>
</asp:Content>
