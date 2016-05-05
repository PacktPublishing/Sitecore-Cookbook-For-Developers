
<%@ Page language="c#" %>
<%@ OutputCache Location="None" VaryByParam="none" %>
<%@ Register src="~/Layouts/Sublayouts/Left-Menu.ascx" tagprefix="uc1" tagname="LeftMenu" %>
<%@ Register Namespace="Sitecore.Web.UI.WebControls" TagPrefix="sc" %>
<%@ Register Namespace="SitecoreCookbook.WebControls" TagPrefix="cb" Assembly="SitecoreCookbook" %>
<!DOCTYPE html>
<html>
<head>
    <title><sc:Text runat="server" Field="Title" DataSource="/sitecore/Content/Home/Configurations" DisableWebEditing = "true" /></title>
    <link rel="stylesheet" href="/css/bootstrap.min.css">
    <link href="/css/desktop.css" rel="stylesheet" />
    <script src="/js/jquery.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
</head>
<body>
  <form method="post" runat="server" id="mainform">
    <div>
        <div id="header">
            <div style="width:200px;">
                <a href="/">
                    <sc:Text runat="server" Field="Logo" DataSource="/sitecore/Content/Global/Configurations" />
                </a>
            </div>
        </div>
        <div id="menu">
        </div>
        <div class="container">
            <div id="breadcrumb">
                <cb:Breadcrumbs runat="server" />
            </div>
            <div id="sidemenu">
				<uc1:leftmenu runat="server" id="LeftMenu" />
            </div>
            <div id="contentarea">
                <article>
					<h3><sc:Text field="Title" runat="server" /></h3>
					<sc:Text field="Body" runat="server" />
                </article>
            </div>
            <div id="footer">
                <sc:Text runat="server" Field="Copyright Text" DataSource="/sitecore/Content/Global/Configurations" />
            </div>
        </div>
    </div>
	</form>
</body>
</html>