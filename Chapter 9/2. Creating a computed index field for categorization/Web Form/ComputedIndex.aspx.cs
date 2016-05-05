using SitecoreCookbook.Search;
using System;
using System.Linq;
using System.Web.UI;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using System.Collections.Generic;
using Sitecore.ContentSearch.Security;

public partial class ComputedIndex : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs args)
    {
        string category = Request["bookcategory"];
        if (!string.IsNullOrEmpty(category))
        {
			string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
            using (var searchContext = ContentSearchManager.GetIndex(index).CreateSearchContext())
            {
                var searchQuery = searchContext.GetQueryable<Book>()
                    .Where(item => item.Categories.Contains(category));
                List<Book> books = searchQuery.ToList();

                repeaterResults.DataSource = books;
                repeaterResults.DataBind();
            }
        }
    }
    public void btnSearch_Click(object sender, EventArgs args)
    {
        string bookcategory = txtSearch.Text;
        Response.Redirect(Request.Path.ToString() + "?bookcategory=" + bookcategory);
    }
}