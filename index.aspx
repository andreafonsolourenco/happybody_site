<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Culture="auto" UICulture="auto" %>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
	<title>Happy Body</title>
	<link rel="shortcut icon" href="general/img/favicon.ico" type="image/x-icon" />

    <link href="https://fonts.googleapis.com/css?family=Amaranth" rel="stylesheet">
    <link href="happybody/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">
    <link href="https://www.w3schools.com/w3css/4/w3.css" rel="stylesheet" type="text/css">
    <link rel="shortcut icon" href="general/img/favicon.ico" type="image/x-icon">
    <link rel="icon" href="general/img/favicon.ico" type="image/x-icon">
    <!-- Theme CSS -->
    <link href="happybody/css/freelancer.min.css" rel="stylesheet">

    <style>
        html, body {
            overflow: hidden;
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        #renderCanvas {
            width: 100%;
            height: 80%;
            max-height: 80% !important;
            -ms-touch-action: none;
            touch-action: none;
        }

        .restOfThePage {
            /*width: 100%;*/
            height: 20%;
            max-height: 20% !important;
            background: #000;
            color: red;
            /*font-family: 'Amaranth', sans-serif;*/
            text-align:center;
            font-size: 3vw;
        }

        .cursor {
            cursor: pointer;
        }

        .text-white {
            color: #FFF !important;
        }
    </style>

    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script', 'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '923313025683308');
        fbq('track', 'PageView');
    </script>
    <noscript>
        <img height="1" width="1" src="https://www.facebook.com/tr?id=923313025683308&ev=PageView&noscript=1"/>
    </noscript>
    <!-- End Facebook Pixel Code -->
</head>


<body>
    <canvas id="renderCanvas"></canvas>
    <div class="restOfThePage" runat="server" id="restOfThePage">
        <div id='divText' class='row text-center' runat='server'></div>
    </div>

    <!-- Bootstrap core JavaScript -->
    <script src="happybody/vendor/jquery/jquery.min.js"></script>
    <script src="happybody/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Custom scripts for this template -->
    <%--<script src="happybody/js/freelancer.min.js"></script>--%>   

    <script type="text/javascript" src="general/js/babylon.js"></script>
    <script type="text/javascript" src="general/js/hand.js"></script>
    

    <script type="text/javascript">
        var flag = 0;
        // Get the canvas element from our HTML above
        var canvas = document.getElementById("renderCanvas");

        // Load the BABYLON 3D engine
        var engine = new BABYLON.Engine(canvas, true);

        // This begins the creation of a function that we will 'call' just after it's built
        var createScene = function () {

            // Now create a basic Babylon Scene object 
            var scene = new BABYLON.Scene(engine);

            // Change the scene background color to green.
            //scene.clearColor = new BABYLON.Color3(0.93, 0.11, 0.14);
            scene.clearColor = new BABYLON.Color3(0, 0, 0);

            // This creates and positions a free camera
            var camera = new BABYLON.FreeCamera("camera1", new BABYLON.Vector3(0, 5, -10), scene);

            // This targets the camera to scene origin
            camera.setTarget(BABYLON.Vector3.Zero());

            // This attaches the camera to the canvas
            camera.attachControl(canvas, false);

            // This creates a light, aiming 0,1,0 - to the sky.
            var light = new BABYLON.HemisphericLight("light1", new BABYLON.Vector3(0, 1, 0), scene);

            // Dim the light a small amount
            light.intensity = .5;

            //// Let's try our built-in 'sphere' shape. Params: name, subdivisions, size, scene
            //var sphere = BABYLON.Mesh.CreateSphere("sphere1", 16, 2, scene);

            //// Move the sphere upward 1/2 its height
            //sphere.position.y = 1;

            //// Let's try our built-in 'ground' shape.  Params: name, width, depth, subdivisions, scene
            //var ground = BABYLON.Mesh.CreateGround("ground1", 6, 6, 2, scene);

            var box = BABYLON.Mesh.CreateBox("box", 4.0, scene, false, BABYLON.Mesh.DEFAULTSIDE);

            var materialBox = new BABYLON.StandardMaterial("texture1", scene);
            materialBox.diffuseTexture = new BABYLON.Texture("../general/img/logo.jpg", scene);
            //materialBox.emissiveColor = new BABYLON.Color3(0, 0, 0);
            //materialBox.emissiveTexture = new BABYLON.Texture("img/logo.png", scene);
            materialBox.backFaceCulling = false;
            box.material = materialBox;

            box.actionManager = new BABYLON.ActionManager(scene);
            scene.actionManager = new BABYLON.ActionManager(scene);

            box.actionManager.registerAction(new BABYLON.InterpolateValueAction(BABYLON.ActionManager.OnPointerOverTrigger, box, "scaling", new BABYLON.Vector3(1.2, 1.2, 1.2), 150));
            box.actionManager.registerAction(new BABYLON.InterpolateValueAction(BABYLON.ActionManager.OnPointerOutTrigger, box, "scaling", new BABYLON.Vector3(1, 1, 1), 150));
            scene.actionManager.registerAction(new BABYLON.IncrementValueAction(BABYLON.ActionManager.OnEveryFrameTrigger, box, "rotation.y", 0.01));
            scene.actionManager.registerAction(new BABYLON.IncrementValueAction(BABYLON.ActionManager.OnEveryFrameTrigger, box, "rotation.x", 0.01));

            box.actionManager.registerAction(new BABYLON.ExecuteCodeAction(BABYLON.ActionManager.OnPickTrigger, function (evt) {
                /*if (flag == 0) {
                    flag++;
                }
                else*/
                    //window.location = "MainMenu.aspx";
            }));

            // Leave this function
            return scene;

        };  // End of createScene function

        var scene = createScene();

        // Register a render loop to repeatedly render the scene
        engine.runRenderLoop(function () {
            scene.render();
        });

        // Watch for browser/canvas resize events
        window.addEventListener("resize", function () {
            engine.resize();
        });

        function goToPage() {
            if (flag == 0) {
                flag++;
            }
            else
                window.location = "MainMenu.aspx";
        }

        function openPagePT() {
            var page = getUrlPage(true) + $('#pagePT').html();
            window.open(page, '_self');
        }

        function openPageEN() {
            var page = getUrlPage(false) + $('#pageEN').html();
            window.open(page, '_self');
        }

        function openPageFR() {
            var page = getUrlPage(false) + $('#pageFR').html();
            window.open(page, '_self');
        }

        function getUrlPage(pt) {
            if (window.location.href.includes('localhost')) {
                var location = window.location.href.replace('index.aspx', '');
                location = location.replace('?data=01/01/2023', '');
                return location;
            }
            else {
                if (pt) {
                    return "https://www.happybody.pt";
                }

                return "https://www.happybody.site";
            }
        }
    </script>

  </body> 
</html>
