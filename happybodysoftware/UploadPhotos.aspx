<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPhotos.aspx.cs" Inherits="UploadPhotos" Culture="auto" UICulture="auto" %>
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

        <form id="formUploadPhoto" runat="server" onsubmit="">

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                    <asp:TextBox ID="photoName" autocomplete="off" runat="server" Height="50" CssClass="form-control" Width="100%" Font-Bold="true" Font-Size="Large" Placeholder="Nome da Fotografia"/>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-bottom: 10px;">
                    <asp:RadioButtonList ID="localizacao" runat="server" RepeatLayout="Flow" CssClass="radio-inline">
                        <asp:ListItem Value="Campanha" Selected>Campanha</asp:ListItem>
                        <asp:ListItem Value="Eventos">Eventos</asp:ListItem>
                        <asp:ListItem Value="Artigos">Artigos</asp:ListItem>
                        <asp:ListItem Value="Newsletter">Newsletter</asp:ListItem>
                        <asp:ListItem Value="Staff">Staff</asp:ListItem>
                        <asp:ListItem Value="IconsModalidades">Icons das Modalidades</asp:ListItem>
                        <asp:ListItem Value="Modalidades">Modalidades</asp:ListItem>
                        <asp:ListItem Value="Landing">Landing Pages</asp:ListItem>
                        <asp:ListItem Value="Horario">Horário</asp:ListItem>
                        <asp:ListItem Value="Banners">Imagens de Capa das Páginas</asp:ListItem>
                        <asp:ListItem Value="Musica">Musica de Fundo do Site</asp:ListItem>
                        <asp:ListItem Value="Video">Vídeo Apresentação</asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <asp:FileUpload id="FileUploadControl" runat="server" CssClass="form-control"/>
                <asp:Button runat="server" id="UploadButton" text="Carregar Foto" onclick="UploadButton_Click" CssClass="form-control"/>
                <br />
                <asp:Label runat="server" id="StatusLabel" text="Estado do Carregamento: " />
            </div>
        </form>
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

            loadServices();
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

        function loadServices() {
            $.ajax({
                type: "POST",
                url: "ServicosWS.aspx/load",
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

        function openServiceSelected(id, x) {
            $('#lblidselected').html(id);

            $.ajax({
                type: "POST",
                url: "ServicosWS.aspx/getServico",
                data: '{"id":"' + id + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divInfo').html(res.d);
                        $('#divTable').fadeOut();
                        $('#divInfo').fadeIn();
                    }
                }
            });
        }

        function closeServiceInfo() {
            $('#divTable').fadeIn();
            $('#divInfo').fadeOut();
            $('#search').val('');
            loadServices();
        }

        $(document).keypress(function (e) {
            if (e.which == 13) {
                loadServices();
            }
        });

        function openNewService() {
            $('#divTable').fadeOut();
            $('#divNew').fadeIn();
        }

        function closeNewService() {
            $('#divTable').fadeIn();
            $('#divNew').fadeOut();
        }

        function showLoadingBackground() {
            $('#black_loading_overlay').fadeIn();
            $('#divLoading').fadeIn();
            $('#close_fab').fadeIn();
            $('#fab').fadeIn();
        }

        function hideLoadingBackground() {
            $('#black_loading_overlay').fadeOut();
            $('#divLoading').fadeOut();
            $('#close_fab').fadeOut();
            $('#fab').fadeOut();
        }
    </script>
</body>
</html>
