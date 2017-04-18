<%@ Page Title="Log_A_Climb" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log_A_Climb.aspx.cs" Inherits="PitchPointsWeb.Log_A_Climb" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="form-horizontal">
        <br />
        <h4>Log a Climb</h4>
        <hr />

        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <div class="input-field col s12">
                    <asp:SqlDataSource
                        ID="getActiveCompetitions"
                        runat="server"
                        ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>"
                        SelectCommand="GetActiveCompetitions"
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="competitionName" DataTextField="CompetitionTitle" DataSourceID="getActiveCompetitions" runat="server" Width="100 px" />
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

</asp:Content>
