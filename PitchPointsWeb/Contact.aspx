<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PitchPointsWeb.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About Us</h2>

    <p>Pitch Points is currently an undergraduate client project that gave our team an opportunity to solve a problem while working with the activates we love and frequent. Being active climbers, we always noticed and were agonized by the flaws of the common competition scoring methods. From there the project was born under the wing of Hoosier Heights with the goal to use modern tech to revamp both the competition and the scoring process.</p>
    <br />
    <p>We aim to pursue and improve this project after our mandated and grade determining deadline. We appreciate any comments or criticisms, and for inquiries in Premium Accounts please do not hesitate to contact us. </p>

    <h2>Contact Us</h2>

    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
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
                    CssClass="text-danger" ErrorMessage="The last name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Subject" CssClass="col-md-2 control-label">Subject</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Subject" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Subject"
                    CssClass="text-danger" ErrorMessage="The subject field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Message" CssClass="col-md-2 control-label">Message</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Message" CssClass="form-control" TextMode="MultiLine" Width="280px" Height="50px" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Message"
                    CssClass="text-danger" ErrorMessage="The message field is required." />
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
