<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PitchPointsWeb.Contact" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function completeForm() {
            swal({
                title: 'Error!',
                text: 'Please make sure you fill out each form on this page.',
                type: 'error'
            });
        }
        function success() {
            swal({
                title: 'Success!',
                text: 'Your message has been sent to the Pitch Points team!',
                type: 'success'
            });
        }
    </script>
    <br />
    <h3>About Us</h3>
    <hr />
    <p class="flow-text">Pitch Points is currently an undergraduate client project that gave our team an opportunity to solve a problem while working with the activities we love and frequent. Being active climbers, we always noticed and were agonized by the flaws of the common competition scoring methods. From there the project was born under the wing of Hoosier Heights with the goal to use modern tech to revamp both the competition and the scoring process.</p>
    <br />
    <p class="flow-text">We aim to pursue and improve this project after our mandated and grade determining deadline. We appreciate any comments or criticisms, and for inquiries in Premium Accounts please do not hesitate to contact us. </p>
    <br />
    <h3>Contact Us</h3>
    <hr />
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="first_name" type="text" class="validate" runat="server">
                    <label for="first_name" data-error="Must input first name" data-success="">First Name</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="last_name" type="text" class="validate" runat="server">
                    <label for="last_name" data-error="Must input last name" data-success="">Last Name</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="message_subject" type="text" class="validate" runat="server">
                    <label for="message_subject" data-error="Must input subject line" data-success="">Subject</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <textarea id="message_content" class="materialize-textarea" style="width:280px" runat="server"></textarea>
                    <label for="message_content" data-error="Must input message" data-success="">Message</label>
                    <br />
                    <asp:Button runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
