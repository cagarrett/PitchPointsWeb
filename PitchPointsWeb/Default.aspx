<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            console.log('carousel');
            $('#mainCarousel').carousel();
        });
    </script>
    <div>
        <div class="row">
            <div class="col s6">
                <h2>Home Page</h2>
            </div>
        </div>
    </div>
    <div class="container">
        <div id="mainCarousel" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#mainCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#mainCarousel" data-slide-to="1"></li>
                <li data-target="#mainCarousel" data-slide-to="2"></li>
                <li data-target="#mainCarousel" data-slide-to="3"></li>
            </ol>
            <div class="carousel-inner" role="listbox">
                <div class="item active">
                    <img src="Assets/Bathang.png" alt="Bat hang">
                </div>
                <div class="item">
                    <img src="Assets/Climb.png" alt="Climb">
                </div>
                <div class="item">
                    <img src="Assets/EvoComp.png" alt="Competition">
                </div>
                <div class="item">
                    <img src="Assets/Boulder.png" alt="Boulder">
                </div>
                <div class="item">
                    <img src="Assets/Snake_Pit.png" alt="SnakePit">
                </div>
            </div>
            <a class="left carousel-control" href="#mainCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#mainCarousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
</asp:Content>
