<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertCustomer.aspx.cs" Inherits="InsertCustomer" Culture="auto" UICulture="auto" %>

<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Happy Body</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link href="bootstrap/dist/css/bootstrap-theme.min.css" rel="stylesheet" type='text/css' />
    <link href="bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" type='text/css' />
    <link href="bootstrap/dist/css/bootstrap-table.min.css" rel="stylesheet" type='text/css' />
    <link href="jquery/jquery-ui.theme.min.css" rel="stylesheet" type='text/css' />
    <link href="jquery/jquery-ui.structure.min.css" rel="stylesheet" type='text/css' />
    <link href="jquery/jquery-ui.min.css" rel="stylesheet" type='text/css>' />
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type='text/css' />
    <link rel="stylesheet" type="text/css" href="css/software.css" />

    <link href="alertify/css/alertify.min.css" rel="stylesheet" type='text/css' />
    <link href="alertify/css/themes/semantic.min.css" rel="stylesheet" type='text/css' />
    <link href="alertify/css/themes/default.min.css" rel="stylesheet" type='text/css' />
    <!-- Bootstrap Date-Picker Plugin -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <!-- Clock Picker -->
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/dist/bootstrap-clockpicker.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/github.min.css" />
    <meta charset="utf-8" />

    <style type="text/css">
        .header {
            background-color: #000;
            position: fixed;
            top: 0;
            width: 100%;
            height: 100px;
            z-index: 100;
            padding: 5px;
            line-height: 100px;
            font-family: 'Amaranth', sans-serif;
            color: #FFF;
            font-size: 2vw;
        }

        .footer {
            font-family: 'Amaranth', sans-serif;
            bottom: 0;
            height: 80px;
            width: 100%;
            position: fixed;
            background-color: #000;
            text-align: center;
            border-top: 3px solid #e6e7e8;
            vertical-align: middle;
            z-index: 101;
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
            width: 100%;
            height: auto;
            border: 1px #000 solid;
            font-family: 'Noto Sans', sans-serif !important;
        }

            table thead {
                background-color: #000;
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
            background-color: #FFF;
            color: #000;
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

        .nopadding {
            padding: 0 !important;
            margin: 0 !important;
        }

        #divLoading {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 999999;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }

            #divLoading.show {
                display: block;
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
        <span class="variaveis" id="lblescrita" runat="server"></span>
        <span class="variaveis" id="lblleitura" runat="server"></span>
        <span class="variaveis" id="lblidpreinscricao" runat="server"></span>

        <div id="formSocio">
            <div class='col-lg-12 col-nmd-12 col-sm-12 col-xs-12'>
                <input id='btnClean' value='Limpar' runat='server' type='button' onclick='limparDados();' style='background-color: #4282b5; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;' />
                <input id='btnAdd' value='Inserir' runat='server' type='button' onclick='createXML();' style='background-color: #4282b5; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;' />
                <input id='btnSelectPreInscricao' value='Selecionar Pré-Inscrição' runat='server' type='button' onclick='openPreInscricao();' style='background-color: #4282b5; float: right; width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;' />
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 line">
                Nº Sócio
                <input type="number" class='form-control' id='nrSocio' name="nrSocio" placeholder='Nº Sócio' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 line">
                Nome
                <input type='text' class='form-control' id='name' name="name" placeholder='Nome' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 line">
                Morada
                <input type='text' class='form-control' id='morada' name="morada" placeholder='Morada' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Cod. Postal
                <input type='text' class='form-control' id='codpostal' name="codpostal" placeholder='Código Postal: xxxx-xxx' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 line">
                Localidade
                <input type='text' class='form-control' id='localidade' name="localidade" placeholder='Localidade' required="required" style="width: 100%; margin: auto; float: left" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Telefone
                <input type='text' class='form-control' id='tlf' name="tlf" placeholder='Telefone' required="required" style="width: 100%; margin: auto; float: left" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Tlf Emergência
                <input type='text' class='form-control' id='tlfemergencia' name="tlfemergencia" placeholder='Telefone de Emergência' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Email
                <input type='text' class='form-control' id='email' name="email" placeholder='Email' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Data de Nascimento
                <input type='text' class='form-control' id='datanasc' name="datanasc" placeholder='Data de Nascimento' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                CC Nº
                <input type='text' class='form-control' id='cc' name="cc" placeholder='Nº CC' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Validade CC
                <input type='text' class='form-control' id='validadecc' name="validadecc" placeholder='Validade CC' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Profissão
                <input type='text' class='form-control' id='profissao' name="profissao" placeholder='Profissão' required="required" style="width: 100%; margin: auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line" id="comercialDiv" runat="server"></div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 line">
                Sexo
                <form style="width: 100%" id="sexo" name="sexo">
                    <label class="radio-inline">
                        <input type="radio" name="optradio" value="M" checked="checked">Masculino</label>
                    <label class="radio-inline">
                        <input type="radio" name="optradio" value="F">Feminino</label>
                </form>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line' id='tipoContratoDiv' runat='server'></div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line' id='estadoContratoDiv' runat='server'></div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Data Início:</div>
                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                    <input type='text' class='form-control' id='dataInício' name='dataInício' placeholder='Data Início' />
                </div>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Data Fim:</div>
                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                    <input type='text' class='form-control' id='dataFim' name='dataFim' placeholder='Data Fim' />
                </div>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Débito Direto:</div>
                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                    <input type='checkbox' class='form-control' id='debitoDireto' name='debitoDireto' style='width: 100%; margin: auto; height: 50px;' />
                </div>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding'>Não deseja receber informação:</div>
                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding'>
                    <input type='checkbox' class='form-control' id='naodesejapublicidade' name='naodesejapublicidade' style='width: 100%; margin: auto; height: 50px;' />
                </div>
            </div>
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                Notas
                <textarea class='form-control' id='notas' name='notas' style='width: 100%; margin: auto; height: auto;' rows='5'></textarea>
            </div>
        </div>

        <div id="divPreInscricao" style="display: none;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="searchDiv">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 50%; float: left;" />
                <input type="image" src="img/icons/icon_search.png" onclick="loadPreInscricao();" style="width: auto; height: 50px; float: left; margin: auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
                <img src='img/icons/icon_close.png' style='cursor: pointer; float: right; vertical-align: middle; height: 25px; width: auto;' onclick='closePreInscricao();' />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 10px;" id="preInscricaoTable"></div>
            <div class="variaveis" id="divVariaveisPreInscricao"></div>
        </div>
    </div>

    <div id="divLoading">
        <img src="img/loading.gif" id="loadingImg" />
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

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if ($('#divPreInscricao').is(":visible")) {
                    loadPreInscricao();
                }
            }
        });

        $(document).ready(function () {
            var date = new Date();

            var mes = "";

            if (date.getMonth() + 1 < 10)
                mes = '0' + (date.getMonth() + 1).toString();
            else
                mes = (date.getMonth() + 1).toString();

            $('#datanasc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val();
            $('#validadecc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true }).val();
            $('#dataInício').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: '01/' + mes + '/' + date.getFullYear(), defaultDate: '01/' + mes + '/' + date.getFullYear(), }).val('01/' + mes + '/' + date.getFullYear().toString());

            $('#dataInício').change(function () {
                loadFinalDate();
            });

            loadFinalDate();

            permissoes();
        });

        $(window).on('resize', function () {

        });

        $(window).scroll(function () {

        });

        function permissoes() {
            if ($('#lblescrita').html() == '0') {
                $('#formSocio').html('Não tem permissões de inserção de novos sócios!');
                $('#btnAdd').fadeOut();
                $('#btnSelectPreInscricao').fadeOut();
            }
        }

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
            var date = new Date();

            var mes = "";

            if (date.getMonth() + 1 < 10)
                mes = '0' + (date.getMonth() + 1).toString();
            else
                mes = (date.getMonth() + 1).toString();

            $('#dataInício').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: '01/' + mes + '/' + date.getFullYear(), defaultDate: '01/' + mes + '/' + date.getFullYear() }).val('01/' + mes + '/' + date.getFullYear().toString());
            $("#debitoDireto").prop("checked", false);
            $("#naodesejapublicidade").prop("checked", false);
            $('#notas').val('');
            loadFinalDate();
        }

        function loadFinalDate() {
            $.ajax({
                type: "POST",
                url: "InsertCustomer.aspx/loadFinalDate",
                data: '{"id_tipocontrato":"' + $('#tipoContrato').val() + '", "data_inicio":"' + $('#dataInício').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#dataFim').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: res.d, defaultDate: res.d }).val(res.d);
                    }
                }
            });
        }

        function createXML() {
            var xml = "<NOVO_SOCIO>";

            var nr = $('#nrSocio').val();

            if ($('#nrSocio').val() == "" || $('#nrSocio').val() == undefined) {
                nr = "0";
            }

            var myRadio = $('input[name=optradio]');
            var checkedValue = myRadio.filter(':checked').val();

            var publicidade;

            if ($('#naodesejapublicidade').is(':checked'))
                publicidade = "1";
            else
                publicidade = "0";

            var notas = $('#notas').val();
            notas = replaceAll(notas, "\"", "''");
            notas = replaceAll(notas, "'", "#");

            var morada = $('#morada').val();
            morada = replaceAll(morada, "\"", "''");
            morada = replaceAll(morada, "'", "#");

            xml += "<SOCIO>";
            xml += "<NOME>" + $('#name').val() + "</NOME>";
            xml += "<MORADA>" + morada + "</MORADA>";
            xml += "<COD_POSTAL>" + $('#codpostal').val() + "</COD_POSTAL>";
            xml += "<LOCALIDADE>" + $('#localidade').val() + "</LOCALIDADE>";
            xml += "<TLF_EMERGENCIA>" + $('#tlfemergencia').val() + "</TLF_EMERGENCIA>";
            xml += "<TLM>" + $('#tlf').val() + "</TLM>";
            xml += "<DATA_NASC>" + $('#datanasc').val() + "</DATA_NASC>";
            xml += "<CC_NR>" + $('#cc').val() + "</CC_NR>";
            xml += "<VALIDADE_CC>" + $('#validadecc').val() + "</VALIDADE_CC>";
            xml += "<PROFISSAO>" + $('#profissao').val() + "</PROFISSAO>";
            xml += "<ID_COMERCIAL>" + $('#comercial').val() + "</ID_COMERCIAL>";
            xml += "<SEXO>" + checkedValue + "</SEXO>";
            xml += "<EMAIL>" + $('#email').val() + "</EMAIL>";
            xml += "<NR_SOCIO>" + nr + "</NR_SOCIO>";
            xml += "<NOTAS>" + notas + "</NOTAS>";
            xml += "<NAO_QUER_PUB>" + publicidade + "</NAO_QUER_PUB>";
            xml += "</SOCIO>";

            var debito_direto;

            if ($('#debitoDireto').is(':checked'))
                debito_direto = "1";
            else
                debito_direto = "0";

            xml += "<CONTRATO>";
            xml += "<ID_TIPO>" + $('#tipoContrato').val() + "</ID_TIPO>";
            xml += "<ID_ESTADO>" + $('#estadoContrato').val() + "</ID_ESTADO>";
            xml += "<DATA_INICIO>" + $('#dataInício').val() + "</DATA_INICIO>";
            xml += "<DATA_FIM>" + $('#dataFim').val() + "</DATA_FIM>";
            xml += "<NOTAS>" + notas + "</NOTAS>";
            xml += "<DD>" + debito_direto + "</DD>";
            xml += "</CONTRATO>";

            xml += "</NOVO_SOCIO>";

            createNewCustomer(xml);
        }

        function createNewCustomer(xml) {
            alertify.confirm('Sócios', 'Tem a certeza que deseja inserir um novo Sócio?',
                function () {
                    $("#divLoading").addClass('show');

                    if ($('#name').val() == "") {
                        alertify.message('O campo nome é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#morada').val() == "") {
                        alertify.message('O campo morada é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#localidade').val() == "") {
                        alertify.message('O campo localidade é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#codpostal').val() == "") {
                        alertify.message('O campo código-postal é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#tlf').val() == "") {
                        alertify.message('O campo telefone é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#tlfemergencia').val() == "") {
                        alertify.message('O campo telefone de emergência é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#email').val() == "") {
                        alertify.message('O campo email é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#datanasc').val() == "") {
                        alertify.message('O campo data de nascimento é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#cc').val() == "") {
                        alertify.message('O campo cartão de cidadão é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#validadecc').val() == "") {
                        alertify.message('O campo validade do cartão de cidadão é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#profissao').val() == "") {
                        alertify.message('O campo profissão é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    if ($('#comercial').val() == "") {
                        alertify.message('O campo comercial é obrigatório!');
                        $("#divLoading").removeClass('show');
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "InsertCustomer.aspx/insertNewCustomer",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "xml":"' + xml + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                $("#divLoading").removeClass('show');
                                //$('#morada').val(res.d);

                                if (parseInt(res.d) > 0) {
                                    limparDados();
                                    alertify.message('Novo sócio adicionado com sucesso!');

                                    if ($('#lblidpreinscricao').html() != '') {
                                        eliminaPreInscricao();
                                    }
                                }
                                else {
                                    alertify.message(res.d);
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function openPreInscricao() {
            loadPreInscricao();
            $('#divPreInscricao').fadeIn();
            $('#formSocio').fadeOut();
        }

        function closePreInscricao() {
            $('#divPreInscricao').fadeOut();
            $('#formSocio').fadeIn();
            $('#search').val('');
        }

        function loadPreInscricao() {
            $.ajax({
                type: "POST",
                url: "InsertCustomer.aspx/loadPreInscricao",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + $('#search').val().trim() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#preInscricaoTable').html(res.d);
                    }
                }
            });
        }

        function getPreInscricaoInfo(id) {
            $.ajax({
                type: "POST",
                url: "InsertCustomer.aspx/loadData",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divVariaveisPreInscricao').html(res.d);
                        fillValues();
                        closePreInscricao();
                        $('#lblidpreinscricao').html(id);
                    }
                }
            });
        }

        function fillValues() {
            var name = $('#preinscricao_nome').html();
            var morada = $('#preinscricao_morada').html();
            var codpostal = $('#preinscricao_codpostal').html();
            var localidade = $('#preinscricao_localidade').html();
            var tlf_emergencia = $('#preinscricao_tlf_emergencia').html();
            var telemovel = $('#preinscricao_telemovel').html();
            var data_nascimento = $('#preinscricao_data_nascimento').html();
            var cc_nr = $('#preinscricao_cc_nr').html();
            var validade_cc = $('#preinscricao_validade_cc').html();
            var profissao = $('#preinscricao_profissao').html();
            var email = $('#preinscricao_email').html();
            var nao_quer_publicidade = $('#preinscricao_nao_quer_publicidade').html();
            var data_primeiro_treino = $('#preinscricao_data_primeiro_treino').html();
            var data_hora_primeira_af = $('#preinscricao_data_hora_primeira_af').html();
            var notas = "";

            if ($('#notas').val().trim() != '') {
                notas += '\nSócio inserido através de Pré-Inscrição pelo Website!';
            }
            else {
                notas += 'Sócio inserido através de Pré-Inscrição pelo Website!';
            }

            $('#name').val(name);
            $('#morada').val(morada);
            $('#codpostal').val(codpostal);
            $('#localidade').val(localidade);
            $('#tlfemergencia').val(tlf_emergencia);
            $('#tlf').val(telemovel);
            $('#cc').val(cc_nr);
            $('#profissao').val(profissao);
            $('#email').val(email);

            if (nao_quer_publicidade == '1') {
                $('#naodesejapublicidade').prop('checked', true);
            }
            else {
                $('#naodesejapublicidade').prop('checked', false);
            }

            $('#dataInício').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: data_primeiro_treino, defaultDate: data_primeiro_treino }).val(data_primeiro_treino);
            $('#datanasc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: data_nascimento, defaultDate: data_nascimento }).val(data_nascimento);
            $('#validadecc').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: validade_cc, defaultDate: validade_cc }).val(validade_cc);
            loadFinalDate();
            $('#notas').val(notas);

            alertify.alert('1ª Avaliação Física', 'O Sócio pediu para a sua 1ª Avaliação Física ser marcada para ' + data_hora_primeira_af);
        }

        function eliminaPreInscricao() {
            alertify.confirm('Sócios', 'Deseja eliminar a Pré-Inscrição usada para a inserção deste novo sócio?',
                function () {
                    $("#divLoading").addClass('show');

                    $.ajax({
                        type: "POST",
                        url: "InsertCustomer.aspx/deletePreInscricao",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id":"' + $('#lblidpreinscricao').html() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                $("#divLoading").removeClass('show');

                                if (parseInt(res.d) >= 0) {
                                    alertify.message('Pré-Inscrição eliminada com sucesso!');
                                }
                                else {
                                    alertify.message(res.d);
                                }

                                $('#lblidpreinscricao').html('');
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }
    </script>
</body>
</html>
