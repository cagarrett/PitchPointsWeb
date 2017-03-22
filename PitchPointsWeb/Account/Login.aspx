<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PitchPointsWeb.Account.Login" Async="true" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function incorrectPassword() {
            swal({
                title: 'Incorrect Password!',
                text: 'The password you entered does not match the username provided.',
                type: 'error'
            });
        }
        function authError() {
            swal({
                title: 'Authentication Error!',
                text: 'We are having trouble authenticating your credentials. Please try again.',
                type: 'error'
            });
        }
        function serverError() {
            swal({
                title: 'Server Error!',
                text: 'We are having trouble with our server. Please try again.',
                type: 'error'
            });
        }
        function accountDoesntExist() {
            swal({
                title: 'No account registered!',
                text: 'This email address has not been registered with us.',
                type: 'error'
            });
        }
    </script>
    <h2><%: Title %></h2>

    <section id="loginForm">
        <div class="form-horizontal">
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
            </asp:PlaceHolder>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" ID="RememberMe" />
                        <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
        </p>
    </section>
</asp:Content>
