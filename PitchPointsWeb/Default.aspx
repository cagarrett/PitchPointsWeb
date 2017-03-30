<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PitchPointsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="row">
            <div class="col s6">
                <h2>Home Page</h2>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="carousel">
            <script>
                $(document).ready(function () {
                    $('.carousel').carousel();
                });
            </script>
            <a class="carousel-item" href="#one!">
                <img src="Assets/Bathang.png" alt="Bat hang"></a>
            <a class="carousel-item" href="#two!">
                <img src="Assets/Climb.png" alt="Climb"></a>
            <a class="carousel-item" href="#three!">
                <img src="Assets/EvoComp.png" alt="Competition"></a>
            <a class="carousel-item" href="#four!">
                <img src="Assets/Boulder.png" alt="Boulder"></a>
            <a class="carousel-item" href="#five!">
                <img src="Assets/Snake_Pit.png" alt="SnakePit"></a>
        </div>
    </div>
</asp:Content>
