<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news_old.aspx.cs" Inherits="news_old" Culture="auto" UICulture="auto" %>

<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Happy Body</title>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="HappyBody Gym">
    <meta name="author" content="André Afonso Lourenço">
    <meta name="keywords" content="HappyBody, HappyBody Gym, Coimbra, Ginásio">

    <!-- Custom fonts for this theme -->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">
    <link href="https://www.w3schools.com/w3css/4/w3.css" rel="stylesheet" type="text/css">
    <link rel="shortcut icon" type="image/ico" href="img/favicon.ico" />

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet">

    <style type="text/css">
        .background_page {
            background-image: url("img/news_capa.jpg") !important;
            background-repeat: no-repeat !important;
            background-position: center !important; /* Center the image */
            background-size: cover !important; /* Resize the background image to cover the entire container */
        }

        .margin_20 {
            margin-top: 20px;
        }

        .variaveis {
            display: none !important;
        }
    </style>
</head>
<body id="page-top">
    <!-- Navigation -->
    <nav class="navbar navbar-expand-lg bg-secondary text-uppercase fixed-top" id="mainNav" runat="server"></nav>

    <div id="divNavbar" style="width: 100%"></div>

    <!-- Masthead -->
    <header class="masthead text-white text-center background_page" id="header">
        <div class="container d-flex align-items-center flex-column">
            <h1 class="masthead-heading text-uppercase mb-0" id="pageTitle" runat="server"></h1>
        </div>
    </header>

    <!-- Portfolio Section -->
    <section class="page-section portfolio" id="staff">
        <div class="container">

            <!-- Portfolio Section Heading -->
            <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="newsletterTitle" runat="server">Newsletter</h2>

            <!-- Icon Divider -->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fas fa-star"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>

            <!-- Portfolio Grid Items -->
            <div class="row" id="newsletterRow" runat="server"></div>

        </div>
    </section>

    <!-- Portfolio Section -->
    <section class="page-section portfolio" id="eventos">
        <div class="container">

            <!-- Portfolio Section Heading -->
            <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="eventosTitle" runat="server"></h2>

            <!-- Icon Divider -->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fas fa-star"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>

            <!-- Portfolio Grid Items -->
            <div class="row" id="eventosRow" runat="server"></div>

        </div>
    </section>

    <!-- Portfolio Section -->
    <section class="page-section portfolio" id="artigos">
        <div class="container">

            <!-- Portfolio Section Heading -->
            <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="artigosTitle" runat="server"></h2>

            <!-- Icon Divider -->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fas fa-star"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>

            <!-- Portfolio Grid Items -->
            <div class="row" id="artigosRow" runat="server"></div>

        </div>
    </section>

    <!-- Footer -->
    <footer class="footer text-center">
        <div class="container">
            <div class="row" id="footerDiv" runat="server"></div>
        </div>
    </footer>

    <!-- Copyright Section -->
    <section class="copyright py-4 text-center text-white">
        <div class="container">
            <small>Copyright &copy; André Afonso Lourenço 2020</small>
        </div>
    </section>

    <!-- Scroll to Top Button (Only visible on small and extra-small screen sizes) -->
    <div class="scroll-to-top d-lg-none position-fixed ">
        <a class="js-scroll-trigger d-block text-center text-white rounded" href="#page-top">
            <i class="fa fa-chevron-up"></i>
        </a>
    </div>

    <div id="divNewsletterDescription" runat="server"></div>
    <div id="divArtigosDescription" runat="server"></div>
    <div id="divEventosDescription" runat="server"></div>

    <!-- Bootstrap core JavaScript -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>

    <!-- Custom scripts for this template -->
    <script src="js/freelancer.min.js"></script>

    <script type="text/javascript">
        var slideIndex = 1;
        var elementOpened = 0;
        showDivs(slideIndex, parseInt($('#counterEventsStart').html()));

        function plusDivs(n) {
            showDivs(slideIndex += n, elementOpened);
        }

        function showDivs(n, y) {
            var i;
            var x = document.getElementsByClassName("mySlides slideImgEvents" + y);
            elementOpened = y;

            if (n > x.length) { slideIndex = 1 }
            if (n < 1) { slideIndex = x.length }
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            x[slideIndex - 1].style.display = "block";
        }

        $(document).ready(function () {
            calcSizesEvents();
            calcSizesArtigos();
        });

        $(window).on('resize', function () {
            calcSizesEvents();
            calcSizesArtigos();
        });

        $(window).scroll(function () {

        });

        function calcSizesEvents() {
            var x = parseInt($('#counterEventsStart').html());
            var nrEvents = parseInt($('#counterEventsFinish').html());
            var maxSize = 0;

            for (i = x; i <= nrEvents; i++) {
                if (maxSize < $('#imgEventos' + i.toString()).height()) {
                    maxSize = $('#imgEventos' + i.toString()).height();
                }
            }

            for (i = x; i <= nrEvents; i++) {
                $('#imgEventos' + i.toString()).height(maxSize);
            }
        }

        function calcSizesArtigos() {
            var x = parseInt($('#counterArtigosStart').html());
            var nrEvents = parseInt($('#counterArtigosFinish').html());
            var maxSize = 0;

            for (i = x; i <= nrEvents; i++) {
                if (maxSize < $('#imgArtigos' + i.toString()).height()) {
                    maxSize = $('#imgArtigos' + i.toString()).height();
                }
            }

            for (i = x; i <= nrEvents; i++) {
                $('#imgArtigos' + i.toString()).height(maxSize);
            }
        }
    </script>
</body>
</html>
