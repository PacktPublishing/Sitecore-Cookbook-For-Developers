using SitecoreCookbook.Search;
using System;
using System.Linq;
using System.Web.UI;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;

public partial class BoostResults : System.Web.UI.Page
{
    private void SearchResults(string query)
    {
        string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
        using (var searchContext = ContentSearchManager.GetIndex(index).CreateSearchContext())
        {
            var filter = PredicateBuilder.True<Product>();

            var template = PredicateBuilder.False<Product>()
                  .Or(i => i.TemplateName.Equals("product"))
                  .Or(i => i.TemplateName.Equals("blog").Boost(20f));
            filter = filter.And(template);

            var fields = PredicateBuilder.False<Product>()
                .Or(i => i.ProductName.Contains(query))
                .Or(i => i.Description.Contains(query));
            filter = filter.And(fields);

            var searchResults = searchContext.GetQueryable<Product>()
                .Where(filter).GetResults()
                .OrderByDescending(i => i.Score);

            foreach (var item in searchResults)
            {
                phResults.Controls.Add(new LiteralControl("<b>" + item.Score.ToString("00.00") + "</b>  <a href='#' >" + item.Document.ProductName + "</a><hr/>"));
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchResults(txtSearchText.Text);   
    }
}