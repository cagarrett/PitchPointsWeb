<%@ Page Title="Create Competition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCompetition.aspx.cs" Inherits="PitchPointsWeb.Admin.CreateCompetition" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
    <h3>Create Competition</h3>
    <hr />
    <div class="row">
        <div class="col s6">
            <div class="input-field">
                <input id="tbxTitle" type="text" class="validate" runat="server">
                <label for="tbxTitle" data-success="">Competition Title</label>
            </div>
            <div class="input-field">
                <label for="tbxDate" data-success="">Date of Competition</label>
                <input id="tbxDate" type="text" class="datepicker" runat="server">
            </div>
            <div class="row">
                <div class="col s3">
                    <div class="input-field">
                        <label for="tbxStartTime" data-success="">Start Time</label>
                        <input id="tbxStartTime" class="timepicker" type="time">
                    </div>
                </div>
                <div class="col s3">
                    <div class="input-field">
                        <label for="tbxEndTime" data-success="">End Time</label>
                        <input id="tbxEndTime" class="timepicker" type="time">
                    </div>
                </div>
            </div>
        </div>
        <div class="col s6">
            <div class="input-field">
                <input id="tbxAddress1" type="text" class="validate" runat="server">
                <label for="tbxAddress1" data-success="">Address Line 1</label>
            </div>
            <div class="input-field">
                <input id="tbxAddress2" type="text" class="validate" runat="server">
                <label for="tbxAddress2" data-success="">Address Line 2</label>
            </div>
            <div class="row">
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxCity" type="text" class="validate" runat="server">
                        <label for="tbxCity" data-success="">City</label>
                    </div>
                </div>
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxState" type="text" class="validate" runat="server">
                        <label for="tbxState" data-success="">State</label>
                    </div>
                </div>
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxZip" type="text" class="validate" runat="server">
                        <label for="tbxZip" data-success="">Zip</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.timepicker').pickatime({
            default: 'now',
            twelvehour: false, // change to 12 hour AM/PM clock from 24 hour
            donetext: 'OK',
            autoclose: false,
            vibrate: true // vibrate the device when dragging clock hand
        });
    </script>
    <asp:Table ID="ruleTable" runat="server">
    </asp:Table>
</asp:Content>
