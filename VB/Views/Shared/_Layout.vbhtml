<!DOCTYPE HTML>
<html>
<head>
    <title>@ViewBag.Title</title>
    @Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report}
    )
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/jquery-1.4.4.min.js")" type="text/javascript"></script>
    @Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
        New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.Chart},
        New Script With {.ExtensionSuite = ExtensionSuite.Report}
    )
    <style type="text/css">
        .errorMessage
        {
            color: Red;
            font-family: Tahoma;
            font-size: 9pt;
        }
        
        .resultMessage
        {
            color: Green;
            font-family: Tahoma;
            font-size: 9pt;
        }
    </style>
</head>
<body>
    @RenderBody()
</body>
</html>
