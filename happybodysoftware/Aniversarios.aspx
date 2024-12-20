<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Aniversarios.aspx.cs" Inherits="Aniversarios" Culture="auto" UICulture="auto" %>
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

    <meta charset="utf-8" />

    <style type="text/css">
        .header {
            background-color: #000;
            position:fixed;
            top: 0;
            width: 100%;
            height: 100px;
            z-index: 100;
            padding:5px;
            line-height: 100px;
            font-family: 'Amaranth', sans-serif;
            color: #FFF;
            font-size: 2vw;
        }

        .footer {
            font-family: 'Amaranth', sans-serif;
            bottom: 0; 
            height:80px;
            width: 100%; 
            position:fixed;
            background-color: #000;
            text-align:center; 
            border-top:3px solid #e6e7e8;
            vertical-align:middle;
            z-index:101;
        }

        .line {
            margin-bottom: 5px;
            margin-top: 5px;
        }

        input {
            color: #000 !important;
        }

        .btn { 
            width: auto; 
            height: 40px; 
            font-size: 1.5vw; 
            line-height: 40px; 
            color: #000; 
            cursor: pointer; 
            vertical-align: middle; 
            border: none; 
            margin: 20px 2px 20px 2px; 
            padding: 0 10px; 
            -moz-border-radius: 2px; 
            -webkit-border-radius: 2px; 
            border-radius: 2px;
            background-color: red !important;
            text-align: center !important;
        }

        .btn:hover {
            background-color: red !important;
        }

        .variaveis {
            display: none;
        }

        table {
            width:100%; 
            height: auto;
            font-family: 'Noto Sans', sans-serif !important;
        }

        table thead {
            background-color:#000; 
            color: #FFF; 
            font-size: large; 
            font-weight: bold;
        }

            table thead tr {
                height: 50px;
                -moz-border-radius: 4px !important;
                -webkit-border-radius: 4px !important;
                border-radius: 4px !important;
            }

        table thead tr th {
            padding: 5px;
        }

        .headerLeft {
            width: 70%;
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
        }

        .headerRight {
            width: 30%;
            border-left: 1px red solid;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
        }

        .headerColspan {
            width: 100%;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
        }
        
        table tbody {
            background-color:#FFF;
            color:#000;
            font-size: medium;
            border: 1px #000 solid; 
        }

            table tbody tr {
                height: 40px;
                -moz-border-radius: 4px !important;
                -webkit-border-radius: 4px !important;
                border-radius: 4px !important;
                /*cursor: pointer;*/
            }

        table tbody tr:hover {
            /*background-color: #D3D3D3;*/
        }

        .tbodyTrSelected {
            background-color: red;
        }

        .tbodyTrSelected:hover {
            background-color: red;
        }

        .tbodyTrTdLeft {
            width: 75%;
        }

        .tbodyTrTdRight {
            width: 25%;
        }

        table tbody tr td {
            padding: 5px;
            border: 1px #000 solid;
        }

        .btnTop {
            width: auto;
            height: auto; 
            font-size: 12pt; 
            text-align: center; 
            line-height: 40px; 
            color: #FFFFFF; 
            cursor: pointer; 
            vertical-align: middle; 
            border: none; 
            margin: 20px 2px 20px 2px; 
            padding: 0 10px; 
            -moz-border-radius: 2px; 
            -webkit-border-radius: 2px; 
            border-radius: 2px;
        }

        .showCustomerInfo {
            width:100% !important;
            height:100% !important;
            border-collapse:collapse !important;
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
			opacity:.80;
			filter: alpha(opacity=80);
		}

        .popup{
            display:none;
			position: fixed;
			width: 90%;
			height: auto;
            max-height: 95%;
			background-color: #FFF;
			z-index:1002;
            padding: 10px;
		}

        .removeScroll {
            overflow: hidden;
        }
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divBtns" runat="server" style="margin-bottom: 25px;">
            <input id="btnGrid" type="button" class="form-control" value="Mensal" style="width: 45%; float: left; margin: auto; height: 40px; font-size: small" onclick="changeGrid();" />
            <input type="button" class="form-control" value="Exportar" style="width: 45%; float: right; margin: auto; height: 40px; font-size: small" onclick="exportar();" />
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable" runat="server" style="margin-bottom: 25px;">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divTableMes" runat="server">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divVariaveis" runat="server">
            
        </div>

        <div id="black_overlay" onclick="closePopup();">
            <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closePopup();" />
        </div>

        <div id="popup" class="popup" runat="server">
            Assunto
            <textarea class='form-control' id='emailSubjectText' style='width: 100%; margin: auto; height: auto; margin-bottom: 5px;' rows='2'></textarea>

            Introdução
            <textarea class='form-control' id='emailIntroText' style='width: 100%; margin: auto; height: auto; margin-bottom: 5px;' rows='2'></textarea>

            Texto do Email
            <textarea class='form-control' id='emailBodyText' style='width: 100%; margin: auto; height: auto; margin-bottom: 5px;' rows='10'></textarea>

            <input type='button' class='form-control' value='Cancelar' style='width: 48%; margin-bottom: 5px; float: left;' onclick='closePopup();' />
            <input type='button' class='form-control' value='Enviar Email' style='width: 48%; margin-bottom: 5px; float: right;' onclick='sendEmail();' />
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

    <script type="text/javascript" src="js/jquery.btechco.excelexport.js"></script>
    <script type="text/javascript" src="js/jquery.base64.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var linhaSelecionada;
        var nrSocio;
        var tipo;
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        (function ($) {
            $.fn.extend({
                center: function (options) {
                    var options = $.extend({ // Default values
                        inside: window, // element, center into window
                        transition: 0, // millisecond, transition time
                        minX: 0, // pixel, minimum left element value
                        minY: 0, // pixel, minimum top element value
                        withScrolling: true, // booleen, take care of the scrollbar (scrollTop)
                        vertical: true, // booleen, center vertical
                        horizontal: true // booleen, center horizontal
                    }, options);
                    return this.each(function () {
                        var props = { position: 'absolute' };
                        if (options.vertical) {
                            var top = ($(options.inside).height() - $(this).outerHeight()) / 2;
                            if (options.withScrolling) top += $(options.inside).scrollTop() || 0;
                            top = (top > options.minY ? top : options.minY);
                            $.extend(props, { top: '10px' });
                        }
                        if (options.horizontal) {
                            var left = ($(options.inside).width() - $(this).outerWidth()) / 2;
                            if (options.withScrolling) left += $(options.inside).scrollLeft() || 0;
                            left = (left > options.minX ? left : options.minX);
                            $.extend(props, { left: left + 'px' });
                        }
                        if (options.transition > 0) $(this).animate(props, options.transition);
                        else $(this).css(props);
                        return $(this);
                    });
                }
            });
        })(jQuery);

        $(document).ready(function () {
            $('#popup').center();
            loadGridDiario();
            loadGridMensal();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function sendEmail() {
            var assunto = $('#emailSubjectText').val().trim();
            var intro = $('#emailIntroText').val().trim();
            var sendto = $('#emailSocio').html();
            var sendcc = '';
            var sendbcc = 'happybodyfitcoach@gmail.com';
            var body = $('#emailBodyText').val().trim();

            $.ajax({
                type: "POST",
                url: "Aniversarios.aspx/sendEmailFromTemplate",
                data: '{"assunto":"' + assunto + '", "intro":"' + intro + '", "sendto":"' + sendto + '", "sendcc":"' + sendcc
                    + '", "sendbcc":"' + sendbcc + '", "body":"' + body + '", "nr_socio":"' + nrSocio + '", "tipo":"' + tipo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        alertify.message(res.d);

                        if (res.d.indexOf('erro') > -1 || res.d.indexOf('Erro') > -1) {
                            
                        }
                        else {
                            closePopup();

                            if (tipo == 'ANIVERSARIO-DIARIO')
                                loadGridDiario();
                            else
                                loadGridMensal();
                        }
                    }
                }
            });
        }

        function openEmailTemplate(nr_socio, enviado) {
            if (enviado == '1') {
                alertify.message('Já foi enviado um email para este sócio!');
                return;
            }

            $.ajax({
                type: "POST",
                url: "Aniversarios.aspx/loadTextEmail",
                data: '{"nr_socio":"' + nr_socio + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //alertify.message(res.d);
                        $('#divVariaveis').html(res.d);
                        openPopup();

                        nrSocio = nr_socio;
                        tipo = 'ANIVERSARIO-DIARIO';
                    }
                }
            });
        }

        function openEmailTemplateMes(nr_socio, enviado) {
            if (enviado == '1') {
                alertify.message('Já foi enviado um email para este sócio!');
                return;
            }

            $.ajax({
                type: "POST",
                url: "Aniversarios.aspx/loadTextEmailMes",
                data: '{"nr_socio":"' + nr_socio + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //alertify.message(res.d);
                        $('#divVariaveis').html(res.d);
                        openPopup();

                        nrSocio = nr_socio;
                        tipo = 'ANIVERSARIO-MENSAL';
                    }
                }
            });
        }

        function closePopup() {
            $('#black_overlay').fadeOut();
            $('#popup').fadeOut();
            $('#content').removeClass('removeScroll');
            $('html, body').animate({
                scrollTop: $('#content').offset().top
            }, 'slow');

            $('#emailIntroText').val('');
            $('#emailBodyText').val('');
            $('#emailSubjectText').val('');
        }

        function openPopup() {
            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
            $('#content').addClass('removeScroll');

            $('#emailIntroText').val($('#emailIntro').html());
            $('#emailBodyText').val(replaceAll($('#emailText').html(), '"', '\''));
            $('#emailSubjectText').val($('#emailSubject').html());
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function exportExcel() {
            $('#tableGrid').btechco_excelexport({
                containerid: 'tableGrid'
               , datatype: $datatype.Table
               , filename: 'exportacao_aniversarios_dia'
            });
        }

        function exportExcelMes() {
            $('#tableGridMes').btechco_excelexport({
                containerid: 'tableGridMes'
               , datatype: $datatype.Table
               , filename: 'exportacao_aniversarios_mes'
            });
        }

        function changeGrid() {
            if ($('#tableGrid').is(":visible")) {
                $('#btnGrid').attr('value', 'Diário');
                $('#divTable').fadeOut();
                $('#divTableMes').fadeIn();
            }
            else {
                $('#btnGrid').attr('value', 'Mensal');
                $('#divTable').fadeIn();
                $('#divTableMes').fadeOut();
            }
        }

        function exportar() {
            if ($('#tableGrid').is(":visible")) {
                exportExcel();
            }
            else {
                exportExcelMes();
            }
        }

        function loadGridDiario() {
            $.ajax({
                type: "POST",
                url: "Aniversarios.aspx/getAniversarioDiario",
                data: '{"operatorID":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTable').html(res.d);
                    }
                }
            });
        }

        function loadGridMensal() {
            $.ajax({
                type: "POST",
                url: "Aniversarios.aspx/getAniversarioMensal",
                data: '{"operatorID":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTableMes').html(res.d);
                    }
                }
            });
        }
    </script>
</body>
</html>
