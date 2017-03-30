<%@ Page Title="CompInfo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompInfo.aspx.cs" Inherits="PitchPointsWeb.CompInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Upcoming Competitions</h2>

    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="row">
            <div class="col s6">
                <h1>General</h1>
                <asp:GridView ID="CompetitionsGridView" CssClass="bordered responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="CompDataSource" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField DataField="CompTitle"
                            HeaderText="Comp Title"
                            InsertVisible="False" ReadOnly="True"
                            SortExpression="CompTitle" />
                        <asp:BoundField DataField="CompDetails"
                            HeaderText="Comp Details"
                            SortExpression="CompDetails" />
                        <asp:BoundField DataField="Description"
                            HeaderText="Description"
                            SortExpression="Description" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="CompDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetActiveCompetitions" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue=" " Name="email" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>


            <div class="col s6">
                <h1>Location</h1>
                <asp:GridView ID="RulesView" CssClass="bordered responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="CompDataSource" CellPadding="2" ForeColor="#333333" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField DataField="CompTitle"
                            HeaderText="Comp Title"
                            InsertVisible="False" ReadOnly="True"
                            SortExpression="CompTitle" />
                        <asp:BoundField DataField="CompDetails"
                            HeaderText="Comp Details"
                            SortExpression="CompDetails" />
                        <asp:BoundField DataField="Description"
                            HeaderText="Description"
                            SortExpression="Description" />
                    </Columns>
                    <RowStyle Width="50px" />
                </asp:GridView>
            </div>
        </div>
        <h1>Competition Rules</h1>
        <asp:GridView ID="GridView3" CssClass="bordered responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="CompDataSource" CellPadding="2" ForeColor="#333333" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="CompTitle"
                    HeaderText="Comp Title"
                    InsertVisible="False" ReadOnly="True"
                    SortExpression="CompTitle" />
                <asp:BoundField DataField="CompDetails"
                    HeaderText="Comp Details"
                    SortExpression="CompDetails" />
                <asp:BoundField DataField="Description"
                    HeaderText="Description"
                    SortExpression="Description" />
            </Columns>
            <RowStyle Width="50px" />
        </asp:GridView>
        <br />
        <asp:Button runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" />
        
        <div class="form-group">
            <div class="col-md-10">
            </div>
        </div>
        <div class="col-md-offset-2 col-md-10">
        </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
