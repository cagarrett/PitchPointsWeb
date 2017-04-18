<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" EnableEventValidation="False" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Home Page</h2>
    <div class="container">
        <div class="carousel">
            <script>
                $(document).ready(function () {
                    $('.carousel').carousel();
                });
            </script>
            <a class="carousel-item" href="#one!">
                <img src="Assets/HHWall.PNG" alt="Bat hang"></a>
            <a class="carousel-item" href="#two!">
                <img src="Assets/Climb.png" alt="Climb"></a>
            <a class="carousel-item" href="#three!">
                <img src="Assets/HHILogoWall.PNG" alt="Competition"></a>
            <a class="carousel-item" href="#four!">
                <img src="Assets/NuLuLogo.PNG" alt="Boulder"></a>
            <a class="carousel-item" href="#five!">
                <img src="Assets/Snake_Pit.png" alt="SnakePit"></a>
            <a class="carousel-item" href="#five!">
                <img src="Assets/HHComp.PNG" alt="SnakePit"></a>
        </div>
        <h1>Upcoming Competitions</h1>
        <asp:GridView ID="CompetitionsGridView" class="centered" CssClass="bordered centered highlight responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="UpCompDataSource" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="CompTitle"
                    HeaderText="  Competition Title"
                    InsertVisible="False" ReadOnly="True"
                    SortExpression="CompTitle" />
                <asp:BoundField DataField="CompDetails"
                    HeaderText="   Competition Details"
                    SortExpression="CompDetails" />
                <asp:BoundField DataField="Description"
                    HeaderText="   Description"
                    SortExpression="Description" />
                <asp:BoundField DataField="Date"
                    HeaderText="   Date"
                    SortExpression="Date" />
                <asp:BoundField DataField="City"
                    HeaderText="  City"
                    SortExpression="City" />
                <asp:BoundField DataField="State"
                    HeaderText="  State"
                    SortExpression="State" />
                <asp:TemplateField HeaderText="Logged Ticket">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnview" runat="server" href='<%# String.Format("/CompInfo.aspx?ID={0}", Eval("Id")) %>'  Text="Logged Ticket" CommandName="More Info">More Info
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="UpCompDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsDB %>" SelectCommand="GetActiveCompetitions" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue=" " Name="email" Type="String" />
                <asp:Parameter DefaultValue="False" Name="onlyReturnRegistered" Type="Boolean" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
