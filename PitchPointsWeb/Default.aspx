<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Leaderboard</h2>

    <div class="container">
        <div id="leaderboardCarousel" class="carousel slide" data-ride="carousel" data-interval="false">
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                                <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                                <asp:BoundField DataField="CompetitionID" HeaderText="CompetitionID" SortExpression="CompetitionID" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PitchPointsMainConnectionString %>" SelectCommand="SELECT * FROM [Climber]"></asp:SqlDataSource>
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
                       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                           <Columns>
                               <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                               <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                               <asp:BoundField DataField="CompetitionID" HeaderText="CompetitionID" SortExpression="CompetitionID" />
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
                        <asp:GridView ID="GridView3" runat="server">
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
                        <asp:GridView ID="GridView4" runat="server">
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
