<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Culture="auto" UICulture="auto" %>

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
    <link href="bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">

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
            left: 1rem;
            color: #FFF;
            border-radius: 50px;
            text-align: center;
            cursor: pointer;
            z-index: 1032 !important;
        }

        .divCampaign {
            position:fixed;
            top:50%;
            left:50%;
            -webkit-transform:translate(-50%,-50%);
            -moz-transform:translate(-50%,-50%);
            -ms-transform:translate(-50%,-50%);
            -o-transform:translate(-50%,-50%);
            transform:translate(-50%,-50%);
            width:auto;
            height:75%;
            z-index:1032 !important;
            opacity:1;
            max-height:75% !important;
        }

        .dtp-header {
            background-color: #800000 !important;
        }

        .dtp-time {
            background-color: #FF1717 !important; 
        }

        .dtp-date {
            background-color: #FF1717 !important; 
        }

        .dtp table.dtp-picker-days tr > td > a.selected {
            background-color: #800000 !important;
        }

        .dtp .p10 > a {
            color: #800000 !important;
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

    <div id="divNavbar" style="width:100%"></div>

    <!-- Masthead -->
    <header class="text-white text-center background_page masthead w-100 h-100" id="header" runat="server">
        <div class='container d-flex align-items-center flex-column w-100 h-100' id='headerContainer'>
             <!-- Masthead Avatar Image -->
             <img class='masthead-avatar' src='img/logo.png' alt='HappyBody Gym' style="margin: auto !important">

             <!-- Masthead Heading -->
             <!-- <h1 class='masthead-heading text-uppercase mb-0'>Atelier - Arte dos Trapos</h1> -->

             <!-- Icon Divider -->
             <!-- <div class='divider-custom divider-light'>
                 <div class='divider-custom-line'></div>
                 <div class='divider-custom-icon'>
                     <i class='fas fa-star'></i>
                 </div>
                 <div class='divider-custom-line'></div>
             </div> -->

             <!-- Masthead Subheading -->
             <!-- <p class='masthead-subheading font-weight-light mb-0'>Vestuário e Acessórios</p> -->
            </div>
    </header>

    <!-- About Section -->
  <section class="page-section bg-primary text-white mb-0" id="about">
    <div class="container">

      <!-- About Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-white" id="aboutTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom divider-light">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- About Section Content -->
      <div class="row" id="divAbout" runat="server"></div>

        <div class="row" id="divParceiros" runat="server"></div>

      <!-- About Section Button -->
      <!--<div class="text-center mt-4">
        <a class="btn btn-xl btn-outline-light" href="https://startbootstrap.com/themes/freelancer/">
          <i class="fas fa-download mr-2"></i>
          Free Download!
        </a>
      </div>-->

    </div>
  </section>

    <!-- Portfolio Section -->
  <section class="page-section portfolio" id="services">
    <div class="container">

      <!-- Portfolio Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="servicesTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- Portfolio Grid Items -->
      <div class="row" id="servicesRow" runat="server"></div>

    </div>
  </section>

    <!-- About Section -->
  <section class="page-section bg-primary text-white mb-0" id="marcarVisita">
    <div class="container">

      <!-- About Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-white" id="marcarVisitaTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom divider-light">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- About Section Content -->
      <div class="row" id="marcarVisitaFormDiv" runat="server"></div>

      <!-- About Section Button -->
      <!--<div class="text-center mt-4">
        <a class="btn btn-xl btn-outline-light" href="https://startbootstrap.com/themes/freelancer/">
          <i class="fas fa-download mr-2"></i>
          Free Download!
        </a>
      </div>-->

    </div>
  </section>

    <!-- Contact Section -->
  <section class="page-section" id="contact">
    <div class="container">

      <!-- Contact Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="contactsTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- Contact Section Form -->
      <div class="row" id="divContactsForm" runat="server"></div>

    </div>
  </section>

    <!-- Footer -->
  <%--<footer class="footer text-center">
    <div class="container">
      <div class="row" id="footerDiv" runat="server"></div>
    </div>
  </footer>--%>

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

    <span id="nrServices" runat="server" class="variaveis"></span>
    <span id="showVideo" runat="server" class="variaveis"></span>
    <span id="separator" runat="server" class="variaveis"></span>
    <span id="datepicker_clean" runat="server" class="variaveis"></span>
    <span id="datepicker_cancel" runat="server" class="variaveis"></span>
    <span id="datepicker_confirm" runat="server" class="variaveis"></span>
    <span id="preinscricao_generic_error" runat="server" class="variaveis"></span>
    <span id="servidor_site" runat="server" class="variaveis"></span>

    <div id="divServicesDescriptions" runat="server"></div>

    <div class="black_overlay" id="black_overlay_campaign" runat="server">
        <div id='divCampaign' role='document' runat="server"></div> 
    </div>

    <div id="divBackgroundImages" runat="server" class="variaveis"></div>

    <!-- Bootstrap core JavaScript -->
  <script src="vendor/jquery/jquery.min.js"></script>
  <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Custom scripts for this template -->
<script src="js/freelancer.min.js"></script>

    <!-- Plugin JavaScript -->
  <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

  <!-- Contact Form JavaScript -->
  <script src="js/jqBootstrapValidation.js"></script>
  <script src="js/contact_me.js"></script>

    <script type="text/javascript" src="bootstrap-material-datetimepicker/moment-with-locales.js"></script>
    <script type="text/javascript" src="bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>

  

    <script type="text/javascript">
        var imgId = 1;
        var imgBackgroundSrc = $('#imgBackground' + imgId).attr('src');
        var video = null;
        var isMobile = $('#isMobile').html();
        var emailDelay = 3000;

        $(document).ready(function () {
            setBackgroundImage();

            var separator = $('#separator').html();
            if (separator != '') {
                $('a[href=\'#' + separator + '\']').click();
            }

            calcFabLanguageAndSound();

            setDatePicker();

            setCampaign();
        });

        $(window).on('resize', function () {
            calcFabLanguageAndSound();
        });

        $(window).scroll(function () {
            
        });

        function openDestination() {
            window.open('https://www.google.pt/maps/place/Gin%C3%A1sio+Happy+Body/@40.217096,-8.4125097,17z/data=!3m1!4b1!4m5!3m4!1s0xd22f971d514a5e9:0x31e678dad25d53fa!8m2!3d40.217096!4d-8.410321', '_blank');
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

        function closeCampaign() {
            $('#black_overlay_campaign').fadeOut();
            checkSound();
        }

        function changeLanguagePage(language) {
            var sep = "";
            var server = $('#servidor_site').html();
            if ($('#separator').html() != '') {
                sep = '&separator=' + $('#separator').html();
            }

            if (window.location.href.includes('localhost')) {
                window.location.href = "index.aspx?language=" + language + sep;
            }
            else if (window.location.href.includes('happybody.pt')) {
                window.location.href = "https://happybody.pt/" + server + "index.aspx?language=" + language + sep;
            }
            else {
                window.location.href = "https://happybody.site/" + server + "index.aspx?language=" + language + sep;
            }
        }

        function setBackgroundImage() {
            $('#header').css("background-image", "url(" + imgBackgroundSrc + ")");
            imgId++;
            setInterval(function () {
                changeBackgroundImage();   
            }, 5000);
        }

        function changeBackgroundImage() {
            imgBackgroundSrc = $('#imgBackground' + imgId).attr('src');
            $('#header').css("background-image", "url(" + imgBackgroundSrc + ")"); 
            imgId++;

            if (imgId == parseInt($('#totalBackgroundImages').html()) + 1) {
                imgId = 1;
            }
        }

        function onFocus(x, y) {
            /*if (x == 0) {
                $("#dataAF").prop('type', 'date');
            }

            if (x == 1) {
                $("#horaAF").prop('type', 'time');
            }

            if (x == 2) {
                $("#dataTreino").prop('type', 'date');
            }

            if (x == 3) {
                $("#validadecc").prop('type', 'date');
            }

            if (x == 4) {
                $("#datanascimento").prop('type', 'date');
            }

            if (x == 5) {
                $("#dataFormService" + y).prop('type', 'date');
            }

            if (x == 6) {
                $("#horaFormService" + y).prop('type', 'time');
            }*/
        }

        function outFocus(x, y) {
            /*if (x == 0) {
                $("#dataAF").prop('type', 'text');
            }

            if (x == 1) {
                $("#horaAF").prop('type', 'text');
            }

            if (x == 2) {
                $("#dataTreino").prop('type', 'text');
            }

            if (x == 3) {
                $("#validadecc").prop('type', 'text');
            }

            if (x == 4) {
                $("#datanascimento").prop('type', 'text');
            }

            if (x == 5) {
                $("#dataFormService" + y).prop('type', 'text');
            }

            if (x == 6) {
                $("#horaFormService" + y).prop('type', 'text');
            }*/
        }

        function closeFormServices(x) {
            $('#divFormServices' + x).addClass('variaveis');
            $('#divServices' + x).fadeIn();
        }

        function openFormServices(x) {
            $('#divFormServices' + x).removeClass('variaveis');
            $('#divServices' + x).fadeOut();
        }

        function validatePhoneNumber(x) {
            /*if (x.length < 9) {
                return false;
            }*/

            if (x === 'NULL' || x === '' || x === null || x === undefined) {
                return false;
            }

            return true;
        }

        function validateZipCode(str)
        {
            //var expr = /^[0-9]{4}(?:-[0-9]{3})?$/;
            //return expr.test(str);

            if (str === 'NULL' || str === '' || str === null || str === undefined) {
                return false;
            }

            return true;
        }

        function mailValidation(val) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(val);
        }

        function textValidation(str) {
            var s1 = '<';
            var s2 = '>';

            if (str.indexOf(s1) != -1 || str.indexOf(s2) != -1) {
                return false;
            }

            return true;
        }

        function sendMarcarVisitaMail() {
            $('#successFormMarcarVisita').html('');
            showLoadingBackground();
            $('#mvnameDanger').fadeOut();
            $('#mvemailDanger').fadeOut();
            $('#mvtlfDanger').fadeOut();

            var name = $('#mvname').val().trim();
            var email = $('#mvemail').val().trim();
            var tlf = $('#mvtlf').val().trim();

            if (name.length == 0) {
                $('#mvnameDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!mailValidation(email)) {
                $('#mvemailDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!validatePhoneNumber(tlf)) {
                $('#mvtlfDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            var assunto = "Happy Body - Marcar Visita";
            var intro = assunto;
            var sendto = email;
            var body = "Nome: " + name + "<br />Telefone: " + tlf + "<br />Email: " + email + "<br />Deseja marcar uma visita às instalações do Ginásio";
            //sendEmail(assunto, intro, sendto, '', '', body, '1', 'x');

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

            $.ajax({
                type: "POST",
                url: "emails.aspx/sendMarcarTreinoEmail",
                data: '{"name":"' + name + '", "email":"' + email + '", "tlf":"' + tlf + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        hideLoadingBackground();
                        $('#successFormMarcarVisita').html(ret);
                        $('#successFormMarcarVisita').fadeIn();

                        if (parseInt(val) >= 0) {
                            $('#mvname').val('');
                            $('#mvemail').val('');
                            $('#mvtlf').val('');
                            setTimeout(function () {
                                $('#successFormMarcarVisita').fadeOut();
                            }, emailDelay);
                        }
                    }
                }
            });
        }

        function sendContactosMail() {
            $('#success').html('');
            showLoadingBackground();
            $('#nameDanger').fadeOut();
            $('#emailDanger').fadeOut();
            $('#tlfDanger').fadeOut();
            $('#subjectDanger').fadeOut();
            $('#textareaDanger').fadeOut();

            var name = $('#name').val().trim();
            var email = $('#email').val().trim();
            var tlf = $('#tlf').val().trim();
            var subject = $('#subject').val().trim();
            var textarea = $('#textarea').val().trim();

            if (name.length == 0) {
                $('#nameDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!mailValidation(email)) {
                $('#emailDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!validatePhoneNumber(tlf)) {
                $('#tlfDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (subject.length == 0 || !textValidation(subject)) {
                $('#subjectDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            if (textarea.length == 0 || !textValidation(textarea)) {
                $('#textareaDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            var assunto = "Happy Body - Contacto - " + subject;
            var intro = "Happy Body - Contacto<br />" + subject;
            var sendto = email;
            var body = "Nome: " + name + "<br />Telefone: " + tlf + "<br />Email: " + email + "<br />Assunto: " + subject + "<br />" + textarea;
            //sendEmail(assunto, intro, sendto, '', '', body, '2', 'x');

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

            $.ajax({
                type: "POST",
                url: "emails.aspx/sendContactEmail",
                data: '{"name":"' + name + '", "email":"' + email + '", "tlf":"' + tlf + '", "subject":"' + subject + '", "emailText":"' + textarea + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        hideLoadingBackground();
                        $('#success').html(ret);
                        $('#success').fadeIn();

                        if (parseInt(val) >= 0) {
                            $('#name').val('');
                            $('#email').val('');
                            $('#tlf').val('');
                            $('#subject').val('');
                            $('#textarea').val('');
                            setTimeout(function () {
                                $('#success').fadeOut();
                            }, emailDelay);
                        }
                    }
                }
            });
        }

        function sendServicesMail(x) {
            $('#successFormServices' + x).html('');
            showLoadingBackground();
            $('#nameFormDanger' + x).fadeOut();
            $('#emailFormDanger' + x).fadeOut();
            $('#phoneFormDanger' + x).fadeOut();
            $('#ageFormDanger' + x).fadeOut();
            $('#dateFormDanger' + x).fadeOut();
            $('#horaFormDanger' + x).fadeOut();

            var name = $('#nameFormService' + x).val().trim();
            var email = $('#emailFormService' + x).val().trim();
            var tlf = $('#phoneFormService' + x).val().trim();
            var age = $('#ageFormService' + x).val().trim();
            var date = $('#dataFormService' + x).val().trim();
            var hour = $('#horaFormService' + x).val().trim();
            var servico = $('#titleServices' + x).html().trim();
            var idServico = $('#idServiceDivFormServices' + x).html().trim();

            if (name.length == 0) {
                $('#nameFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!mailValidation(email)) {
                $('#emailFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!validatePhoneNumber(tlf)) {
                $('#phoneFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            if (age.length == 0) {
                $('#ageFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            if (date.length == 0) {
                $('#dateFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            if (hour.length == 0) {
                $('#horaFormDanger' + x).fadeIn();
                hideLoadingBackground();
                return;
            }

            var assunto = "Happy Body - Serviços - " + servico;
            var intro = "Happy Body - Serviços<br />" + servico;
            var sendto = email;
            var body = "Nome: " + name + "<br />Idade: " + age + "<br />Telefone: " + tlf + "<br />Email: " + email + "<br />Deseja marcar " + servico + " para dia " + date + " às " + hour + " horas.";
            //sendEmail(assunto, intro, sendto, '', '', body, '0', x);

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

            $.ajax({
                type: "POST",
                url: "emails.aspx/sendServicesEmail",
                data: '{"name":"' + name + '", "email":"' + email + '", "tlf":"' + tlf + '", "age":"' + age + '", "date":"' + date + '", "hour":"' + hour + '", "idService":"' + idServico + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        hideLoadingBackground();
                        $('#successFormServices' + x).html(ret);
                        $('#successFormServices' + x).fadeIn();

                        if (parseInt(val) >= 0) {
                            $('#nameFormService' + x).val('');
                            $('#emailFormService' + x).val('');
                            $('#phoneFormService' + x).val('');
                            $('#ageFormService' + x).val('');
                            $('#dataFormService' + x).val('');
                            $('#horaFormService' + x).val('');
                            setTimeout(function () {
                                $('#successFormServices' + x).fadeOut();
                                closeFormServices(x);
                            }, emailDelay);
                        }
                    }
                }
            });
        }

        function sendCampaignMail() {
            $('#successFormCampaign').html('');
            showLoadingBackground();
            $('#emailFormCampaignDanger').fadeOut();
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

            var email = $('#emailFormCampaign').val().trim();

            if (!mailValidation(email)) {
                $('#emailFormCampaignDanger').fadeIn();
                hideLoadingBackground();
                return;
            }

            $.ajax({
                type: "POST",
                url: "emails.aspx/sendCampaignEmail",
                data: '{"sendto":"' + email + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        hideLoadingBackground();
                        $('#successFormCampaign').html(ret);
                        $("#successFormCampaign").fadeIn();

                        if (parseInt(val) >= 0) {
                            $('#emailFormCampaign').val('');
                            setTimeout(function () {
                                $("#successFormCampaign").fadeOut();
                                closeCampaign();
                            }, emailDelay);
                        }
                    }
                }
            });
        }

        function sendEmail(assunto, intro, sendto, sendcc, sendbcc, body, tipo, x) {
            var language = "";
            var emailCampaignDelay = 1500;

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

            $.ajax({
                type: "POST",
                url: "index.aspx/sendEmailFromTemplate",
                data: '{"assunto":"' + assunto + '", "intro":"' + intro + '", "sendto":"' + sendto + '", "sendcc":"' + sendcc
                    + '", "sendbcc":"' + sendbcc + '", "body":"' + body + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        switch (tipo) {
                            case '0':
                                $('#successFormServices').html(res.d);
                                $("#successFormServices").fadeIn().delay(emailDelay).fadeOut();

                                $('#nameFormService' + x).val('');
                                $('#emailFormService' + x).val('');
                                $('#phoneFormService' + x).val('');
                                $('#ageFormService' + x).val('');
                                $('#dataFormService' + x).val('');
                                $('#horaFormService' + x).val('');
                                break;
                            case '1':
                                $('#successFormMarcarVisita').html(res.d);
                                $("#successFormMarcarVisita").fadeIn().delay(emailDelay).fadeOut();

                                $('#mvname').val('');
                                $('#mvemail').val('');
                                $('#mvtlf').val('');
                                break;
                            case '2':
                                $('#success').html(res.d);
                                $("#success").fadeIn().delay(emailDelay).fadeOut();

                                $('#name').val('');
                                $('#email').val('');
                                $('#phone').val('');
                                $('#subject').val('');
                                $('#textarea').val('');
                                break;
                            case '3':
                                $('#successFormCampaign').html(res.d);
                                $("#successFormCampaign").fadeIn();
                                setTimeout(function () {
                                    $("#successFormCampaign").fadeOut();
                                    closeCampaign();
                                }, emailCampaignDelay);
                                $('#emailFormCampaign').val('');
                                break;
                            default:
                                break;
                        }

                        hideLoadingBackground();
                    }
                }
            });
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

        function sendPreInscricao() {
            showLoadingBackground();
            $('#successPreInscricao').html('');
            $('#errorPreInscricao').html('');
            $('#successPreInscricao').fadeOut();
            $('#errorPreInscricao').fadeOut();
            $('#dangernamePreInscricao').fadeOut();
            $('#dangerMorada').fadeOut();
            $('#dangercodpostal').fadeOut();
            $('#dangerlocalidade').fadeOut();
            $('#dangertelemovel').fadeOut();
            $('#dangeremailPreInscricao').fadeOut();
            $('#dangerprofissao').fadeOut();
            $('#dangerccnr').fadeOut();
            $('#dangervalidadecc').fadeOut();
            $('#dangerdatanascimento').fadeOut();
            $('#dangercontactoemergencia').fadeOut();
            $('#dangergraucontactoemergencia').fadeOut();
            $('#dangerdataAF').fadeOut();
            $('#dangerhoraAF').fadeOut();
            $('#dangerdataTreino').fadeOut();

            var name = $('#namePreInscricao').val().trim();
            var email = $('#emailPreInscricao').val().trim();
            var tlf = $('#telemovel').val().trim();
            var morada = $('#morada').val().trim();
            var codpostal = $('#codpostal').val().trim();
            var localidade = $('#localidade').val().trim();
            var profissao = $('#profissao').val().trim();
            var ccnr = $('#ccnr').val().trim();
            var validadecc = $('#validadecc').val().trim();
            var datanascimento = $('#datanascimento').val().trim();
            var contactoEmergencia = $('#contactoemergencia').val().trim();
            var grauContactoEmergencia = $('#graucontactoemergencia').val().trim();
            var dataAF = $('#dataAF').val().trim();
            var dataTreino = $('#dataTreino').val().trim();
            var consentimento = $("#checkboxConsentimento").attr("checked") ? '1' : '0';
            var language = "";

            if ($('#fab').hasClass('pt')) {
                language = 'PT';
            }
            else if ($('#fab').hasClass('en')) {
                language = 'EN';
            }
            else if ($('#fab').hasClass('fr')) {
                language = 'FR';
                ccnr = 'CH - SEM CC';
                validadecc = '31/12/2100';
                contactoEmergencia = '0';
                grauContactoEmergencia = 'CH';
            }
            else if ($('#fab').hasClass('es')) {
                language = 'ES';
            }

            if (name.length == 0) {
                $('#dangernamePreInscricao').fadeIn();
                $('#errorPreInscricao').html($('#dangernamePreInscricao').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!mailValidation(email)) {
                $('#dangeremailPreInscricao').fadeIn();
                $('#errorPreInscricao').html($('#dangeremailPreInscricao').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!validatePhoneNumber(tlf)) {
                $('#dangertelemovel').fadeIn();
                $('#errorPreInscricao').html($('#dangertelemovel').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (!validateZipCode(codpostal)) {
                $('#dangercodpostal').fadeIn();
                $('#errorPreInscricao').html($('#dangercodpostal').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (morada.length == 0) {
                $('#dangermorada').fadeIn();
                $('#errorPreInscricao').html($('#dangermorada').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (localidade.length == 0) {
                $('#dangerlocalidade').fadeIn();
                $('#errorPreInscricao').html($('#dangerlocalidade').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (profissao.length == 0) {
                $('#dangerprofissao').fadeIn();
                $('#errorPreInscricao').html($('#dangerprofissao').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (ccnr.length == 0) {
                $('#dangerccnr').fadeIn();
                $('#errorPreInscricao').html($('#dangerccnr').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (validadecc.length == 0) {
                $('#dangervalidadecc').fadeIn();
                $('#errorPreInscricao').html($('#dangervalidadecc').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (datanascimento.length == 0) {
                $('#dangerdatanascimento').fadeIn();
                $('#errorPreInscricao').html($('#dangerdatanascimento').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (contactoEmergencia.length == 0) {
                $('#dangercontactoemergencia').fadeIn();
                $('#errorPreInscricao').html($('#dangercontactoemergencia').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (grauContactoEmergencia.length == 0) {
                $('#dangergraucontactoemergencia').fadeIn();
                $('#errorPreInscricao').html($('#dangergraucontactoemergencia').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (dataAF.length == 0) {
                $('#dangerdataAF').fadeIn();
                $('#errorPreInscricao').html($('#dangerdataAF').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            if (dataTreino.length == 0) {
                $('#dangerdataTreino').fadeIn();
                $('#errorPreInscricao').html($('#dangerdataTreino').html());
                $("#errorPreInscricao").fadeIn();
                hideLoadingBackground();
                return;
            }

            $.ajax({
                type: "POST",
                url: "index.aspx/savePreInscricao",
                data: '{"nome":"' + name + '", "morada":"' + morada + '", "codpostal":"' + codpostal
                    + '", "localidade":"' + localidade + '", "tlf_emergencia":"' + (contactoEmergencia + ' ' + grauContactoEmergencia)
                    + '", "tlm":"' + tlf + '", "dataNascimento":"' + datanascimento + '", "cc":"' + ccnr
                    + '", "validadeCC":"' + validadecc + '", "profissao":"' + profissao
                    + '", "email":"' + email + '", "publicidade":"' + consentimento
                    + '", "dataAF":"' + (dataAF + ' 00:00') + '", "dataTreino":"' + dataTreino + '", "lingua":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        if (parseInt(val) < 0) {
                            hideLoadingBackground();
                            $('#errorPreInscricao').html($('#preinscricao_generic_error').html());
                            $("#errorPreInscricao").fadeIn().delay(emailDelay).fadeOut();
                        }
                        else {
                            callSendPreInscricaoMail(val, language);
                        }
                    }
                }
            });
        }

        function callSendPreInscricaoMail(id, language) {
            $.ajax({
                type: "POST",
                url: "emails.aspx/sendPreInscricaoEmail",
                data: '{"id_pre_inscricao":"' + id + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var val = dados[0];
                        var ret = dados[1];

                        hideLoadingBackground();
                        $('#successPreInscricao').html(ret);
                        $("#successPreInscricao").fadeIn();

                        if (parseInt(val) >= 0) {
                            $('#namePreInscricao').val('');
                            $('#emailPreInscricao').val('');
                            $('#telemovel').val('');
                            $('#morada').val('');
                            $('#codpostal').val('');
                            $('#localidade').val('');
                            $('#profissao').val('');
                            $('#ccnr').val('');
                            $('#validadecc').val('');
                            $('#datanascimento').val('');
                            $('#contactoemergencia').val('');
                            $('#graucontactoemergencia').val('');
                            $('#dataAF').val('');
                            $('#dataTreino').val('');
                            $('#checkboxConsentimento').prop('checked', false);
                            setTimeout(function () {
                                $("#successPreInscricao").fadeOut();
                            }, emailDelay);
                        }
                    }
                }
            });
        }

        function convertDateToStr(date) {
            var str = "";
            var minDay = parseInt(date.getDate());
            var minMonth = parseInt(date.getMonth()) + 1;
            var minYear = parseInt(date.getFullYear());

            if (minDay < 10) {
                str += '0' + minDay;
            }
            else {
                str += '' + minDay;
            }

            if (minMonth < 10) {
                str += '/0' + minMonth;
            }
            else {
                str += '/' + minMonth;
            }

            str += '/' + minYear;

            return str;
        }

        function setDatePicker() {
            var lingua = '';
            if ($('#fab').hasClass('pt')) {
                lingua = 'pt';
            }
            else if ($('#fab').hasClass('en')) {
                lingua = 'en';
            }
            else if ($('#fab').hasClass('fr')) {
                lingua = 'fr';
            }
            else if ($('#fab').hasClass('es')) {
                lingua = 'es';
            }

            var today = new Date();
            var cleanText = $('#datepicker_clean').html();
            var cancelText = $('#datepicker_cancel').html();
            var confirmText = $('#datepicker_confirm').html();

            //var minDate = getFirstDayOfMonth(new Date(today).getFullYear(), 0);
            //var maxDate = getLastDayOfMonth(new Date(today).getFullYear(), new Date(today).getMonth());
            //var minDueDate = getFirstDayOfMonth(new Date(today).getFullYear(), new Date(today).getMonth());
            //var maxDueDate = addMonths(1, new Date(minDueDate));// = addMonth(1, minDueDate);
            //maxDueDate = getLastDayOfMonth(maxDueDate.getFullYear(), maxDueDate.getMonth());
            
            //$('#validadecc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'auto top', autoclose: true, language: lingua });
            //$('#validadecc').datepicker('setDate', today);
            //$('#validadecc').val(convertDateToStr(today));
            $('#validadecc').bootstrapMaterialDatePicker({
                time: false,
                clearButton: true,
                format: 'DD/MM/YYYY',
                lang: lingua,
                cancelText: cancelText,
                clearText: cleanText,
                okText: confirmText,
                nowButton: false,
                switchOnClick: true,
                minDate: today
            });

            //$('#datanascimento').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'auto top', autoclose: true, language: lingua });
            //$('#datanascimento').datepicker('setDate', today);
            //$('#datanascimento').val(convertDateToStr(today));
            $('#datanascimento').bootstrapMaterialDatePicker({
                time: false,
                clearButton: true,
                format: 'DD/MM/YYYY',
                lang: lingua,
                cancelText: cancelText,
                clearText: cleanText,
                okText: confirmText,
                confirmText: confirmText,
                nowButton: false,
                switchOnClick: true
            });

            //$('#dataAF').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'auto top', autoclose: true, language: lingua });
            //$('#dataAF').datepicker('setDate', today);
            //$('#dataAF').val(convertDateToStr(today));
            $('#dataAF').bootstrapMaterialDatePicker({
                time: false,
                clearButton: true,
                format: 'DD/MM/YYYY',
                lang: lingua,
                cancelText: cancelText,
                clearText: cleanText,
                okText: confirmText,
                confirmText: confirmText,
                nowButton: false,
                switchOnClick: true,
                minDate: today
            });

            /*$('#horaAF').timepicker({
                timeFormat: 'HH:mm',
                interval: 30,
                minTime: '7:00',
                maxTime: '21:00',
                defaultTime: '10:00',
                startTime: '07:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });*/
            
            //$('#dataTreino').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'auto top', autoclose: true, language: lingua });
            //$('#dataTreino').datepicker('setDate', today);
            //$('#dataTreino').val(convertDateToStr(today));
            $('#dataTreino').bootstrapMaterialDatePicker({
                time: false,
                clearButton: true,
                format: 'DD/MM/YYYY',
                lang: lingua,
                cancelText: cancelText,
                clearText: cleanText,
                okText: confirmText,
                confirmText: confirmText,
                nowButton: false,
                switchOnClick: true,
                minDate: today
            });

            for (i = 1; i <= parseInt($('#nrServices').html()); i++) {
                //$('#dataFormService' + i).datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'auto top', autoclose: true, language: lingua });
                //$('#dataFormService' + i).datepicker('setDate', today);
                //$('#dataFormService' + i).val(convertDateToStr(today));
                $('#dataFormService' + i).bootstrapMaterialDatePicker({
                    time: false,
                    clearButton: true,
                    format: 'DD/MM/YYYY',
                    lang: lingua,
                    cancelText: cancelText,
                    clearText: cleanText,
                    okText: confirmText,
                    confirmText: confirmText,
                    nowButton: false,
                    switchOnClick: true,
                    minDate: today
                });

                $('#horaFormService' + i).bootstrapMaterialDatePicker({
                    shortTime: false,
                    date: false,
                    clearButton: true,
                    format: 'HH:mm',
                    lang: lingua,
                    cancelText: cancelText,
                    clearText: cleanText,
                    okText: confirmText,
                    confirmText: confirmText,
                    nowButton: false,
                    switchOnClick: true
                });
            }
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

        function setCampaign() {
            if ($('#divCampaign').html() != '') {
                $('#divCampaign').removeClass('variaveis');
            }

            // Create a timestamp
            var timestamp = new Date().getTime();

            // Get the image element
            var image = document.getElementById("imageCampaign");

            if (image != undefined && image != null) {
                // Adding the timestamp parameter to image src
                image.src = image.src + "?t=" + timestamp;
            }
        }
    </script>
</body>
</html>
