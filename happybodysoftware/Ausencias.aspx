<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ausencias.aspx.cs" Inherits="Ausencias" Culture="auto" UICulture="auto" %>
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
        }

        table thead tr th {
            padding: 5px;
        }

        .headerTable {
            width: 100%;
            text-align: center;
        }
        
        table tbody {
            background-color:#FFF;
            color:#000;
            font-size: medium;
            border: 1px #000 solid;
        }
        
        table tbody tr {
            height: 40px;
            cursor: pointer;
        }

        table tbody tr:hover {
            background-color: #D3D3D3;
        }

        .tbodyTrSelected {
            background-color: red;
        }

        .tbodyTrSelected:hover {
            background-color: red;
        }

        .tbodyTrTd {
            width: 100%;
        }

        table tbody tr td {
            padding: 5px;
            border: 1px #000 solid;
        }

        .headerLeft {
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
            text-align: center; 
            width: 75%;
        }

        .headerRight {
            border-left: 1px red solid;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
            text-align: center; 
            width: 25%;
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
        <span class="variaveis" id="lblnrsocioselected" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable" style="margin-bottom: 15px;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadAusencias();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                <form id="formDays">
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="7" />7 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="15" checked/>15 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="30" />30 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="45" />45 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="60" />60 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="75" />75 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="90" />90 Dias</label>
                    <label class="radio-inline"><input type="radio" name="daysmargin" value="999999" />+ 90 Dias</label>
                </form>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
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

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var linhaSelecionada;
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
            $('#search').val('');

            loadAusencias();
            $('#content').height($(parent.window).width());

            $('#formDays input').on('change', function () {
                loadAusencias();
            });

            $('#popup').center();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function loadAusencias() {
            var dia_min = "";
            var dia_max = "";
            var query = "";
            var text = $('#search').val().trim();
            var array = new Array();

            if (text != "") {
                if (isNaN(text)) {
                    array = $('#search').val().trim().split(' ');

                    for (a in array) {
                        if (query == "") {
                            query += "( NOME LIKE '%" + array[a] + "%' ";
                        }
                        else {
                            query += " AND NOME LIKE '%" + array[a] + "%' ";
                        }
                    }

                    query += ") OR EMAIL LIKE '%" + $('#search').val().trim() + "%' ";
                }
                else {
                    query += " NR_SOCIO = @filtro OR TELEMOVEL = @filtro ";
                }
            }
            else {
                query = "";
            }

            switch ($('input[name=daysmargin]:checked', '#formDays').val()) {
                case '7': dia_min = '0'; break;
                case '15': dia_min = '8'; break;
                case '30': dia_min = '16'; break;
                case '45': dia_min = '31'; break;
                case '60': dia_min = '46'; break;
                case '75': dia_min = '61'; break;
                case '90': dia_min = '76'; break;
                case '999999': dia_min = '91'; break;
            }

            dia_max = $('input[name=daysmargin]:checked', '#formDays').val();

            $.ajax({
                type: "POST",
                url: "Ausencias.aspx/load",
                data: '{"filtro":"' + text + '", "query":"' + query + '", "dia_min":"' + dia_min + '", "dia_max":"' + dia_max + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#table').html(res.d);
                    }
                }
            });
        };

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadAusencias();
            }
        });

        function openEmailTemplate(nr_socio, nao_quer_receber) {
            if (nao_quer_receber == 0) {
                $.ajax({
                    type: "POST",
                    url: "Ausencias.aspx/loadTextEmail",
                    data: '{"nr_socio":"' + nr_socio + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            //alertify.message(res.d);
                            $('#divVariaveis').html(res.d);
                            openPopup();
                            $('#lblnrsocioselected').html(nr_socio);
                        }
                    }
                });
            }
            else {
                alertify.message('O sócio selecionado não pretende receber emails do Ginásio');
            }
        }

        function openPopup() {
            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
            $('#content').addClass('removeScroll');

            $('#emailIntroText').val($('#emailIntro').html());
            $('#emailBodyText').val(replaceAll($('#emailText').html(), '"', '\''));
            $('#emailSubjectText').val($('#emailSubject').html());
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

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function sendEmail() {
            var assunto = $('#emailSubjectText').val().trim();
            var intro = $('#emailIntroText').val().trim();
            var sendto = $('#emailSocio').html();
            var sendcc = '';
            var sendbcc = 'happybodyfitcoach@gmail.com';
            var body = $('#emailBodyText').val().trim();
            var nr_socio = $('#lblnrsocioselected').html();

            $.ajax({
                type: "POST",
                url: "Ausencias.aspx/sendEmailFromTemplate",
                data: '{"assunto":"' + assunto + '", "intro":"' + intro + '", "sendto":"' + sendto + '", "sendcc":"' + sendcc
                    + '", "sendbcc":"' + sendbcc + '", "body":"' + body + '", "nr_socio":"' + nr_socio + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        alertify.message(res.d);

                        if (res.d.indexOf('erro') > -1 || res.d.indexOf('Erro') > -1) {
                            
                        }
                        else {
                            closePopup();
                        }

                        loadAusencias();
                    }
                }
            });
        }
    </script>
</body>
</html>
