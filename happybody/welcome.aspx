<%@ Page Language="C#" AutoEventWireup="true" CodeFile="welcome.aspx.cs" Inherits="welcome" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
	<title>Happy Body</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="HappyBody Gym">
    <meta name="author" content="André Afonso Lourenço">
    <meta name="keywords" content="HappyBody, HappyBody Gym, Coimbra, Ginásio">

    <!-- Custom fonts for this theme -->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">
    <link href="https://www.w3schools.com/w3css/4/w3.css" rel="stylesheet" type="text/css">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet">

    <style>
        html, body {
            overflow: hidden;
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        .cursor {
            cursor: pointer;
        }

        video {
           object-fit: contain;
        }
    </style>

    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script', 'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '923313025683308');
        fbq('track', 'PageView');
    </script>
    <noscript>
        <img height="1" width="1" src="https://www.facebook.com/tr?id=923313025683308&ev=PageView&noscript=1"/>
    </noscript>
    <!-- End Facebook Pixel Code -->
</head>


<body style="background-color: #FFF">
    <div id="divVideo" class="col-12 col-md-12 col-xl-12 col-sm-12 col-lg-12">
        <video poster="img/logo.png" id="video" loop controls controlsList="nodownload" playsinline>
            <source src="video/video.mp4" type="video/mp4" id="videoSrcMP4" />
            <source src="video/video.webm" type="video/webm" id="videoSrcWEBM" />
            <source src="video/video.mkv" type="video/mkv" id="videoSrcMKV" />
            <img alt=":-(" src="img/logo.png" title="Infelizmente, o seu dispositivo não suporta vídeo!" />
        </video>
    </div>

    <div id="divText" class="row text-center" runat="server"></div>

    <!-- Masthead -->
    <header class="text-white text-center background_page masthead w-100 h-100" id="header">
        <div class="container d-flex align-items-center flex-column w-100 h-100">
            <!-- Masthead Heading -->
            <h1 class="masthead-heading text-uppercase mb-0" id="pageTitle" runat="server" style="margin: auto !important"></h1>
        </div>
    </header>


    <!-- Bootstrap core JavaScript -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>

    <!-- Custom scripts for this template -->
    <%--<script src="js/freelancer.min.js"></script>--%> 
    

    <script type="text/javascript">
        $(document).ready(function () {
            resizeScreen();
            resizeVideo();
            $('#video').bind('contextmenu', function () { return false; });

            // Create a timestamp
            var timestamp = new Date().getTime();

            $('video source').each(function (num, val) {
                var src = $(this).attr('src');
                $(this).attr('src', (src + "?t=" + timestamp));
            });
        });

        $(window).on('resize', function () {
            resizeScreen();
            resizeVideo();
        });

        $(window).scroll(function () {
            resizeScreen();
            resizeVideo();
        });

        function resizeScreen() {
            var divVideoHeight = parseFloat($(window).height()) * 0.7;
            var divTextHeight = parseFloat($(window).height()) * 0.3;
            $('#divVideo').css('height', divVideoHeight);
            $('#divText').css('height', divTextHeight);
        }

        function resizeVideo() {
            $('#video').height($('#divVideo').height());
            $('#video').width($('#divVideo').width());
        }

        function openPage() {
            var page = getUrlPage() + $('#page').html() + $('#languageToRemove').html() + '&campaign=1';
            window.open(page, '_self');
        }

        function getUrlPage() {
            if (window.location.href.includes('localhost')) {
                var location = window.location.href.replace('welcome.aspx', '');
                location = location.replace('?data=01/01/2023', '');
                location = location.replace($('#languageToRemove').html(), '');
                location = location.replace('happybody', '');
                return location;
            }
            else {
                return "https://www.happybody.site/";
            }
        }
    </script>

  </body> 
</html>
