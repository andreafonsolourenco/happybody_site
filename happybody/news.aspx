<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="news" Culture="auto" UICulture="auto" %>

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
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet">

    <style type="text/css">
        .pt {
            background-image: url(img/icons/pt.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .en {
            background-image: url(img/icons/en.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .es {
            background-image: url(img/icons/es.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .fr {
            background-image: url(img/icons/fr.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .close_fab {
            background-image: url(img/icons/close.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .mute {
            background-image: url(img/icons/mute.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .sound {
            background-image: url(img/icons/sound.svg);
            background-size: 100% 100%;
            background-position: center center;
            background-repeat: no-repeat;
        }

        .fab_sound {
            position: fixed;
            left: 20px;
            color: #FFF;
            border-radius: 50px;
            text-align: center;
            cursor: pointer;
            z-index: 1032 !important;
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
<body id="page-top">
    <audio loop id="music">
        <source src="musica/music_background.ogg" type="audio/ogg">
        <source src="musica/music_background.mp3" type="audio/mp3">
        <source src="musica/music_background.wav" type="audio/vnd.wave">
        Your browser does not support the audio element.
    </audio>

    <!-- Navigation -->
    <nav class="navbar navbar-expand-lg bg-secondary text-uppercase fixed-top" id="mainNav" runat="server"></nav>

    <div id="divNavbar" style="width: 100%"></div>

    <!-- Masthead -->
    <header class="text-white text-center background_page masthead w-100 h-100" id="header">
        <div class="container d-flex align-items-center flex-column w-100 h-100">
            <!-- Masthead Heading -->
            <h1 class="masthead-heading text-uppercase mb-0" id="pageTitle" runat="server" style="margin: auto !important"></h1>
        </div>
    </header>

    <!-- Portfolio Section -->
    <section class="page-section portfolio" id="news">
        <div class="container">

            <!-- Portfolio Section Heading -->
            <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="newsTitle" runat="server"></h2>

            <!-- Icon Divider -->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fas fa-star"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>

            <!-- Portfolio Grid Items -->
            <div class="row" id="newsRow" runat="server"></div>
            <br /><br />
        </div>

        <div class="container">
            <br /><br />
            <!-- Contact Section Heading -->
            <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="commentsTitle" runat="server"></h2>

            <!-- Icon Divider -->
            <div class="divider-custom">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fas fa-star"></i>
                </div>
                <div class="divider-custom-line"></div>
            </div>

            <!-- Contact Section Form -->
            <div class="row" id="divCommentsForm" runat="server"></div>

            <div class="row" id="divScrollComments" runat="server"></div>
        </div>
    </section>

    <!-- Copyright Section -->
  <section class="copyright py-4 text-center text-white" id="footer" runat="server"></section>

    <!-- Scroll to Top Button (Only visible on small and extra-small screen sizes) -->
    <div class="scroll-to-top d-lg-none position-fixed ">
        <a class="js-scroll-trigger d-block text-center text-white rounded" href="#page-top">
            <i class="fa fa-chevron-up"></i>
        </a>
    </div>

    <a class="fab_language bg-secondary" id="fab" runat="server" onclick="openLanguageMenu();">
        <%--<i class="fa fa-plus margin_22"></i>--%>
    </a>

    <a class="fab_sound bg-secondary" id="fab_sound" runat="server" onclick="checkSound();">
        <%--<i class="fa fa-plus margin_22"></i>--%>
    </a>

    <a class="close_fab fab_language bg-secondary variaveis" id="close_fab" runat="server" onclick="closeLanguageMenu();"></a>

    <div class="black_overlay" id="black_overlay">
        <div id='divLanguages' class='divLanguages col-sm-12 col-md-12 col-lg-12 col-xl-12'>
            <div class="text-white w-100 text-center">
                <h4 id="selectLanguageTitle" runat="server"></h4>
            </div>
            <div class="pt divSelectLanguage col-sm-3 col-md-3 col-lg-3 col-xl-3" onclick="changeLanguagePage('PT');"></div>
            <div class="en divSelectLanguage col-sm-3 col-md-3 col-lg-3 col-xl-3" onclick="changeLanguagePage('EN');"></div>
            <div class="fr divSelectLanguage col-sm-3 col-md-3 col-lg-3 col-xl-3" onclick="changeLanguagePage('FR');"></div>
            <div class="es divSelectLanguage col-sm-3 col-md-3 col-lg-3 col-xl-3" onclick="changeLanguagePage('ES');"></div>
        </div>
    </div>

    <div class="black_overlay_loading" id="black_loading_overlay">
        <div id='divLoading' class='divLoading text-center'>
            <img src="img/icons/loading.svg" />
        </div>
    </div>

    <div id="divArtigosDescription" runat="server"></div>

    <div id="divBackgroundImages" runat="server" class="variaveis"></div>
    <span id="separator" runat="server" class="variaveis"></span>
    <span id="servidor_site" runat="server" class="variaveis"></span>

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
        var imgId = 1;
        var imgBackgroundSrc = $('#imgBackground' + imgId).attr('src');
        var emailDelay = 3000;

        $(document).ready(function () {
            $('#header').css("background-image", "url(" + imgBackgroundSrc + ")");
            imgId++;
            setInterval(function () {
                changeBackgroundImage();
            }, 5000);

            var separator = $('#separator').html();
            if (separator != '') {
                $('a[href=\'#' + separator + '\']').click();
            }

            calcFabLanguageAndSound();
        });

        $(window).on('resize', function () {
            calcFabLanguageAndSound();
        });

        $(window).scroll(function () {
            
        });

        function sendComment() {
            var language = "";

            if ($('#fab').hasClass('pt')) {
                language = 'PT';
            }
            else if ($('#fab').hasClass('en')) {
                language = 'EN';
            }
            else if ($('#fab').hasClass('fr')) {
                language = 'FR';
            }
            else if ($('#fab').hasClass('es')) {
                language = 'ES';
            }

            var comment = $('#comment').val().trim();

            $.ajax({
                type: "POST",
                url: "news.aspx/saveComment",
                data: '{"comment":"' + comment + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        if (parseInt(val) > 0) {
                            $("#success").fadeIn().delay(emailDelay).fadeOut();
                            $('#comment').val('');
                        }
                        else {
                            if (parseInt(val) == -1) {
                                $("#insertValidMessage").fadeIn().delay(emailDelay).fadeOut();
                            }
                            else {
                                $("#error").fadeIn().delay(emailDelay).fadeOut();
                            }
                        }
                    }
                }
            });
        }       

        function openLanguageMenu() {
            $('#black_overlay').fadeIn();
            $('#divLanguages').fadeIn();
            $('#fab').fadeOut();
            $('#close_fab').removeClass('variaveis');
        }

        function closeLanguageMenu() {
            $('#black_overlay').fadeOut();
            $('#divLanguages').fadeIn();
            $('#fab').fadeIn();
            $('#close_fab').addClass('variaveis');
        }

        function changeLanguagePage(language) {
            var sep = "";
            var server = $('#servidor_site').html();
            if ($('#separator').html() != '') {
                sep = '&separator=' + $('#separator').html();
            }

            if (window.location.href.includes('localhost')) {
                window.location.href = "news.aspx?language=" + language + sep;
            }
            else if (window.location.href.includes('happybody.pt')) {
                window.location.href = "https://happybody.pt/" + server + "news.aspx?language=" + language + sep;
            }
            else {
                window.location.href = "https://happybody.site/" + server + "news.aspx?language=" + language + sep;
            }
        }

        function changeBackgroundImage() {
            imgBackgroundSrc = $('#imgBackground' + imgId).attr('src');
            $('#header').css("background-image", "url(" + imgBackgroundSrc + ")"); 
            imgId++;

            if (imgId == parseInt($('#totalBackgroundImages').html()) + 1) {
                imgId = 1;
            }
        }

        function calcFabLanguageAndSound() {
            var screenWidth = screen.width;
            var fabTop = parseFloat($('#mainNav').css('padding-top').replace('px', ''));

            $('#fab_sound').css('height', '3rem');
            $('#fab_sound').css('width', '3rem');

            $('#fab').css('height', '3rem');
            $('#fab').css('width', '3rem');
            $('#fab').css('top', fabTop);
            $('#close_fab').css('height', '3rem');
            $('#close_fab').css('width', '3rem');
            $('#close_fab').css('top', fabTop);

            if ($("#buttonNavbar").is(":visible") == true || screenWidth < 1700) {
                $('#fab_sound').css('right', '1rem');
                $('#fab_sound').css('bottom', '1rem');
                $('#fab_sound').css('top', '');
            }
            else {
                $('#fab_sound').css('right', '');
                $('#fab_sound').css('bottom', '');
                $('#fab_sound').css('top', fabTop);
            }
        }

        function checkSound() {
            if (document.getElementById("fab_sound").classList.contains("sound")) {
                document.getElementById("music").pause();
                $('#fab_sound').removeClass('sound');
                $('#fab_sound').addClass('mute');
            }
            else {
                document.getElementById("music").play();
                $('#fab_sound').removeClass('mute');
                $('#fab_sound').addClass('sound');
            }
        }

        function showLoadingBackground() {
            $('#black_loading_overlay').fadeIn();
            $('#divLoading').fadeIn();
            $('#close_fab').fadeIn();
            $('#fab').fadeIn();
        }

        function hideLoadingBackground() {
            $('#black_loading_overlay').fadeOut();
            $('#divLoading').fadeOut();
            $('#close_fab').fadeOut();
            $('#fab').fadeOut();
        }

        function loadPrivacyPolicy() {
            var language = "";
            var link = "";

            if ($('#fab').hasClass('pt')) {
                language = 'PT';
            }
            else if ($('#fab').hasClass('en')) {
                language = 'EN';
            }
            else if ($('#fab').hasClass('fr')) {
                language = 'FR';
            }
            else if ($('#fab').hasClass('es')) {
                language = 'ES';
            }

            if (window.location.href.includes('localhost')) {
                link = '../politicaprivacidade/index.aspx?language=' + language;
            }
            else if (window.location.href.includes('happybody.pt')) {
                link = "https://happybody.pt/politicaprivacidade/index.aspx?language=" + language;
            }
            else {
                link = "https://happybody.site/politicaprivacidade/index.aspx?language=" + language;
            }

            window.open(link, '_blank');
        }
    </script>
</body>
</html>
