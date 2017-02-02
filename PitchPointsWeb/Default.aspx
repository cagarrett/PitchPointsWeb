<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Leaderboard</h2>

    <div class="container">
        <div id="leaderboardCarousel" class="carousel slide" data-ride="carousel" data-interval="false">
            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item active">
                    <div class="panel-body">
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
                    </div>
                    <!-- End Item 
                <div class="panel-footer">
                    <div class="row">
                        <div class="col col-xs-4">
                            Page 1 of 5
                        </div>
                        <div class="col col-xs-8">
                            <ul class="pagination hidden-xs pull-right">
                                <li><a href="#">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                            </ul>
                            <ul class="pagination visible-xs pull-right">
                                <li><a href="#">«</a></li>
                                <li><a href="#">»</a></li>
                            </ul>
                        </div>
                    </div>
                </div>-->

                    <!--<div class="container">
                        <div class="col-md-10 col-md-offset-1">
                            <div class="panel panel-default panel-table">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col col-xs-6">
                                            <h3 class="panel-title">Beginner</h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>-->
                </div>
                <!-- End Item -->
                <div class="item">
                    <div class="panel-body">
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
