<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractType.aspx.cs" Inherits="ContractType" Culture="auto" UICulture="auto" %>
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
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" media="screen" href="http://tarruda.github.com/bootstrap-datetimepicker/assets/css/bootstrap-datetimepicker.min.css">

    <link href="alertify/css/alertify.min.css" rel="stylesheet" type='text/css'/>
    <link href="alertify/css/themes/semantic.min.css" rel="stylesheet" type='text/css'/>
    <link href="alertify/css/themes/default.min.css" rel="stylesheet" type='text/css'/>
    <!-- Bootstrap Date-Picker Plugin -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
    <!-- Clock Picker -->
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/dist/bootstrap-clockpicker.min.css" />
    <link rel="stylesheet" type="text/css" href="clockpicker/assets/css/github.min.css" />

    <!-- Bootstrap 4 -->
    <%--<link rel="stylesheet" type="text/css" href="bootstrap_4/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="bootstrap_4/css/bootstrap-grid.min.css" />
    <link rel="stylesheet" type="text/css" href="bootstrap_4/css/bootstrap-reboot.min.css" />--%>

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
            /*border: 1px #000 solid;*/ 
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
            border: 0 !important;
        }

        table thead tr th {
            padding: 5px;
            -moz-border-radius: 4px !important; 
            -webkit-border-radius: 4px !important; 
            border-radius: 4px !important;
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

        .addon-euro {
            background-color: gray !important;
        }
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable">
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8" style="margin-bottom: 10px;">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadTiposContrato();" style="float: right; width: auto; height: 50px; margin: auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="margin-bottom: 10px;">
                <input type="button" value="Novo Tipo de Contrato" onclick="openNewContractType();" style="float: right; background-color: #4282b5; width: 80%; height: 50px; font-size: 12pt; text-align: center; line-height: 50px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px; display: none;" id="divInfo">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px; display:none;" id="divNew">
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>
                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='createContractType();' style='background-color: #4282b5; 
                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeNewContractType();'/>
            </div>
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: "Noto Sans", sans-serif !important; height:100%'>
                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Código:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Designação:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Acessos por semana:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sábado:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nº Avaliações Físicas:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>PT:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nutrição:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Coach:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Duração:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Preço:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Jóia:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ativo:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Renovação Automática:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Horário Livre:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Segunda:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Terça:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Quarta:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Quinta:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sexta:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Sábado:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Domingo:</div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>Tolerância Entrada:</div>
                    <div style='height:200px; width:100%; margin-bottom: 10px'>Notas:</div>
                </div>
                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='text' class='form-control' id='codigoNew' name='codigoNew' placeholder='Código' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='text' class='form-control' id='designacaoNew' name='designacaoNew' placeholder='Designação' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='acessosNew' name='acessosNew' placeholder='Acessos por semana' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='sabadoNew' name='sabadoNew' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='nravaliacoesNew' name='nravaliacoesNew' placeholder='Nº Avaliações Físicas' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='ptNew' name='ptNew' placeholder='PT' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='nutricaoNew' name='nutricaoNew' placeholder='Nutrição' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='coachNew' name='coachNew' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='duracaoNew' name='duracaoNew' placeholder='Duração' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px' class='input-group'>
                        <input type='number' class='form-control' id='precoNew' name='precoNew' placeholder='Preço' style='margin: auto; height: 100%;' aria-describedby='basic-addon-preco'/>
                        <span class='input-group-addon addon-euro' id='basic-addon-preco'>€</span>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px' class='input-group'>
                        <input type='number' class='form-control' id='joiaNew' name='joiaNew' placeholder='Jóia' style='margin: auto; height: 100%;' aria-describedby='basic-addon-joia'/>
                        <span class='input-group-addon addon-euro' id='basic-addon-joia'>€</span>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='ativoNew' name='ativoNew' style='width: 100%; margin: auto; height: 100%;'/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='renovacaoNew' name='renovacaoNew' style='width: 100%; margin: auto; height: 100%;' checked/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='horariolivre' name='horariolivre' style='width: 100%; margin: auto; height: 100%;' checked/>
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='segNew' name='segNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaSegNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaSegNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='terNew' name='terNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaTerNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaTerNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='quaNew' name='quaNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaQuaNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaQuaNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='quiNew' name='quiNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaQuiNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaQuiNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='sexNew' name='sexNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaSexNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaSexNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='sabNew' name='sabNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaSabNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaSabNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='checkbox' class='form-control' id='domNew' name='domNew' style='width: 33.33%; margin: auto; height: 100%; float: left' />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaEntradaDomNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                        <input data-format='hh:mm' type='text' class='form-control' id='horaSaidaDomNew' style='width: 33.33%; margin: auto; height: 100%; float: left' readonly />
                    </div>
                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                        <input type='number' class='form-control' id='toleranciaNew' name='toleranciaNew' placeholder='Tolerância Entrada' style='width: 100%; margin: auto; height: 100%;' />
                    </div>
                    <div style='height:200px; width:100%; margin-bottom: 10px'>
                        <textarea class='form-control' id='notasNew' name='notasNew' style='width: 100%; margin: auto; height: 100%;' rows='5'></textarea>
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

    <!-- Bootstrap 4 -->
    <%--<script type="text/javascript" src="bootstrap_4/js/bootstrap.min.js"></script>--%>

    <script type="text/javascript">
        var linhaSelecionada;
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            $('#search').val('');

            loadTiposContrato();
            $('#content').height($(parent.window).width());

            $('#horaSaidaSegNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaSegNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaTerNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaTerNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaQuaNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaQuaNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaQuiNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaQuiNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaSexNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaSexNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaSabNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaSabNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaSaidaDomNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
            $('#horaEntradaDomNew').clockpicker({
                placement: 'bottom',
                align: 'left',
                autoclose: true,
                'default': $('#horaSaidaDom').val()
            });
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            //var h = $('#ln_0').height() + $('#ln_1').height();
            //$('#img_socio').height(h);
            $('#img_socio').width($('#col_0_0').width());
        });

        $(window).scroll(function () {
            
        });

        function loadTiposContrato() {
            $.ajax({
                type: "POST",
                url: "ContractType.aspx/load",
                data: '{"filtro":"' + $('#search').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#table').html(res.d);
                    }
                }
            });
        };

        function openTypeContractInfo(id, x) {
            $('#lblidselected').html(id);

            $.ajax({
                type: "POST",
                url: "ContractType.aspx/getTypeContractData",
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
                        //$('#img_socio').width($('#col_0_0').width());
                        $('#horaSaidaSeg').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaSeg').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaTer').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaTer').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaQua').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaQua').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaQui').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaQui').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaSex').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaSex').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaSab').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaSab').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaSaidaDom').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                        $('#horaEntradaDom').clockpicker({
                            placement: 'bottom',
                            align: 'left',
                            autoclose: true,
                            'default': $('#horaSaidaDom').val()
                        });
                    }
                }
            });
        }

        function closeCustomerInfo() {
            $('#divTable').fadeIn();
            $('#divInfo').fadeOut();
            $('#search').val('');
            loadTiposContrato();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadTiposContrato();
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

        function updateInfo() {
            alertify.confirm('Tipo de Contrato', 'Tem a certeza que deseja atualizar a informação relativa ao Tipo de Contrato?',
                function () {
                    var sabado;
                    var ativo;
                    var coach;
                    var renovacao;
                    var horario_livre;
                    var seg;
                    var ter;
                    var qua;
                    var qui;
                    var sex;
                    var sab;
                    var dom;
                    var preco;
                    var joia;

                    if ($('#sabado').is(':checked'))
                        sabado = "1";
                    else
                        sabado = "0";

                    if ($('#ativo').is(':checked'))
                        ativo = "1";
                    else
                        ativo = "0";

                    if ($('#coach').is(':checked'))
                        coach = "1";
                    else
                        coach = "0";

                    if ($('#renovacao').is(':checked'))
                        renovacao = "1";
                    else
                        renovacao = "0";

                    if ($('#horariolivre').is(':checked'))
                        horario_livre = "1";
                    else
                        horario_livre = "0";

                    if ($('#seg').is(':checked'))
                        seg = "1";
                    else
                        seg = "0";

                    if ($('#ter').is(':checked'))
                        ter = "1";
                    else
                        ter = "0";

                    if ($('#qua').is(':checked'))
                        qua = "1";
                    else
                        qua = "0";

                    if ($('#qui').is(':checked'))
                        qui = "1";
                    else
                        qui = "0";

                    if ($('#sex').is(':checked'))
                        sex = "1";
                    else
                        sex = "0";

                    if ($('#sab').is(':checked'))
                        sab = "1";
                    else
                        sab = "0";

                    if ($('#dom').is(':checked'))
                        dom = "1";
                    else
                        dom = "0";

                    preco = $('#preco').val().replace(",", ".");
                    joia = $('#joia').val().replace(",", ".");

                    $.ajax({
                        type: "POST",
                        url: "ContractType.aspx/editar",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_tipo_contrato":"' + $('#lblidselected').html() + '", "codigo":"' + $('#codigo').val()
                            + '", "designacao":"' + $('#designacao').val() + '", "acessos":"' + $('#acessos').val() + '", "sabado":"' + sabado
                            + '", "nravaliacoes":"' + $('#nravaliacoes').val() + '", "pt":"' + $('#pt').val() + '", "nutricao":"' + $('#nutricao').val() + '", "coach":"' + coach
                            + '", "duracao":"' + $('#duracao').val() + '", "preco":"' + preco + '", "joia":"' + joia
                            + '", "ativo":"' + ativo + '", "notas":"' + $('#notas').val() + '", "renovacao_automatica":"' + renovacao
                            + '", "horario_livre":"' + horario_livre + '", "tolerancia":"' + $('#tolerancia').val() + '", "seg":"' + seg + '", "ter":"' + ter + '", "qua":"' + qua + '", "qui":"' + qui + '", "sex":"' + sex + '", "sab":"' + sab + '", "dom":"' + dom
                            + '", "hora_ent_seg":"' + $('#horaEntradaSeg').val() + '", "hora_sai_seg":"' + $('#horaSaidaSeg').val() + '", "hora_ent_ter":"' + $('#horaEntradaTer').val() + '", "hora_sai_ter":"' + $('#horaSaidaTer').val() + '", "hora_ent_qua":"' + $('#horaEntradaQua').val() + '", "hora_sai_qua":"' + $('#horaSaidaQua').val()
                            + '", "hora_ent_qui":"' + $('#horaEntradaQui').val() + '", "hora_sai_qui":"' + $('#horaSaidaQui').val() + '", "hora_ent_sex":"' + $('#horaEntradaSex').val() + '", "hora_sai_sex":"' + $('#horaSaidaSex').val() + '", "hora_ent_sab":"' + $('#horaEntradaSab').val() + '", "hora_sai_sab":"' + $('#horaSaidaSab').val()
                            + '", "hora_ent_dom":"' + $('#horaEntradaDom').val() + '", "hora_sai_dom":"' + $('#horaSaidaDom').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#search').val('');
                                //closeCustomerInfo();

                                //$('#divInfo').html(res.d);

                                if (res.d.indexOf('Erro') > -1)
                                    alertify.message(res.d);
                                else {
                                    openTypeContractInfo($('#lblidselected').html(), 0);
                                    registaLog($('#lbloperatorid').html(), 'TIPOSDECONTRATO', 'Atualização do tipo de contrato ' + $('#codigo').val());
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function createContractType() {
            alertify.confirm('Tipo de Contrato', 'Tem a certeza que deseja criar um novo Tipo de Contrato?',
                function () {
                    var sabado;
                    var ativo;
                    var coach;
                    var renovacao;
                    var horario_livre;
                    var seg;
                    var ter;
                    var qua;
                    var qui;
                    var sex;
                    var sab;
                    var dom;

                    if ($('#sabadoNew').is(':checked'))
                        sabado = "1";
                    else
                        sabado = "0";

                    if ($('#ativoNew').is(':checked'))
                        ativo = "1";
                    else
                        ativo = "0";

                    if ($('#coachNew').is(':checked'))
                        coach = "1";
                    else
                        coach = "0";

                    if ($('#renovacaoNew').is(':checked'))
                        renovacao = "1";
                    else
                        renovacao = "0";

                    if ($('#horariolivreNew').is(':checked'))
                        horario_livre = "1";
                    else
                        horario_livre = "0";

                    if ($('#segNew').is(':checked'))
                        seg = "1";
                    else
                        seg = "0";

                    if ($('#terNew').is(':checked'))
                        ter = "1";
                    else
                        ter = "0";

                    if ($('#quaNew').is(':checked'))
                        qua = "1";
                    else
                        qua = "0";

                    if ($('#quiNew').is(':checked'))
                        qui = "1";
                    else
                        qui = "0";

                    if ($('#sexNew').is(':checked'))
                        sex = "1";
                    else
                        sex = "0";

                    if ($('#sabNew').is(':checked'))
                        sab = "1";
                    else
                        sab = "0";

                    if ($('#domNew').is(':checked'))
                        dom = "1";
                    else
                        dom = "0";

                    if ($('#codigoNew').val().trim() == "") {
                        alertify.message('É necessário inserir código no tipo de contrato!');
                        return;
                    }

                    if ($('#designacaoNew').val().trim() == "") {
                        alertify.message('É necessário inserir designação no tipo de contrato!');
                        return;
                    }

                    if ($('#acessosNew').val().trim() == "") {
                        alertify.message('É necessário inserir nr de acessos no tipo de contrato!');
                        return;
                    }

                    if ($('#nravaliacoesNew').val().trim() == "") {
                        alertify.message('É necessário inserir nr de avaliações no tipo de contrato!');
                        return;
                    }

                    if ($('#ptNew').val().trim() == "") {
                        alertify.message('É necessário inserir PT no tipo de contrato!');
                        return;
                    }

                    if ($('#nutricaoNew').val().trim() == "") {
                        alertify.message('É necessário inserir nutrição no tipo de contrato!');
                        return;
                    }

                    if ($('#duracaoNew').val().trim() == "") {
                        alertify.message('É necessário inserir duração no tipo de contrato!');
                        return;
                    }

                    if ($('#precoNew').val().trim() == "") {
                        alertify.message('É necessário inserir preço no tipo de contrato!');
                        return;
                    }

                    if ($('#joiaNew').val().trim() == "") {
                        alertify.message('É necessário inserir Jóia no tipo de contrato!');
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "ContractType.aspx/criar",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "codigo":"' + $('#codigoNew').val()
                            + '", "designacao":"' + $('#designacaoNew').val() + '", "acessos":"' + $('#acessosNew').val() + '", "sabado":"' + sabado
                            + '", "nravaliacoes":"' + $('#nravaliacoesNew').val() + '", "pt":"' + $('#ptNew').val() + '", "nutricao":"' + $('#nutricaoNew').val() + '", "coach":"' + coach
                            + '", "duracao":"' + $('#duracaoNew').val() + '", "preco":"' + $('#precoNew').val() + '", "joia":"' + $('#joiaNew').val()
                            + '", "ativo":"' + ativo + '", "notas":"' + $('#notasNew').val() + '", "renovacao_automatica":"' + renovacao
                            + '", "horario_livre":"' + horario_livre + '", "tolerancia":"' + $('#toleranciaNew').val() + '", "seg":"' + seg + '", "ter":"' + ter + '", "qua":"' + qua + '", "qui":"' + qui + '", "sex":"' + sex + '", "sab":"' + sab + '", "dom":"' + dom
                            + '", "hora_ent_seg":"' + $('#horaEntradaSegNew').val() + '", "hora_sai_seg":"' + $('#horaSaidaSegNew').val() + '", "hora_ent_ter":"' + $('#horaEntradaTerNew').val() + '", "hora_sai_ter":"' + $('#horaSaidaTerNew').val() + '", "hora_ent_qua":"' + $('#horaEntradaQuaNew').val() + '", "hora_sai_qua":"' + $('#horaSaidaQuaNew').val()
                            + '", "hora_ent_qui":"' + $('#horaEntradaQuiNew').val() + '", "hora_sai_qui":"' + $('#horaSaidaQuiNew').val() + '", "hora_ent_sex":"' + $('#horaEntradaSexNew').val() + '", "hora_sai_sex":"' + $('#horaSaidaSexNew').val() + '", "hora_ent_sab":"' + $('#horaEntradaSabNew').val() + '", "hora_sai_sab":"' + $('#horaSaidaSabNew').val()
                            + '", "hora_ent_dom":"' + $('#horaEntradaDomNew').val() + '", "hora_sai_dom":"' + $('#horaSaidaDomNew').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#search').val('');
                                //closeCustomerInfo();

                                //$('#divNew').html(res.d);

                                if (res.d.indexOf('Erro') > -1)
                                    alertify.message(res.d);
                                else {
                                    closeNewContractType();
                                    loadTiposContrato();
                                    registaLog($('#lbloperatorid').html(), 'TIPOSDECONTRATO', 'Criação do tipo de contrato ' + $('#codigoNew').val());
                                    $('#codigoNew').val('');
                                    $('#designacaoNew').val('');
                                    $('#acessosNew').val('');
                                    $('#nravaliacoesNew').val('');
                                    $('#ptNew').val('');
                                    $('#nutricaoNew').val('');
                                    $('#duracaoNew').val('');
                                    $('#precoNew').val('');
                                    $('#joiaNew').val('');
                                    $('#notasNew').val('');
                                    $('#sabadoNew').attr('checked', false);
                                    $('#ativoNew').attr('checked', false);
                                    $('#coachNew').attr('checked', false);
                                    $('#renovacaoNew').attr('checked', true);
                                    $('#horaEntradaSegNew').val('');
                                    $('#horaSaidaSegNew').val('');
                                    $('#horaEntradaTerNew').val('');
                                    $('#horaSaidaTerNew').val('');
                                    $('#horaEntradaQuaNew').val('');
                                    $('#horaSaidaQuaNew').val('');
                                    $('#horaEntradaQuiNew').val('');
                                    $('#horaSaidaQuiNew').val('');
                                    $('#horaEntradaSexNew').val('');
                                    $('#horaSaidaSexNew').val('');
                                    $('#horaEntradaSabNew').val('');
                                    $('#horaSaidaSabNew').val('');
                                    $('#horaEntradaDomNew').val('');
                                    $('#horaSaidaDomNew').val('');
                                    $('#toleranciaNew').val('');
                                    $('#horariolivreNew').attr('checked', false);
                                    $('#segNew').attr('checked', false);
                                    $('#terNew').attr('checked', false);
                                    $('#quaNew').attr('checked', false);
                                    $('#quiNew').attr('checked', false);
                                    $('#sexNew').attr('checked', false);
                                    $('#sabNew').attr('checked', false);
                                    $('#domNew').attr('checked', false);
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

        function openNewContractType() {
            $('#divTable').fadeOut();
            $('#divNew').fadeIn();
        }

        function closeNewContractType() {
            $('#divTable').fadeIn();
            $('#divNew').fadeOut();
        }

        function deleteContractType() {
            alertify.confirm('Tipo de Contrato', 'Tem a certeza que deseja remover o Tipo de Contrato?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "ContractType.aspx/apagar",
                        data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_tipo_contrato":"' + $('#lblidselected').html() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                $('#search').val('');
                                closeCustomerInfo();
                                alertify.message(res.d);
                                loadTiposContrato();
                                registaLog($('#lbloperatorid').html(), 'TIPOSDECONTRATO', 'Eliminou tipo de contrato ' + $('#codigo').val());
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }
    </script>
</body>
</html>
