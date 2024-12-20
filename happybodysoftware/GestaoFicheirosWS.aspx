<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestaoFicheirosWS.aspx.cs" Inherits="GestaoFicheirosWS" Culture="auto" UICulture="auto" %>

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
            font-family: 'Noto Sans', sans-serif !important;
        }

            table thead {
                background-color: #000;
                color: #FFF;
                font-size: large;
                font-weight: bold;
            }

        .headerLeft {
            width: 75%;
            border-right: 1px red solid;
            -webkit-border-top-left-radius: 4px !important;
            border-top-left-radius: 4px !important;
            -webkit-border-bottom-left-radius: 4px !important;
            border-bottom-left-radius: 4px !important;
            text-align: left;
        }

        .headerCenter {
            width: 15%;
            border-right: 1px red solid;
            border-left: 1px red solid;
            text-align: center;
        }

        .headerRight {
            width: 10%;
            border-left: 1px red solid;
            -webkit-border-top-right-radius: 4px !important;
            border-top-right-radius: 4px !important;
            -webkit-border-bottom-right-radius: 4px !important;
            border-bottom-right-radius: 4px !important;
            text-align: right;
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
            width: 100% !important;
            height: 100% !important;
            border-collapse: collapse !important;
        }
    </style>
</head>
<body style="background-color: #FFF !important">
    <div style="width: 100%;" id="content">
        <span class="variaveis" id="lbloperatorid" runat="server"></span>

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divTable">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="divSelectPath" runat="server">
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="divSelectFiles" runat="server">
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divImages" runat="server" style="height: 300px; text-align: center; margin-top: 20px;">
                <img src="" style="width: auto; height: 100%;" id="imageSelected" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="height: 50px; text-align: center; margin-top: 20px;">
                <input type="button" class="form-control" value="Apagar Ficheiro Selecionado" style="width: 100%; height: 100%" onclick="deleteFile();" />
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
            $('#content').height($(parent.window).width());
            loadImages();
        });

        $(window).on('resize', function () {
            $('#content').height($(parent.window).width());
        });

        $(window).scroll(function () {

        });

        function loadImages() {
            if ($('#selectPath').val() == '0') {
                $('#table').html('Por favor, selecione um caminho');
                return;
            }

            var caminho = "";

            if ($('#selectPath').val() != 'img') {
                caminho = $('#generalPath').html() + 'img//' + $('#selectPath').val();
            }
            else {
                caminho = $('#generalPath').html() + $('#selectPath').val();
            }

            $.ajax({
                type: "POST",
                url: "GestaoFicheirosWS.aspx/load",
                data: '{"caminho":"' + caminho + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    if (res.d != null) {
                        $('#divSelectFiles').html(res.d);
                        changeImg();
                    }
                }
            });
        }

        function changeImg() {
            if ($('#selectFiles').val().trim() == '0') {
                $("#imageSelected").attr("src", "");
            }
            else {
                var caminho = "";

                if ($('#selectPath').val() != 'img') {
                    caminho = $('#generalPath').html() + 'img//' + $('#selectPath').val();
                }
                else {
                    caminho = $('#generalPath').html() + $('#selectPath').val();
                }

                var image = "..//" + caminho + "//" + $('#selectFiles').val().trim();
                $("#imageSelected").attr("src", image);
            }
        }

        function deleteFile() {
            var caminho = "";

            if ($('#selectPath').val() != 'img') {
                    caminho = $('#generalPath').html() + 'img//' + $('#selectPath').val();
                }
                else {
                    caminho = $('#generalPath').html() + $('#selectPath').val();
            }

            caminho += "//" + $('#selectFiles').val().trim();

            alertify.confirm('Gestão de Ficheiros', 'Deseja eliminar o ficheiro selecionado?',
                function () {
                    $.ajax({
                        type: "POST",
                        url: "GestaoFicheirosWS.aspx/deleteFile",
                        data: '{"caminho":"' + caminho + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (res) {
                            if (res.d != null) {
                                alertify.message(res.d);
                                loadImages();
                            }
                        }
                    });
                }
                , function () { }).set('labels', { ok: 'Sim', cancel: 'Não' });
        }

    </script>
</body>
</html>
