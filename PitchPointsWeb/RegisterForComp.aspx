<%@ Page Title="RegisterForComp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterForComp.aspx.cs" Inherits="PitchPointsWeb.RegisterForComp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Upcoming Competitions</h2>

    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
       <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Subject" CssClass="col-md-2 control-label">Category</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Subject" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Subject"
                    CssClass="text-danger" ErrorMessage="The subject field is required." />
            </div>
        </div>
        <div class="col-md-offset-2 col-md-10">
            <asp:Button runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-default" />
        </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </div>


</asp:Content>
