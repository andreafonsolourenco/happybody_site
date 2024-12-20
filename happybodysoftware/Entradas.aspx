<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Entradas.aspx.cs" Inherits="Entradas" Culture="auto" UICulture="auto" %>
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
    <!-- Calendar -->
    <link rel="stylesheet" type="text/css" href="calendar_plugin/zabuto_calendar.min.css" />

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
            font-size: 1.3vw; 
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
            border: none; 
            /*border: 1px #000 solid;*/
            font-family: 'Noto Sans', sans-serif !important;
        }

        body {
            font-size: 1.3vw !important;
        }

        table thead {
            /*background-color:#000; 
            color: #FFF; 
            font-size: large; 
            font-weight: bold;*/
        }

            table thead tr {
                /*height: 50px;*/
            }

        table thead tr th {
            /*padding: 5px;*/
            -moz-border-radius: 4px !important;
            -webkit-border-radius: 4px !important;
            border-radius: 4px !important;
        }

        .headerLeft {
            width: 70%;
            border-right: 1px red solid;
        }

        .headerRight {
            width: 30%;
            border-left: 1px red solid;
        }
        
        table tbody {
            /*background-color:#FFF;
            color:#000;
            font-size: medium;*/
            font-size: 1.1vw !important;
        }
        
        table tbody tr {
            height: 20px;
            max-height: 20px;
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
            width: 90%;
        }

        .tbodyTrTdRight {
            width: 10%;
        }

        table tbody tr td {
            padding: 5px;
            /*border: 1px #000 solid;*/
        }

        .tableLine {
            font-size: 1.3vw !important;
            cursor: pointer;
        }

        .tableLine:hover {
            background-color: #D3D3D3;
        }

        .tableLineSelected {
            font-size: 1.3vw;
            cursor: pointer;
            background-color: #D3D3D3;
        }

        .tableLineSelected:hover {
            background-color: red;
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

        .audio {
            display:none;
        }

        .nopadding {
           padding: 0 !important;
           margin: 0 !important;
        }

        input:active, 
        input:focus
        {
            border:1px solid #ccc !important;
        }

        .padding-bottom {
            padding-bottom: 10px !important;
        }

        .position-relative {
            position: relative !important;
            font-family: 'Noto Sans', sans-serif !important;
        }

        #divLoading {
            display:none;
			position: fixed;
			top: 0%;
			left: 0%;
			width: 100%;
			height: 100%;
			background-color: black;
			z-index:999999;
			-moz-opacity: 0.8;
			opacity:.80;
			filter: alpha(opacity=80);
        }

        #divLoading.show {
            display : block;
        }

        #divLoading img {
            max-height: 100%;  
            max-width: 100%; 
            width: auto;
            height: 50%;
            position: absolute;  
            top: 0;  
            bottom: 0;  
            left: 0;  
            right: 0;  
            margin: auto;
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
        <span class="variaveis" id="lblmeslimitecalendario" runat="server"></span>
        <span class="variaveis" id="lblanolimitecalendario" runat="server"></span>
        <span class="variaveis" id="lblnrsocioultimaavaliacao" runat="server"></span>
        <div class="variaveis" id="ultimoRegistoDiv" runat="server"></div>
        <div class="variaveis" id="datacalendardiv" runat="server"></div>

        <audio id="error_sound" class="audio">
            <source src="sound/error_sound.mp3" type="audio/mpeg" />
        </audio>

        <audio id="enter_sound" class="audio">
            <source src="sound/enter_sound.mp3" type="audio/mpeg" />
        </audio>

        <div id="divLoading">
            <img src="img/loading.gif" id="loadingImg"/>
        </div>	

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divEntradas">
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 nopadding" id="divInfo">
                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 nopadding" style="height: 100%">
                    <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6 nopadding" style="height: 15%;">
                        <input type='text' autocomplete="off" onfocus="focusNrSocio(1);" onfocusout="focusNrSocio(0);" class='form-control' id='searchSocio' name='searchSocio' placeholder='Nº Sócio' style='height: 50%; width: 100%; margin: auto; font-size: 1.3vw; background-color: #000 !important; color: #FFF !important'/>
                        <input id="saidaGeral" type="button" value="Saídas Automáticas" onclick="saida();" style="background-color: #4282b5; width: 50%; height: 50%; margin: auto; font-size: 0.8vw !important; text-align: center; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px; float: left"/>
                        <input id="removerEntrada" type="button" value="Remover" onclick="removerEntrada();" style="background-color: #4282b5; width: 50%; height: 50%; margin: auto; font-size: 1.3vw !important; text-align: center; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px; float: right" disabled/>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
                    <div class="col-lg-5 col-md-5 col-xs-5 col-sm-5 nopading" id="labelAvisos" style="float: right; height: 15%; text-align: center; font-weight: bold; display: flex; justify-content: center; -ms-align-content: center; -webkit-align-content: center; align-content: center; -ms-flex-direction: column; -webkit-flex-direction: column; flex-direction: column; font-size: 1.2vw !important;">

                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding" id="divTable" style="height: 75%; text-align: center; overflow-x: hidden; padding-top: 8.5px !important;">

                    </div>
                    <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12 nopadding" id="divInfoContract" style="background-color: gray; height: 10%; font-weight: bold; overflow-x: hidden; padding-top: 2px !important; font-size: 1.1vw !important; color: #FFF; text-align: left;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;">
                        <div style="height: 50%; width: 100%; padding-left: 2px !important; padding-right: 2px !important;" id="atividade">
                            
                        </div>
                        <div style="height: 50%; width: 100%;  padding-left: 2px !important; padding-right: 2px !important;" id="validade">
                            
                        </div>
                    </div>
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7" style="height: 100%">
                    <div style="height: 35%">
                        <div class="col-lg-7 col-md-7 col-xs-7 col-sm-7 nopadding" style="height: 100%; padding-right: 2px;">
                            <div style="height: 50%; width: 100%; float: left; font-size: 1.2vw !important;">
                                <div style="height: 100%; width: 33.33%; float: left;">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divEntrada">
                                        Hora Entrada
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="horaEntrada">
                                        
                                    </div>
                                </div>
                                <div style="height: 100%; width: 33.33%; float: left;">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divSaida">
                                        Hora Saída
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="horaSaida">

                                    </div>
                                </div>
                                <div style="height: 100%; width: 33.33%; float: left;">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divAniversario">
                                        Aniv.
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="aniversario">

                                    </div>
                                </div>
                            </div>
                            <div style="height: 50%; width: 100%; float: left; font-size: 1.2vw !important;">
                                <div style="height: 100%; width: 33.33%; float: left;">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divCacifo">
                                        Cacifo
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divImgCacifo">

                                    </div>
                                </div>
                                <div style="height: 100%; width: 33.33%; float: left;">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divToalha">
                                        Toalha
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divImgToalha">

                                    </div>
                                </div>
                                <div style="height: 100%; width: 33.33%; float: left; cursor: pointer;" onclick="changeDateAF();" id="divAlertAF">
                                    <div style="height: 50%; width: 100%; text-align: center; background-color: #000; color: #FFF; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divRoupao">
                                        AF
                                    </div>
                                    <div style="height: 50%; width: 100%; text-align: center; color: #000; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="divImgRoupao">

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-xs-1 col-sm-1" id="divEmBranco" style="height: 100%;">

                        </div>
                        <div class="col-lg-4 col-md-4 col-xs-4 col-sm-4" id="foto" style="height: 100%; background-color: gray; text-align: center; background-repeat: no-repeat; background-size: 100% 100%; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;">

                        </div>
                    </div>
                    <div style="width: 100%; height: 8%; background-color: #000; color: #FFF;-moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" id="horario">
                        Horário: 
                    </div>
                    <div style="width: 100%; height: 2%;" id="Div1"></div>
                    <div style="height: 55%">
                        <textarea class='form-control' id='notas' name='notas' placeholder='Notas' style='width: 100%; margin: auto; height: 100%; font-size: 1.2vw; color: #FFF !important;' disabled></textarea>
                    </div>
                </div>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 nopadding" id="divCalendario">
                
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divGrafico" style="margin-top: 2px;">
            <div id="dados" style="width:100%; background-color: #000; color: #FFF; display: inline-block; float: left; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;">
                <div class="col-lg-4 col-xs-4 col-sm-4 col-md-4" style="text-align: left;">
                    <span runat="server" id="clientesInstalacoes"></span>
                </div>
                <div class="col-lg-4 col-xs-4 col-sm-4 col-md-4"  style="text-align: center;">
                    <span runat="server" id="totalDia"></span>
                </div>
                <div class="col-lg-4 col-xs-4 col-sm-4 col-md-4"  style="text-align: right;">
                    <span runat="server" id="totalDiaAnterior"></span>
                </div>
            </div>
            <div id="grafico" style="height: 300px; width: 100%; margin-top: 10px;">
                GRÁFICO
            </div>
            <div id="dadosGrafico" class="variaveis"></div>
        </div>
        <div style="display: none" id="res" runat="server"></div>

        <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" runat="server" style="margin-bottom: 25px;">
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='margin-bottom: 10px;'>
                <input type='text' class='form-control' id='search' name='search' placeholder='Pesquisa' style='height: 50px; width: 75%; margin: auto; float: left;'/>
                <input type='image' src='img/icons/icon_search.png' onclick='loadSocios();' style='float:right; width:auto; height:50px; margin:auto;' />
            </div>
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='margin-bottom: 10px; display:none'>
                <label class='checkbox-inline'><input type='checkbox' value='NR_SOCIO' id='check_nrsocio'>Nº Sócio</label>
                <label class='checkbox-inline'><input type='checkbox' value='NOME' id='check_nome'>Nome</label>
                <label class='checkbox-inline'><input type='checkbox' value='TELEMOVEL' id='check_telemovel'>Telemóvel</label>
            </div>

            <div style="width:100%" id="divEntradas">

            </div>
        </div>

        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" runat="server" style="margin-bottom: 25px;">
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='margin-bottom: 10px;'>
                <input type='text' class='form-control' id='pesquisaEntradas' name='pesquisaEntradas' placeholder='Pesquisa' style='height: 50px; width: 75%; margin: auto; float: left;'/>
                <input type='image' src='img/icons/icon_search.png' onclick='loadEntradas();' style='float:right; width:auto; height:50px; margin:auto;' />
            </div>

            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='margin-bottom: 10px; display:none'>
                <label class='checkbox-inline'><input type='checkbox' value='NR_SOCIO' id='check_nrsocio_entradas'>Nº Sócio</label>
                <label class='checkbox-inline'><input type='checkbox' value='NOME' id='check_nome_entradas'>Nome</label>
                <label class='checkbox-inline'><input type='checkbox' value='TELEMOVEL' id='check_telemovel_entradas'>Telemóvel</label>
            </div>

            <div style="width:100%" id="divSaidas">

            </div>
        </div>--%>

        <div id="black_overlay" onclick="closePopUpChangeDataAF();">
            <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closePopUpChangeDataAF();" />
        </div>

        <div id="popup" class="popup" runat="server">
            <span style="font-weight: bold">Data Próxima Avaliação:</span>
            <input type='text' class='form-control' id='dataProximaAvaliacao' name="dataProximaAvaliacao" placeholder='Data Próxima Avaliação' style="width: 100%; margin: auto;"/>
            <div class="input-group clockpicker" style="width: 100%">
                <input type="text" class="form-control" id="horaProximaAvaliacao" placeholder='Hora Próxima Avaliação'>
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-time"></span>
                </span>
            </div>

            <input type='button' class='form-control' value='Cancelar' style='width: 48%; margin-bottom: 5px; float: left;' onclick='closePopUpChangeDataAF();' />
            <input type='button' class='form-control' value='Alterar Data' style='width: 48%; margin-bottom: 5px; float: right;' onclick='alertChangeDate();' />
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

    <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <!-- Calendar -->
    <script type="text/javascript" src="calendar_plugin/zabuto_calendar.min.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var linhaSelecionada;
        var searchNrSocio = 0;
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
            //loadSocios();
            //loadEntradas();
            resizeDivs();
            loadNrClientesInstalacoes();
            totalClientesDia();
            loadEntradas();
            grafico();

            $('#notas').on('change', function () {
                updateNotas();
            });

            $('#popup').center();

            $('#dataProximaAvaliacao').datepicker({ format: 'dd-mm-yyyy', changeYear: true, changeMonth: true, setDate: $('#dataProximaAvaliacao').val() });
            $('.clockpicker').clockpicker({
                format: 'PT',
                placement: 'bottom',
                align: 'center',
                donetext: 'Guardar'
            });
        });

        $(window).on('resize', function () {
            resizeDivs();
        });

        $(window).scroll(function () {
            
        });

        function resizeDivs() {
            $('#divEntradas').height($(window).height() * 0.8);
            $('#divInfo').height($('#divEntradas').height());
            $('#divCalendario').height($('#divInfo').height());
            $('#divGrafico').height($(window).height() - $('#divEntradas').height());
            $('#horario').css("line-height", ($('#horario').height() + "px"));
            $('#labels').css("line-height", (($('#divInfoContract').height() / 2) + "px"));
            $('#info').css("line-height", (($('#divInfoContract').height() / 2) + "px"));
            $('#foto').height($('#foto').height() - $('#divImgRoupao').height());
            $('#divCacifo').css("line-height", ($('#divCacifo').height() + "px"));
            $('#divToalha').css("line-height", ($('#divToalha').height() + "px"));
            $('#divRoupao').css("line-height", ($('#divRoupao').height() + "px"));
            $('#divAniversario').css("line-height", ($('#divAniversario').height() + "px"));
            $('#divEntrada').css("line-height", ($('#divEntrada').height() + "px"));
            $('#divSaida').css("line-height", ($('#divSaida').height() + "px"));

            $('#divCalendario').html('');
            $('#divCalendario').html('<div id="calendar1" style="width:100%"></div>');

            $('#calendar1').height($('#divCalendario').height());
            //$('#calendar2').height($('#divCalendario').height() / 3);
            //$('#calendar3').height($('#divCalendario').height() / 3);

            $("#calendar1").zabuto_calendar({
                language: "pt",
                year: parseInt($('#lblanolimitecalendario').html()),
                month: parseInt($('#lblmeslimitecalendario').html()),
                show_previous: 2,
                show_next: false,
                cell_border: true,
                today: false,
                show_days: true,
                weekstartson: 0,
                nav_icon: {
                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                    next: '<i class="fa fa-chevron-circle-right"></i>'
                }
            });
        }

        function loadSocios() {
            var query = "";
            var text = $('#search').val().trim();
            var array = new Array();

            if (text != "") {
                if (isNaN(text)) {
                    array = $('#search').val().trim().split(' ');

                    for (a in array) {
                        if (query == "") {
                            query += " soc.NOME LIKE '%" + array[a] + "%' ";
                        }
                        else {
                            query += " AND soc.NOME LIKE '%" + array[a] + "%' ";
                        }
                    }
                }
                else {
                    query += " soc.NR_SOCIO = @FILTRO OR soc.TELEMOVEL = @FILTRO ";
                }
            }
            else {
                query += " 1=1 ";
            }

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadSocios",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + text + '", "query":"' + query + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divEntradas').html(res.d);
                    }
                }
            });
        };

        function loadEntradas() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadEntradas",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTable').html(res.d);
                    }
                }
            });
        };

        function declareEntrada() {
            //var estado = $('#estado_' + nr).html();
            //var inativo = $('#inativo_' + nr).html();
            //var nrsocio = $('#nrsocio_' + nr).html();

            //if (inativo == 1) {
            //    alertify.message('O contrato do sócio nº' + nrsocio + ' encontra-se ' + estado + '!');
            //    $('#error_sound').each(function () { this.play(); });
            //    return;
            //}

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/verificaEntradaSaida",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "nr_socio":"' + $('#searchSocio').val().trim() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#res').html(res.d);
                        $("#divLoading").removeClass('show');

                        if (parseInt($('#resEntradaSaida').html()) >= 0) {
                            $('#searchSocio').val('');
                            //$('#pesquisaEntradas').val('');
                            //alertify.message(res.d);
                            //loadSocios();
                            loadEntradas();
                            loadAvisos();
                            $('#enter_sound').each(function () { this.play(); });
                            loadInfoUltimoRegisto();
                            loadNrClientesInstalacoes();
                            totalClientesDia();
                            grafico();
                            document.getElementById("labelAvisos").style.backgroundColor = 'green';
                        }
                        else {
                            $('#error_sound').each(function () { this.play(); });
                            document.getElementById("labelAvisos").style.backgroundColor = 'red';
                        }

                        $('#labelAvisos').html($('#msgEntradaSaida').html());
                    }
                }
            });
        }

        function declareSaida(id, nr_linha) {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/saida",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_entrada":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#lblidselected').html('');
                        selectRow(nr_linha);
                    }
                }
            });
        }

        function loadNrClientesInstalacoes() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/nrClientesInstalacoes",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao carregar o nº de sócios presentes nas instalações!');
                        }
                        else {
                            $('#clientesInstalacoes').html('Clientes nas instalações: ' + res.d);
                        }
                    }
                }
            });
        };

        function totalClientesDia() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/totalClientesDia",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao carregar o nº de sócios durante o dia de hoje!');
                        }
                        else {
                            $('#totalDia').html('Total no dia: ' + res.d);
                        }
                    }
                }
            });
        };

        $.UrlExists = function (url) {
            var http = new XMLHttpRequest();
            http.open('HEAD', url, false);
            http.send();
            return http.status != 404;
        }

        function selectRow(nr_linha) {
            if ($('#lblidselected').html() == '') {
                $('#ln_' + nr_linha).removeClass('tableLine');
                $('#ln_' + nr_linha).addClass('tableLineSelected');
                $('#lblidselected').html(nr_linha);
                var fotoHeight = $('#foto').height();
                var fotoWidth = $('#foto').width();
                if ($.UrlExists('img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg')) {
                    $('#foto').css('background-image', 'url(img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg)');
                    //$('#foto').css('background-size', fotoWidth + 'px ' + fotoHeight + ' px');
                    //$('#foto').html('<img id="fotoSocio" src="img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg" alt="Foto" height="' + fotoHeight + '" width="' + fotoWidth + '" style="margin-left: auto; margin-right: auto; display: block;">');
                } else {
                    $('#foto').css('background-image', '');
                    //$('#foto').css('background-size', fotoWidth + 'px ' + fotoHeight + ' px');
                    //$('#foto').html('');
                }
                $('#horaEntrada').html($('#entrada_' + nr_linha).html());
                $('#aniversario').html($('#aniversario_' + nr_linha).html());
                $('#horario').html('Horário: ' + $('#horario_' + nr_linha).html());
                $('#notas').val($('#notas_' + nr_linha).html());
                $('#atividade').html('Atividade: ' + $('#tipoContrato_' + nr_linha).html());
                $('#validade').html('Validade: ' + $('#validade_' + nr_linha).html());
                $('#horaSaida').html($('#saida_' + nr_linha).html());
                $('#removerEntrada').prop('disabled', false);
                $('#notas').prop('disabled', false);

                var divImgCacifoHeight = $('#divImgCacifo').height();
                var divImgCacifoWidth = "auto";
                var valueCacifo = "";

                if ($('#cacifo_' + nr_linha).html() == "0")
                    valueCacifo = "no.png";
                else
                    valueCacifo = "yes.png";

                $('#divImgCacifo').html('<img id="imgCacifo" src="img/icons/' + valueCacifo + '" height="' + divImgCacifoHeight + '" width="' + divImgCacifoWidth + '" onclick="updateCacifo(' + nr_linha + ')">');

                var divImgToalhaHeight = $('#divImgToalha').height();
                var divImgToalhaWidth = "auto";
                var valueToalha = "";

                if ($('#toalha_' + nr_linha).html() == "0")
                    valueToalha = "no.png";
                else
                    valueToalha = "yes.png";

                $('#divImgToalha').html('<img id="imgToalha" src="img/icons/' + valueToalha + '" height="' + divImgToalhaHeight + '" width="' + divImgToalhaWidth + '" onclick="updateToalha(' + nr_linha + ')">');

                var divImgRoupaoHeight = $('#divImgRoupao').height();
                var divImgRoupaoWidth = "auto";
                var valueRoupao = "";

                //if ($('#roupao_' + nr_linha).html() == "0")
                //    valueRoupao = "no.png";
                //else
                //    valueRoupao = "yes.png";

                //$('#divImgRoupao').html('<img id="imgRoupao" src="img/icons/' + valueRoupao + '" height="' + divImgRoupaoHeight + '" width="' + divImgRoupaoWidth + '" onclick="updateRoupao(' + nr_linha + ')">');
                $('#divImgRoupao').html($('#af_' + nr_linha).html());

                if ($('#afforaprazo_' + nr_linha).html() == '1') {
                    $('#divAlertAF').css('background-color', 'red');
                }
                else {
                    $('#divAlertAF').css('background-color', 'white');
                }

                loadEntradasSocio($('#nr_socio_' + nr_linha).html());

                $('#lblnrsocioultimaavaliacao').html($('#nr_socio_' + nr_linha).html());
            }
            else {
                if ($('#lblidselected').html() == nr_linha) {
                    $('#ln_' + nr_linha).removeClass('tableLineSelected');
                    $('#ln_' + nr_linha).addClass('tableLine');
                    $('#lblidselected').html('');
                    $('#foto').css('background-image', '');
                    $('#horaEntrada').html('');
                    $('#aniversario').html('');
                    $('#horario').html('Horário: ');
                    $('#notas').val('');
                    $('#atividade').html('Atividade:');
                    $('#validade').html('Validade:');
                    $('#horaSaida').html('');
                    $('#removerEntrada').prop('disabled', true);
                    $('#notas').prop('disabled', true);
                    $('#divImgCacifo').html('');
                    $('#divImgToalha').html('');
                    $('#divImgRoupao').html('');
                    loadEntradasSocio(0);
                    $('#lblnrsocioultimaavaliacao').html('');

                    $('#divAlertAF').css('background-color', 'white');
                }
                else {
                    $('#ln_' + $('#lblidselected').html()).removeClass('tableLineSelected');
                    $('#ln_' + $('#lblidselected').html()).addClass('tableLine');
                    $('#ln_' + nr_linha).removeClass('tableLine');
                    $('#ln_' + nr_linha).addClass('tableLineSelected');
                    $('#lblidselected').html(nr_linha);
                    var fotoHeight = $('#foto').height();
                    var fotoWidth = $('#foto').width();

                    if ($.UrlExists('img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg')) {
                        $('#foto').css('background-image', 'url(img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg)');
                        //$('#foto').css('background-size', fotoWidth + 'px ' + fotoHeight + ' px');
                        //$('#foto').html('<img id="fotoSocio" src="img/socios/' + $('#nr_socio_' + nr_linha).html() + '.jpg" alt="Foto" height="' + fotoHeight + '" width="' + fotoWidth + '" style="margin-left: auto; margin-right: auto; display: block;">');
                    } else {
                        $('#foto').css('background-image', '');
                        //$('#foto').css('background-size', fotoWidth + 'px ' + fotoHeight + ' px');
                        //$('#foto').html('');
                    }

                    $('#horaEntrada').html($('#entrada_' + nr_linha).html());
                    $('#aniversario').html($('#aniversario_' + nr_linha).html());
                    $('#horario').html('Horário: ' + $('#horario_' + nr_linha).html());
                    $('#notas').val($('#notas_' + nr_linha).html());
                    $('#atividade').html('Atividade: ' + $('#tipoContrato_' + nr_linha).html());
                    $('#validade').html('Validade: ' + $('#validade_' + nr_linha).html());
                    $('#horaSaida').html($('#saida_' + nr_linha).html());
                    $('#removerEntrada').prop('disabled', false);
                    $('#notas').prop('disabled', false);

                    var divImgCacifoHeight = $('#divImgCacifo').height();
                    var divImgCacifoWidth = "auto";
                    var valueCacifo = "";

                    if ($('#cacifo_' + nr_linha).html() == "0")
                        valueCacifo = "no.png";
                    else
                        valueCacifo = "yes.png";

                    $('#divImgCacifo').html('<img id="imgCacifo" src="img/icons/' + valueCacifo + '" height="' + divImgCacifoHeight + '" width="' + divImgCacifoWidth + '" onclick="updateCacifo(' + nr_linha + ')">');

                    var divImgToalhaHeight = $('#divImgToalha').height();
                    var divImgToalhaWidth = "auto";
                    var valueToalha = "";

                    if ($('#toalha_' + nr_linha).html() == "0")
                        valueToalha = "no.png";
                    else
                        valueToalha = "yes.png";

                    $('#divImgToalha').html('<img id="imgToalha" src="img/icons/' + valueToalha + '" height="' + divImgToalhaHeight + '" width="' + divImgToalhaWidth + '" onclick="updateToalha(' + nr_linha + ')">');

                    var divImgRoupaoHeight = $('#divImgRoupao').height();
                    var divImgRoupaoWidth = "auto";
                    var valueRoupao = "";

                    //if ($('#roupao_' + nr_linha).html() == "0")
                    //    valueRoupao = "no.png";
                    //else
                    //    valueRoupao = "yes.png";

                    //$('#divImgRoupao').html('<img id="imgRoupao" src="img/icons/' + valueRoupao + '" height="' + divImgRoupaoHeight + '" width="' + divImgRoupaoWidth + '" onclick="updateRoupao(' + nr_linha + ')">');
                    $('#divImgRoupao').html($('#af_' + nr_linha).html());

                    if ($('#afforaprazo_' + nr_linha).html() == '1') {
                        $('#divAlertAF').css('background-color', 'red');
                    }
                    else {
                        $('#divAlertAF').css('background-color', 'white');
                    }

                    loadEntradasSocio($('#nr_socio_' + nr_linha).html());

                    $('#lblnrsocioultimaavaliacao').html($('#nr_socio_' + nr_linha).html());
                }
            }
        }

        function focusNrSocio(x) {
            searchNrSocio = x;
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if (searchNrSocio == 1) {
                    $("#divLoading").addClass('show');
                    declareEntrada();
                }
            }
        });

        function loadInfoUltimoRegisto() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadInfoUltimoRegisto",
                data: '{"id_entrada":"' + $('#resEntradaSaida').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d != "") {
                            $('#ultimoRegistoDiv').html(res.d);

                            var fotoHeight = $('#foto').height();
                            var fotoWidth = $('#foto').width();

                            if ($.UrlExists('img/socios/' + $('#ultimoRegistoFoto').html())) {
                                $('#foto').css('background-image', 'url(img/socios/' + $('#ultimoRegistoFoto').html());
                            } else {
                                $('#foto').css('background-image', '');
                            }

                            $('#horaEntrada').html($('#ultimoRegistoHoraEntrada').html());
                            $('#horaSaida').html($('#ultimoRegistoHoraSaida').html());
                            $('#aniversario').html($('#ultimoRegistoAniversario').html());
                            $('#horario').html('Horário: ' + $('#ultimoRegistoHorario').html());
                            $('#notas').val($('#ultimoRegistoNotas').html());
                            $('#atividade').html('Atividade: ' + $('#ultimoRegistoTipoContrato').html());
                            $('#validade').html('Validade: ' + $('#ultimoRegistoValidadeContrato').html());
                            $('#notas').prop('disabled', true);

                            var divImgCacifoHeight = $('#divImgCacifo').height();
                            var divImgCacifoWidth = "auto";
                            var valueCacifo = "";

                            if ($('#ultimoRegistoCacifo').html() == "0")
                                valueCacifo = "no.png";
                            else
                                valueCacifo = "yes.png";

                            $('#divImgCacifo').html('<img id="imgCacifo" src="img/icons/' + valueCacifo + '" height="' + divImgCacifoHeight + '" width="' + divImgCacifoWidth + '">');

                            var divImgToalhaHeight = $('#divImgToalha').height();
                            var divImgToalhaWidth = "auto";
                            var valueToalha = "";

                            if ($('#ultimoRegistoToalha').html() == "0")
                                valueToalha = "no.png";
                            else
                                valueToalha = "yes.png";

                            $('#divImgToalha').html('<img id="imgToalha" src="img/icons/' + valueToalha + '" height="' + divImgToalhaHeight + '" width="' + divImgToalhaWidth + '">');

                            var divImgRoupaoHeight = $('#divImgRoupao').height();
                            var divImgRoupaoWidth = "auto";
                            var valueRoupao = "";

                            //if ($('#roupao_' + nr_linha).html() == "0")
                            //    valueRoupao = "no.png";
                            //else
                            //    valueRoupao = "yes.png";

                            //$('#divImgRoupao').html('<img id="imgRoupao" src="img/icons/' + valueRoupao + '" height="' + divImgRoupaoHeight + '" width="' + divImgRoupaoWidth + '" onclick="updateRoupao(' + nr_linha + ')">');
                            $('#divImgRoupao').html($('#ultimoRegistoAF').html());

                            if ($('#ultimoRegistoAFForaPrazo').html() == '1') {
                                $('#divAlertAF').css('background-color', 'red');
                            }
                            else {
                                $('#divAlertAF').css('background-color', 'white');
                            }

                            loadEntradasSocio($('#ultimoRegistoNrSocio').html());

                            $('#lblnrsocioultimaavaliacao').html($('#ultimoRegistoNrSocio').html());
                        }
                    }
                }
            });
        }

        function removerEntrada() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/removerEntrada",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_entrada":"' + $('#id_entrada_' + $('#lblidselected').html()).html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#searchSocio').val('');
                        loadEntradas();
                        //restoreTable();
                        loadNrClientesInstalacoes();
                        totalClientesDia();
                        grafico();

                        $('#lblidselected').html('');
                        $('#foto').css('background-image', '');
                        $('#horaEntrada').html('');
                        $('#aniversario').html('');
                        $('#horario').html('Horário: ');
                        $('#notas').val('');
                        $('#atividade').html('Atividade:');
                        $('#validade').html('Validade:');
                        $('#horaSaida').html('');
                        $('#removerEntrada').prop('disabled', true);
                        $('#notas').prop('disabled', true);
                        $('#divImgCacifo').html('');
                        $('#divImgToalha').html('');
                        $('#divImgRoupao').html('');
                        $('#divAlertAF').css('background-color', 'white');

                        loadEntradasSocio(0);
                    }
                }
            });
        }

        function updateCacifo(nr_linha) {
            var cacifo = "";
            var imgCacifo = "";

            if ($('#cacifo_' + nr_linha).html() == "0") {
                imgCacifo = "yes.png";
                cacifo = "1";
            }
            else {
                imgCacifo = "no.png";
                cacifo = "0";
            }

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/updateCacifo",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_entrada":"' + $('#id_entrada_' + nr_linha).html() + '", "cacifo":"' + cacifo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao atualizar a entrega de cacifo!');
                        }
                        else {
                            $('#imgCacifo').attr('src', 'img/icons/' + imgCacifo);
                            $('#cacifo_' + nr_linha).html(cacifo);
                        }
                    }
                }
            });
        }

        function updateToalha(nr_linha) {
            var toalha = "";
            var imgToalha = "";

            if ($('#toalha_' + nr_linha).html() == "0") {
                imgToalha = "yes.png";
                toalha = "1";
            }
            else {
                imgToalha = "no.png";
                toalha = "0";
            }

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/updateToalha",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_entrada":"' + $('#id_entrada_' + nr_linha).html() + '", "toalha":"' + toalha + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao atualizar a entrega de toalha!');
                        }
                        else {
                            $('#imgToalha').attr('src', 'img/icons/' + imgToalha);
                            $('#toalha_' + nr_linha).html(toalha);
                        }
                    }
                }
            });
        }

        function updateRoupao(nr_linha) {
            var roupao = "";
            var imgRoupao = "";

            if ($('#roupao_' + nr_linha).html() == "0") {
                imgRoupao = "yes.png";
                roupao = "1";
            }
            else {
                imgRoupao = "no.png";
                roupao = "0";
            }

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/updateRoupao",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_entrada":"' + $('#id_entrada_' + nr_linha).html() + '", "roupao":"' + roupao + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao atualizar a entrega de roupão!');
                        }
                        else {
                            $('#imgRoupao').attr('src', 'img/icons/' + imgRoupao);
                            $('#roupao_' + nr_linha).html(roupao);
                        }
                    }
                }
            });
        }

        function updateNotas() {
            var nrLinha = $('#lblidselected').html();

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/updateNotas",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "notas":"' + $('#notas').val() + '", "nr_socio":"' + $('#nr_socio_' + nrLinha).html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '-1') {
                            alertify.message('Ocorreu um erro ao atualizar as notas!');
                        }
                        else {
                            $('#notas_' + nrLinha).html($('#notas').val());
                        }
                    }
                }
            });
        }

        function saida() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/declaraSaidaGeral",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#searchSocio').val('');
                        loadEntradas();
                        //restoreTable();
                        loadNrClientesInstalacoes();
                        totalClientesDia();
                        grafico();
                    }
                }
            });
        }

        function grafico() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadGrafico",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#grafico').width($('#dados').width());

                        if (res.d == '') {
                            
                        }
                        else {
                            $('#dadosGrafico').html(res.d);

                            var chart = new CanvasJS.Chart("grafico", {
                                animationEnabled: true,

                                theme: "light1",

                                title: {
                                    text: "Entradas do Dia"
                                },

                                axisY: {
                                    title: "Nº Entradas"
                                },

                                //axisY2: {
                                //    interlacedColor: "rgba(1,77,101,.2)",
                                //    gridColor: "rgba(1,77,101,.1)",
                                //    title: "Hora"
                                //},
                                data: [{
                                    type: "column",
                                    showInLegend: false,
                                    dataPoints: [
                                        { y: parseInt($('#nrEntradas7').html()), label: $('#entradaGrafico7').html() },
                                        { y: parseInt($('#nrEntradas73').html()), label: $('#entradaGrafico73').html() },
                                        { y: parseInt($('#nrEntradas8').html()), label: $('#entradaGrafico8').html() },
                                        { y: parseInt($('#nrEntradas83').html()), label: $('#entradaGrafico83').html() },
                                        { y: parseInt($('#nrEntradas9').html()), label: $('#entradaGrafico9').html() },
                                        { y: parseInt($('#nrEntradas93').html()), label: $('#entradaGrafico93').html() },
                                        { y: parseInt($('#nrEntradas10').html()), label: $('#entradaGrafico10').html() },
                                        { y: parseInt($('#nrEntradas103').html()), label: $('#entradaGrafico103').html() },
                                        { y: parseInt($('#nrEntradas11').html()), label: $('#entradaGrafico11').html() },
                                        { y: parseInt($('#nrEntradas113').html()), label: $('#entradaGrafico113').html() },
                                        { y: parseInt($('#nrEntradas12').html()), label: $('#entradaGrafico12').html() },
                                        { y: parseInt($('#nrEntradas123').html()), label: $('#entradaGrafico123').html() },
                                        { y: parseInt($('#nrEntradas13').html()), label: $('#entradaGrafico13').html() },
                                        { y: parseInt($('#nrEntradas133').html()), label: $('#entradaGrafico133').html() },
                                        { y: parseInt($('#nrEntradas14').html()), label: $('#entradaGrafico14').html() },
                                        { y: parseInt($('#nrEntradas143').html()), label: $('#entradaGrafico143').html() },
                                        { y: parseInt($('#nrEntradas15').html()), label: $('#entradaGrafico15').html() },
                                        { y: parseInt($('#nrEntradas153').html()), label: $('#entradaGrafico153').html() },
                                        { y: parseInt($('#nrEntradas16').html()), label: $('#entradaGrafico16').html() },
                                        { y: parseInt($('#nrEntradas163').html()), label: $('#entradaGrafico163').html() },
                                        { y: parseInt($('#nrEntradas17').html()), label: $('#entradaGrafico17').html() },
                                        { y: parseInt($('#nrEntradas173').html()), label: $('#entradaGrafico173').html() },
                                        { y: parseInt($('#nrEntradas18').html()), label: $('#entradaGrafico18').html() },
                                        { y: parseInt($('#nrEntradas183').html()), label: $('#entradaGrafico183').html() },
                                        { y: parseInt($('#nrEntradas19').html()), label: $('#entradaGrafico19').html() },
                                        { y: parseInt($('#nrEntradas193').html()), label: $('#entradaGrafico193').html() },
                                        { y: parseInt($('#nrEntradas20').html()), label: $('#entradaGrafico20').html() },
                                        { y: parseInt($('#nrEntradas203').html()), label: $('#entradaGrafico203').html() },
                                        { y: parseInt($('#nrEntradas21').html()), label: $('#entradaGrafico21').html() }
                                    ]
                                }]
                            });

                            chart.render();

                            $('.canvasjs-chart-credit').addClass('variaveis');
                            $('.canvasjs-chart-canvas').addClass('position-relative');
                            $('.canvasjs-chart-container').addClass('padding-bottom');
                        }
                    }
                }
            });
        };

        function loadEntradasSocio(nr_socio) {
            $('#divCalendario').html('');
            $('#divCalendario').html('<div id="calendar1" style="width:100%"></div>');

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadDatasEntradasSocio",
                data: '{"nr_socio":"' + nr_socio + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d == '') {
                            $("#calendar1").zabuto_calendar({
                                language: "pt",
                                year: parseInt($('#lblanolimitecalendario').html()),
                                month: parseInt($('#lblmeslimitecalendario').html()),
                                show_previous: 2,
                                show_next: false,
                                cell_border: true,
                                today: false,
                                show_days: true,
                                weekstartson: 0,
                                nav_icon: {
                                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                                    next: '<i class="fa fa-chevron-circle-right"></i>'
                                }
                            });
                            //$("#calendar2").zabuto_calendar();
                            //$("#calendar3").zabuto_calendar();
                        }
                        else {
                            $('#datacalendardiv').html(res.d);

                            var eventData = [];

                            for (var i = 0; i < parseInt($('#nrTotalEntradas').html()) ; i++) {
                                eventData.push({"date":$('#dataEntrada_' + i.toString()).html(), "badge": false});
                            }

                            $("#calendar1").zabuto_calendar({
                                language: "pt",
                                year: parseInt($('#lblanolimitecalendario').html()),
                                month: parseInt($('#lblmeslimitecalendario').html()),
                                show_previous: 2,
                                show_next: false,
                                cell_border: true,
                                today: false,
                                show_days: true,
                                weekstartson: 0,
                                nav_icon: {
                                    prev: '<i class="fa fa-chevron-circle-left"></i>',
                                    next: '<i class="fa fa-chevron-circle-right"></i>'
                                },
                                data: eventData
                            });
                            //$("#calendar2").zabuto_calendar();
                            //$("#calendar3").zabuto_calendar();
                        }
                    }
                }
            });
        }

        function loadAvisos() {
            $.ajax({
                type: "POST",
                url: "Entradas.aspx/loadAvisos",
                data: '{"id_entrada":"' + $('#resEntradaSaida').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d != "") {
                            alertify.alert('Alertas', res.d);
                        }
                    }
                }
            });
        }

        function changeDateAF() {
            if ($('#divImgRoupao').html() != '') {
                openPopUpChangeDataAF();
            }
        }

        function closePopUpChangeDataAF() {
            $('#black_overlay').fadeOut();
            $('#popup').fadeOut();
            $('#content').removeClass('removeScroll');
            $('html, body').animate({
                scrollTop: $('#content').offset().top
            }, 'slow');

            $('#dataProximaAvaliacao').val('');
            $('#horaProximaAvaliacao').val('');
        } 

        function openPopUpChangeDataAF() {
            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
            $('#content').addClass('removeScroll');

            $('#dataProximaAvaliacao').val('');
            $('#horaProximaAvaliacao').val('');
        }

        function alertChangeDate() {
            alertify.confirm('Entradas', 'Tem a certeza que deseja atualizar a data da próxima avaliação física do sócio nº' + $('#lblnrsocioultimaavaliacao').html() + '?',
                function () {
                    if ($('#dataProximaAvaliacao').val() == '' || $('#horaProximaAvaliacao').val() == '') {
                        alertify.message('Por favor, insira uma data e uma hora para a próxima avaliação física!');
                        return;
                    }

                    change();
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function change() {
            var data = $('#dataProximaAvaliacao').val() + ' ' + $('#horaProximaAvaliacao').val();

            $.ajax({
                type: "POST",
                url: "Entradas.aspx/changeDateAF",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "data":"' + data + '", "nr_socio":"' + $('#lblnrsocioultimaavaliacao').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        closePopUpChangeDataAF();
                        alertify.message('Data da próxima avaliação alterada com sucesso!');
                        $('#divImgRoupao').html(data);
                        loadNrClientesInstalacoes();
                        totalClientesDia();
                        loadEntradas();
                        grafico();
                    }
                }
            });
        }
    </script>
</body>
</html>
