<%@ Page language="c#" autoeventwireup="true" %>
<head>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript">
        function SearchText() {
            $(".SearchTextbox").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/Cookbook/autocomplete.ashx",
                        data: {
                            searchtext: decodeURIComponent(request.term)
                        },
                        async: true,
                        success: function (data) {
                            response(data);
                        },
                        error: function (result) {
                            alert("Error loading the data" + result.responseText);
                        }
                    });
                },
                minLength: 3,
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li class='ui-corner-all'>")
                .append("<a href='?q=" + item.label + "'> <span style='float:left;'>" + item.label + "</span></a>").appendTo(ul);
            };
        }
    </script>
    <style>
        ul {
            text-transform: uppercase;
            font-size: 11px !important;
        }
    </style>
</head>
<h2>Auto complete example</h2>
<div style="width: 100%">
    <div style="border: 1px solid #999; padding: 10px; margin: 0 auto; width: 300px;">
        Autocomplete:
        <asp:TextBox class="SearchTextbox" ID="SearchTextbox" runat="server" onkeydown="SearchText();" />
    </div>
    <div id="searchresults" runat="server" visible="false" style="border: 1px solid #999; padding: 10px; margin: 0 auto; width: 300px;">
        <h3>Search Results:</h3>
    </div>
</div>
