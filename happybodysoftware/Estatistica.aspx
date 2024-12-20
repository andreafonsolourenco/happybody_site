<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Estatistica.aspx.cs" Inherits="Estatistica" Culture="auto" UICulture="auto" %>
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

        .headerLeft {
            border-right: 1px red solid;
            width: auto;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
        }

        .headerRight {
            width: auto;
            border-left: 1px red solid;
            text-align: center;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
        }

        .headerLeftOneLine {
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
            width: 70%;
        }

        .headerRightOneLine {
            border-left: 1px red solid;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
            width: 30%;
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

        .tableOut:hover {
            background-color: #FFF;
        }

        .tbodyTrSelected {
            background-color: red;
        }

        .tbodyTrSelected:hover {
            background-color: red;
        }

        .tbodyTrTdLeft {
            width: 30%;
        }

        .tbodyTrTdRight {
            width: 70%;
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

        .nopadding {
            padding: 0 !important;
            margin: 0 !important;
        }

        .padding-bottom {
            padding-bottom: 10px !important;
        }

        .position-relative {
            position: relative !important;
            font-family: 'Noto Sans', sans-serif !important;
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

        .popup{
            display:none;
			position: fixed;
			/*top: 50%; 
            left: 50%;*/
			width: 90%;
			height: auto;
            max-height: 95%;
			/*background-color: #FFF;*/
			z-index:1002;
            /*overflow: auto;*/
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
        <span class="variaveis" id="lbldatainicio" runat="server"></span>
        <span class="variaveis" id="lbldatafim" runat="server"></span>
        <div class="variaveis" id="divDadosGrafico" runat="server"></div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server" style="position: fixed; top: 0; left: 0; z-index: 999; background-color: white;">
            <div id="divSelectStatsType" runat="server" >

            </div>
            <input type="button" class="form-control" value="Exportar" style="width: 15%; margin: auto; height: 40px; font-size: small; float: right" onclick="exportExcel();" />

            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding variaveis" runat="server" id="divEntradasAno" style="margin-top: 5px; line-height: 40px;">

            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding variaveis" runat="server" id="divEntradasMes" style="margin-top: 5px; line-height: 40px;">

            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding variaveis" runat="server" id="divDataInicioContrato" style="margin-top: 5px; line-height: 40px;">
                <span style="width: auto; float:left; font-weight: bold; margin-right: 5px">DE: </span>
                <input type='text' class='form-control' id='dataInicio' style='width: auto; margin: auto; height: 40px; float:left' onchange="loadStats();"/>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding variaveis" runat="server" id="divDataFimContrato" style="margin-top: 5px; line-height: 40px;">
                <span style="width: auto; float: left; font-weight: bold; margin-right: 5px">A: </span>
                <input type='text' class='form-control' id='dataFim' style='width: auto; margin: auto; height: 40px; float:left' onchange="loadStats();"/>
            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable" runat="server" style="margin-top: 120px;">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divGraficoEntradas" runat="server" style="margin-bottom: 25px; margin-top: 20px;">
            
        </div>
    </div>

    <div id="black_overlay" onclick="closePopup();">
        <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closePopup();" />
    </div>
    <div id="popup" class="popup" runat="server"></div>
    <div id="divLoading">
        <img src="img/loading.gif" id="loadingImg"/>
    </div>
    <%--<div id="divConfirm" style="width:75%; height:auto;z-index:1002; display: none; position: fixed; top: 50%; left: 50%; -moz-transform: translate(-50%, -50%); -ms-transform: translate(-50%, -50%); -o-transform: translate(-50%, -50%); -webkit-transform: translate(-50%, -50%); transform: translate(-50%, -50%); background-color: #FFFFFF">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="background-color: #4282b5; color:white; font-size: small; padding:5px;">
                        Deseja confirmar o recebimento do carkit?
                    </td>
                </tr>
                <tr>
                    <td style="height:5px;"></td>
                </tr>
                <tr>
                    <td style="float:right; padding:5px;">
                        <input id="btnCancel" value="Cancelar" type="button" onclick="showDivConfirm();" style="background-color: #4282b5; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin-left: 2px; margin-right: 2px; margin-top: 20px; margin-bottom: 20px; padding: 0 15px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;" />
                        <input id="btnConfirm" value="Confirmar" type="button" onclick="checkDivergencia();" style="background-color: #4282b5; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin-left: 2px; margin-right: 2px; margin-top: 20px; margin-bottom: 20px; padding: 0 15px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;" />
                    </td>
                </tr>
            </table>
        </div>--%>

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

    <script type="text/javascript" src="js/jquery.btechco.excelexport.js"></script>
    <script type="text/javascript" src="js/jquery.base64.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var linhaSelecionada;
        var dataGrafico = [];
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
            loadStats();

            $('#dataInicio').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val($('#lbldatainicio').html());
            $('#dataFim').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val($('#lbldatafim').html());

            $('#popup').center();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            $('#popup').center({ transition: 300 });
        });

        $(window).scroll(function () {
            
        });

        function loadStats() {
            if ($('#statsType').val() == '-1') {
                return;
            }

            $("#divLoading").addClass('show');

            if ($('#statsType').val() == '4') {
                $('#divEntradasAno').fadeIn();
                $('#divEntradasMes').fadeIn();
                $('#divDataInicioContrato').fadeOut();
                $('#divDataFimContrato').fadeOut();

                loadAno();
            }
            else if ($('#statsType').val() == '5') {
                $('#divDataInicioContrato').fadeIn();
                $('#divDataFimContrato').fadeIn();
                $('#divEntradasAno').fadeOut();
                $('#divEntradasMes').fadeOut();
                $('#divGraficoEntradas').fadeOut();

                loadContratosTable();
            }
            else {
                $('#divEntradasAno').fadeOut();
                $('#divEntradasMes').fadeOut();
                $('#divDataInicioContrato').fadeOut();
                $('#divDataFimContrato').fadeOut();
                $('#divGraficoEntradas').fadeOut();

                $.ajax({
                    type: "POST",
                    url: "Estatistica.aspx/loadStats",
                    data: '{"flag":"' + $('#statsType').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            $('#divTable').html(res.d);

                            $("#divLoading").removeClass('show');
                        }
                    }
                });
            }
        }

        function loadAno() {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadFiltroAno",
                data: '{"operatorID":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divEntradasAno').html(res.d);

                        loadMes();
                    }
                }
            });
        }

        function loadMes() {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadFiltroMes",
                data: '{"ano":"' + $('#entradasAnoSelect').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divEntradasMes').html(res.d);

                        loadEntradasTable();
                    }
                }
            });
        }

        function loadEntradasTable() {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadStatsEntradas",
                data: '{"ano":"' + $('#entradasAnoSelect').val() + '", "mes":"' + $('#entradasMesSelect').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTable').html(res.d);

                        loadDadosGraficoEntradas();
                    }
                }
            });
        }

        function loadDadosGraficoEntradas() {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadDadosGraficoEntradas",
                data: '{"ano":"' + $('#entradasAnoSelect').val() + '", "mes":"' + $('#entradasMesSelect').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divDadosGrafico').html(res.d);

                        if ($('#nrDias').html() == "0") {
                            $('#divGraficoEntradas').html('');
                            $('#divGraficoEntradas').fadeOut();
                        }
                        else {
                            $('#divGraficoEntradas').html('');
                            $('#divGraficoEntradas').fadeIn();
                            $('#divGraficoEntradas').css({"height" : "300px"});

                            setArray();

                            var chart = new CanvasJS.Chart("divGraficoEntradas", {
                                animationEnabled: true,

                                theme: "light1",

                                title: {
                                    text: "Entradas de " + $('#mesGrafico_0').html()
                                },

                                axisY: {
                                    title: "Nº Entradas"
                                },

                                data: [{
                                    type: "column",
                                    showInLegend: false,
                                    dataPoints: dataGrafico
                                }]
                            });

                            chart.render();

                            $('.canvasjs-chart-credit').addClass('variaveis');
                            $('.canvasjs-chart-canvas').addClass('position-relative');
                            $('.canvasjs-chart-container').addClass('padding-bottom');

                            $("#divLoading").removeClass('show');
                        }
                    }
                }
            });
        }

        function setArray() {
            dataGrafico = [];

            for (var i = 0; i < parseInt($('#nrdias').html()) ; i++) {
                dataGrafico.push({ y: parseInt($('#nrEntradasGrafico_' + i.toString()).html()), label: $('#diaGrafico_' + i.toString()).html() });
            }
        }

        function loadContratosTable() {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadStatsContratos",
                data: '{"data_inicio":"' + $('#dataInicio').val() + '", "data_fim":"' + $('#dataFim').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTable').html(res.d);

                        $("#divLoading").removeClass('show');
                    }
                }
            });
        }

        function exportExcel() {
            $("#tableGrid").btechco_excelexport({
                containerid: "tableGrid"
               , datatype: tableGrid.Table
               , filename: $("#statsType option:selected").text()
            });
            registaLog($('#lbloperatorid').html(), 'ESTATÍSTICAS', 'Exportação da seleção de ' + $("#statsType option:selected").text());
        }

        function closePopup() {
            $('#black_overlay').fadeOut();
            $('#popup').fadeOut();
            $('#content').removeClass('removeScroll');
            $('html, body').animate({
                scrollTop: $('#content').offset().top
            }, 'slow');
        }

        function openPopup() {
            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
            $('#content').addClass('removeScroll');
            $('html, body').animate({
                scrollTop: $('#content').offset().top
            }, 'slow');
        }

        function openPopupTipos(id_tipo) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosTipoContrato",
                data: '{"id_tipo":"' + id_tipo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupEstados(id_estado) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosEstadoContrato",
                data: '{"id_estado":"' + id_estado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function exportPopupTable() {
            $("#tablePopup").btechco_excelexport({
                containerid: "tablePopup"
               , datatype: $datatype.Table
               , filename: 'Listagem ' + $("#statsType option:selected").text()
            });
            registaLog($('#lbloperatorid').html(), 'ESTATÍSTICAS', 'Exportação da sub seleção de ' + $("#statsType option:selected").text());
        }

        function openPopupIdades(inf, sup) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosIdades",
                data: '{"margem_inferior":"' + inf + '", "margem_superior":"' + sup + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupPagantesNaoPagantes(tabela, id_estado, id_estado_pagamento) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosPagantes",
                data: '{"tabela":"' + tabela + '", "id_estado":"' + id_estado + '", "id_estado_pagamento":"' + id_estado_pagamento + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupEstadosMesSeguinte(id_estado) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosEstadoContratoMesSeguinte",
                data: '{"id_estado":"' + id_estado + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupPagantesMesSeguinte(pagante) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosPagantesMesSeguinte",
                data: '{"pagante":"' + pagante + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupProfissoes(rank) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosProfissoes",
                data: '{"rank":"' + rank + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }

        function openPopupSexo(rank) {
            $.ajax({
                type: "POST",
                url: "Estatistica.aspx/loadSociosSexo",
                data: '{"rank":"' + rank + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                        openPopup();
                    }
                }
            });
        }
    </script>
</body>
</html>
