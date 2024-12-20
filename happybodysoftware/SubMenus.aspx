<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubMenus.aspx.cs" Inherits="SubMenus" Culture="auto" UICulture="auto" %>
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
    <!-- Emojis -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet">

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
                -moz-border-radius: 4px !important;
                -webkit-border-radius: 4px !important;
                border-radius: 4px !important;
            }

        table thead tr th {
            padding: 5px;
        }

        .headerLeft {
            width: 70%;
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
        }

        .headerRight {
            width: 30%;
            border-left: 1px red solid;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
        }

        .headerColspan {
            width: 100%;
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
                -moz-border-radius: 4px !important;
                -webkit-border-radius: 4px !important;
                border-radius: 4px !important;
                /*cursor: pointer;*/
            }

        table tbody tr:hover {
            /*background-color: #D3D3D3;*/
        }

        .tbodyTrSelected {
            background-color: red;
        }

        .tbodyTrSelected:hover {
            background-color: red;
        }

        .tbodyTrTdLeft {
            width: 75%;
        }

        .tbodyTrTdRight {
            width: 25%;
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

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="searchDiv">
            <input type='text' autocomplete="off" class='form-control' id='search' name="search" placeholder='Pesquisa' required="required" style="height: 50px; width: 75%; margin: auto; float: left;"/>
            <input type="image" src="img/icons/icon_search.png" onclick="loadGrid();" style="float:right; width:auto; height:50px; margin:auto; -moz-border-radius: 4px !important; -webkit-border-radius: 4px !important; border-radius: 4px !important;" />
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;" id="newMenuBtnDiv">
            <input type='button' class='form-control' id='new' onclick="showNew();" value="Novo Menu" style="height: 50px; width: 100%; margin: auto; float: left;"/>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable" runat="server" style="margin-bottom: 25px;">
            
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divNew" runat="server" style="margin-bottom: 25px;">
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>
                <input id='btnSaveNew' value='Guardar' runat='server' type='button' onclick='newMenu();' style='background-color: #4282b5; float: left;
                        width: auto; height: 40px; font-size: 12pt; text-align: center; line-height: 40px; color: #FFFFFF; cursor: pointer; vertical-align: middle; border: none; margin: 20px 2px 20px 2px; padding: 0 10px; -moz-border-radius: 2px; -webkit-border-radius: 2px; border-radius: 2px;'/>
                <img src='img/icons/icon_close.png' style='cursor:pointer; float: right; vertical-align: middle;' onclick='closeNew();'/>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                Título
                <input type='text' class='form-control' id='tituloNew' placeholder='Título' required='required' style='width: 100%; margin: auto;' value=''/>
            </div>
            <div class='col-lg-6 col-md-6 col-sm-6 col-xs-6 line'>
                Página
                <input type='text' class='form-control' id='paginaNew' placeholder='Página' required='required' style='width: 100%; margin: auto;' value=''/>
            </div>
            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line' id="divSelectMenuNew">
                
            </div>
            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                Administrador
                <input type='checkbox' class='form-control' id='administradorNew' style='width: 100%; margin: auto; height: 50px;'/>
            </div>
            <div class='col-lg-4 col-md-4 col-sm-4 col-xs-4 line'>
                Visivel
                <input type='checkbox' class='form-control' id='visivelNew' style='width: 100%; margin: auto; height: 50px;'/>
            </div>
            <div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 line'>
                Notas
                <textarea class='form-control' id='notasNew' style='width: 100%; margin: auto; height: auto;' rows='5'></textarea>
            </div>
        </div>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 variaveis" id="divEdit" runat="server" style="margin-bottom: 25px;">
            
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

    <script type="text/javascript" src="js/jquery.btechco.excelexport.js"></script>
    <script type="text/javascript" src="js/jquery.base64.js"></script>

    <script type="text/javascript" src="js/happybody_software.js"></script>

    <script type="text/javascript">
        var menuID;
        $(document).ready(function () {
            loadGrid();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {
            
        });

        function loadGrid() {
            $.ajax({
                type: "POST",
                url: "SubMenus.aspx/loadGrid",
                data: '{"filtro":"' + $('#search').val().trim() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divTable').html(res.d);
                    }
                }
            });
        }

        function loadEdit(id) {
            menuID = id;

            $.ajax({
                type: "POST",
                url: "SubMenus.aspx/loadEditMenu",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divEdit').html(res.d);
                        loadMenusEdit();
                        showEdit();
                    }
                }
            });
        }

        function showEdit() {
            $('#divEdit').fadeIn();
            $('#divTable').fadeOut();
            $('#newMenuBtnDiv').fadeOut();
            $('#searchDiv').fadeOut();
        }

        function closeEdit() {
            $('#divEdit').fadeOut();
            $('#divTable').fadeIn();
            $('#newMenuBtnDiv').fadeIn();
            $('#searchDiv').fadeIn();
            loadGrid();
        }

        function showNew() {
            $('#divNew').fadeIn();
            $('#divTable').fadeOut();
            $('#newMenuBtnDiv').fadeOut();
            $('#searchDiv').fadeOut();
            $('#tituloNew').val('');
            $('#paginaNew').val('');
            $('#ordemNew').val('');
            $('#notasNew').val('');
            $('#administradorNew').prop('checked', false);
            $('#visivelNew').prop('checked', false);
            loadMenusNew();
        }

        function closeNew() {
            $('#divNew').fadeOut();
            $('#divTable').fadeIn();
            $('#newMenuBtnDiv').fadeIn();
            $('#searchDiv').fadeIn();
            loadGrid();
        }

        function edit() {
            alertify.confirm('SubMenus', 'Tem a certeza que deseja atualizar a informação relativa ao submenu?',
                function () {
                    if ($('#tituloEdit').val() == "") {
                        alertify.message('O campo Título é obrigatório!');
                        return;
                    }

                    var administrador;
                    var visivel;

                    if ($('#administradorEdit').is(':checked'))
                        administrador = "1";
                    else
                        administrador = "0";

                    if ($('#visivelEdit').is(':checked'))
                        visivel = "1";
                    else
                        visivel = "0";

                    $.ajax({
                        type: "POST",
                        url: "SubMenus.aspx/atualizarMenu",
                        data: '{"operatorID":"' + $('#lbloperatorid').html() + '", "titulo":"' + $('#tituloEdit').val() + '", "pagina":"' + $('#paginaEdit').val()
                            + '", "notas":"' + $('#notasEdit').val() + '", "administrador":"' + administrador + '", "visivel":"' + visivel
                            + '", "submenuID":"' + menuID + '", "menuID":"' + $('#selectMenusEdit').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                if (parseInt(res.d) >= 0) {
                                    alertify.message('SubMenu atualizado com sucesso!');
                                    $('#search').val('');
                                    closeEdit();
                                    loadGrid();
                                }
                                else {
                                    alertify.message('Ocorreu um erro ao atualizar o SubMenu!');
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        };

        function remove() {
            alertify.confirm('SubMenus', 'Tem a certeza que deseja apagar o submenu?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "SubMenus.aspx/deleteMenu",
                        data: '{"id":"' + menuID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                if (parseInt(res.d) >= 0) {
                                    alertify.message('Menu removido com sucesso!');
                                    $('#search').val('');
                                    closeEdit();
                                    loadGrid();
                                }
                                else {
                                    alertify.message('Ocorreu um erro ao remover o Menu!');
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        };

        function newMenu() {
            alertify.confirm('SubMenus', 'Tem a certeza que inserir a informação relativa ao submenu?',
                function () {
                    if ($('#tituloNew').val() == "") {
                        alertify.message('O campo Título é obrigatório!');
                        return;
                    }

                    var administrador;
                    var visivel;

                    if ($('#administradorNew').is(':checked'))
                        administrador = "1";
                    else
                        administrador = "0";

                    if ($('#visivelNew').is(':checked'))
                        visivel = "1";
                    else
                        visivel = "0";

                    $.ajax({
                        type: "POST",
                        url: "SubMenus.aspx/criarMenu",
                        data: '{"operatorID":"' + $('#lbloperatorid').html() + '", "titulo":"' + $('#tituloNew').val() + '", "pagina":"' + $('#paginaNew').val()
                            + '", "notas":"' + $('#notasNew').val() + '", "administrador":"' + administrador + '", "visivel":"' + visivel
                            + '", "id_menu":"' + $('#selectMenusNew').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                //$('#divNew').html(res.d);
                                if (parseInt(res.d) >= 0) {
                                    alertify.message('SubMenu criado com sucesso!');
                                    $('#search').val('');
                                    closeNew();
                                    loadGrid();
                                }
                                else {
                                    alertify.message('Ocorreu um erro ao criar o SubMenu!');
                                }
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        };

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadGrid();
            }
        });

        function loadMenusNew() {
            $.ajax({
                type: "POST",
                url: "SubMenus.aspx/loadMenusSelectNew",
                data: '{"operatorID":"' + $('#lbloperatorid').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divSelectMenuNew').html('Menu' + res.d);
                    }
                }
            });
        }

        function loadMenusEdit() {
            $.ajax({
                type: "POST",
                url: "SubMenus.aspx/loadMenusSelectEdit",
                data: '{"id":"' + $('#idmenuedit').html() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divMenuSelectEdit').html('Menu' + res.d);
                    }
                }
            });
        }
    </script>
</body>
</html>
