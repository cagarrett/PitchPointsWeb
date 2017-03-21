<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Home Page</h2>
    <div class="container">
            <div class="my-carousel">
                <div id="myCarousel" class="carousel slide" data-ride="carousel" style="width: 400px; margin: 0 auto">
                    <!-- Carousel indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                    </ol>
                    <!-- Wrapper for carousel items -->
                    <div class="carousel-inner">
                        <div class="item active">
                            <img src="Assets/Bathang.jpg" alt="Bat hang">
                        </div>
                        <div class="item">
                            <img src="Assets/Climb.jpg" alt="Climb">
                        </div>
                        <div class="item">
                            <img src="Assets/EvoComp.jpg" alt="Competition">
                        </div>
                        <div class="item">
                            <img src="Assets/SnakePit.png" alt="Snake Pit">
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
