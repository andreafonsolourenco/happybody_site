<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pagamentos.aspx.cs" Inherits="Pagamentos" Culture="auto" UICulture="auto" %>
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

        .headerTable {
            width: 100%;
            text-align: center;
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

        .tbodyTrTd {
            width: 100%;
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

        .headerTitle {
            height:75px; 
            line-height: 25px; 
            background-color: #000; 
            color: #FFF; 
            font-size: medium; 
            font-family: 'Noto Sans', sans-serif !important;
            width: 100%;
            margin-bottom: 10px;
            text-align: center;
            -moz-border-radius: 4px !important; 
            -webkit-border-radius: 4px !important; 
            border-radius: 4px !important;
        }

        .addon-euro {
            background-color: gray !important;
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
        <span class="variaveis" id="lblidcontratoselecionado" runat="server"></span>
        <span class="variaveis" id="lblnrsocioselecionado" runat="server"></span>
        <span class="variaveis" id="lblescrita" runat="server"></span>
        <span class="variaveis" id="lblleitura" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divSearch">
            <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Nº Sócio' required="required" style="height: 50px; width: 25%; margin: auto; float: left;"/>
            <input type="image" src="img/icons/icon_search.png" onclick="loadPagamentos();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px; margin-top: 10px;" id="divInfo">
            Por favor, indique um nº de sócio
        </div>

        <div id="black_overlay" onclick="closePopup();">
            <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closePopup();" />
        </div>
        <div id="popup" class="popup" runat="server">
            Tipo de Contrato
            <div id="tipoContratoNewDiv">
                <select class='form-control' id='tiposContratoNew' style='width:100%; height: 40px; font-size: small;'>
                    <option value="0">Selecione o Tipo de Contrato</option>
                </select>
            </div>
            Estado do Contrato
            <div id="estadoContratoNewDiv">
                <select class='form-control' id='estadosContratoNew' style='width:100%; height: 40px; font-size: small;'>
                    <option value="0">Selecione o Estado do Contrato</option>
                </select>
            </div>
            Data de Início
            <input type="text" class="form-control" placeholder='Data de Início' style='width: 100%; margin-bottom: 5px;' id='dataInicioNew' />
            Data de Fim
            <input type="text" class="form-control" placeholder='Data de Fim' style='width: 100%; margin-bottom: 5px;' id='dataFimNew' />
            Valor
            <div style='width:100%;' class='input-group' style="margin-bottom: 5px;">
                <input type='number' class='form-control' id='valorNew' placeholder='Valor' style='width: 100%; height: 34px;' aria-describedby='basic-addon-valor'/>
                <span class='input-group-addon addon-euro' id='basic-addon-valor'>€</span>
            </div>
            Débito Direto
            <input type="checkbox" class='form-control' style='margin-bottom: 5px;' id='debitoDiretoNew' />
            Notas
            <textarea class='form-control' id='notasNew' style='width: 100%; margin: auto; height: auto; margin-bottom: 5px;' rows='5'>Contrato criado manualmente através da plataforma</textarea>
            <input type='button' class='form-control' value='Cancelar' style='width: 48%; margin-bottom: 5px; float: left;' onclick='closePopup();' />
            <input type='button' class='form-control' value='Guardar' style='width: 48%; margin-bottom: 5px; float: right;' onclick='renovaContrato();' />
        </div>

        <div id="divDadosNewContrato" class="variaveis" runat="server"></div>
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
            $('#content').height($(parent.window).width());
            loadEstados();
            loadTipos();
            $('#popup').center();
            //$('#valorNew').height($('#basic-addon-valor').height());

            if ($('#lblleitura').html() == '0') {
                $('#divSearch').fadeOut();
                $('#divInfo').html('Não tem permissões de leitura para esta página!');
            }
            else {
                $('#divSearch').fadeIn();
            }
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            $('#popup').center({ transition: 300 });
        });

        $(window).scroll(function () {
            
        });

        function removerPagamento(id_pagamento) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            alertify.confirm('Remover Pagamento', 'Tem a certeza que deseja remover o pagamento?', function () {
                $.ajax({
                    type: "POST",
                    url: "Pagamentos.aspx/removerPagamento",
                    data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_pagamento":"' + id_pagamento + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            if (parseInt(res.d) == 0) {
                                loadPagamentos();
                                alertify.message('Pagamento removido com sucesso!');
                            }
                            else {
                                alertify.message('Ocorreu um erro ao apagar o pagamento para o contrato indicado!');
                            }
                        }
                    }
                });
            }
                , function () { }).set('labels', { ok: 'Remover', cancel: 'Cancelar' });
        }

        function loadPagamentos() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/load",
                data: '{"nr_socio":"' + $('#search').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfo').html(res.d);
                        loadAvisos();

                        for (i = 0; i < parseInt($('#countContratos').html()) ; i++) {
                            if (i <= 1) {
                                for (j = 0; j < parseInt($('#countElements_' + i.toString()).html()) ; j++) {
                                    loadStatus($('#id_status_' + i.toString() + '_' + j.toString()).html(), '#divStatus_' + i.toString() + '_' + j.toString(), 'selectStatus_' + i.toString() + '_' + j.toString(), $('#id_pagamento_' + i.toString() + '_' + j.toString()).html(), i.toString(), j.toString());
                                }
                            }
                        }

                        for (i = 0; i < parseInt($('#countContratos').html()) ; i++) {
                            $('#img_plus_' + i.toString()).height('118');
                            $('#img_plus_' + i.toString()).width('auto');

                            if (i == 0) {
                                $('#icon_left_' + i.toString()).attr('src', 'img/icons/icon_arrow_left_black.png');
                            }

                            if (i == parseInt($('#countContratos').html()) - 1) {
                                $('#icon_right_' + i.toString()).attr('src', 'img/icons/icon_arrow_right_black.png');
                            }
                        }
                    }
                }
            });
        };

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if ($('#search').is(':focus'))
                    if ($('#lblleitura').html() == '1')
                        loadPagamentos();
            }
        });

        function loadStatus(id_status_default, div, nome, id_pagamento, x, y) {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadStatus",
                data: '{"id_status_default":"' + id_status_default + '", "nome":"' + nome + '", "id_pagamento":"' + id_pagamento
                    + '", "nr_contrato":"' + x + '", "nr_pagamento":"' + y + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $(div).html(res.d);
                    }
                }
            });
        };

        function updateStatus(id_pagamento, nr_cont, nr_pag) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/updateStatus",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_pagamento":"' + id_pagamento + '", "id_status":"' + $('#selectStatus_' + nr_cont.toString() + '_' + nr_pag.toString()).val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        alertify.message(res.d);
                        loadPagamentos();
                    }
                }
            });
        };

        function criaNovoPagamento(id_contrato) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            alertify.confirm('Criar Pagamento', 'Tem a certeza que deseja adicionar um mês de pagamento a este contrato?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "Pagamentos.aspx/insertNovoPagamento",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_contrato":"' + id_contrato + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                if (parseInt(res.d) > 0) {
                                    loadPagamentos();
                                    alertify.message('Novo pagamento para o contrato indicado inserido com sucesso!');
                                }
                                else {
                                    alertify.message('Ocorreu um erro ao inserir um novo pagamento para o contrato indicado!');
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function criaPagamentoContratoAnterior(id_contrato, id_contrato_apagar) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            alertify.confirm('Criar Pagamento', 'Tem a certeza que deseja adicionar um mês de pagamento ao anterior contrato do sócio? O contrato atual será apagado e será adicionado um novo mês de pagamento ao contrato antigo.',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "Pagamentos.aspx/inserPagamentoContratoAnterior",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_contrato":"' + id_contrato + '", "id_contrato_anterior":"' + id_contrato_apagar +'"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#search').val(res.d);
                                if (parseInt(res.d) > 0) {
                                    loadPagamentos();
                                    alertify.message('Novo pagamento para o contrato indicado inserido com sucesso!');
                                }
                                else if (parseInt(res.d) == 0) {
                                    alertify.message('Novo pagamento não foi inserido pois a data atual não é no mês imediatamente a seguir ao último pagamento do anterior contrato!');
                                }
                                else {
                                    alertify.message('Ocorreu um erro ao inserir um novo pagamento para o contrato indicado!');
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function renovaContrato() {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            alertify.confirm('Criar Contrato', 'Tem a certeza que deseja criar um novo contrato para o sócio indicado?',
                function () {
                    var dd = "";

                    if ($('#debitoDiretoNew').is(":checked"))
                        dd = "1";
                    else
                        dd = "0";

                    $.ajax({
                        type: "POST",
                        url: "Pagamentos.aspx/criaNovoContrato",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_tipo":"' + $('#tiposContratoNew').val() + '", "id_estado":"' + $('#estadosContratoNew').val()
                            + '", "dataInicio":"' + $('#dataInicioNew').val() + '", "dataFim":"' + $('#dataFimNew').val() + '", "valor":"' + $('#valorNew').val()
                            + '", "debitoDireto":"' + dd + '", "notas":"' + $('#notasNew').val() + '", "id_contrato":"' + $('#lblidcontratoselecionado').html()
                            + '", "nr_socio":"' + $('#lblnrsocioselecionado').html() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#popup').html(res.d);
                                if (res.d.indexOf('erro') > -1 || res.d.indexOf('Erro') > -1) {
                                    alertify.message(res.d);
                                }
                                else {
                                    loadPagamentos();
                                    alertify.message('Novo contrato criado com sucesso!');
                                    closePopup();
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function alteraValorPagamento(id_pagamento, conta, contaContratos) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            var val = $('#pagamento_' + conta.toString() + '_' + contaContratos.toString()).val().trim();

            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/updateValorPagamento",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_pagamento":"' + id_pagamento + '", "valor":"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //$('#search').val(res.d);
                        alertify.message(res.d);
                        ////loadPagamentos();
                        $('#pagamento_' + conta.toString() + '_' + contaContratos.toString()).focus();
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
        }

        function openPopup(id_contrato, nr_socio) {
            if ($('#lblescrita').html() == '0') {
                alertify.message('Não tem permissões para efetuar esta ação!');
                return;
            }

            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
            $('#content').addClass('removeScroll');
            $('#lblidcontratoselecionado').html(id_contrato);
            $('#lblnrsocioselecionado').html(nr_socio);

            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/criaNovoContratoDados",
                data: '{"id_contrato":"' + id_contrato + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        //$('#popup').html(res.d);
                        $('#divDadosNewContrato').html(res.d);
                        $('#dataInicioNew').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataInicioNovoContrato').html() }).val($('#dataInicioNovoContrato').html());
                        $('#dataFimNew').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: $('#dataFimNovoContrato').html() }).val($('#dataFimNovoContrato').html());
                        $('#valorNew').val($('#valorNovoContrato').html());

                        if ($('#debitoDiretoNovoContrato').html() == '1') {
                            $('#debitoDiretoNew').attr('checked', true);
                        }
                        else {
                            $('#debitoDiretoNew').attr('checked', false);
                        }

                        $('#notasNew').val($('#notasNovoContrato').html());
                        loadEstados();
                        loadTipos();

                        $('#dataInicioNew').change(function () {
                            loadFinalDate();
                        });

                        //var h = 10;

                        //for (i = 0; i < parseInt($('#countContratos').html()) ; i++) {
                        //    var x = i-1;
                        //    if (i > 0) {
                        //        h += 10;
                        //        h += $('#pagamentosContrato' + x.toString()).height();
                        //        h += $('#headerContrato' + x.toString()).height();

                        //        $('#headerContrato' + i.toString()).css("margin-top", h + "px");
                        //        $('#pagamentosContrato' + i.toString()).css("margin-top", (h + $('#headerContrato' + i.toString()).height()) + "px");
                        //    }
                        //}
                    }
                }
            });
        }

        function loadEstados() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadStatusContrato",
                data: '{"id_status":"' + $('#idEstadoNovoContrato').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#estadoContratoNewDiv').html(res.d);
                    }
                }
            });
        }

        function loadTipos() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadTipoContrato",
                data: '{"id_tipo":"' + $('#idTipoNovoContrato').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#tipoContratoNewDiv').html(res.d);
                    }
                }
            });
        }

        function loadFinalDate() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadFinalDate",
                data: '{"id_tipocontrato":"' + $('#tiposContratoNew').val() + '", "data_inicio":"' + $('#dataInicioNew').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#dataFimNew').datepicker({ format: 'dd/mm/yyyy', changeYear: true, changeMonth: true, setDate: res.d, defaultDate: res.d }).val(res.d);
                        loadPrecoTipoContrato();
                    }
                }
            });
        }

        function loadPrecoTipoContrato() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadPrecoTipoContrato",
                data: '{"id_tipocontrato":"' + $('#tiposContratoNew').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#valorNew').val(res.d);
                    }
                }
            });
        }

        function fadeContracts(x, val) {
            var y = x + val;

            if (val == -1 && x != 0) {
                $('#divGeralContrato' + x.toString()).hide();
                $('#divGeralContrato' + y.toString()).fadeIn();
            }

            if (val == 1 && x != parseInt($('#countContratos').html()) - 1) {
                $('#divGeralContrato' + x.toString()).hide();
                $('#divGeralContrato' + y.toString()).fadeIn();
            }
        }

        function loadAvisos() {
            $.ajax({
                type: "POST",
                url: "Pagamentos.aspx/loadAvisos",
                data: '{"nr_socio":"' + $('#search').val() + '"}',
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
    </script>
</body>
</html>
