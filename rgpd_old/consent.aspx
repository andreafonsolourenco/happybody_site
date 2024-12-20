<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consent.aspx.cs" Inherits="consent" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Happy Body</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link href="bootstrap/dist/css/bootstrap-theme.min.css" rel="stylesheet" type='text/css'/>
    <link href="bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" type='text/css'/>
    <link href="bootstrap/dist/css/bootstrap-table.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.theme.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.structure.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.min.css" rel="stylesheet" type='text/css>' />
    <link rel="stylesheet" type="text/css" href="css/software.css" />

    <link href="alertify/css/alertify.min.css" rel="stylesheet" type='text/css'/>
    <link href="alertify/css/themes/semantic.min.css" rel="stylesheet" type='text/css'/>
    <link href="alertify/css/themes/default.min.css" rel="stylesheet" type='text/css'/>
    <!-- Bootstrap Date-Picker Plugin -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <!-- Clock Picker -->
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/dist/bootstrap-clockpicker.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/github.min.css" />
    <!-- Emojis -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

    <meta charset="utf-8" />

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
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%; height: 100%" id="content">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 header" id="header" runat="server">
            <img src="img/logo.png" style="width:auto; height: 100%"/>
        </div>

        <span style="display: none" id="pagelink" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 contentCenter" id="textContent" runat="server">
            <div style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; color: #000">CONSENTIMENTO DO TITULAR DE DADOS INFORMADO</div>
            Nos termos do RGPD, consinto que os meus dados pessoais (nome, email e telefone ou telemóvel) sejam utilizados para:<br />
            - Felicitações de aniversário / estatística;<br />
            - Divulgações de todas as informações, campanhas, horários e aulas;<br />
            - Marketing ou publicidade (novidades, preços do ginásio);<br />
            - Ofertas;<br />
            - Descontos;<br />
            - Newsletters, artigos e vídeos.<br /><br />
            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding" style="font-weight: bold;">
                <div class="checkbox nopadding" style="font-weight: bold;">
                    <label><input type="checkbox" id="autorizacao">Fui informado dos meus direitos de acesso, retificação, oposição e esquecimento.<br /> 
                    A conservação dos dados será efetuada desde a presente data até indicação do contrário via email para happybodyfitcoach@gmail.com<br /><br /></label>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                Nome: <input type="text" class='form-control' placeholder="Nome" id="name" />
            </div>
            <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding">Marcar Treino Experimental:</div>
                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                    <input type='text' class='form-control' id='data' placeholder='Data'/>
                </div>
                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                    <input type='text' class='form-control' id='hora' placeholder='Hora' />
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                Email: <input type="email" class='form-control' placeholder="Email" id="email" />
            </div>
            <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding">
                Telefone / Telemóvel: <input type="tel" class='form-control' placeholder="Telefone / Telemóvel" id="tlf" />
            </div>
        </div>

        <div class="footer">
            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding">
                <input type="button" class='form-control' value="Enviar" style="width: 100%;" onclick="checkValues();" />
            </div>
        </div>
    </div>

    <script type="text/javascript" src="js/jquery-3.2.0.js"></script>
    <script type="text/javascript" src="jquery/external/jquery/jquery.js"></script>
    <script type="text/javascript" src="bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="bootstrap/dist/js/bootstrap-table.js"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    
    <script type="text/javascript" src="alertify/alertify.min.js"></script>
    <!-- DateTime Picker -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="clockpicker/dist/bootstrap-clockpicker.min.js"></script>
    <script type="text/javascript" src="clockpicker/assets/js/highlight.min.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            $('#hora').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': 'now'
            });

            $('#data').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, orientation: 'bottom' });
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function sendEmail(nome, email, telefone, autorizo, treinooferecidopor) {
            $.ajax({
                type: "POST",
                url: "consent.aspx/sendEmailFromTemplate",
                data: '{"nome":"' + nome + '", "email":"' + email + '", "telefone":"' + telefone + '", "autorizo":"' + autorizo
                    + '", "treinooferecidopor":"' + treinooferecidopor + '", "link":"' + $('#pagelink').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //$('#textContent').html(res.d);
                        if (res.d == '0') {
                            alertify.message('Consetimento de dados enviado com sucesso!');
                            $('#email').val('');
                            $('#tlf').val('');
                            $('#name').val('');
                            $('#data').val('');
                            $('#hora').val('');
                            $('#autorizacao').removeAttr('checked');
                        }
                        else {
                            //alertify.message('Consetimento de dados enviado com sucesso!');
                            alertify.message('Ocorreu um erro ao enviar a declaração de consentimento de dados. Por favor, tente novamente. Obrigado.');
                        }
                    }
                }
            });
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function checkValues() {
            var autorizacao = "0";
            var nome = $('#name').val().trim();
            var email = $('#email').val().trim();
            var tlf = $('#tlf').val().trim();
            var data = $('#data').val().trim();
            var hora = $('#hora').val().trim();

            if ($('#autorizacao').is(":checked")) {
                autorizacao = "1";
            }

            if (autorizacao == '0') {
                alertify.message('Por favor, para enviar o consentimento tem de selecionar a respetiva opção! Obrigado.');
                return;
            }

            if (nome == '') {
                alertify.message('Por favor, preencha o nome de quem lhe ofereceu o treino antes de enviar o consentimento! Obrigado.');
                return;
            }

            if (data == '') {
                alertify.message('Por favor, preencha a data do treino experimental antes de enviar o consentimento! Obrigado.');
                return;
            }

            if (hora == '') {
                alertify.message('Por favor, preencha a hora do treino experimental antes de enviar o consentimento! Obrigado.');
                return;
            }

            if (tlf == '') {
                alertify.message('Por favor, preencha o seu telefone / telemóvel antes de enviar o consentimento! Obrigado.');
                return;
            }

            if (email == '') {
                alertify.message('Por favor, preencha o seu email antes de enviar o consentimento! Obrigado.');
                return;
            }

            if (email.indexOf('@') > -1) {

            }
            else {
                alertify.message('Por favor, insira um email válido antes de enviar o consentimento! Obrigado.');
                return;
            }

            var treinooferecidopor = data + ' ' + hora;

            alertify.confirm('Consetimento do titular de dados informado', 'Tem a certeza que deseja enviar a declaração de consentimento?',
                function () {
                    sendEmail(nome, email, tlf, autorizacao, treinooferecidopor);
                },
                function () { }).set('labels', { ok: 'Enviar', cancel: 'Cancelar' });
        }
    </script>
</body>
</html>
