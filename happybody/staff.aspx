<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff.aspx.cs" Inherits="staff" Culture="auto" UICulture="auto" %>

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

        .recrutamento_messages {
            margin-top:10px;
            margin-bottom: 10px;
            font-size: 1.5rem;
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
    <header class="text-white text-center background_page masthead w-100 h-100" id="header">
        <div class="container d-flex align-items-center flex-column w-100 h-100">
            <!-- Masthead Heading -->
            <h1 class="masthead-heading text-uppercase mb-0" id="pageTitle" runat="server" style="margin: auto !important"></h1>
        </div>
    </header>

    <!-- Portfolio Section -->
  <section class="page-section portfolio" id="staff">
    <div class="container">

      <!-- Portfolio Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-secondary mb-0" id="staffTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- Portfolio Grid Items -->
      <div class="row" id="staffRow" runat="server"></div>

    </div>
  </section>

    <!-- About Section -->
  <section class="page-section bg-primary text-white mb-0" id="recrutamento">
    <div class="container">

      <!-- About Section Heading -->
      <h2 class="page-section-heading text-center text-uppercase text-white" id="recTitle" runat="server"></h2>

      <!-- Icon Divider -->
      <div class="divider-custom divider-light">
        <div class="divider-custom-line"></div>
        <div class="divider-custom-icon">
          <i class="fas fa-star"></i>
        </div>
        <div class="divider-custom-line"></div>
      </div>

      <!-- About Section Content -->
      <div class="row" id="recrutamentoFormDiv" runat="server">
          <form id="formUploadCV" runat="server" class="w-100 variaveis">
              <div class='col-lg-12 mx-auto placeholdercolor text-center variaveis'>
                  <asp:TextBox ID="name" runat="server" TextMode="SingleLine" name="name" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="email" runat="server" TextMode="SingleLine" name="email" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="tlf" runat="server" TextMode="SingleLine" name="tlf" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="type" runat="server" TextMode="SingleLine" name="type" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="text" runat="server" TextMode="MultiLine" Rows="10" name="text" CssClass='form-control placeholdercolor' />
                  <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="variaveis" />
                  <asp:Button runat="server" ID="recrutamentoButton" OnClick="VemTrabalhar_Click" CssClass="btn btn-light btn-xl mw-100 w-100 variaveis" />
                  <asp:TextBox ID="error" runat="server" TextMode="SingleLine" name="error" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="success" runat="server" TextMode="SingleLine" name="success" CssClass='form-control placeholdercolor' />
                  <asp:TextBox ID="idCandidatura" runat="server" TextMode="SingleLine" name="idCandidatura" CssClass='variaveis' />
              </div>
        </form>
          <div class="w-100" runat="server" id="recrutamentoVisible">

          </div>
      </div>

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

    <span id="nrStaff" runat="server" class="variaveis"></span>
    <span id="separator" runat="server" class="variaveis"></span>
    <div id="divStaffDescription" runat="server"></div>
    <div id="divBackgroundImages" runat="server" class="variaveis"></div>
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
        var anyfileuploadstring = "";

        $(document).ready(function () {
            $('#header').css("background-image", "url(" + imgBackgroundSrc + ")");
            imgId++;
            setInterval(function () {
                changeBackgroundImage();
            }, 5000);

            $('#recrutamentoError').fadeOut();
            $('#recrutamentoSuccess').fadeOut();

            var errorMsgOnServer = $('#error').val();
            var successMsgOnServer = $('#success').val();

            var separator = $('#separator').html();
            if (separator != '' && errorMsgOnServer == '' && successMsgOnServer == '') {
                $('a[href=\'#' + separator + '\']').click();
            }

            calcFabLanguageAndSound();

            anyfileuploadstring = $('#fileUploadedName').html();

            $('#FileUploadControl').change(function () {
                var path = $(this).val();
                if (path != '' && path != null) {
                    var q = path.substring(path.lastIndexOf('\\') + 1);
                    $('#fileUploadedName').html('<br />' + q);
                }
                else {
                    $('#fileUploadedName').html(anyfileuploadstring);
                }
            });

            if (errorMsgOnServer != '' || successMsgOnServer != '') {
                if (errorMsgOnServer != '') {
                    $('#recrutamentoError').html(errorMsgOnServer);
                    $('#recrutamentoError').fadeIn();
                }

                if (successMsgOnServer != '') {
                    $('#recrutamentoSuccess').html(successMsgOnServer);
                    $('#recrutamentoSuccess').fadeIn();
                    callSendRecruitmentMail($('#idCandidatura').val());
                }

                $("html, body").animate({
                    scrollTop: $(
                        'html, body').get(0).scrollHeight
                }, 2000);
            }
        });

        $(window).on('resize', function () {
            calcFabLanguageAndSound();
        });

        $(window).scroll(function () {
            
        });

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
                window.location.href = "staff.aspx?language=" + language + sep;
            }
            else if (window.location.href.includes('happybody.pt')) {
                window.location.href = "https://happybody.pt/" + server + "staff.aspx?language=" + language + sep;
            }
            else {
                window.location.href = "https://happybody.site/" + server + "staff.aspx?language=" + language + sep;
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

        function simulateClickOnFileUploadButton() {
            $('#FileUploadControl').click();
        }

        function simulateClickOnSendButton() {
            var errorMsgFile = $('#errorMsgFile').html();
            var errorMsgName = $('#dangerNomeRecrutamento').html();
            var errorMsgEmail = $('#dangerEmailRecrutamento').html();
            var errorMsgTlf = $('#dangerTelefoneRecrutamento').html();
            var errorMsgText = $('#dangerTextoRecrutamento').html();
            var filename = $('#fileUploadedName').html();
            var nome = $('#nomeRecrutamento').val();
            var email = $('#emailRecrutamento').val();
            var telefone = $('#telefoneRecrutamento').val();
            var text = $('#textoRecrutamento').val();
            var type = $('#tipoRecrutamento').val();
            $('#recrutamentoError').fadeOut();
            $('#recrutamentoSuccess').fadeOut();
            $('#recrutamentoError').html('');
            $('#recrutamentoSuccess').html('');

            if (filename.indexOf(errorMsgFile) !== -1) {
                $('#recrutamentoError').html(errorMsgFile);
                $('#recrutamentoError').fadeIn();
                return;
            }

            if (!textValidation(nome)) {
                $('#recrutamentoError').html(errorMsgName);
                $('#recrutamentoError').fadeIn();
                return;
            }

            if (!mailValidation(email)) {
                $('#recrutamentoError').html(errorMsgEmail);
                $('#recrutamentoError').fadeIn();
                return;
            }

            if (!textValidation(telefone)) {
                $('#recrutamentoError').html(errorMsgTlf);
                $('#recrutamentoError').fadeIn();
                return;
            }

            if (!textValidation(text)) {
                $('#recrutamentoError').html(errorMsgText);
                $('#recrutamentoError').fadeIn();
                return;
            }

            $('#name').val(nome);
            $('#type').val(type);
            $('#email').val(email);
            $('#tlf').val(telefone);
            $('#text').val(text);
            $('#error').val('');
            $('#success').val('');

            $('#recrutamentoButton').click();
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

            return (str && str.length > 0);
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

        function callSendRecruitmentMail(id) {
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
                url: "emails.aspx/sendRecruitmentEmail",
                data: '{"idRecrutamento":"' + id + '", "language":"' + language + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        
                    }
                }
            });
        }
    </script>
</body>
</html>
