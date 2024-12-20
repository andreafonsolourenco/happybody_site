<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seguros.aspx.cs" Inherits="Seguros" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Atualizar Cliente</title>
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
            width: 90%;
            border-right: 1px red solid;
            text-align: center;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
        }

        .headerRight {
            width: 10%;
            border-left: 1px red solid;
            text-align: center;
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

        .tbodyTrTdLeft {
            width: 90%;
        }

        .tbodyTrTdRight {
            width: 10%;
        }

        table tbody tr td {
            padding: 5px;
            border: 1px #000 solid;
        }

        .row-no-padding > [class*="col-"] {
            padding-left: 0 !important;
            padding-right: 0 !important;
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
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>
        <span class="variaveis" id="lblnrsocio" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px;" id="divTable">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="divSearch">
                <input type='text' autocomplete="off" class='form-control' id='search' name='search' placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadSocios();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                <input type="button" class="form-control" value="Exportar" style="width: 100%; margin: auto; height: 40px; font-size: small;" onclick="exportExcel();" />
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                <label><input type='checkbox' class='form-control' id='dentroPrazo' style='width: auto; height: 15px; margin: auto;' checked/>Dentro do Prazo</label>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                <label><input type='checkbox' class='form-control' id='foraPrazo' style='width: auto; height: 15px; margin: auto;'/>Fora do Prazo</label>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                <label><input type='checkbox' class='form-control' id='pago' style='width: auto; height: 15px; margin: auto;' checked/>Pago</label>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                <label><input type='checkbox' class='form-control' id='naoPago' style='width: auto; height: 15px; margin: auto;'/>Não Pago</label>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                <label><input type='checkbox' class='form-control' id='acabarMesCorrente' style='width: auto; height: 15px; margin: auto;'/>Acaba Este Mês</label>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="formEdit"></div>
    </div>

    <div id="divLoading">
        <img src="img/loading.gif" id="loadingImg"/>
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

    <script type="text/javascript" src="js/jquery.btechco.excelexport.js"></script>
    <script type="text/javascript" src="js/jquery.base64.js"></script>

    <script type="text/javascript">
        var linhaSelecionada;
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            $('#search').val('');

            $('#dentroPrazo').on('click', function () {
                if ($('#dentroPrazo').is(':checked')) {
                    $('#foraPrazo').attr('checked', false);
                }

                loadSocios();
            });

            $('#foraPrazo').on('click', function () {
                if ($('#foraPrazo').is(':checked')) {
                    $('#dentroPrazo').attr('checked', false);
                }

                loadSocios();
            });

            $('#pago').on('click', function () {
                if ($('#pago').is(':checked')) {
                    $('#naoPago').attr('checked', false);
                }

                loadSocios();
            });

            $('#naoPago').on('click', function () {
                if ($('#naoPago').is(':checked')) {
                    $('#pago').attr('checked', false);
                }

                loadSocios();
            });

            $('#acabarMesCorrente').on('click', function () {
                loadSocios();
            });

            loadSocios();
        });

        $(window).on('resize', function () {
            
        });

        $(window).scroll(function () {
            
        });


        function loadSocios() {
            $("#divLoading").addClass('show');

            var query = "";
            var text = $('#search').val().trim();
            var array = new Array();

            if (text != "") {
                if (isNaN(text)) {
                    array = $('#search').val().trim().split(' ');

                    for (a in array) {
                        if (query == "") {
                            query += "( nome LIKE '%" + array[a] + "%' ";
                        }
                        else {
                            query += " AND nome LIKE '%" + array[a] + "%' ";
                        }
                    }

                    query += ") OR email LIKE '%" + $('#search').val().trim() + "%' ";
                }
                else {
                    query += " nr_socio = " + text + " OR telemovel = '" + text + "' ";
                }
            }
            else {
                query += " 1=1 ";
            }
            
            var dentroPrazo = "";
            var foraPrazo = "";
            var pago = "";
            var naoPago = "";
            var acabarMesCorrente = "";

            if ($('#dentroPrazo').is(":checked"))
                dentroPrazo = "1";

            if ($('#foraPrazo').is(":checked"))
                foraPrazo = "1";

            if ($('#pago').is(":checked"))
                pago = "1";

            if ($('#naoPago').is(":checked"))
                naoPago = "1";

            if ($('#acabarMesCorrente').is(":checked"))
                acabarMesCorrente = "1";

            $.ajax({
                type: "POST",
                url: "Seguros.aspx/load",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + text + '", "query":"' + query + '", "dentroPrazo":"' + dentroPrazo + '", "foraPrazo":"' + foraPrazo + '", "pago":"' + pago + '", "naoPago":"' + naoPago + '", "acabarMesCorrente":"' + acabarMesCorrente + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#table').html(res.d);

                        $("#divLoading").removeClass('show');
                    }
                }
            });
        };

        function show() {
            $('#divSearch').fadeOut();
            $('#formEdit').fadeIn();
            $('#divTable').fadeOut();
        }

        function back() {
            $('#divSearch').fadeIn();
            $('#formEdit').fadeOut();
            $('#divTable').fadeIn();
            $('#lblidselected').html('');
            $('#search').val('');

            loadSocios();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadSocios();
            }
        });

        function openCustomerInfo(id, x) {
            $('#lblidselected').html(id);
            $('#lblnrsocio').html(x);

            $.ajax({
                type: "POST",
                url: "Seguros.aspx/getCustomerData",
                data: '{"nr_socio":"' + x + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#formEdit').html(res.d);

                        $('#dataInicioEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataInicio').val() });
                        $('#dataFimEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataFim').val() });

                        $('#dataInicioEdit').change(function () {
                            loadFinalDate();
                        });
                    }
                }
            });

            show();
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        function loadFinalDate() {
            $.ajax({
                type: "POST",
                url: "Seguros.aspx/loadFinalDate",
                data: '{"data_inicio":"' + $('#dataInicio').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#dataFimEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: res.d, defaultDate: res.d }).val(res.d);
                    }
                }
            });
        }

        function updateInfo() {
            $("#divLoading").addClass('show');

            if ($('#dataInicioEdit').val() == "") {
                alertify.message('O campo data de início é obrigatório!');
                $("#divLoading").removeClass('show');
                return;
            }

            if ($('#dataFimEdit').val() == "") {
                alertify.message('O campo data de fim é obrigatório!');
                $("#divLoading").removeClass('show');
                return;
            }

            var pago;
            var renovacao_paga;

            if ($('#pagoEdit').is(':checked'))
                pago = "1";
            else
                pago = "0";

            if ($('#renovacao_pagaEdit').is(':checked'))
                renovacao_paga = "1";
            else
                renovacao_paga = "0";

            $.ajax({
                type: "POST",
                url: "Seguros.aspx/updateSeguro",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_seguro":"' + $('#id_seguro').html() + '", "id_socio":"' + $('#id_socio').html()
                    + '", "data_inicio":"' + $('#dataInicioEdit').val() + '", "data_fim":"' + $('#dataFimEdit').val()
                    + '", "pago":"' + pago + '", "notas":"' + $('#notasEdit').val() + '", "renovacao_paga":"' + renovacao_paga + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //$('#formEdit').html(res.d);

                        if (res.d.indexOf('erro') > -1) {
                            
                        }
                        else {
                            openCustomerInfo($('#lblidselected').html(), $('#lblnrsocio').html());
                            back();
                        }

                        alertify.message(res.d);
                        $("#divLoading").removeClass('show');
                    }
                }
            });

        };

        function exportExcel() {
            $("#tableGrid").btechco_excelexport({
                containerid: "tableGrid"
               , datatype: $datatype.Table
               , filename: 'Seguros'
            });
        }
    </script>
</body>
</html>
