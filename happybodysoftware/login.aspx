<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" Culture="auto" UICulture="auto" %>

<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">

	<link rel="icon" type="image/png" href="img/favicon.ico"/>
	<link rel="stylesheet" type="text/css" href="css/login.css">

    <title>Login</title>

    <script type="text/javascript">
        $(window).resize(function () {

        });

        $(document).ready(function () {
            window.history.pushState(null, "", window.location.href);
            window.onpopstate = function () {
                window.history.pushState(null, "", window.location.href);
            };
        });

        $(document).keypress(function (e) {
            if (e.which == 13) {
                
            }
        });

        var vContinue = document.getElementById("continue");
        var vLogin = document.getElementById("login");

        vContinue.addEventListener("click", function() {
           document.body.className += ' denied';
        }, false);

        var pfx = ["webkit", "moz", "MS", "o", ""];
        function PrefixedEvent(element, type, callback) {
            for (var p = 0; p < pfx.length; p++) {
                if (!pfx[p]) type = type.toLowerCase();
                element.addEventListener(pfx[p]+type, callback, false);
            }
        }

        PrefixedEvent(vLogin, "AnimationEnd", function () {
            document.body.className = '';
        });
    </script>
</head>


<body>
    <form runat="server">
        <div id="login" class="login-form-container">
            <header>
                <img src="img/logo.png" />
            </header>
            <fieldset>
                <div class="input-wrapper">
                    <asp:TextBox ID="username" runat="server" TextMode="SingleLine" placeholder="Username" CssClass="input-text-and-password" />
                </div>
                <div class="input-wrapper">
                    <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Password" CssClass="input-text-and-password" />
                </div>
                <div class="error-message-wrapper">
                    <asp:Label runat="server" ID="errorMessage" CssClass="error-message"/>
                </div>
                <asp:Button runat="server" ID="continue" OnClick="ValidateLogin" Text="LOGIN" CssClass="button-login" />
            </fieldset>
        </div>
    </form>
</body>
</html>
