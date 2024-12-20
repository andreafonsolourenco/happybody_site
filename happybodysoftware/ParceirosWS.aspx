<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParceirosWS.aspx.cs" Inherits="ParceirosWS" Culture="auto" UICulture="auto" %>
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
                <input type="image" src="img/icons/icon_search.png" onclick="loadParceiros();" style="float:right; width:auto; height:50px; margin:auto;" />
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="margin-bottom: 10px;">
                <input type="button" value="Novo Parceiro" onclick="openNewParceiro();" style="float: right; background-color: #4282b5; width: 80%; height: 50px; font-size: 12pt; text-align: center; line-height: 50px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; padding: 0 5px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;" />
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
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: "Noto Sans", sans-serif !important; height:100%'>
                                            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='text-align:left; font-family: "Noto Sans", sans-serif !important; height:100%'>
                                                <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Nome:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Ordem:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Website:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Facebook:</div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>Instagram:</div>
                                                    <div style='height:200px; width:100%; margin-bottom: 10px'>Imagem</div>
                                                </div>
                                                <div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='nomeNew' name='nomeNew' placeholder='Nome' style='width: 100%; margin: auto; height: 100%;'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='number' class='form-control' id='ordemNew' name='ordemNew' placeholder='Ordem' style='width: 100%; margin: auto; height: 100%;'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='websiteNew' name='websiteNew' placeholder='Website' style='width: 100%; margin: auto; height: 100%;' value='{5}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='facebookNew' name='facebookNew' placeholder='Facebook' style='width: 100%; margin: auto; height: 100%;' value='{6}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px'>
                                                        <input type='text' class='form-control' id='instagramNew' name='instagramNew' placeholder='Instagram' style='width: 100%; margin: auto; height: 100%;' value='{7}'/>
                                                    </div>
                                                    <div style='height:50px; width:100%; margin-bottom: 10px' id='divImagesNew' runat="server"></div>
                                                    <div style='height:150px; width:100%; margin-bottom: 10px; text-align: center;'>
                                                        <img src='' alt="Sem imagem associada" style='height: 100%; width: auto' id='imgParceiroNew'/>
                                                    </div>
                                                    <span class='variaveis' id='imgNameNew'>{3}</span>
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

            loadParceiros();
            $('#content').height($(parent.window).width());
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function loadParceiros() {
            $.ajax({
                type: "POST",
                url: "ParceirosWS.aspx/load",
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

        function openParceiroSelected(id, x) {
            $('#lblidselected').html(id);

            $.ajax({
                type: "POST",
                url: "ParceirosWS.aspx/getParceiro",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfo').html(res.d);
                        $('#divTable').fadeOut();
                        $('#divInfo').fadeIn();
                        loadImagesSelect();
                    }
                }
            });
        }

        function closeParceirosInfo() {
            $('#divTable').fadeIn();
            $('#divInfo').fadeOut();
            $('#search').val('');
            loadParceiros();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadParceiros();
            }
        });

        function updateInfo() {
            $.ajax({
                type: "POST",
                url: "ParceirosWS.aspx/editar",
                data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id_parceiro":"' + $('#lblidselected').html()
                    + '", "nome":"' + $('#nome').val() + '", "ordem":"' + $('#ordem').val() + '", "icon":"' + $('#selectImg').val()
                    + '", "website":"' + $('#website').val() + '", "facebook":"' + $('#facebook').val() + '", "instagram":"' + $('#instagram').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d.indexOf('Erro') > -1)
                            alertify.message(res.d);
                        else {
                            alertify.message(res.d);
                            closeParceirosInfo();
                            loadParceiros();
                        }
                    }
                }
            });
        }

        function createStaff() {
            $.ajax({
                type: "POST",
                url: "ParceirosWS.aspx/criar",
                data: '{"id_operador":"' + $('#lbloperatorid').html()
                    + '", "nome":"' + $('#nomeNew').val() + '", "ordem":"' + $('#ordemNew').val() + '", "icon":"' + $('#selectImgNew').val()
                    + '", "website":"' + $('#websiteNew').val() + '", "facebook":"' + $('#facebookNew').val() + '", "instagram":"' + $('#instagramNew').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        if (res.d.indexOf('Erro') > -1)
                            alertify.message(res.d);
                        else {
                            alertify.message(res.d);
                            closeNewParceiro();
                            loadParceiros();
                            clearValues();
                        }
                    }
                }
            });
        }

        function clearValues() {
            $('#nomeNew').val('');
            $('#ordemNew').val('');
            $('#websiteNew').val('');
            $('#facebookNew').val('');
            $('#instagramNew').val('');
            $('#selectImgNew').val('0');
        }

        function openNewParceiro() {
            $('#divTable').fadeOut();
            $('#divNew').fadeIn();
        }

        function closeNewParceiro() {
            $('#divTable').fadeIn();
            $('#divNew').fadeOut();
        }

        function deleteParceiro() {
            alertify.confirm('Remover Parceiro', 'Tem a certeza que deseja remover o parceiro?', function () {
                $.ajax({
                    type: "POST",
                    url: "ParceirosWS.aspx/delete",
                    data: '{"id_operador":"' + $('#lbloperatorid').html() + '", "id":"' + $('#lblidselected').html() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (res) {
                        if (res.d != null) {
                            closeParceirosInfo();
                            loadParceiros();
                        }
                    }
                });
            }
                , function () { }).set('labels', { ok: 'Remover', cancel: 'Cancelar' });
        }

        function changeImg() {
            $('#imgParceiro').attr("src", "img/loading.gif");
            $('#imgParceiro').attr("src", $('#pathToFiles').html() + $('#selectImg').val().trim());
        }

        function loadImagesSelect() {
            $.ajax({
                type: "POST",
                url: "ParceirosWS.aspx/loadImages",
                data: '{"imgDefault":"' + $('#imgName').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divImages').html(res.d);
                    }
                }
            });
        }

        function changeImgNew() {
            $('#imgParceiroNew').attr("src", "img/loading.gif");
            $('#imgParceiroNew').attr("src", $('#pathToFilesNew').html() + $('#selectImgNew').val().trim());
        }
    </script>
</body>
</html>
