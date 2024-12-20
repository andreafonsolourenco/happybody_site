<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
    <META HTTP-EQUIV="EXPIRES" CONTENT="Mon, 22 Jul 2002 11:12:01 GMT">

	<title>HappyBody - Menu</title>
	<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link href="bootstrap/dist/css/bootstrap-theme.min.css" rel="stylesheet" type='text/css'/>
    <link href="bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.theme.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.structure.min.css" rel="stylesheet" type='text/css'/>
    <link href="jquery/jquery-ui.min.css" rel="stylesheet" type='text/css>' />
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type='text/css'/>
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
    
    <style>
        .menuTitles {
            padding-top:5px; 
            padding-bottom:5px;
            width:100%;
            height: auto;
            font-weight: bold;
            font-family: 'Noto Sans', sans-serif;
        }

        .menuLine {
            padding-top:5px; 
            padding-bottom:5px;
            width:100%;
            height: auto;
            font-family: 'Noto Sans', sans-serif;
        }

        .menuLine:hover {
            background-color: #E0E0E0;
        }

        .clickable {
            cursor: pointer;
        }

        .subMenuLine {
            padding-left:5px;
            background-color:#D3D3D3;
            display: none;
        }

        .menu {
            background-color:#BDBDBD; 
            height:100%;
        }

        .black_overlay{
            display:none;
			position: fixed;
			top: 0%;
			left: 0%;
			width: 100%;
			height: 100%;
			background-color: black;
			z-index:1001;
			-moz-opacity: 0.6;
			opacity:.60;
			filter: alpha(opacity=60);
		}

        .divPopUp {
            position: fixed;
            top: 50%;
            left: 50%;
            -webkit-transform: translate(-50%, -50%);
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            display: none;
            width: 75%;
            height: auto;
            z-index: 1002;
            background-color: #000;
        }

        input {
            padding-left: 50px !important; 
            height: 50px !important;
            font-size: 1.5vw !important;
        }

        .login {
            background: gray url(img/icon_face.png) no-repeat left;
        }

        .pass {
            background: gray url(img/icon_pass.png) no-repeat left; 
        }
    </style>
</head>

