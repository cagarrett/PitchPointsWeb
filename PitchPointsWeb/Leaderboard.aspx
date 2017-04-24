<%@ Page Title="Leaderboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Leaderboard</h2>
    <p>Select Competition</p>
    <asp:DropDownList ID="DropDownList1" DataSourceID="SqlDataSource5"  DataTextField="CompTitle" DataValueField="id" CssClass="browser-default" runat="server"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetCompetitions" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>


    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#beginner">Beginner</a></li>
            <li><a data-toggle="tab" href="#intermediate">Intermediate</a></li>
            <li><a data-toggle="tab" href="#advanced">Advanced</a></li>
            <li><a data-toggle="tab" href="#open">Open</a></li>
        </ul>

        <div class="tab-content">
            <div id="beginner" class="tab-pane fade in active">
                <h3>Beginner</h3>
                <asp:Timer runat="server" ID="BeginnerUpdateTimer" Interval="5000" OnTick="BeginnerUpdateTimer_Tick" />
                <asp:UpdatePanel runat="server" ID="BeginnerTimedPanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BeginnerUpdateTimer" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label runat="server" ID="BeginnerDateStampLabel" />
                        <asp:GridView ID="GridView1" class="bordered responsive-table centered highlight" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="User ID " SortExpression="UserID" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name " SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name " SortExpression="LastName" />
                                <asp:BoundField DataField="Points" HeaderText="Points " ReadOnly="True" SortExpression="Points" />
                                <asp:BoundField DataField="Falls" HeaderText="Falls " ReadOnly="True" SortExpression="Falls" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetCategoryLeaderboard" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="1" Name="catId" Type="Int32" />
                                <asp:Parameter DefaultValue="7" Name="compId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div id="intermediate" class="tab-pane fade">
                <h3>Intermediate</h3>

                <asp:Timer runat="server" ID="IntermediateUpdateTimer" Interval="5000" OnTick="IntermediateUpdateTimer_Tick" />
                <asp:UpdatePanel runat="server" ID="IntermediateUpdatePanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="IntermediateUpdateTimer" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label runat="server" ID="IntermediateDateStampLabel" />
                        <asp:GridView ID="GridView2" class="bordered responsive-table centered highlight" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="User ID " SortExpression="UserID" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name " SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name " SortExpression="LastName" />
                                <asp:BoundField DataField="Points" HeaderText="Points " ReadOnly="True" SortExpression="Points" />
                                <asp:BoundField DataField="Falls" HeaderText="Falls " ReadOnly="True" SortExpression="Falls" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetCategoryLeaderboard" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="2" Name="catId" Type="Int32" />
                                <asp:Parameter DefaultValue="7" Name="compId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="advanced" class="tab-pane fade">
                <h3>Advanced</h3>
                <asp:Timer runat="server" ID="AdvancedUpdateTimer" Interval="5000" OnTick="AdvancedUpdateTimer_Tick" />
                <asp:UpdatePanel runat="server" ID="AdvancedUpdatePanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="AdvancedUpdateTimer" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label runat="server" ID="AdvancedDateStampLabel" />
                        <asp:GridView ID="GridView3" class="bordered responsive-table centered highlight" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="User ID " SortExpression="UserID" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name " SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name " SortExpression="LastName" />
                                <asp:BoundField DataField="Points" HeaderText="Points " ReadOnly="True" SortExpression="Points" />
                                <asp:BoundField DataField="Falls" HeaderText="Falls " ReadOnly="True" SortExpression="Falls" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetCategoryLeaderboard" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="3" Name="catId" Type="Int32" />
                                <asp:Parameter DefaultValue="7" Name="compId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="open" class="tab-pane fade">
                <h3>Open</h3>
                <asp:Timer runat="server" ID="OpenUpdateTimer" Interval="5000" OnTick="OpenUpdateTimer_Tick" />
                <asp:UpdatePanel runat="server" ID="OpenUpdatePanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="OpenUpdateTimer" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label runat="server" ID="OpenDateStampLabel" />
                        <asp:GridView ID="GridView4" class="bordered responsive-table centered highlight" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="User ID " SortExpression="UserID" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name " SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name " SortExpression="LastName" />
                                <asp:BoundField DataField="Points" HeaderText="Points " ReadOnly="True" SortExpression="Points" />
                                <asp:BoundField DataField="Falls" HeaderText="Falls " ReadOnly="True" SortExpression="Falls" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetCategoryLeaderboard" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="4" Name="catId" Type="Int32" />
                                <asp:Parameter DefaultValue="7" Name="compId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
