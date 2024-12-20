<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipaWS.aspx.cs" Inherits="EquipaWS" Culture="auto" UICulture="auto" %>
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
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>
        <span class="variaveis" id="lblidselected" runat="server"></span>
        <span class="variaveis" id="lblphotopath" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable">
            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8" style="margin-bottom: 10px;">
                <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
                <input type="image" src="img/icons/icon_search.png" onclick="loadStaff();" style="float:right; width:auto; height:50px; margin:auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="margin-bottom: 10px;">
                <input type="button" value="Novo Elemento" onclick="openNewStaff();" style="float: right; background-color: #4282b5; width: 80%; height: 50px; font-size: 12pt; text-align: center; line-height: 50px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="table">

            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px; display: none;" id="divInfo">
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom:120px; display:none;" id="divNew">
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:left; margin-top: 5px; margin-bottom: 10px;'>
                <input id='btnSave' value='Guardar' runat='server' type='button' onclick='createStaff();' style='background-color: #4282b5; 
                    width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6' style='text-align:right; margin-top: 5px; margin-bottom: 10px;'>
                <img src='img/icons/icon_close.png' style='cursor:pointer;' onclick='closeNewStaff();'/>
            </div>
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: 'Noto Sans', sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Função:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Texto:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Inglês:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Espanhol:</div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>Tradução Francês:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem 1</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem 2</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nomeNew' name='nomeNew' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordemNew' name='ordemNew' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;'/>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcaoNew' name='funcaoNew' placeholder='Função' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_enNew' name='funcao_enNew' placeholder='Função EN' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_esNew' name='funcao_esNew' placeholder='Função ES' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='funcao_frNew' name='funcao_frNew' placeholder='Função FR' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='textoNew' name='textoNew' placeholder='Texto' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_enNew' name='texto_enNew' placeholder='Texto EN' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_esNew' name='texto_esNew' placeholder='Texto ES' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px'>
                                                        <textarea class='form-control' id='texto_frNew' name='texto_frNew' placeholder='Texto FR' style='width: 100%; margin: auto; height: 100%;' ></textarea>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImagesNew1' runat="server"></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='' alt="Sem imagem associada" style='height: 100%; width: auto' id='imgEquipaNew1'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImagesNew2' runat="server"></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='' alt="Sem imagem associada" style='height: 100%; width: auto' id='imgEquipaNew2'/>
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
        //override defaults
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-danger";
        alertify.defaults.theme.input = "form-control";

        $(document).ready(function () {
            $('#search').val('');

            loadStaff();
            $('#content').height($(parent.window).width());
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
            //var h = $('#ln_0').height() + $('#ln_1').height();
            //$('#img_socio').height(h);
            $('#img_socio').width($('#col_0_0').width());
        });

        $(window).scroll(function () {
            
        });

        function loadStaff() {
            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/load",
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

        function openStaffSelected(id, x) {
            $('#lblidselected').html(id);

            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/getStaff",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfo').html(res.d);
                        $('#divTable').fadeOut();
                        $('#divInfo').fadeIn();
                        loadImagesSelect1();
                        loadImagesSelect2();
                        loadModalidadesStaff();
                    }
                }
            });
        }

        function loadModalidadesStaff() {
            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/loadModalidadeStaff",
                data: '{"id_staff":"' + $('#lblidselected').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divModalidadesStaff').html(res.d);
                    }
                }
            });
        }

        function closeStaffInfo() {
            $('#divTable').fadeIn();
            $('#divInfo').fadeOut();
            $('#search').val('');
            loadStaff();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadStaff();
            }
        });

        function updateInfo() {
            var funcao = $('#funcao').val();
            funcao = replaceAll(funcao, "\"", "''");
            funcao = replaceAll(funcao, "\n", "<br />");

            var funcao_en = $('#funcao_en').val();
            funcao_en = replaceAll(funcao_en, "\"", "''");
            funcao_en = replaceAll(funcao_en, "\n", "<br />");

            var funcao_es = $('#funcao_es').val();
            funcao_es = replaceAll(funcao_es, "\"", "''");
            funcao_es = replaceAll(funcao_es, "\n", "<br />");

            var funcao_fr = $('#funcao_fr').val();
            funcao_fr = replaceAll(funcao_fr, "\"", "''");
            funcao_fr = replaceAll(funcao_fr, "\n", "<br />");

            var texto = $('#texto').val();
            texto = replaceAll(texto, "\"", "''");
            texto = replaceAll(texto, "\n", "<br />");

            var texto_en = $('#texto_en').val();
            texto_en = replaceAll(texto_en, "\"", "''");
            texto_en = replaceAll(texto_en, "\n", "<br />");

            var texto_es = $('#texto_es').val();
            texto_es = replaceAll(texto_es, "\"", "''");
            texto_es = replaceAll(texto_es, "\n", "<br />");

            var texto_fr = $('#texto_fr').val();
            texto_fr = replaceAll(texto_fr, "\"", "''");
            texto_fr = replaceAll(texto_fr, "\n", "<br />");

            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/editar",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_staff":"' + $('#lblidselected').html()
                    + '", "nome":"' + $('#nome').val() + '", "funcao":"' + funcao + '", "funcao_en":"' + funcao_en
                    + '", "funcao_es":"' + funcao_es + '", "funcao_fr":"' + funcao_fr
                    + '", "texto":"' + texto + '", "texto_en":"' + texto_en
                    + '", "texto_es":"' + texto_es + '", "texto_fr":"' + texto_fr
                    + '", "ordem":"' + $('#ordem').val() + '", "foto_1":"' + $('#selectImg1').val() + '", "foto_2":"' + $('#selectImg2').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d.indexOf('Erro') > -1)
                            alertify.message(res.d);
                        else {
                            alertify.message(res.d);
                            createXML();
                            //closeStaffInfo();
                            //loadStaff();
                        }
                    }
                }
            });
        }

        function createStaff() {
            var funcao = $('#funcaoNew').val();
            funcao = replaceAll(funcao, "\"", "''");

            var funcao_en = $('#funcao_enNew').val();
            funcao_en = replaceAll(funcao_en, "\"", "''");

            var funcao_es = $('#funcao_esNew').val();
            funcao_es = replaceAll(funcao_es, "\"", "''");

            var funcao_fr = $('#funcao_frNew').val();
            funcao_fr = replaceAll(funcao_fr, "\"", "''");

            var texto = $('#textoNew').val();
            texto = replaceAll(texto, "\"", "''");

            var texto_en = $('#texto_enNew').val();
            texto_en = replaceAll(texto_en, "\"", "''");

            var texto_es = $('#texto_esNew').val();
            texto_es = replaceAll(texto_es, "\"", "''");

            var texto_fr = $('#texto_frNew').val();
            texto_fr = replaceAll(texto_fr, "\"", "''");

            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/criar",
                data: '{"id_operador":"' + $('#lbloperatorid').html()
                    + '", "nome":"' + $('#nomeNew').val() + '", "funcao":"' + funcao + '", "funcao_en":"' + funcao_en
                    + '", "funcao_es":"' + funcao_es + '", "funcao_fr":"' + funcao_fr
                    + '", "texto":"' + texto + '", "texto_en":"' + texto_en
                    + '", "texto_es":"' + texto_es + '", "texto_fr":"' + texto_fr
                    + '", "ordem":"' + $('#ordemNew').val() + '", "foto_1":"' + $('#selectImgNew1').val() + '", "foto_2":"' + $('#selectImgNew2').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d.indexOf('Erro') > -1)
                            alertify.message(res.d);
                        else {
                            alertify.message(res.d);
                            closeNewStaff();
                            loadStaff();
                            clearValues();
                        }
                    }
                }
            });
        }

        function clearValues() {
            $('#nomeNew').val('');
            $('#funcaoNew').val();
            $('#funcao_enNew').val();
            $('#funcao_esNew').val();
            $('#funcao_frNew').val();
            $('#textoNew').val('');
            $('#texto_enNew').val('');
            $('#texto_esNew').val('');
            $('#texto_frNew').val('')
            $('#ordemNew').val('');
            $('#selectImgNew1').val('0');
            $('#selectImgNew2').val('0');
        }

        function openNewStaff() {
            $('#divTable').fadeOut();
            $('#divNew').fadeIn();
        }

        function closeNewStaff() {
            $('#divTable').fadeIn();
            $('#divNew').fadeOut();
        }

        function deleteStaff() {
            alertify.confirm('Remover Elemento do Staff', 'Tem a certeza que deseja remover o elemento do staff?', function () {
                $.ajax({
                    type: "POST",
                    url: "EquipaWS.aspx/delete",
                    data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id":"' + $('#lblidselected').html() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            closeStaffInfo();
                            loadStaff();
                        }
                    }
                });
            }
                , function () { }).set('labels', { ok: 'Remover', cancel: 'Cancelar' });
        }

        function changeImg1() {
            $('#imgEquipa1').attr("src", "img/loading.gif");
            $('#imgEquipa1').attr("src", $('#pathToFiles1').html() + $('#selectImg1').val().trim());
        }

        function changeImg2() {
            $('#imgEquipa2').attr("src", "img/loading.gif");
            $('#imgEquipa2').attr("src", $('#pathToFiles2').html() + $('#selectImg2').val().trim());
        }

        function loadImagesSelect1() {
            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/loadImages1",
                data: '{"imgDefault":"' + $('#imgName1').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divImages1').html(res.d);
                    }
                }
            });
        }

        function loadImagesSelect2() {
            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/loadImages2",
                data: '{"imgDefault":"' + $('#imgName2').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divImages2').html(res.d);
                    }
                }
            });
        }

        function changeImgNew1() {
            $('#imgEquipaNew1').attr("src", "img/loading.gif");
            $('#imgEquipaNew1').attr("src", $('#pathToFilesNew1').html() + $('#selectImgNew1').val().trim());
        }

        function changeImgNew2() {
            $('#imgEquipaNew2').attr("src", "img/loading.gif");
            $('#imgEquipaNew2').attr("src", $('#pathToFilesNew2').html() + $('#selectImgNew2').val().trim());
        }

        function createXML() {
            var xml = "<MODALIDADES_STAFF><ID_STAFF>";
            xml += $('#lblidselected').html();
            xml += "</ID_STAFF>";

            for (i = 0; i < parseInt($('#countElementsModalidades').html()); i++) {
                if ($('#presente_' + i).is(":checked")) {
                    xml += "<MODALIDADE><ID>";
                    xml += $('#id_modalidade_' + i).html();
                    xml += "</ID></MODALIDADE>";
                }
            }

            xml += "</MODALIDADES_STAFF>";

            updateModalidadesStaff(xml);

        }

        function updateModalidadesStaff(xml) {
            $.ajax({
                type: "POST",
                url: "EquipaWS.aspx/updateModalidadesStaff",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "xml":"' + xml + '"}',
                contentType: "application/json; charset:utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (parseInt(res.d) >= 0) {
                            alertify.message('Modalidades lecionadas atualizadas com sucesso!');
                            closeStaffInfo();
                            loadStaff();
                        }
                        else {
                            alertify.message('Ocorreu um erro ao atualizar as modalidades lecionadas. Por favor, tente novamente!');
                        }
                    }
                }
            });
        }
    </script>
</body>
</html>
