<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PitchPointsWeb.Account.Register" Async="true" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });
    </script>
    <br />
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />

        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="first_name" type="text" class="validate" runat="server" />
                    <label for="first_name" data-error="Must input first name" data-success="">First Name</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="last_name" type="text" class="validate" runat="server" />
                    <label for="last_name" data-error="Must input last name" data-success="">Last Name</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <input type="date" class="datepicker" runat="server" id="date_of_birth" style="width: 290px" />
                <label for="date_of_birth" data-error="This date is not valid" data-success="right">Date of Birth</label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="email" type="email" class="validate" runat="server" />
                    <label for="email" data-error="Invalid email input" data-success="">Email</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="password" type="password" class="validate" runat="server" />
                    <label for="password" data-error="Invalid password input" data-success="">Password</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="confirm_password" type="password" class="validate" runat="server" />
                    <label for="confirm_password" data-error="Invalid password input" data-success="">Confirm Password</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