<body>
    <div style="height:auto; width:100%;" id="header">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border-bottom: 10px solid red; height: 100px; margin-top: 5px;">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="float:left; vertical-align:middle; text-align:center; background: url(img/logo.png) no-repeat left center; background-size:contain; height:100%;"></div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="float:right; vertical-align:middle; text-align:center; height: 100%; cursor:pointer;" id="divPhotoOperator" runat="server"></div>
            <span class="variaveis" id="lbloperatorid" runat="server"></span>
            <span class="variaveis" id="lbloperatorcode" runat="server"></span>
            <span class="variaveis" id="lblcv" runat="server"></span>
            <span class="variaveis" id="lblavaliacoestresdias" runat="server"></span>
            <span class="variaveis" id="lblavaliacoeshoje" runat="server"></span>
            <span class="variaveis" id="lblavisos" runat="server"></span>
            <span class="variaveis" id="lblip" runat="server"></span>
            <span class="variaveis" id="lblmacaddress" runat="server"></span>
            <span class="variaveis" id="lblmachinename" runat="server"></span>
            <span class="variaveis" id="lblnremailsnovos" runat="server"></span>
            <span class="variaveis" id="lblultimadataemail" runat="server"></span>
        </div>
    </div>

    <div style="width:100%;" id="content">
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 menu" id="menu" runat="server"></div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" id="framePage" runat="server">
            <span style="font-family: 'Amaranth', sans-serif !important; font-size: xx-large; text-align: center">Bem-Vindo ao Software Happy Body!</span>
        </div>
    </div>

    <div id="div_mudar_user" class="black_overlay">
        <div id="divClose" style="width: 100%; height: 50px; top: 0; left: 0; text-align: right;" onclick="closeChangeUser();"><img src="img/icons/icon_close.png" style="width: auto; height: 100%; cursor: pointer;"/></div>
    </div>

    <div class="divPopUp" id="divPopUp">
        <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding: 5px;">
                    <input type="text" onfocus="focusUserPass(1);" onfocusout="focusUserPass(0);" class="form-control login" id="txtLogin" runat="server" placeholder="Utilizador" />
                    <input type="password" onfocus="focusUserPass(1);" onfocusout="focusUserPass(0);" class="form-control pass" id="txtPassword" runat="server" placeholder="Password" />
                </td>
            </tr>
            <tr>
                <td style="height:5px;"></td>
            </tr>
            <tr>
                <td>
                    <span style="color: red; display: none" id="lblerror"></span>
                </td>
            </tr>
            <tr>
                <td style="height:5px;"></td>
            </tr>
            <tr>
                <td style="float:right; padding:5px;">
                    <input id='btnCancel' value='Cancelar' runat='server' type='button' onclick='closeChangeUser();' style='background: #4282b5 url(img/icons/no.png) no-repeat center left; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                    <input id='btnConfirm' value='Confirmar' runat='server' type='button' onclick='getOperatorData();' style='background: #4282b5 url(img/icons/yes.png) no-repeat center left; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                </td>
            </tr>
        </table>
    </div>

    
    <script type="text/javascript" src="js/jquery-3.2.0.js"></script>
    <script type="text/javascript" src="jquery/external/jquery/jquery.js"></script>
    <script type="text/javascript" src="bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    
    <script type="text/javascript" src="alertify/alertify.min.js"></script>
    <!-- DateTime Picker -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="clockpicker/dist/bootstrap-clockpicker.min.js"></script>
    <script type="text/javascript" src="clockpicker/assets/js/highlight.min.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var flag = 0;
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            window.history.pushState(null, "", window.location.href);        
            window.onpopstate = function() {
                window.history.pushState(null, "", window.location.href);
            };

            $('#content').height($(window).height() - 100);
            $('#menu').height($('#content').height());
            $('#framePage').height($('#content').height());

            var avisoAvaliacoes = "";
            var avisoCandidaturasEmails = "";

            if ($('#lblcv').html() != "0")
                avisoCandidaturasEmails += "Tem candidaturas novas por visualizar!";

            if ($('#lblnremailsnovos').html() != "0") {
                if (avisoCandidaturasEmails != "") {
                    avisoCandidaturasEmails += "<br />";
                }
                avisoCandidaturasEmails += "Tem novos emails por visualizar!";
            }

            if (avisoCandidaturasEmails != "")
                alertify.alert("AVISO!", avisoCandidaturasEmails);

            if ($('#lblavaliacoeshoje').html() != "")
                avisoAvaliacoes += "Os seguintes sócios têm avaliações marcadas para hoje: <br />" + $('#lblavaliacoeshoje').html();

            if ($('#lblavaliacoestresdias').html() != "")
                avisoAvaliacoes += "Os seguintes sócios têm avaliações marcadas para daqui a 3 dias: <br />" + $('#lblavaliacoestresdias').html();

            if(avisoAvaliacoes != '')
                alertify.alert('AVISO!', avisoAvaliacoes);

            if ($('#lblavisos').html() != "")
                alertify.alert('AVISO!', $('#lblavisos').html());

            setInterval(function () {
                verifyNewEmail();
            }, 60000);
        });

        $(window).on('resize', function () {
            $('#content').height($(window).height() - 100);
            $('#menu').height($('#content').height());
            $('#framePage').height($('#content').height());
        });

        $(window).scroll(function () {

        });

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if(flag==1)
                    getOperatorData();
            }
        });

        function focusUserPass(x) {
            flag = x;
        }

        function verifyNewEmail() {
            $.ajax({
                type: "POST",
                url: "MainMenu.aspx/countEmailsReceivedAfterLastDate",
                data: '{"lastdate":"' + $('#lblultimadataemail').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var dados = res.d.split('<#SEP#>');
                        var ultimaData = dados[0];
                        var existeEmailNovo = dados[1];

                        if (parseInt(existeEmailNovo) == 1) {
                            alertify.alert('AVISO!', 'Recebeu emails novos!');
                        }

                        $('#lblultimadataemail').html(ultimaData);
                    }
                }
            });

        };

        function getOperatorData() {
            $.ajax({
                type: "POST",
                url: "MainMenu.aspx/getOperator",
                data: '{"user":"' + $('#txtLogin').val() + '", "pass":"' + $('#txtPassword').val() + '", "idAntigo":"' + $('#lblmacaddress').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var result = res.d;

                        if (result != null) {
                            if (result == "Utilizador não encontrado!") {
                                window.location = "login.aspx?msg=" + result;
                            }
                            else {
                                registaLog($('#lbloperatorid').html(), 'LOGOUT', 'Logout efetuado');
                                registaLog(result, 'LOGIN', 'Login efetuado com o user ' + $('#txtLogin').val());
                                encryptSessionID(result);
                            }
                        }
                    }
                }
            });

        };

        function cancelSession() {
            $.ajax({
                type: "POST",
                url: "MainMenu.aspx/cancelSession",
                data: '{"sessionID":"' + $('#lblmacaddress').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var result = res.d;

                        if (result != null) {
                            ret();
                        }
                    }
                }
            });

        };

        function botaoSocios() {
            window.location.href = 'inserirsocio.aspx?id=' + $('#lbloperatorid').html();
        }

        function exit() {
            alertify.confirm('Sair', 'Deseja sair do programa?', function () { cancelSession(); }
                , function () {  }).set('labels', { ok: 'Sair', cancel: 'Cancelar' });
        }

        function ret() {
            window.location.href = 'login.aspx';
            registaLog($('#lbloperatorid').html(), 'LOGOUT', 'Logout efetuado');
        }

        function closeChangeUser() {
            $('#div_mudar_user').fadeOut();
            $('#divPopUp').fadeOut();
        }

        function openPage(x) {
            //alert(x);
            var submenus = "#submenus_" + x.toString();
            var page = "#pagina_" + x.toString();
            var title = "#titulo_" + x.toString();
            var nrMenus = parseInt($('#nrMenus').html());

            if ($(title).html() == "Mudar de Utilizador") {
                $('#div_mudar_user').fadeIn();
                $('#divPopUp').fadeIn();
            }
            else {
                for (i = 0; i < nrMenus; i++) {
                    var xpto = "#submenus_" + i.toString();
                    if ($(xpto).html() != "0") {
                        for (j = 0; j < parseInt($(xpto).html()) ; j++) {
                            var sub = "#subMenu_" + i.toString() + "_" + j.toString();
                            $(sub).fadeOut();
                        }
                    }
                }

                if ($(submenus).html() == "0") {
                    if ($(title).html() == "Sair") {
                        exit();
                    }
                    else {
                        var p = "<h2 id='titlePageSelected' style='font-family: 'Noto Sans', sans-serif !important; font-size:large; border-bottom: 1px black solid; margin-bottom:2px; height:auto; line-height:normal;'>" + $(title).html() + "</h2>"
                        + "<iframe src='" + $(page).html() + "?id=" + $('#lbloperatorid').html() + "' style='width:100%; overflow-y: hidden;' frameBorder='0' id='pageSelected'></iframe>";

                        $('#framePage').html(p);
                        $('#pageSelected').height($('#framePage').height() - $('#titlePageSelected').height());
                        registaLog($('#lbloperatorid').html(), $(title).html().split(" ").join(""), 'Seleção do menu para a página ' + $(title).html());
                    }
                }
                else {
                    for (i = 0; i < parseInt($(submenus).html()) ; i++) {
                        var sub = "#subMenu_" + x.toString() + "_" + i.toString();
                        $(sub).fadeIn();
                    }
                }
            }
        }

        function openSubMenuPage(x, y) {
            var page = "#pagina_" + x.toString() + "_" + y.toString();
            var title = "#titulo_" + x.toString() + "_" + y.toString();

            var p = "<h2 id='titlePageSelected' style='font-family: 'Noto Sans', sans-serif !important; font-size:large; border-bottom: 1px black solid; margin-bottom:2px; height:auto; line-height:normal;'>" + $(title).html() + "</h2>"
                        + "<iframe src='" + $(page).html() + "?id=" + $('#lbloperatorid').html() + "' style='width:100%; overflow-y: hidden;' frameBorder='0' id='pageSelected'></iframe>";

            $('#framePage').html(p);
            $('#pageSelected').height($('#framePage').height() - $('#titlePageSelected').height());
            registaLog($('#lbloperatorid').html(), $(title).html().split(" ").join(""), 'Seleção do submenu para a página ' + $(title).html());
        }

        function encryptSessionID(str) {
            $.ajax({
                type: "POST",
                url: "MainMenu.aspx/Encrypt",
                data: '{"str":"' + str + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var result = res.d;

                        if (result != null) {
                            window.location = "MainMenu.aspx?id=" + result;
                        }
                    }
                }
            });
        };
    </script>

  </body> 
</html>
