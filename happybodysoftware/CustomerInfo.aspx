<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerInfo.aspx.cs" Inherits="CustomerInfo" Culture="auto" UICulture="auto" %>
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
            border: 1px #000 solid; 
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
            width: 30%;
            border-right: 1px red solid;
        }

        .headerRight {
            width: 70%;
            border-left: 1px red solid;
        }
        
        table tbody {
            background-color:#FFF;
            color:#000;
            font-size: medium;
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

        .row-no-padding > [class*="col-"] {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .row-no-padding-right > [class*="col-"] {
            padding-right: 0 !important;
        }

        .nopadding {
           padding: 0 !important;
           margin: 0 !important;
        }
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>
        <span class="variaveis" id="lblmeslimitecalendario" runat="server"></span>
        <span class="variaveis" id="lblanolimitecalendario" runat="server"></span>
        <span class="variaveis" id="lblescrita" runat="server"></span>
        <span class="variaveis" id="lblleitura" runat="server"></span>
        <div class="variaveis" id="datacalendardiv" runat="server"></div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="searchDiv">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadSocios();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                <input type='button' class='form-control' id='btnListagemSociosEmail' value='Listagem Sócio com Email' style="height: 50px; width: 100%; margin-bottom: 10px;" onclick="exportExcel();"/>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px; display:none;">
                <label class="checkbox-inline"><input type="checkbox" value="NR_SOCIO" id="check_nrsocio">Nº Sócio</label>
                <label class="checkbox-inline"><input type="checkbox" value="NOME" id="check_nome">Nome</label>
                <label class="checkbox-inline"><input type="checkbox" value="TELEMOVEL" id="check_telemovel">Telemóvel</label>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" style="margin-bottom:120px;" id="divInfo">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="listagemSociosComEmail" runat="server"></div>
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

    <!-- Calendar -->
    <script type="text/javascript" src="calendar_plugin/zabuto_calendar.min.js"></script>

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

            permissoes();
            $('#content').height($(parent.window).width());
            //var h = $('#ln_0').height() + $('#ln_1').height();
            //$('#img_socio').height(h);
            $('#img_socio').width($('#col_0_0').width());
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            //var h = $('#ln_0').height() + $('#ln_1').height();
            //$('#img_socio').height(h);
            $('#img_socio').width($('#col_0_0').width());
        });

        $(window).scroll(function () {
            
        });

        function permissoes() {
            if ($('#lblleitura').html() == '0') {
                $('#searchDiv').fadeOut();
                $('#table').fadeIn();
                $('#table').html('Não tem permissões de visualização nesta página!');
            }
            else {
                $('#searchDiv').fadeIn();
                $('#table').fadeIn();
                loadSocios();
            }
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
                            query += "( soc.NOME LIKE '%" + array[a] + "%' ";
                        }
                        else {
                            query += " AND soc.NOME LIKE '%" + array[a] + "%' ";
                        }
                    }

                    query += ") OR soc.EMAIL LIKE '%" + $('#search').val().trim() + "%' ";
                    query += " OR (SELECT TOP 1 CONVERT(VARCHAR(10), DATA_INICIO, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc) = @FILTRO ";
                    query += " OR (SELECT TOP 1 CONVERT(VARCHAR(10), DATA_FIM, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc) = @FILTRO ";
                    query += " OR REPLACE((SELECT TOP 1 CONVERT(VARCHAR(10), DATA_INICIO, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc), '/', '-') = @FILTRO ";
                    query += " OR REPLACE((SELECT TOP 1 CONVERT(VARCHAR(10), DATA_FIM, 103) FROM CONTRATO cont WHERE soc.sociosid = cont.socio_id order by data_fim desc), '/', '-') = @FILTRO ";
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
                url: "CustomerInfo.aspx/load",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + text + '", "query":"' + query + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#table').html(res.d);
                    }
                }
            });
        };

        function openCustomerInfo(id, x) {
            $('#lblidselected').html(x);

            $.ajax({
                type: "POST",
                url: "CustomerInfo.aspx/getCustomerData",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfo').html(res.d);
                        $('#divTable').fadeOut();
                        $('#divInfo').fadeIn();

                        //var h = $('#ln_0').height() + $('#ln_1').height();
                        //$('#img_socio').height(h);

                        if ($.UrlExists($('#fotoSocio').html()))
                            $('#img_socio').height($('#divDados').height());
                        else {
                            //$('#img_socio').height($('#divDados').height());
                            $('#divFotoSocio').html('');
                            $('#divFotoSocio').height($('#divDados').height());
                        }

                        loadEntradasSocio($('#nr_socio').html());
                    }
                }
            });
        }

        function closeCustomerInfo() {
            $('#divTable').fadeIn();
            $('#divInfo').fadeOut();
            $('#search').val('');
            loadSocios();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadSocios();
            }
        });

        function previous() {
            if ($('#lblidselected').html() != "0") {
                var prev = parseInt($('#lblidselected').html())-1;
                var id = $('#id_' + prev.toString()).html();
                openCustomerInfo(id, prev.toString());
            }
        }

        function next() {
            if(parseInt($('#lblidselected').html())!=(parseInt($('#countElements').html())-1)) {
                var n = parseInt($('#lblidselected').html()) + 1;
                var id = $('#id_' + n.toString()).html();
                openCustomerInfo(id, n.toString());
            }
        }

        function saveNotes(id) {
            $.ajax({
                type: "POST",
                url: "CustomerInfo.aspx/saveNotes",
                data: '{"id":"' + id + '", "notas":"' + $('#notes').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        
                    }
                }
            });
        }

        $.UrlExists = function (url) {
            var http = new XMLHttpRequest();
            http.open('HEAD', url, false);
            http.send();
            return http.status != 404;
        }

        function loadEntradasSocio(nr_socio) {
            $('#divCalendar').html('');
            $('#divCalendar').html('<div id="calendar1" style="width:100%"></div>');
            $('#calendar1').height($('#divCalendar').height());
            $('#calendar1').width($('#divCalendar').width());

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
                                eventData.push({ "date": $('#dataEntrada_' + i.toString()).html(), "badge": false });
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

        function exportExcel() {
            $('#tableGridListagem').btechco_excelexport({
                containerid: 'tableGridListagem'
               , datatype: $datatype.Table
               , filename: 'listagem_socios_email'
            });
        }
    </script>
</body>
</html>
