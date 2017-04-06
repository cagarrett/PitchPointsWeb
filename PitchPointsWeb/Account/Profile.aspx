<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="PitchPointsWeb.Account.Profile" Async="true" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Title %></h1>
    <div class="row">
        <div class="col s6">
            
            <div class="row">
                <div class="col s12 m7">
                    <div class="card">
                        <div class="card-content">
                            <h2>Info</h2>
                            <p></p>
                            <asp:Label ID="Label1" runat="server" Text="Email :  ">  </asp:Label><asp:Label ID="EmailLabel" runat="server" Text=""></asp:Label>
                            <p></p>
                            <asp:Label ID="Label2" runat="server" Text="First :  "></asp:Label><asp:Label ID="FirstLabel" runat="server" Text="Label"></asp:Label>
                            <p></p>
                            <asp:Label ID="Label3" runat="server" Text="Last :  "></asp:Label><asp:Label ID="LastLabel" runat="server" Text="Label"></asp:Label>
                            <p></p>
                            <asp:Label ID="Label4" runat="server" Text="Lifetime Points : "></asp:Label><asp:Label ID="LifeTimePointsLabel" runat="server" Text="Label"></asp:Label>
                            <p></p>
                            <asp:Label ID="Label5" runat="server" Text="Falls :  "></asp:Label><asp:Label ID="FallsLabel" runat="server" Text="Label"></asp:Label>
                            <p></p>
                            <asp:Label ID="Label6" runat="server" Text="Participated Comps : "></asp:Label><asp:Label ID="ParticipatedCompsLabel" runat="server" Text="Label"></asp:Label>
                            
                        </div>
                    </div>
                </div>
            </div>
            <%----%>
        </div>
        <div class="col s6">

            <h1>Upcoming Competitions</h1>
            <asp:GridView ID="CompetitionsGridView" CssClass="bordered centered responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="CompDataSource" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
                <Columns>
                    <asp:BoundField DataField="CompTitle"
                        HeaderText="Competition Title"
                        InsertVisible="False" ReadOnly="True"
                        SortExpression="CompTitle" />
                    <asp:BoundField DataField="CompDetails"
                        HeaderText="Competition Details"
                        SortExpression="CompDetails" />
                    <asp:BoundField DataField="Description"
                        HeaderText="Description"
                        SortExpression="Description" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="CompDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetActiveCompetitions" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="9" Name="email" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>

</asp:Content>
