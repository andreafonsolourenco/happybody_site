<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" Culture="auto" UICulture="auto" %>

<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' />
    <link href='http://fonts.googleapis.com/css?family=Roboto:400,500' rel='stylesheet' />
	<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,800,300' rel='stylesheet' type='text/css' />
    <link href="https://fonts.googleapis.com/css?family=Baloo" rel="stylesheet" type='text/css' />
    <link href="https://fonts.googleapis.com/css?family=Baloo|Open+Sans+Condensed:300" rel="stylesheet" type='text/css' />
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

    <script src="fingerprint/fingerprint2.js"></script>


    <title>Login</title>

    <script type="text/javascript">
        $(window).resize(function () {

        });

        $(document).ready(function () {
            window.history.pushState(null, "", window.location.href);
            window.onpopstate = function () {
                window.history.pushState(null, "", window.location.href);
            };

            //new Fingerprint2().get(function (result, components) {
            //    $('#lblname').html(result); // a hash, representing your device fingerprint
            //    //console.log(components) // an array of FP components
            //});
        });

        function getOperatorData() {
            $.ajax({
                type: "POST",
                url: "login.aspx/getOperator",
                data: '{"user":"' + $('#txtLogin').val() + '", "pass":"' + $('#txtPassword').val() + '", "pcname":"' + $('#lblname').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var result = res.d;

                        if (result != null) {
                            if (isInt(result)) {
                                encryptSessionID(result);
                            }
                            else {
                                $("#lblerror").html(res.d);
                                $('#lblerror').fadeIn();
                            }
                        }
                    }
                }
            });
        };

        function encryptSessionID(str) {
            $.ajax({
                type: "POST",
                url: "login.aspx/Encrypt",
                data: '{"str":"' + str + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        var result = res.d;

                        if (result != null) {
                            window.location = "MainMenu.aspx?id=" + result;
                            registaLog(result, 'LOGIN', 'Login efetuado com o user ' + $('#txtLogin').val());
                        }
                    }
                }
            });
        };

        function pedirAutorizacao() {
            if ($('#requestName').val().trim() != '') {
                $.ajax({
                    type: "POST",
                    url: "login.aspx/RequestLicence",
                    data: '{"name":"' + $('#requestName').val().trim() + '", "pcname":"' + $('#lblname').html() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            var result = res.d;

                            if (result != null) {
                                if (parseInt(result) <= 0) {
                                    $('#divNomeRequest').fadeIn();
                                    $('#divMensagem').html('Ocorreu um erro ao enviar pedido de ativação do dispositivo! Por favor, tente novamente!');
                                    $('#divMensagem').fadeIn();
                                }
                                else {
                                    $('#divNomeRequest').fadeOut();
                                    $('#divMensagem').html('O seu pedido de ativação foi enviado com sucesso! Por favor, aguarde até a Administração ativar. Obrigado!');
                                    $('#divMensagem').fadeIn();
                                }
                            }
                        }
                    }
                });
            }
            else {
                $('#divNomeRequest').fadeIn();
                $('#divMensagem').html('Por favor, insira um nome válido para efetuar o pedido!');
                $('#divMensagem').fadeIn();
            }
        };

        $(document).keypress(function (e) {
            if (e.which == 13) {
                getOperatorData();
            }
        });

        function closePopup() {
            $('#black_overlay').fadeOut();
            $('#divRequest').fadeOut();
        }

        function openPopup() {
            $('#black_overlay').fadeIn();
            $('#divNomeRequest').fadeIn();
            $('#divMensagem').fadeOut();
            $('#divRequest').fadeIn();
            $('#requestName').val('');
        }

        function isInt(value) {
            return !isNaN(value) && (function(x) { return (x | 0) === x; })(parseFloat(value))
        }

    </script>


    <style>
        body {
            background-color: #000 !important;
            border: 0 !important;
        }

        .page-wrap {
            position: fixed; 
            top: 50%; 
            left: 50%; 
            -webkit-transform: translate(-50%, -50%);
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            width: 75%;
            height: auto;
            opacity: 1;
            max-height: 90%;
        }

        .logoHappyBody {
            width: 100%;
            height: 100px;
            text-align: center;
        }

        .bg_login {
            width: 100%;
            background-color: red;
            height: 50px;
            color: #FFFFFF;
            font-size: large;
            font-family: 'Amaranth', sans-serif !important;
            vertical-align: middle;
            line-height: 50px;
            text-align: center;
        }

        .errorDiv {
            width: 100%;
            height: 50px;
            color: #FFFFFF;
            font-size: medium;
            font-family: 'Amaranth', sans-serif !important;
            vertical-align: middle;
            line-height: 50px;
            text-align: center;
            display:none;
        }

        input {
            padding-left: 50px !important; 
            height: 50px !important;
            font-size: large !important;
        }

        .login {
            background: gray url(img/icon_face.png) no-repeat left;
        }

        .pass {
            background: gray url(img/icon_pass.png) no-repeat left; 
        }

        .btn {
            background-color: #000 !important;
            text-align: center !important;
            -moz-transition: background-color 0.5s ease;
            -o-transition: background-color 0.5s ease;
            -webkit-transition: background-color 0.5s ease;
            transition: background-color 0.5s ease;
            color: #FFF !important;
            padding: 0 !important;
        }

        .btn:hover {
            background-color: red !important;
        }

        #black_overlay{
            display:none;
			position: fixed;
			top: 0%;
			left: 0%;
			width: 100%;
			height: 100%;
			background-color: black;
			z-index:1001;
			-moz-opacity: 0.8;
			opacity:0.8;
			filter: alpha(opacity=80);
		}
    </style>

</head>


<body id="login">
    <div id="page-wrap" class="page-wrap">
        <form runat="server">
            <div class="logoHappyBody">
                <img src="img/logo.png" style="height: 100%;" />
            </div>

            <div class="bg_login">
                Login
            </div>

            <div class="wrapper_login">
                <input type="text" class="form-control login" aria-label="Tag" id="txtLogin" runat="server" placeholder="Utilizador">
                <input type="password" class="form-control pass" aria-label="Tag" id="txtPassword" runat="server" placeholder="Password">
                <input type="button" class="form-control btn" value="Entrar" onclick="getOperatorData();"/>
            </div>

            <div id="lblerror" align="center" class="errorDiv" runat="server"></div>

            <div id="lblPedirAutorizacao" align="center" runat="server" style="color:#FFF; cursor: pointer; margin-top: 20px; display:none" onclick="openPopup();">Pedir Autorização do Dispositivo</div>
        </form>
    </div>

    <div id="black_overlay" onclick="closePopup();">
        <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closePopup();" />
    </div>
    <div id="divRequest" style="display:none; z-index: 1100; height: 50%; padding: 5px; width: 75%; margin: auto; position: absolute; left:0; right: 0; top: 0; bottom: 0; background-color: #FFF; color: #000;">
        <div id="divNomeRequest">
            Nome:
            <input type="text" class='form-control' id='requestName' placeholder='Nome' required="required" style="width: 100%; margin: auto;"/>
            <input type="button" class='form-control' id='confirmar' value='Confirmar' style="width: 100%; margin: auto;" onclick="pedirAutorizacao();"/>
        </div>
        <div id="divMensagem" style="display:none; font-weight: bold; width: 100%; text-align: center; margin-bottom: 20px; color: red;">
            
        </div>
    </div>

    <span style="display:none" runat="server" id="lblip"></span>
    <span style="display:none;" runat="server" id="lblname"></span>
</body>
</html>
