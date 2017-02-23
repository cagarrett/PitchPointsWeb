<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Leaderboard</h2>

    <div class="container">
        <div id="leaderboardCarousel" class="carousel slide" data-ride="carousel" data-interval="false">
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" Height="149px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                                <asp:BoundField DataField="CompScoreTotal" HeaderText="CompScoreTotal" SortExpression="CompScoreTotal" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsMainConnectionString %>" SelectCommand="GetCategoryLeaderboard" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="3" Name="cat" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <div class="carousel-caption">
                        <h3>Beginner</h3>
                        <p>
                            Beginner details
                        </p>
                    </div>
                </div>
                <!-- End Item -->
                <div class="item">
                    <div class="panel-body">
                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                           <Columns>
                               <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                               <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                               <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                               <asp:BoundField DataField="CompScoreTotal" HeaderText="CompScoreTotal" SortExpression="CompScoreTotal" />
                           </Columns>
                       </asp:GridView>
                    </div>
                    <div class="carousel-caption">
                        <h3>Intermediate</h3>
                        <p>
                            Intermediate details
                        </p>
                    </div>
                </div>
                <!-- End Item -->
                <div class="item">
                    <!--<img src="Assets/carousel_background.jpg">-->
                    <div class="panel-body">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                           <Columns>
                               <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                               <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                               <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                               <asp:BoundField DataField="CompScoreTotal" HeaderText="CompScoreTotal" SortExpression="CompScoreTotal" />
                           </Columns>
                       </asp:GridView>
                    </div>
                    <div class="carousel-caption">
                        <h3>Advanced</h3>
                        <p>
                            Advanced details
                        </p>
                    </div>
                </div>
                <!-- End Item -->
                <div class="item">
                    <!--<img src="Assets/carousel_background.jpg">-->
                    <div class="panel-body">
                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                           <Columns>
                               <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                               <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                               <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                               <asp:BoundField DataField="CompScoreTotal" HeaderText="CompScoreTotal" SortExpression="CompScoreTotal" />
                           </Columns>
                       </asp:GridView>
                    </div>
                    <div class="carousel-caption">
                        <h3>Open</h3>
                        <p>
                            Open Details
                        </p>
                    </div>
                </div>
                <!-- End Item -->
            </div>
            <!-- End Carousel Inner -->
            <ul class="nav nav-pills nav-justified">
                <li data-target="#leaderboardCarousel" data-slide-to="0" class="active"><a href="#">Beginner</a></li>
                <li data-target="#leaderboardCarousel" data-slide-to="1" class="active"><a href="#">Intermediate</a></li>
                <li data-target="#leaderboardCarousel" data-slide-to="2" class="active"><a href="#">Advanced</a></li>
                <li data-target="#leaderboardCarousel" data-slide-to="3" class="active"><a href="#">Open</a></li>
            </ul>
        </div>
        <!-- End Carousel -->
    </div>
</asp:Content>
