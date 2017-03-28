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
                <div class="col-md-offset-0 col-md-10">
                    <div class="input-field col s12">
                        <input id="user_email" type="email" class="validate" runat="server">
                        <label for="user_email" data-error="Invalid email input" data-success="">Email</label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-0 col-md-10">
                    <div class="input-field col s12">
                        <input id="user_password" type="password" class="validate" runat="server">
                        <label for="user_password" data-error="Invalid password input" data-success="">Password</label>
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
