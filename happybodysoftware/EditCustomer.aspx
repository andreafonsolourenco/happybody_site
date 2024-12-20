<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCustomer.aspx.cs" Inherits="EditCustomer" Culture="auto" UICulture="auto" %>
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
            width: 30%;
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
        }

        .headerRight {
            width: 70%;
            border-left: 1px red solid;
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
            width: 30%;
        }

        .tbodyTrTdRight {
            width: 70%;
        }

        table tbody tr td {
            padding: 5px;
            border: 1px #000 solid;
        }

        .row-no-padding > [class*="col-"] {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .addon-euro {
            background-color: gray !important;
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
        <span class="variaveis" id="lblescrita" runat="server"></span>
        <span class="variaveis" id="lblleitura" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px;" id="divTable">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="searchDiv">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadSocios();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px; display:none;">
                <label class="checkbox-inline"><input type="checkbox" value="NR_SOCIO" id="check_nrsocio">Nº Sócio</label>
                <label class="checkbox-inline"><input type="checkbox" value="NOME" id="check_nome">Nome</label>
                <label class="checkbox-inline"><input type="checkbox" value="TELEMOVEL" id="check_telemovel">Telemóvel</label>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="formEdit"></div>
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

        $(document).ready(function () {
            $('#search').val('');

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
        });

        $(window).on('resize', function () {
            
        });

        $(window).scroll(function () {
            
        });

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
                url: "EditCustomer.aspx/load",
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

        function showEdit() {
            $('#btnVoltar').fadeOut();
            $('#formEdit').fadeIn();
            $('#divTable').fadeOut();
        }

        function backEdit() {
            $('#formEdit').fadeOut();
            $('#divTable').fadeIn();
            $('#lblidselected').html('');

            for (i = 0; i < parseInt($('#countElements').html()) ; i++) {
                var ln = '#ln_' + i.toString();

                $(ln).removeClass('tbodyTrSelected');
            }
        }

        function editCustomer() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/getCustomerData",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id":"' + $('#lblidselected').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#formEdit').html(res.d);
                        loadComercialEdit();
                        $('#divClose').height($('#divBtns').height());
                        var notas = $('#notasEdit').val();
                        notas = replaceAll(notas, "@", "\"")
                        notas = replaceAll(notas, "#", "'");
                        var morada = $('#moradaEdit').val();
                        morada = replaceAll(morada, "@", "\"");
                        morada = replaceAll(morada, "#", "'");
                        $('#notasEdit').val(notas);
                        $('#moradaEdit').val(morada);
                    }
                }
            });
        }

        function loadComercialEdit() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/loadComerciaisEdit",
                data: '{"id":"' + $('#id_comercialedit').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#comercialDivEdit').html(res.d);

                        $('#datanascEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#datanascEdit').val() });
                        $('#validadeccEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#validadeccEdit').val() });

                        showEdit();

                        //$('#dataInícioEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataInícioEdit').val() });
                        //$('#dataFimEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataFimEdit').val() });

                        //$('#dataInícioEdit').change(function () {
                        //    loadFinalDate();
                        //});

                        //loadEstadoContrato();
                        //loadTipoContrato();
                    }
                }
            });
        }

        function loadTipoContrato() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/loadTiposContrato",
                data: '{"id":"' + $('#id_tipocontratoedit').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#tipoContratoDivEdit').html(res.d);
                    }
                }
            });
        }

        function loadEstadoContrato() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/loadEstadosContrato",
                data: '{"id":"' + $('#id_estadocontratoedit').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#estadoContratoDivEdit').html(res.d);
                    }
                }
            });
        }

        function edit() {
            alertify.confirm('Sócios', 'Tem a certeza que deseja atualizar a informação relativa a este Sócio?',
                function () {
                    if ($('#nameEdit').val() == "") {
                        alertify.message('O campo nome é obrigatório!');
                        return;
                    }

                    if ($('#moradaEdit').val() == "") {
                        alertify.message('O campo morada é obrigatório!');
                        return;
                    }

                    if ($('#localidadeEdit').val() == "") {
                        alertify.message('O campo localidade é obrigatório!');
                        return;
                    }

                    if ($('#codpostalEdit').val() == "") {
                        alertify.message('O campo código-postal é obrigatório!');
                        return;
                    }

                    if ($('#tlfEdit').val() == "") {
                        alertify.message('O campo telefone é obrigatório!');
                        return;
                    }

                    if ($('#tlfemergenciaEdit').val() == "") {
                        alertify.message('O campo telefone de emerência é obrigatório!');
                        return;
                    }

                    if ($('#emailEdit').val() == "") {
                        alertify.message('O campo email é obrigatório!');
                        return;
                    }

                    if ($('#datanascEdit').val() == "") {
                        alertify.message('O campo data de nascimento é obrigatório!');
                        return;
                    }

                    if ($('#ccEdit').val() == "") {
                        alertify.message('O campo cartão de cidadão é obrigatório!');
                        return;
                    }

                    if ($('#validadeccEdit').val() == "") {
                        alertify.message('O campo validade do cartão de cidadão é obrigatório!');
                        return;
                    }

                    if ($('#profissaoEdit').val() == "") {
                        alertify.message('O campo profissão é obrigatório!');
                        return;
                    }

                    if ($('#comercialEdit').val() == "") {
                        alertify.message('O campo comercial é obrigatório!');
                        return;
                    }

                    var nr = $('#nrSocioEdit').val();

                    if ($('#nrSocioEdit').val() == "" || $('#nrSocioEdit').val() == undefined) {
                        nr = "0";
                    }

                    var myRadio = $('input[name=optradioEdit]');
                    var checkedValue = myRadio.filter(':checked').val();

                    var publicidade;

                    if ($('#publicidadeEdit').is(':checked'))
                        publicidade = "1";
                    else
                        publicidade = "0";

                    var notas = $('#notasEdit').val();
                    notas = replaceAll(notas, "\"", "''");
                    notas = replaceAll(notas, "'", "#");

                    var morada = $('#moradaEdit').val();
                    morada = replaceAll(morada, "\"", "''");
                    morada = replaceAll(morada, "'", "#");

                    $.ajax({
                        type: "POST",
                        url: "EditCustomer.aspx/editar",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "nome":"' + $('#nameEdit').val() + '", "morada":"' + morada
                            + '", "codpostal":"' + $('#codpostalEdit').val() + '", "localidade":"' + $('#localidadeEdit').val() + '", "tlf_emergencia":"' + $('#tlfemergenciaEdit').val()
                            + '", "tlm":"' + $('#tlfEdit').val() + '", "dataNascimento":"' + $('#datanascEdit').val() + '", "cc":"' + $('#ccEdit').val() + '", "validadeCC":"' + $('#validadeccEdit').val()
                            + '", "profissao":"' + $('#profissaoEdit').val() + '", "id_comercial":"' + $('#comercialEdit').val() + '", "sexo":"' + checkedValue
                            + '", "email":"' + $('#emailEdit').val() + '", "id_socio":"' + $('#lblidselected').html()
                            + '", "nrSocio":"' + nr + '", "publicidade":"' + publicidade + '", "notas":"' + notas + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                alertify.message(res.d);
                                //editarContrato();
                                $('#search').val('');
                                backEdit();
                                loadSocios();
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        };

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if ($('#lblleitura').html() == '1')
                    loadSocios();
            }
        });

        function openCustomerInfo(id, x) {
            if ($('#lblescrita').html() == '0') {
                $('#formEdit').html('Não tem permissões para alterar a informação dos sócios!');
                showEdit();
            }
            else {
                $('#lblidselected').html(id);

                editCustomer();
            }
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        function editarContrato() {
            var debito_direto;
            var precoEdit;

            if ($('#debitoDiretoEdit').is(':checked'))
                debito_direto = "1";
            else
                debito_direto = "0";

            precoEdit = $('#precoEdit').val().replace(",", ".");

            if (!isNumber(precoEdit)) {
                alertify.message('Por favor, insira um valor válido para o valor do contrato!');
                return;
            }


            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/editarContrato",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_contrato":"' + $('#id_contratoedit').html()
                    + '", "id_tipocontrato":"' + $('#tipoContratoEdit').val() + '", "id_estadocontrato":"' + $('#estadoContratoEdit').val()
                    + '", "data_inicio":"' + $('#dataInícioEdit').val() + '", "data_fim":"' + $('#dataFimEdit').val()
                    + '", "debito_direto":"' + debito_direto + '", "notas":"' + $('#notasEdit').val() + '", "preco":"' + precoEdit + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //$('#formEdit').val(res.d);
                        //$('#search').val(res.d);
                        $('#search').val('');
                        backEdit();
                        loadSocios();
                    }
                }
            });
        }

        function loadFinalDate() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/loadFinalDate",
                data: '{"id_tipocontrato":"' + $('#tipoContratoEdit').val() + '", "data_inicio":"' + $('#dataInícioEdit').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#dataFimEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: res.d, defaultDate: res.d }).val(res.d);
                        loadPrecoTipoContrato();
                    }
                }
            });
        }

        function loadPrecoTipoContrato() {
            $.ajax({
                type: "POST",
                url: "EditCustomer.aspx/loadPrecoTipoContrato",
                data: '{"id_tipocontrato":"' + $('#tipoContratoEdit').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#precoEdit').val(res.d);
                    }
                }
            });
        }

        function removeCustomer() {
            alertify.confirm('Sócios', 'Tem a certeza que deseja remover este Sócio?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "EditCustomer.aspx/apagaSocio",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_socio":"' + $('#lblidselected').html() + '", "id_contrato":"' + $('#id_contratoedit').html() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#formEdit').html(res.d);

                                if (res.d.indexOf('Erro') > -1) {
                                    alertify.message(res.d);
                                }
                                else {
                                    backEdit();
                                    $('#search').val('');
                                    loadSocios();
                                    alertify.message(res.d);
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }
    </script>
</body>
</html>
