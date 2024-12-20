<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Happy Body</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
	<link href="https://fonts.googleapis.com/css?family=Amaranth" rel="stylesheet">
    <link href="../happybody/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">
    <link href="https://www.w3schools.com/w3css/4/w3.css" rel="stylesheet" type="text/css">
    <link rel="shortcut icon" href="../general/img/favicon.ico" type="image/x-icon">
    <link rel="icon" href="../general/img/favicon.ico" type="image/x-icon">
    <!-- Theme CSS -->
    <link href="../general/css/freelancer.min.css" rel="stylesheet">
    
    <!-- Emojis -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

    <style type="text/css">
        .header {
            background-color: #FFF;
            color: #000;
            position:fixed;
            top: 0;
            width: 100%;
            height: 100px;
            padding:5px;
            line-height: 100px;
            border-bottom:3px solid #e6e7e8;
            text-align: center;
            font-family: 'Roboto', sans-serif;
            z-index: 100;
        }

        .footer {
            background-color: #FFF;
            position:fixed;
            bottom: 0;
            width: 100%;
            height: 50px;
            padding:5px;
            color: #000;
            border-top:3px solid #e6e7e8;
            font-family: 'Roboto', sans-serif;
            z-index: 100;
        }

        .contentCenter {
            text-align: center;
            font-family: 'Roboto', sans-serif;
            font-size: 14px;
        }

        .nopadding {
            padding: 0 !important;
            margin: 0 !important;
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
<body style="background-color: #FFF !important">
    <div style="width: 100%; height: 100%" id="content" runat="server"></div>

    <script type="text/javascript" src="../general/js/babylon.js"></script>
    <script type="text/javascript" src="../general/js/hand.js"></script>
    <!-- Custom scripts for this template -->
    <script src="../general/freelancer.min.js"></script>

    <!-- Bootstrap core JavaScript -->
    <script src="../happybody/vendor/jquery/jquery.min.js"></script>
    <script src="../happybody/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>   

    <script type="text/javascript">
        $(document).ready(function () {
            checkBtnSizes();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).height());
            checkBtnSizes();
        });

        $(window).scroll(function () {
            
        });

        function checkBtnSizes() {
            var sizePT = $('#btnPT').height();
            var sizeEN = $('#btnEN').height();
            var sizeFR = $('#btnFR').height();
            var sizeES = $('#btnES').height();
            var max = 0;

            if (sizePT > max) {
                max = sizePT;
            }

            if (sizeEN > max) {
                max = sizeEN;
            }

            if (sizeFR > max) {
                max = sizeFR;
            }

            if (sizeES > max) {
                max = sizeES;
            }

            $('#btnPT').height(max);
            $('#btnEN').height(max);
            $('#btnFR').height(max);
            $('#btnES').height(max);
        }

        function openRGPDPagePT() {
            openRGPDPage('PT');           
        }

        function openRGPDPageEN() {
            openRGPDPage('EN');
        }

        function openRGPDPageFR() {
            openRGPDPage('FR');
        }

        function openRGPDPageES() {
            openRGPDPage('ES');
        }

        function openRGPDPage(language) {
            if (window.location.href.includes('localhost')) {
                window.location.href = '../rgpd/consent.aspx?pagina=campanha&language=' + language;
            }
            else {
                window.location.href = 'https://www.happybody.site/rgpd/consent.aspx?pagina=campanha&language=' + language;
            }
        }
    </script>
</body>
</html>
