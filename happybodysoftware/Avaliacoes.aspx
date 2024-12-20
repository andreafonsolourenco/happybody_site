<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Avaliacoes.aspx.cs" Inherits="Avaliacoes" Culture="auto" UICulture="auto" %>
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
            margin: 0 !important;
            padding: 0 !important;
        }

        .textAlignLeft {
            text-align: left;
        }

        .textAlignCenter {
            text-align: center;
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
			/*top: 50%; 
            left: 50%;*/
			width: 90%;
			height: auto;
            max-height: 95%;
			/*background-color: #FFF;*/
			z-index:1002;
            /*overflow: auto;*/
		}
    </style>
</head>
<body style="background-color: #FFF !important">

    <div id="black_overlay" onclick="closeListagemMensal();">
        <img src="img/icons/icon_close.png" style="top: 0; height: 20px; width: auto; float: right; cursor: pointer;" onclick="closeListagemMensal();" />
    </div>

    <div id="popup" class="popup" runat="server"></div>

    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>
        <span class="variaveis" id="lblidavaliacao" runat="server"></span>
        <div class="variaveis" id="divInfoSocio" runat="server"></div>
        <div class="variaveis" id="divInfoAvaliacaoEdit" runat="server"></div>
        <div class="variaveis" id="divInfoUltimaAvaliacao" runat="server"></div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding">
            <input type='button' class='form-control' id='btnListagemMensal' value='Listagem Mensal' style="height: 50px; width: 100%; margin-bottom: 10px;" onclick="loadListagemMensal();"/>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding" id="divTable" runat="server" style="margin-bottom: 25px; width: 100%; height: 100%">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="listaSocios" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding" style="margin-bottom: 10px;">
                    <input type='text' autocomplete="off" class='form-control' id='search' name="search" onfocus="focusInput(1);" onfocusout="focusInput(0);" placeholder='Pesquisa' style="height: 50px; width: 75%; margin: auto; float: left;" autocomplete="off"/>
                    <input type="image" src="img/icons/icon_search.png" onclick="loadSocios();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
                </div>
                <div id="tableSocios"></div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="listaAvaliacoes" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding" style="margin-bottom: 10px;">
                    <input type='text' autocomplete="off" class='form-control' id='searchAvaliacoes' name="searchAvaliacoes" onfocus="focusInput(2);" onfocusout="focusInput(0);" placeholder='Pesquisa' style="height: 50px; width: 75%; margin: auto; float: left;"/>
                    <input type="image" src="img/icons/icon_search.png" onclick="" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
                </div>
                <div id="tableAvaliacoes">
                    Por favor, selecione um sócio
                </div>
            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divAvaliacoes" style="margin-bottom: 25px; width: 100%; height: 100%">
            <img src='img/icons/icon_close.png' style='cursor:pointer; float: right; vertical-align: middle; width: auto; height: auto;' onclick='closeNewAvaliacao();'/>

            <div class="container nopadding" style="width: 100%">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#frt">FRT</a></li>
                    <li><a data-toggle="tab" href="#rr">RR</a></li>
                    <li><a data-toggle="tab" href="#save">Guardar Avaliação</a></li>
                </ul>

                <div class="tab-content nopadding" style="width: 100%">
                    <div id="frt" class="tab-pane fade in active">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">Data Próxima Avaliação:</span>
                            <input type='text' class='form-control' id='dataProximaAvaliacao' name="dataProximaAvaliacao" placeholder='Data Próxima Avaliação' style="width: 100%; margin: auto;"/>
                            <div class="input-group clockpicker" style="width: 100%">
                                <input type="text" class="form-control" id="horaProximaAvaliacao" placeholder='Hora Próxima Avaliação'>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                            <%--<input type='text' class='form-control' id='horaProximaAvaliacao' data-format="hh:mm" name="horaProximaAvaliacao" placeholder='Hora Próxima Avaliação' style="width: 48%; margin: auto;"/>--%>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">Nome:</span>
                            <input type='text' class='form-control' id='nome' name="nome" placeholder='Nome' style="width: 100%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 nopadding textAlignLeft">
                            <span style="font-weight: bold">Tipo de Contrato:</span>
                            <input type='text' class='form-control' id='tipoContrato' name="tipoContrato" placeholder='Tipo de Contrato' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 nopadding textAlignLeft">
                            <span style="font-weight: bold">Nº Sócio:</span>
                            <input type='number' class='form-control' id='nrSocio' name="nrSocio" placeholder='Nº Sócio' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft">
                            <span style="font-weight: bold">Duração do Contrato:</span>
                            <input type='text' class='form-control' id='duracaoContrato' name="duracaoContrato" placeholder='Duração do Contrato' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft">
                            <span style="font-weight: bold">Data de Início do Contrato:</span>
                            <input type='text' class='form-control' id='dataInicioContrato' name="dataInicioContrato" placeholder='Data de Início do Contrato' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft">
                            <span style="font-weight: bold">Profissão:</span>
                            <input type='text' class='form-control' id='profissao' name="profissao" placeholder='Profissão' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft" id="protocoloDiv">
                            <span style="font-weight: bold">Seguiu Protocolo AF:<br /></span>
                            <label class="radio-inline"><input type="radio" name="protocolo" value="1">Sim</label>
                            <label class="radio-inline"><input type="radio" name="protocolo" value="0">Não</label>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignCenter">
                            <span style="font-weight: bold">AVALIAÇÃO FÍSICA</span>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">a) Anamenese médica<br /></span>
                            <span style="font-weight: bold">Patologias / Antecedentes patológicos / Medicação:</span>
                            <textarea class="form-control" rows="5" id="anamenesemedica_patologias" style="width: 100%"></textarea> 
                            <span style="font-weight: bold">b) Anamenese desportiva<br /></span>
                            <span style="font-weight: bold">Treina? Há quanto tempo?</span>
                            <textarea class="form-control" rows="2" id="treina_haquantotempo" style="width: 100%"></textarea>
                            <span style="font-weight: bold">O que fez como treino?</span>
                            <textarea class="form-control" rows="2" id="oquefezcomotreino" style="width: 100%"></textarea> 
                            <span style="font-weight: bold">Durante quanto tempo?</span>
                            <textarea class="form-control" rows="2" id="durantequantotempo" style="width: 100%"></textarea>
                            <span style="font-weight: bold">Quantas horas tem disponíveis para treinar em cada treino?</span>
                            <input type='text' class='form-control' id='horasdisponiveistreino' name="horasdisponiveistreino" style="width: 100%; margin: auto;"/>
                            <span style="font-weight: bold">Quantas vezes por semana vem treinar?</span>
                            <input type='text' class='form-control' id='nrvezestreinasemana' name="nrvezestreinasemana" style="width: 100%; margin: auto;"/> 
                            <span style="font-weight: bold">Aulas ou Musculação?</span>
                            <textarea class="form-control" rows="2" id="aulasoumusculacao" style="width: 100%"></textarea>
                            <span style="font-weight: bold">Objetivo do Treino?</span>
                            <textarea class="form-control" rows="2" id="objetivodotreino" style="width: 100%"></textarea>
                            <span style="font-weight: bold">Preferência por um desporto</span>
                            <textarea class="form-control" rows="2" id="preferenciaporumdesporto" style="width: 100%"></textarea>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignCenter">
                            <span style="font-weight: bold">AVALIAÇÃO PRÁTICA</span>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <textarea class="form-control" rows="20" id="avaliacaopratica" style="width: 100%"></textarea> 
                        </div>
                    </div>
                    <div id="rr" class="tab-pane fade">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft" style="text-align: left">
                            <span style="font-weight: bold">Data:</span>
                            <input type='text' class='form-control' id='data' name="data" placeholder='Data' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Altura:</span>
                            <input type='number' class='form-control' id='altura' name="altura" placeholder='Altura' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">% MG:</span>
                            <input type='number' class='form-control' id='percmg' name="percmg" placeholder='% MG' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">kg Osso:</span>
                            <input type='number' class='form-control' id='kgosso' name="kgosso" placeholder='kg Osso' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Metabolismo Basal:</span>
                            <input type='number' class='form-control' id='metabolismobasal' name="metabolismobasal" placeholder='Metabolismo Basal' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">% H2O:</span>
                            <input type='number' class='form-control' id='percagua' name="percagua" placeholder='% H2O' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Perímetro Cintura (cm):</span>
                            <input type='number' class='form-control' id='perimcint' name="perimcint" placeholder='Perímetro Cintura (cm)' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">ICA:</span>
                            <input type='number' class='form-control' id='ica' name="ica" placeholder='ICA' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">FC Repouso:</span>
                            <input type='number' class='form-control' id='fcrepouso' name="fcrepouso" placeholder='FC Repouso' style="width: 95%; margin: auto;"/>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 nopadding textAlignLeft">
                            <span style="font-weight: bold">Idade:</span>
                            <input type='number' class='form-control' id='idade' name="idade" placeholder='Idade' style="width: 95%; margin: auto;" readonly/>
                            <span style="font-weight: bold">Peso (kg):</span>
                            <input type='number' class='form-control' id='peso' name="peso" placeholder='Peso (kg)' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">kg MM:</span>
                            <input type='number' class='form-control' id='kgmm' name="kgmm" placeholder='kg MM' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">IMC:</span>
                            <input type='number' class='form-control' id='imc' name="imc" placeholder='IMC' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Idade Metabólica:</span>
                            <input type='number' class='form-control' id='idademetabolica' name="idademetabolica" placeholder='Idade Metabólica' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Gordura Visceral:</span>
                            <input type='number' class='form-control' id='gordura' name="gordura" placeholder='Gordura Visceral' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">Perímetro Anca (cm):</span>
                            <input type='number' class='form-control' id='perimanca' name="perimanca" placeholder='Perímetro Anca (cm)' style="width: 95%; margin: auto;"/>
                            <span style="font-weight: bold">TA:</span>
                            <input type='text' class='form-control' id='ta' name="ta" placeholder='TA' style="width: 95%; margin: auto;"/>
                            <div id="planotreinoDiv">
                                <span style="font-weight: bold">Plano Treino:<br /></span>
                                <label class="radio-inline"><input type="radio" name="planotreino" value="1">Sim</label>
                                <label class="radio-inline"><input type="radio" name="planotreino" value="0">Não</label>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">Notas:</span>
                            <textarea class="form-control" rows="5" id="notas" style="width: 100%"></textarea>
                        </div>
                    </div>
                    <div id="save" class="tab-pane fade">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">Avaliação realizada por:</span>
                            <input type='text' class='form-control' id='operadorAvaliacao' name="operadorAvaliacao" placeholder='Operador' style="width: 95%; margin: auto;" readonly/>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignLeft">
                            <span style="font-weight: bold">Nome do Colaborador:</span>
                            <input type='text' class='form-control' id='nomeOperadorAvaliacao' name="nomeOperadorAvaliacao" placeholder='Operador' style="width: 95%; margin: auto;"/>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignCenter">
                            <input id='btnSave' value='Guardar Avaliação' runat='server' type='button' onclick='clickButtonSaveAvaliacao();' style='background-color: #4282b5; 
                                width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 nopadding textAlignCenter" runat="server" id="divError" style="color: #000"></div>
                    </div>
                </div>
            </div>
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
        var flagFocus = 0;
        var id_avaliacao_value = 0;
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
            loadSocios();
            $('#dataProximaAvaliacao').datepicker({ format: 'dd-mm-yyyy', changeYear: true, changeMonth: true, setDate: $('#dataProximaAvaliacao').val() });
            $('#data').datepicker({ format: 'dd-mm-yyyy', changeYear: true, changeMonth: true, setDate: $('#dataProximaAvaliacao').val() });
            $('.clockpicker').clockpicker({
                format: 'PT',
                placement: 'bottom',
                align: 'left',
                donetext: 'Guardar'
            });

            $('#popup').center();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            $('#popup').center({ transition: 300 });
        });

        $(window).scroll(function () {
            
        });

        function focusInput(x) {
            flagFocus = x;
        }

        function openFRT() {
            $('#tabFRT').addClass('active');
            $('#tabRR').removeClass('active');
        }

        function openRR() {
            $('#tabRR').addClass('active');
            $('#tabFRT').removeClass('active');
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
                url: "Avaliacoes.aspx/load",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "filtro":"' + text + '", "query":"' + query + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#tableSocios').html(res.d);
                    }
                }
            });
        };

        function novaAvaliacao(id_socio) {
            $('#divAvaliacoes').fadeIn();
            $('#divTable').fadeOut();
            $('#btnListagemMensal').fadeOut();

            loadInfoSocio();
            loadInfoUltimaAvaliacao();

            id_avaliacao_value = 0;
        }

        function closeNewAvaliacao() {
            alertify.confirm('Avaliações', 'Tem a certeza que deseja cancelar a avaliação? Todos os dados inseridos serão perdidos.',
                function () {
                    $('#divAvaliacoes').fadeOut();
                    $('#divTable').fadeIn();
                    $('#btnListagemMensal').fadeIn();
                    clearAvaliacao();
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function selectSocio(id_socio, x) {
            for (var i = 0; i < parseInt($('#countElements').html()) ; i++) {
                $('#ln_' + i.toString()).removeClass('tbodyTrSelected');
            }

            if ($('#lblidselected').html() == '' || $('#lblidselected').html() != id_socio) {
                $('#lblidselected').html(id_socio);
                $('#ln_' + x.toString()).addClass('tbodyTrSelected');

                loadAvaliacoes();
            }
            else {
                $('#lblidselected').html('');
                $('#tableAvaliacoes').html('Por favor, selecione um sócio');
            }
        }

        function loadAvaliacoes() {
            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/loadAvaliacoes",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_socio":"' + $('#lblidselected').html() + '", "data":"' + $('#searchAvaliacoes').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#tableAvaliacoes').html(res.d);
                    }
                }
            });
        }

        function loadInfoSocio() {
            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/loadInfoAvaliacao",
                data: '{"id_socio":"' + $('#lblidselected').html() + '", "id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfoSocio').html(res.d);

                        $('#tipoContrato').val($('#tipoContrato_' + $('#lblidselected').html()).html());
                        $('#nrSocio').val($('#nrSocio_' + $('#lblidselected').html()).html());
                        $('#duracaoContrato').val($('#duracaoContrato_' + $('#lblidselected').html()).html());
                        $('#dataInicioContrato').val($('#dataInicioContrato_' + $('#lblidselected').html()).html());
                        $('#profissao').val($('#profissao_' + $('#lblidselected').html()).html());
                        $('#data').val($('#dataAvaliacao_' + $('#lblidselected').html()).html());
                        $('#idade').val($('#idade_' + $('#lblidselected').html()).html());
                        $('#operadorAvaliacao').val($('#operador_' + $('#lblidselected').html()).html());
                        $('#nome').val($('#nomeSocio_' + $('#lblidselected').html()).html());
                    }
                }
            });
        }

        function loadInfoUltimaAvaliacao() {
            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/loadDataLastAvaliacao",
                data: '{"id_socio":"' + $('#lblidselected').html() + '", "id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfoUltimaAvaliacao').html(res.d);

                        $('input[name=protocolo]:checked').val($('#ultima_af_seguiuprotocoloaf').html());
                        $('#anamenesemedica_patologias').val($('#ultima_af_patologias').html());
                        $('#treina_haquantotempo').val($('#ultima_af_treinaquantotempo').html());
                        $('#oquefezcomotreino').val($('#ultima_af_quefezcomotreino').html());
                        $('#durantequantotempo').val($('#ultima_af_durantequantotempo').html());
                        $('#horasdisponiveistreino').val($('#ultima_af_horasdisponiveis').html());
                        $('#nrvezestreinasemana').val($('#ultima_af_nrvezes').html());
                        $('#aulasoumusculacao').val($('#ultima_af_aulasmusculacao').html());
                        $('#objetivodotreino').val($('#ultima_af_objetivo').html());
                        $('#preferenciaporumdesporto').val($('#ultima_af_preferenciadesporto').html());
                        $('#avaliacaopratica').val($('#ultima_af_avaliacaopratica').html());
                    }
                }
            });
        }

        

        function saveAvaliacao() {
            alertify.confirm('Avaliações', 'Tem a certeza que deseja guardar a informação relativa à avaliação?',
                function () {
                    registaAvaliacao();
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function updateAvaliacao() {
            alertify.confirm('Avaliações', 'Tem a certeza que deseja atualizar a informação relativa à avaliação?',
                function () {
                    alteraAvaliacao();
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function deleteAvaliacao(id) {
            alertify.confirm('Avaliações', 'Tem a certeza que deseja eliminar a avaliação selecionada?',
                function () {
                    $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/eliminaAvaliacao",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_avaliacao":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (parseInt(res.d) < 0) {
                            alertify.message('Ocorreu um erro ao eliminar a avaliação. Por favor, tente novamente!');
                        }
                        else {
                            alertify.message('Avaliação eliminada com sucesso!');
                            loadAvaliacoes();
                        }
                    }
                }
            });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function clearAvaliacao() {
            $('#tipoContrato').val('');
            $('#nrSocio').val('');
            $('#duracaoContrato').val('');
            $('#dataInicioContrato').val('');
            $('#profissao').val('');
            $('#anamenesemedica_patologias').val('');
            $('#treina_haquantotempo').val('');
            $('#oquefezcomotreino').val('');
            $('#durantequantotempo').val('');
            $('#horasdisponiveistreino').val('');
            $('#nrvezestreinasemana').val('');
            $('#aulasoumusculacao').val('');
            $('#objetivodotreino').val('');
            $('#preferenciaporumdesporto').val('');
            $('#data').val('');
            $('#altura').val('');
            $('#percmg').val('');
            $('#kgosso').val('');
            $('#metabolismobasal').val('');
            $('#percagua').val('');
            $('#perimcint').val('');
            $('#ica').val('');
            $('#fcrepouso').val('');
            $('#idade').val('');
            $('#peso').val('');
            $('#kgmm').val('');
            $('#imc').val('');
            $('#idademetabolica').val('');
            $('#gordura').val('');
            $('#perimanca').val('');
            $('#ta').val('');
            $('#notas').val('');
            $('#operador').val('');
            $('#avaliacaopratica').val('');
            $('#dataProximaAvaliacao').val('');
            $('#horaProximaAvaliacao').val('');
            $('input[name=protocolo]').removeAttr('checked');
            $('input[name=planotreino]').removeAttr('checked');
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                if (flagFocus == 1)
                    loadSocios();
                else if (flagFocus == 2) {
                    if ($('#lblidselected').html() == '') {
                        alertify.alert('Avaliações', 'Por favor, para pesquisar uma avaliação selecione primeiro o sócio que pretende pesquisar!"');
                        return;
                    }

                    loadAvaliacoes();
                }
            }
        });

        function clickButtonSaveAvaliacao() {
            if (id_avaliacao_value > 0) {
                updateAvaliacao();
            }
            else {
                saveAvaliacao();
            }
        }

        function registaAvaliacao() {
            var dataproxav = "";

            if ($('#dataProximaAvaliacao').val().trim() != '') {
                dataproxav = $('#dataProximaAvaliacao').val().trim() + ' ' + $('#horaProximaAvaliacao').val().trim();
            }

            var notas = $('#notas').val();
            notas = replaceAll(notas, "\"", "''");

            var patologiasantecedentesmedicacao = $('#anamenesemedica_patologias').val();
            patologiasantecedentesmedicacao = replaceAll(patologiasantecedentesmedicacao, "\"", "''");

            var treinaquantotempo = $('#treina_haquantotempo').val();
            treinaquantotempo = replaceAll(treinaquantotempo, "\"", "''");

            var oquefezcomotreino = $('#oquefezcomotreino').val();
            oquefezcomotreino = replaceAll(oquefezcomotreino, "\"", "''");

            var durantequantotempo = $('#durantequantotempo').val();
            durantequantotempo = replaceAll(durantequantotempo, "\"", "''");

            var horasdisponiveistreino = $('#horasdisponiveistreino').val();
            horasdisponiveistreino = replaceAll(horasdisponiveistreino, "\"", "''");

            var nrvezestreinosemana = $('#nrvezestreinasemana').val();
            nrvezestreinosemana = replaceAll(nrvezestreinosemana, "\"", "''");

            var aulasoumusculacao = $('#aulasoumusculacao').val();
            aulasoumusculacao = replaceAll(aulasoumusculacao, "\"", "''");

            var objetivotreino = $('#objetivodotreino').val();
            objetivotreino = replaceAll(objetivotreino, "\"", "''");

            var preferenciadesporto = $('#preferenciaporumdesporto').val();
            preferenciadesporto = replaceAll(preferenciadesporto, "\"", "''");

            var avaliacaopratica = $('#avaliacaopratica').val();
            avaliacaopratica = replaceAll(avaliacaopratica, "\"", "''");

            var ta = $('#ta').val();
            ta = replaceAll(ta, "\"", "''");

            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/registaAvaliacao",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_socio":"' + $('#lblidselected').html() 
                    + '", "seguiuprotocoloaf":"' + $('input[name=protocolo]:checked').val()
                    + '", "patologiasantecedentesmedicacao":"' + patologiasantecedentesmedicacao
                    + '", "treinaquantotempo":"' + treinaquantotempo
                    + '", "oquefezcomotreino":"' + oquefezcomotreino
                    + '", "durantequantotempo":"' + durantequantotempo
                    + '", "horasdisponiveistreino":"' + horasdisponiveistreino
                    + '", "nrvezestreinosemana":"' + nrvezestreinosemana
                    + '", "aulasoumusculacao":"' + aulasoumusculacao
                    + '", "objetivotreino":"' + objetivotreino
                    + '", "preferenciadesporto":"' + preferenciadesporto
                    + '", "avaliacaopratica":"' + avaliacaopratica
                    + '", "idade":"' + $('#idade').val() + '", "altura":"' + $('#altura').val() + '", "peso":"' + $('#peso').val() + '", "percmg":"' + $('#percmg').val() +
                    '", "kgmm":"' + $('#kgmm').val() + '", "kgosso":"' + $('#kgosso').val() + '", "imc":"' + $('#imc').val() +
                    '", "metabolismobasal":"' + $('#metabolismobasal').val() + '", "idademetabolica":"' + $('#idademetabolica').val() + '", "percagua":"' + $('#percagua').val() +
                    '", "gorduravisceral":"' + $('#gordura').val() + '", "perimcintura":"' + $('#perimcint').val() + '", "perimanca":"' + $('#perimanca').val() +
                    '", "ica":"' + $('#ica').val() + '", "ta":"' + ta + '", "fcrepouso":"' + $('#fcrepouso').val() +
                    '", "planotreino":"' + $('input[name=planotreino]:checked').val() + '", "notas":"' + notas + '", "data":"' + $('#data').val() +
                    '", "dataproximaavaliacao":"' + dataproxav + '", "nomeoperador":"' + $('#nomeOperadorAvaliacao').val().trim() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if(res.d.indexOf('erro') > -1 || res.d.indexOf('Erro') > -1) {
                            $('#divError').val(res.d);
                        }
                        else {
                            alertify.message(res.d);

                            $('#divAvaliacoes').fadeOut();
                            $('#divTable').fadeIn();
                            $('#btnListagemMensal').fadeIn();
                            clearAvaliacao();
                            $('#searchAvaliacoes').val('');
                            $('#search').val('');
                            selectSocio($('#lblidselected').html(), '0');
                            $('#lblidselected').html('');
                            loadSocios();
                            $('.nav-tabs a[href="#frt"]').tab('show');

                            id_avaliacao_value = 0;
                        }
                    }
                }
            });
        }

        function alteraAvaliacao() {
            var dataproxav = "";

            if ($('#dataProximaAvaliacao').val().trim() != '') {
                dataproxav = $('#dataProximaAvaliacao').val().trim() + ' ' + $('#horaProximaAvaliacao').val().trim();
            }

            var notas = $('#notas').val();
            notas = replaceAll(notas, "\"", "''");

            var patologiasantecedentesmedicacao = $('#anamenesemedica_patologias').val();
            patologiasantecedentesmedicacao = replaceAll(patologiasantecedentesmedicacao, "\"", "''");

            var treinaquantotempo = $('#treina_haquantotempo').val();
            treinaquantotempo = replaceAll(treinaquantotempo, "\"", "''");

            var oquefezcomotreino = $('#oquefezcomotreino').val();
            oquefezcomotreino = replaceAll(oquefezcomotreino, "\"", "''");

            var durantequantotempo = $('#durantequantotempo').val();
            durantequantotempo = replaceAll(durantequantotempo, "\"", "''");

            var horasdisponiveistreino = $('#horasdisponiveistreino').val();
            horasdisponiveistreino = replaceAll(horasdisponiveistreino, "\"", "''");

            var nrvezestreinosemana = $('#nrvezestreinasemana').val();
            nrvezestreinosemana = replaceAll(nrvezestreinosemana, "\"", "''");

            var aulasoumusculacao = $('#aulasoumusculacao').val();
            aulasoumusculacao = replaceAll(aulasoumusculacao, "\"", "''");

            var objetivotreino = $('#objetivodotreino').val();
            objetivotreino = replaceAll(objetivotreino, "\"", "''");

            var preferenciadesporto = $('#preferenciaporumdesporto').val();
            preferenciadesporto = replaceAll(preferenciadesporto, "\"", "''");

            var avaliacaopratica = $('#avaliacaopratica').val();
            avaliacaopratica = replaceAll(avaliacaopratica, "\"", "''");

            var ta = $('#ta').val();
            ta = replaceAll(ta, "\"", "''");

            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/alteraAvaliacao",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_socio":"' + $('#lblidselected').html() + 
                    '", "seguiuprotocoloaf":"' + $('input[name=protocolo]:checked').val()
                    + '", "patologiasantecedentesmedicacao":"' + patologiasantecedentesmedicacao
                    + '", "treinaquantotempo":"' + treinaquantotempo
                    + '", "oquefezcomotreino":"' + oquefezcomotreino
                    + '", "durantequantotempo":"' + durantequantotempo
                    + '", "horasdisponiveistreino":"' + horasdisponiveistreino
                    + '", "nrvezestreinosemana":"' + nrvezestreinosemana
                    + '", "aulasoumusculacao":"' + aulasoumusculacao
                    + '", "objetivotreino":"' + objetivotreino
                    + '", "preferenciadesporto":"' + preferenciadesporto
                    + '", "avaliacaopratica":"' + avaliacaopratica
                    + '", "idade":"' + $('#idade').val() + 
                    '", "altura":"' + $('#altura').val() + '", "peso":"' + $('#peso').val() + '", "percmg":"' + $('#percmg').val() +
                    '", "kgmm":"' + $('#kgmm').val() + '", "kgosso":"' + $('#kgosso').val() + '", "imc":"' + $('#imc').val() +
                    '", "metabolismobasal":"' + $('#metabolismobasal').val() + '", "idademetabolica":"' + $('#idademetabolica').val() + '", "percagua":"' + $('#percagua').val() +
                    '", "gorduravisceral":"' + $('#gordura').val() + '", "perimcintura":"' + $('#perimcint').val() + '", "perimanca":"' + $('#perimanca').val() +
                    '", "ica":"' + $('#ica').val() + '", "ta":"' + ta + '", "fcrepouso":"' + $('#fcrepouso').val() +
                    '", "planotreino":"' + $('input[name=planotreino]:checked').val() + '", "notas":"' + notas + '", "data":"' + $('#data').val() + 
                    '", "id_avaliacao":"' + $('#lblidavaliacao').html() + '", "id_operador_avaliacao":"' + $('#idopavaliacao_avaliacao' + $('#lblidavaliacao').html()).html() +
                    '", "dataproximaavaliacao":"' + dataproxav + '", "nomeoperador":"' + $('#nomeOperadorAvaliacao').val().trim() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if(res.d.indexOf('erro') > -1 || res.d.indexOf('Erro') > -1) {
                            $('#divError').html(res.d);
                        }
                        else {
                            alertify.message(res.d);

                            $('#divAvaliacoes').fadeOut();
                            $('#divTable').fadeIn();
                            $('#btnListagemMensal').fadeIn();
                            clearAvaliacao();
                            $('#searchAvaliacoes').val('');
                            $('#search').val('');
                            selectSocio($('#lblidselected').html(), '0');
                            $('#lblidselected').html('');
                            loadSocios();
                            $('.nav-tabs a[href="#frt"]').tab('show');

                            id_avaliacao_value = 0;
                        }
                    }
                }
            });
        }

        function openAvaliacaoInfo(id_avaliacao) {
            $('#lblidavaliacao').html(id_avaliacao);

            id_avaliacao_value = id_avaliacao;

            loadInfoSocio();

            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/loadInfoAvaliacaoEdit",
                data: '{"id_avaliacao":"' + id_avaliacao + '", "id_socio":"' + $('#lblidselected').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfoAvaliacaoEdit').html(res.d);

                        if ($('#seguiuprotocoloaf_avaliacao' + $('#lblidavaliacao').html()).html() == '1') {
                            var prot = '<span style="font-weight: bold">Seguiu Protocolo AF:<br /></span>'
                            + '<label class="radio-inline"><input type="radio" name="protocolo" value="1" checked>Sim</label>'
                            + '<label class="radio-inline"><input type="radio" name="protocolo" value="0">Não</label>';

                            $('#protocoloDiv').html(prot);
                        }
                        else {
                            var prot = '<span style="font-weight: bold">Seguiu Protocolo AF:<br /></span>'
                            + '<label class="radio-inline"><input type="radio" name="protocolo" value="1">Sim</label>'
                            + '<label class="radio-inline"><input type="radio" name="protocolo" value="0" checked>Não</label>';

                            $('#protocoloDiv').html(prot);
                        }

                        if ($('#planotreino_avaliacao' + $('#lblidavaliacao').html()).html() == '1') {
                            var planoTreinoDiv = '<span style="font-weight: bold">Plano Treino:<br /></span>'
                            + '<label class="radio-inline"><input type="radio" name="planotreino" value="1" checked>Sim</label>'
                            + '<label class="radio-inline"><input type="radio" name="planotreino" value="0">Não</label>';

                            $('#planotreinoDiv').html(planoTreinoDiv);
                        }
                        else {
                            var planoTreinoDiv = '<span style="font-weight: bold">Plano Treino:<br /></span>'
                            + '<label class="radio-inline"><input type="radio" name="planotreino" value="1">Sim</label>'
                            + '<label class="radio-inline"><input type="radio" name="planotreino" value="0" checked>Não</label>';

                            $('#planotreinoDiv').html(planoTreinoDiv);
                        }

                        $('#anamenesemedica_patologias').val($('#patologias_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#treina_haquantotempo').val($('#treinaqtotempo_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#oquefezcomotreino').val($('#oquefezcomotreino_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#durantequantotempo').val($('#duranteqtotempo_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#horasdisponiveistreino').val($('#horasdisponiveistreino_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#nrvezestreinasemana').val($('#nrvezestreinosemana_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#aulasoumusculacao').val($('#aulasoumusculacao_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#objetivodotreino').val($('#objetivotreino_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#avaliacaopratica').val($('#avaliacaopratica_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#preferenciaporumdesporto').val($('#preferenciadesporto_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#data').val($('#dataavaliacao_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#altura').val($('#altura_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#percmg').val($('#percmg_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#kgosso').val($('#kgosso_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#metabolismobasal').val($('#metabolismobasal_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#percagua').val($('#percagua_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#perimcint').val($('#perimetrocintura_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#ica').val($('#ica_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#fcrepouso').val($('#fcrepouso_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#idade').val($('#idade_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#peso').val($('#peso_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#kgmm').val($('#kmgg_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#imc').val($('#imc_avaliacao' + $('#lblidavaliacao').html()).html()).toString();
                        $('#idademetabolica').val($('#idademetabolica_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#gordura').val($('#gorduravisceral_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#perimanca').val($('#perimetroanca_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#ta').val($('#ta_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#notas').val($('#notas_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#operador').val($('#opavaliacao_avaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#dataProximaAvaliacao').val($('#dataproximaavaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#horaProximaAvaliacao').val($('#horaproximaavaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#nomeOperadorAvaliacao').val($('#nomeopavaliacao' + $('#lblidavaliacao').html()).html().toString());
                        $('#dataProximaAvaliacao').datepicker({ format: 'dd-mm-yyyy', changeYear: true, changeMonth: true, setDate: $('#dataProximaAvaliacao').val() });
                        $('#data').datepicker({ format: 'dd-mm-yyyy', changeYear: true, changeMonth: true, setDate: $('#dataProximaAvaliacao').val() });

                        $('#divAvaliacoes').fadeIn();
                        $('#divTable').fadeOut();
                        $('#btnListagemMensal').fadeOut();
                        $('.nav-tabs a[href="#frt"]').tab('show');
                    }
                }
            });
        }

        var isDate = function (date) {
            return (new Date(date) !== "Invalid Date" && !isNaN(new Date(date))) ? true : false;
        }

        function closeListagemMensal() {
            $('#black_overlay').fadeOut();
            $('#popup').fadeOut();
            $('html, body').animate({
                scrollTop: $('#content').offset().top
            }, 'slow');
        }

        function openListagemMensal() {
            $('#black_overlay').fadeIn();
            $('#popup').fadeIn();
        }

        function loadListagemMensal() {
            $.ajax({
                type: "POST",
                url: "Avaliacoes.aspx/loadListagemMensal",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#popup').html(res.d);
                    }
                }
            });

            openListagemMensal();
        }

        function enviarEmail(nr_socio) {
            alertify.confirm('Avaliações Mês', 'Tem a certeza que deseja enviar o email para o sócio ' + nr_socio + ' relativamente à Avaliação Física deste mês?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "Avaliacoes.aspx/sendEmailFromTemplate",
                        data: '{"nr_socio":"' + nr_socio + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                loadListagemMensal();
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }
    </script>
</body>
</html>
