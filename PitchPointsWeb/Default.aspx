<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

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
        <h2>Upcoming Competitions</h2>
        <asp:GridView ID="CompetitionsGridView" CssClass="bordered responsive-table" runat="server" AutoGenerateColumns="False" DataSourceID="CompDataSource" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
                <Columns>
                    <asp:BoundField DataField="CompTitle"
                        HeaderText="Comp Title"
                        InsertVisible="False" ReadOnly="True"
                        SortExpression="CompTitle" />
                    <asp:BoundField DataField="LocationID"
                        HeaderText="LocationID"
                        SortExpression="LocationID" />
                    <asp:BoundField DataField="CompDetails"
                        HeaderText="Comp Details"
                        SortExpression="CompDetails" />
                    <asp:BoundField DataField="StartDate"
                        HeaderText="StartDate"
                        SortExpression="StartDate" />
                    <asp:BoundField DataField="EndDate"
                        HeaderText="EndDate"
                        SortExpression="EndDate" />
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
</asp:Content>
