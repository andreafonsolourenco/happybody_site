<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consent.aspx.cs" Inherits="consent" Culture="auto" UICulture="auto" %>
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

        .contentJustify {
            margin-top: 110px;
            margin-bottom: 110px;
            text-align: justify;
            font-family: 'Roboto', sans-serif;
            font-size: 14px;
        }

        .nopadding {
            padding: 0 !important;
            margin: 0 !important;
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
<body style="background-color: #FFF !important">
    <div style="width: 100%; height: 100%" id="content">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 header" id="header" runat="server">
            <img src="../general/img/logo.png" style="width:auto; height: 100%"/>
        </div>

        <span style="display: none" id="pagelink" runat="server"></span>
        <span style="display: none" id="language" runat="server"></span>
        <span style="display: none" id="clean" runat="server"></span>
        <span style="display: none" id="cancel" runat="server"></span>
        <span style="display: none" id="confirm" runat="server"></span>
        <span style="display: none" id="validfield" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 contentJustify" id="textContent" runat="server" style="padding-top: 10px;"></div>

        <div class="footer">
            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding" id="footerDiv" runat="server"></div>
        </div>
    </div>

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
            setDatePicker();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function sendEmail(nome, email, telefone, autorizo, treinooferecidopor) {
            //$.ajax({
            //    type: "POST",
            //    url: "consent.aspx/sendEmailFromTemplate",
            //    data: '{"nome":"' + nome + '", "email":"' + email + '", "telefone":"' + telefone + '", "autorizo":"' + autorizo
            //        + '", "treinooferecidopor":"' + treinooferecidopor + '", "link":"' + $('#pagelink').html() + '"}',
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (res) {
            //        if (res.d != null) {
            //            //$('#textContent').html(res.d);
            //            if (res.d == '0') {
            //                alertify.message('Consetimento de dados enviado com sucesso!');
            //                $('#email').val('');
            //                $('#tlf').val('');
            //                $('#name').val('');
            //                $('#data').val('');
            //                $('#hora').val('');
            //                $('#autorizacao').removeAttr('checked');
            //            }
            //            else {
            //                //alertify.message('Consetimento de dados enviado com sucesso!');
            //                alertify.message('Ocorreu um erro ao enviar a declaração de consentimento de dados. Por favor, tente novamente. Obrigado.');
            //            }
            //        }
            //    }
            //});
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function checkValues() {
            //var autorizacao = "0";
            //var nome = $('#name').val().trim();
            //var email = $('#email').val().trim();
            //var tlf = $('#tlf').val().trim();
            //var data = $('#data').val().trim();
            //var hora = $('#hora').val().trim();

            //if ($('#autorizacao').is(":checked")) {
            //    autorizacao = "1";
            //}

            //if (autorizacao == '0') {
            //    alertify.message('Por favor, para enviar o consentimento tem de selecionar a respetiva opção! Obrigado.');
            //    return;
            //}

            //if (nome == '') {
            //    alertify.message('Por favor, preencha o nome de quem lhe ofereceu o treino antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //if (data == '') {
            //    alertify.message('Por favor, preencha a data do treino experimental antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //if (hora == '') {
            //    alertify.message('Por favor, preencha a hora do treino experimental antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //if (tlf == '') {
            //    alertify.message('Por favor, preencha o seu telefone / telemóvel antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //if (email == '') {
            //    alertify.message('Por favor, preencha o seu email antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //if (email.indexOf('@') > -1) {

            //}
            //else {
            //    alertify.message('Por favor, insira um email válido antes de enviar o consentimento! Obrigado.');
            //    return;
            //}

            //var treinooferecidopor = data + ' ' + hora;

            //alertify.confirm('Consetimento do titular de dados informado', 'Tem a certeza que deseja enviar a declaração de consentimento?',
            //    function () {
            //        sendEmail(nome, email, tlf, autorizacao, treinooferecidopor);
            //    },
            //    function () { }).set('labels', { ok: 'Enviar', cancel: 'Cancelar' });
        }

        function setDatePicker() {
            var lingua = $('#language').html();

            switch (lingua) {
                case 'PT': lingua = 'pt'; break;
                case 'EN': lingua = 'en'; break;
                case 'ES': lingua = 'es'; break;
                case 'FR':
                default:
                    lingua = 'fr';
                    break;
            }

            var today = new Date();
            var cleanText = $('#clean').html();
            var cancelText = $('#cancel').html();
            var confirmText = $('#confirm').html();

            $('#data').bootstrapMaterialDatePicker({
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

            $('#hora').bootstrapMaterialDatePicker({
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

        function validatePhoneNumber(x) {
            /*if (x.length < 9) {
                return false;
            }*/

            if (x === 'NULL' || x === '' || x === null || x === undefined) {
                return false;
            }

            return true;
        }

        function validateZipCode(str) {
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

        function sendRGPDEmail() {
            var autorizacao = "0";
            var nome = $('#name').val().trim();
            var email = $('#email').val().trim();
            var tlf = $('#tlf').val().trim();
            var data = $('#data').val().trim();
            var hora = $('#hora').val().trim();
            var error = $('#validfield').html();

            if ($('#autorizacao').is(":checked")) {
                autorizacao = "1";
            }
            else {
                error = error.replace('[FIELD]', $('#title').html());
                alertify.error(error);
                return;
            }

            if (nome == '' || nome.length == 0 || nome == null || nome == undefined) {
                error = error.replace('[FIELD]', $('#namelabel').html());
                alertify.error(error);
                return;
            }

            if (data == '' || data.length == 0 || data == null || data == undefined) {
                error = error.replace('[FIELD]', $('#datalabel').html());
                alertify.error(error);
                return;
            }

            if (hora == '' || hora.length == 0 || hora == null || hora == undefined) {
                error = error.replace('[FIELD]', $('#horalabel').html());
                alertify.error(error);
                return;
            }

            if (!validatePhoneNumber(tlf)) {
                error = error.replace('[FIELD]', $('#tlflabel').html());
                alertify.error(error);
                return;
            }

            if (!mailValidation(email)) {
                error = error.replace('[FIELD]', $('#emaillabel').html());
                alertify.error(error);
                return;
            }

            var treinooferecidopor = data + ' ' + hora;

            var language = $('#language').html();

            $.ajax({
                type: "POST",
                url: "consent.aspx/saveRGPDConsent",
                data: '{"nome":"' + nome + '", "email":"' + email + '", "telefone":"' + tlf + '", "autorizo":"' + autorizacao
                    + '", "treinooferecidopor":"' + treinooferecidopor + '", "link":"' + $('#pagelink').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '0') {
                            $.ajax({
                                type: "POST",
                                url: "../happybody/emails.aspx/sendRGPDConsentEmail",
                                data: '{"name":"' + nome + '", "email":"' + email + '", "tlf":"' + tlf + '", "date":"' + data + '", "hour":"' + hora + '", "language":"' + language + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (res) {
                                    if (res.d != null) {
                                        var dados = res.d.split('<#SEP#>');
                                        var val = dados[0];
                                        var ret = dados[1];

                                        if (parseInt(val) >= 0) {
                                            $('#email').val('');
                                            $('#tlf').val('');
                                            $('#name').val('');
                                            $('#data').val('');
                                            $('#hora').val('');
                                            $('#autorizacao').removeAttr('checked');
                                            alertify.message(ret);
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            });
        }
    </script>
</body>
</html>
