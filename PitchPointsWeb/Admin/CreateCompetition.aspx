<%@ Page Title="Create Competition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCompetition.aspx.cs" Inherits="PitchPointsWeb.Admin.CreateCompetition" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
    <h3>Create Competition</h3>
    <hr />
    <div class="row">
        <div class="col s6">
            <div class="input-field">
                <input id="tbxTitle" type="text" class="validate" runat="server" />
                <label for="tbxTitle" data-success="">Competition Title</label>
            </div>
            <div class="input-field">
                <label for="tbxDate" data-success="">Date of Competition</label>
                <input id="tbxDate" type="date" class="datepicker" runat="server" />
            </div>
            <div class="row">
                <div class="col s3">
                    <div class="input-field">
                        <label for="tbxStartTime" data-success="">Start Time</label>
                        <input id="tbxStartTime" class="timepicker" type="time" />
                    </div>
                </div>
                <div class="col s3">
                    <div class="input-field">
                        <label for="tbxEndTime" data-success="">End Time</label>
                        <input id="tbxEndTime" class="timepicker" type="time" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col s6">
            <div class="input-field">
                <input id="tbxAddress1" type="text" class="validate" runat="server" />
                <label for="tbxAddress1" data-success="">Address Line 1</label>
            </div>
            <div class="input-field">
                <input id="tbxAddress2" type="text" class="validate" runat="server" />
                <label for="tbxAddress2" data-success="">Address Line 2</label>
            </div>
            <div class="row">
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxCity" type="text" class="validate" runat="server" />
                        <label for="tbxCity" data-success="">City</label>
                    </div>
                </div>
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxState" type="text" class="validate" runat="server" />
                        <label for="tbxState" data-success="">State</label>
                    </div>
                </div>
                <div class="col s2">
                    <div class="input-field">
                        <input id="tbxZip" type="text" class="validate" runat="server" />
                        <label for="tbxZip" data-success="">Zip</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col s6">
            <h5>Rules</h5>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="ruleGridView" ShowFooter="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text="Delete" OnClick="deleteRuleButton_OnClick" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton runat="server" Text="Add" OnClick="addRuleButton_OnClick" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" placeholder="Rule description" ID="ruleDescriptionTextBox" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" placeholder="Rule description" ID="ruleTextBox" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col s6">
            <h5>Routes</h5>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="routeGridView" ShowFooter="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" Text="Delete" OnClick="deleteRouteButton_OnClick" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton runat="server" Text="Add" OnClick="addRouteButton_OnClick" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="routeIDInput" Enabled="False" Text="" Width="20 px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" Text="1" ID="routeID" Enabled="False" Width="20 px" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:DropDownList ID="categoryInput" runat="server" CssClass="browser-default" Width="100 px">
                                        <asp:ListItem Text="Beginner" Value="0" />
                                        <asp:ListItem Text="Intermediate" Value="1" />
                                        <asp:ListItem Text="Advanced" Value="2" />
                                        <asp:ListItem Text="Open" Value="3" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="category" runat="server" CssClass="browser-default" Width="100 px">
                                        <asp:ListItem Text="Beginner" Value="0" />
                                        <asp:ListItem Text="Intermediate" Value="1" />
                                        <asp:ListItem Text="Advanced" Value="2" />
                                        <asp:ListItem Text="Open" Value="3" />
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade">
                                <ItemTemplate>
                                    <asp:DropDownList ID="gradeInput" runat="server" CssClass="browser-default" Width="100 px">
                                        <asp:ListItem Text="V0" Value="100" />
                                        <asp:ListItem Text="V1" Value="120" />
                                        <asp:ListItem Text="V2" Value="140" />
                                        <asp:ListItem Text="V3" Value="160" />
                                        <asp:ListItem Text="V4" Value="180" />
                                        <asp:ListItem Text="V5" Value="200" />
                                        <asp:ListItem Text="V6" Value="220" />
                                        <asp:ListItem Text="V7" Value="240" />
                                        <asp:ListItem Text="V8" Value="260" />
                                        <asp:ListItem Text="V9" Value="280" />
                                        <asp:ListItem Text="V10" Value="300" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="gradeInput" runat="server" CssClass="browser-default" Width="100 px">
                                        <asp:ListItem Text="V0" Value="100" />
                                        <asp:ListItem Text="V1" Value="120" />
                                        <asp:ListItem Text="V2" Value="140" />
                                        <asp:ListItem Text="V3" Value="160" />
                                        <asp:ListItem Text="V4" Value="180" />
                                        <asp:ListItem Text="V5" Value="200" />
                                        <asp:ListItem Text="V6" Value="220" />
                                        <asp:ListItem Text="V7" Value="240" />
                                        <asp:ListItem Text="V8" Value="260" />
                                        <asp:ListItem Text="V9" Value="280" />
                                        <asp:ListItem Text="V10" Value="300" />
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Points">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ClientIDMode="static" ID="routePointsValue" Enabled="False" Text="" Width="50 px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox runat="server" Text="" ClientIDMode="static" ID="routePointsValue" Enabled="False" Width="50 px" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:Button runat="server" OnClick="btnCreate_Click" Text="Create" CssClass="btn btn-primary" />
    </div>
    <script>
        $(document).ready(function () {
            $('select').material_select();
            CreateCompStartup();
        });
        function CreateCompStartup() {
            $('.browser-default').change(function () {
                console.log(this.value);
                var parent = $(this).closest("td").next();
                var input = parent.find("input");
                input.val(this.value);
                return false;
            });
        }
        $('.timepicker').pickatime({
            default: 'now',
            twelvehour: false, // change to 12 hour AM/PM clock from 24 hour
            donetext: 'OK',
            autoclose: false,
            vibrate: true // vibrate the device when dragging clock hand
        });
        $('.datepicker').pickadate({
            selectMonths: true, // Creates a dropdown to control month
            selectYears: 15 // Creates a dropdown of 15 years to control year
        });
        function gradeChanged(gradeDropdown) {
            var pointCell = gradeDropdown.parentNode
            alert(JSON.stringify(pointCell));
            var pointBox = pointCell.find('routePointsValue');
            //var pointBox = pointCell.find("td.routePointsValue");
            //pointBox.value = gradeDropdown.value;
            //alert(JSON.stringify(pointBox));
            return false;
        }
    </script>
</asp:Content>
