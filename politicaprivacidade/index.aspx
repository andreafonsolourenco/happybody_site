<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Happy Body - Política de Privacidade</title>
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
    <link href="../happybody/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">

    <link href="../general/alertify/css/alertify.min.css" rel="stylesheet" type='text/css'/>
    <link href="../general/alertify/css/themes/semantic.min.css" rel="stylesheet" type='text/css'/>
    <link href="../general/alertify/css/themes/default.min.css" rel="stylesheet" type='text/css'/>
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
            margin-top: 110px;
            margin-bottom: 110px;
            text-align: left;
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
    <div style="width: 100%; height: 100%; padding-left: 20px !important; padding-right: 20px !important;" id="content" runat="server"></div>

    <span style="display: none" id="language" runat="server"></span>

    <script type="text/javascript" src="../general/js/babylon.js"></script>
    <script type="text/javascript" src="../general/js/hand.js"></script>
    <!-- Custom scripts for this template -->
    <script src="../general/freelancer.min.js"></script>

    <!-- Bootstrap core JavaScript -->
    <script src="../happybody/vendor/jquery/jquery.min.js"></script>
    <script src="../happybody/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>  
    
    <script type="text/javascript" src="../general/alertify/alertify.min.js"></script>

    <script type="text/javascript" src="../happybody/bootstrap-material-datetimepicker/moment-with-locales.js"></script>
    <script type="text/javascript" src="../happybody/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>

    <script type="text/javascript">
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function loadConsentPage() {
            var language = $('#language').html();

            if (window.location.href.includes('localhost')) {
                window.location.href = '../rgpd/consent.aspx?language=' + language;
            }
            else {
                window.location.href = 'https://www.happybody.site/rgpd/consent.aspx?language=' + language;
            }
        }
    </script>
</body>
</html>
