<%@ control language="c#" autoeventwireup="true" targetschema="http://schemas.microsoft.com/intellisense/ie5" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        var item = Sitecore.Context.Item.Parent;
		
		var children = new List<Sitecore.Data.Items.Item>();
        foreach (var child in item.GetChildren())
        {
            children.Add((Sitecore.Data.Items.Item)child);
        }

        rptMenu.DataSource = children;
        rptMenu.DataBind();
    }


    private void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            Sitecore.Data.Items.Item dataItem = (Sitecore.Data.Items.Item)e.Item.DataItem;

            HyperLink itemLink = (HyperLink)e.Item.FindControl("itemLink");
            itemLink.Target = Sitecore.Links.LinkManager.GetItemUrl(dataItem);
        }
    }
</script>
    <ol class="sidemenu">
        <asp:repeater id="rptMenu" runat="server">
	<ItemTemplate>
		<li>
            <asp:HyperLink runat="server" id="itemLink">
			    <sc:Text DisableWebEditing="true" runat="server" Field="Title" Item="<%# Container.DataItem %>" />
            </asp:HyperLink>
            </li>
	</ItemTemplate>
</asp:repeater>
    </ol>
