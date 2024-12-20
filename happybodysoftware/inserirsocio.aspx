<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inserirsocio.aspx.cs" Inherits="inserirsocio" Culture="auto" UICulture="auto" %>
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
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px;" id="divTable">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                <input type='text' class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadSocios();" style="float:right; width:auto; height:50px; margin:auto;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="formEdit"></div>
        
        <div id="formSocio" class="variaveis">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 line">
                <input type="number" class='form-control' id='nrSocio' name="nrSocio" placeholder='Nº' required="required" style="width: 100%; margin: auto;"/>
            </div>
            <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11 line">
                <input type='text' class='form-control' id='name' name="name" placeholder='Nome' required="required" style="width: 100%; margin: auto;"/>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 line">
                <input type='text' class='form-control' id='morada' name="morada" placeholder='Morada' required="required" style="width: 100%; margin: auto;"/>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='codpostal' name="codpostal" placeholder='Código Postal: xxxx-xxx' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 line">
                <input type='text' class='form-control' id='localidade' name="localidade" placeholder='Localidade' required="required" style="width: 100%; margin: auto; float: left"/>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='tlf' name="tlf" placeholder='Telefone' required="required" style="width: 100%; margin: auto; float: left"/>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='tlfemergencia' name="tlfemergencia" placeholder='Telefone de Emergência' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='email' name="email" placeholder='Email' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='datanasc' name="datanasc" placeholder='Data de Nascimento' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='cc' name="cc" placeholder='Nº CC' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='validadecc' name="validadecc" placeholder='Validade CC' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <input type='text' class='form-control' id='profissao' name="profissao" placeholder='Profissão' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line" id="comercialDiv" runat="server"></div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                <form style="width:100%" id="sexo" name="sexo">
                    <label class="radio-inline"><input type="radio" name="optradio" value="M" checked="checked">Masculino</label>
                    <label class="radio-inline"><input type="radio" name="optradio" value="F">Feminino</label>
               </form>
            </div>
        </div>
    </div>

    <div class="footer" id="footer">
        <input id="btnVoltar" type="button" class="form-control btn" value="Voltar" onclick="ret();" />
        <input id="btnInsert" type="button" class="form-control btn" value="Inserir" onclick="showForm();"/>
        <input id="btnEdit" type="button" class="form-control btn variaveis" value="Alterar" onclick="editCustomer();"/>
        <input id="btnTable" type="button" class="form-control btn variaveis" value="Voltar" onclick="showTable();"/>
        <input id="btnClean" type="button" class="form-control btn variaveis" value="Limpar" onclick="limparDados();"/>
        <input id="btnAdd" type="button" class="form-control btn variaveis" value="Adicionar" onclick="insertSocio();"/>
        <input id="btnBackEdit" type="button" class="form-control btn variaveis" value="Voltar" onclick="backEdit();"/>
        <input id="btnAtualizar" type="button" class="form-control btn variaveis" value="Atualizar" onclick="edit();"/>
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

    <script type="text/javascript">
        var linhaSelecionada;
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            $('#content').height($(window).height() - $('#header').height());

            $('#datanasc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val($('#mindate').html());
            $('#validadecc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val($('#maxdate').html());

            $('#search').val('');

            loadSocios();
        });

        $(window).on('resize', function () {
            $('#content').height($(window).height() - $('#header').height());
        });

        $(window).scroll(function () {
            
        });

        function ret() {
            window.location.href = 'MainMenu.aspx?id=' + $('#lbloperatorid').html();
        }

        function insertSocio() {
            if ($('#name').val() == "") {
                alertify.message('O campo nome é obrigatório!');
                return;
            }

            if ($('#morada').val() == "") {
                alertify.message('O campo morada é obrigatório!');
                return;
            }

            if ($('#localidade').val() == "") {
                alertify.message('O campo localidade é obrigatório!');
                return;
            }

            if ($('#codpostal').val() == "") {
                alertify.message('O campo código-postal é obrigatório!');
                return;
            }

            if ($('#tlf').val() == "") {
                alertify.message('O campo telefone é obrigatório!');
                return;
            }
            
            if ($('#tlfemergencia').val() == "") {
                alertify.message('O campo telefone de emergência é obrigatório!');
                return;
            }

            if ($('#email').val() == "") {
                alertify.message('O campo email é obrigatório!');
                return;
            }

            if ($('#datanasc').val() == "") {
                alertify.message('O campo data de nascimento é obrigatório!');
                return;
            }

            if ($('#cc').val() == "") {
                alertify.message('O campo cartão de cidadão é obrigatório!');
                return;
            }

            if ($('#validadecc').val() == "") {
                alertify.message('O campo validade do cartão de cidadão é obrigatório!');
                return;
            }

            if ($('#profissao').val() == "") {
                alertify.message('O campo profissão é obrigatório!');
                return;
            }

            if ($('#comercial').val() == "") {
                alertify.message('O campo comercial é obrigatório!');
                return;
            }

            var nr = $('#nrSocioEdit').val();

            if ($('#nrSocioEdit').val() == "" || $('#nrSocioEdit').val() == undefined) {
                nr = "0";
            }

            var myRadio = $('input[name=optradio]');
            var checkedValue = myRadio.filter(':checked').val();

            $.ajax({
                type: "POST",
                url: "inserirsocio.aspx/insert",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "nome":"' + $('#name').val() + '", "morada":"' + $('#morada').val()
                    + '", "codpostal":"' + $('#codpostal').val() + '", "localidade":"' + $('#localidade').val() + '", "tlf_emergencia":"' + $('#tlfemergencia').val()
                    + '", "tlm":"' + $('#tlf').val() + '", "dataNascimento":"' + $('#datanasc').val() + '", "cc":"' + $('#cc').val() + '", "validadeCC":"' + $('#validadecc').val()
                    + '", "profissao":"' + $('#profissao').val() + '", "id_comercial":"' + $('#comercial').val() + '", "sexo":"' + checkedValue
                    + '", "email":"' + $('#email').val() + '", "nrSocio":"' + nr + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#search').val('');
                        limparDados();
                        showTable();
                        loadSocios();
                        alertify.message(res.d);
                    }
                }
            });

        };

        function loadSocios() {
            $.ajax({
                type: "POST",
                url: "inserirsocio.aspx/load",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + $('#search').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#table').html(res.d);

                        //$('.grdTable').bootstrapTable();

                        //$('.grdTable').on('click-row.bs.table', function (e, row, $element) {
                        //    linhaSelecionada = $element;
                        //    $element.addClass('highlight').siblings().removeClass('highlight');
                        //});
                    }
                }
            });

        };

        function limparDados() {
            $('#name').val('');
            $('#morada').val('');
            $('#localidade').val('');
            $('#codpostal').val('');
            $('#tlf').val('');
            $('#tlfemergencia').val('');
            $('#email').val('');
            $('#datanasc').val('');
            $('#cc').val('');
            $('#validadecc').val('');
            $('#profissao').val('');
            $('#nrSocio').val('');
            //$('#comercial').val('');
        }

        function showForm() {
            $('#btnVoltar').fadeOut();
            $('#btnInsert').fadeOut();
            $('#divTable').fadeOut();
            $('#btnTable').fadeIn();
            $('#btnClean').fadeIn();
            $('#btnAdd').fadeIn();
            $('#formSocio').fadeIn();
            $('#title').html('Inscrição de Sócio');
        }

        function showTable() {
            $('#btnVoltar').fadeIn();
            $('#btnInsert').fadeIn();
            $('#divTable').fadeIn();
            $('#btnTable').fadeOut();
            $('#btnClean').fadeOut();
            $('#btnAdd').fadeOut();
            $('#formSocio').fadeOut();
            $('#title').html('Sócios');
        }

        function showEdit() {
            $('#btnVoltar').fadeOut();
            $('#btnInsert').fadeOut();
            $('#formEdit').fadeIn();
            $('#divTable').fadeOut();
            $('#title').html('Atualização de Sócio');
            $('#btnEdit').fadeOut();
            $('#btnBackEdit').fadeIn();
            $('#btnAtualizar').fadeIn();
        }

        function backEdit() {
            $('#btnVoltar').fadeIn();
            $('#btnInsert').fadeIn();
            $('#formEdit').fadeOut();
            $('#divTable').fadeIn();
            $('#title').html('Sócios');
            $('#btnEdit').fadeOut();
            $('#btnBackEdit').fadeOut();
            $('#btnAtualizar').fadeOut();
            $('#lblidselected').html('');

            for (i = 0; i < parseInt($('#countElements').html()) ; i++) {
                var ln = '#ln_' + i.toString();

                $(ln).removeClass('tbodyTrSelected');
            }
        }

        function selectRow(x) {
            for (i = 0; i < parseInt($('#countElements').html()); i++) {
                var ln = '#ln_' + i.toString();

                $(ln).removeClass('tbodyTrSelected');
            }

            ln = '#ln_' + x.toString();
            var id = '#id_' + x.toString();

            if($('#lblidselected').html() =="" || $('#lblidselected').html() != $(id).html()) {
                $('#lblidselected').html($(id).html());
                $(ln).addClass('tbodyTrSelected');
                $('#btnEdit').fadeIn();
            }
            else {
                $('#lblidselected').html('');
                $('#btnEdit').fadeOut();
            }
        }

        function editCustomer() {
            $.ajax({
                type: "POST",
                url: "inserirsocio.aspx/getCustomerData",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id":"' + $('#lblidselected').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#formEdit').html(res.d);
                        loadComercialEdit();
                    }
                }
            });
        }

        function loadComercialEdit() {
            $.ajax({
                type: "POST",
                url: "inserirsocio.aspx/loadComerciaisEdit",
                data: '{"id":"' + $('#id_comercialedit').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#comercialDivEdit').html(res.d);

                        $('#datanascEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#datanascEdit').val() });
                        $('#validadeccEdit').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#validadeccEdit').val() });

                        showEdit();
                    }
                }
            });
        }

        function edit() {
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

            $.ajax({
                type: "POST",
                url: "inserirsocio.aspx/editar",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "nome":"' + $('#nameEdit').val() + '", "morada":"' + $('#moradaEdit').val()
                    + '", "codpostal":"' + $('#codpostalEdit').val() + '", "localidade":"' + $('#localidadeEdit').val() + '", "tlf_emergencia":"' + $('#tlfemergenciaEdit').val()
                    + '", "tlm":"' + $('#tlfEdit').val() + '", "dataNascimento":"' + $('#datanascEdit').val() + '", "cc":"' + $('#ccEdit').val() + '", "validadeCC":"' + $('#validadeccEdit').val()
                    + '", "profissao":"' + $('#profissaoEdit').val() + '", "id_comercial":"' + $('#comercialEdit').val() + '", "sexo":"' + checkedValue
                    + '", "email":"' + $('#emailEdit').val() + '", "id_socio":"' + $('#lblidselected').html() + '", "nrSocio":"' + nr + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#search').val('');
                        backEdit();
                        loadSocios();
                        showTable();
                        alertify.message(res.d);
                    }
                }
            });

        };
    </script>
</body>
</html>
