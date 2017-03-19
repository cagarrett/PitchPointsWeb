<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Leaderboard</h2>
    <hr />
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
                        <table class="table-striped table-bordered table-list">
                            <thead>
                                <tr>
                                    <th>Place</th>
                                    <th>Name</th>
                                    <th>Score</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>John Doe</td>
                                    <td>1000</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                            </tbody>
                        </table>
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
                        <table class="table-striped table-bordered table-list">
                            <thead>
                                <tr>
                                    <th>Place</th>
                                    <th>Name</th>
                                    <th>Score</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>John Doe</td>
                                    <td>1000</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                            </tbody>
                        </table>
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
                        <table class="table-striped table-bordered table-list">
                            <thead>
                                <tr>
                                    <th>Place</th>
                                    <th>Name</th>
                                    <th>Score</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>John Doe</td>
                                    <td>1000</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                            </tbody>
                        </table>
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
                        <table class="table-striped table-bordered table-list">
                            <thead>
                                <tr>
                                    <th>Place</th>
                                    <th>Name</th>
                                    <th>Score</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>John Doe</td>
                                    <td>1000</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>John Smith</td>
                                    <td>900</td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
