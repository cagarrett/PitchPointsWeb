<%@ Page Title="Log Route For Climber" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogRouteForClimber.aspx.cs" Inherits="PitchPointsWeb.Admin.LogRouteForClimber" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script>
        function completeForm() {
            swal({
                title: 'Error!',
                text: 'Please make sure you fill out each form on this page.',
                type: 'error'
            });
        }
        function serverError() {
            swal({
                title: 'Error!',
                text: 'There was an issue with logging this climb. Please try again.',
                type: 'error'
            });
        }
        function success() {
            swal({
                title: 'Success!',
                text: 'You have successfully logged this climb!',
                type: 'success'
            });
        }
    </script>
    <h2><%: Title %></h2>
    

    <div class="form-horizontal">
        <br />
        <asp:Label ID="AdminFirstLabel" runat="server" Text=""></asp:Label>
        <asp:Label ID="AdminLastLabel" runat="server" Text=""></asp:Label>
        <hr />

        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="climber_id" type="number" class="validate" runat="server" style="width: 200px">
                    <label for="climber_id" data-error="Must input climber ID" data-success="">Climber ID</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="witness_id" type="number" class="validate" runat="server" style="width: 200px">
                    <label for="witness_id" data-error="Must input witness ID" data-success="">Witness ID</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="route_id" type="number" class="validate" runat="server" style="width: 200px">
                    <label for="route_id" data-error="Must input route ID" data-success="">Route ID</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <input id="falls" type="number" class="validate" runat="server" style="width: 200px">
                    <label for="falls" data-error="Must input number of falls" data-success="">Falls</label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <asp:Button runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>


    <div>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                An error has occurred.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
