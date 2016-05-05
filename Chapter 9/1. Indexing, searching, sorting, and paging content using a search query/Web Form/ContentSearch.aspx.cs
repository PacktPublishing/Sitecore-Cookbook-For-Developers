using Sitecore.Data.Items;
using Sitecore.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Security;
using Sitecore.Data;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;

public partial class ContentSearch : System.Web.UI.Page
{
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ContinueSearch(1);
    }
    public void lnkPageNo_Click(object sender, EventArgs e)
    {
        int pageNo = int.Parse(((LinkButton)sender).Text);
        ContinueSearch(pageNo);
    }

    private void ContinueSearch(int pageNumber)
    {
        string str = txtInput.Text;
        string searchType = Request["searchtype"];
        string orderBy = rbtOrder.SelectedValue;

        int pageSize = int.Parse(ddlPageSize.SelectedValue);

        int totalResults = 0;

        List<SearchResultItem> list = SearchBook(str, orderBy, pageSize, pageNumber, out totalResults);
        repeaterSearch.DataSource = list;
        repeaterSearch.DataBind();

        int totalPages = (totalResults / pageSize);
        if (totalResults % pageSize > 0)
            totalPages++;

        List<string> pages = new System.Collections.Generic.List<string>();
        for (int i = 0; i < totalPages; i++)
        {
            pages.Add((i + 1).ToString());
        }
        repeaterPage.DataSource = pages;
        repeaterPage.DataBind();

        lblResult.Text = "Total " + totalResults.ToString() + " results found.";
    }

    public List<SearchResultItem> SearchBook(string str, string orderBy, int pageSize, int pageNo, out int totalResults)
    {
        string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
        using (var context = ContentSearchManager.GetIndex(index).CreateSearchContext())
        {
            // Filter documents based on Item Path and Name of item
            var query = context.GetQueryable<SearchResultItem>()
                .Where(p => p.Path.StartsWith("/sitecore/content/home"))
                .Where(p => p.Name.Contains(str));

            // Get item count
            totalResults = query.Count();

            // Sort documents
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy == "name")
                    query = query.OrderBy(p => p.Name);
                else if (orderBy == "date")
                    query = query.OrderBy(p => p.Updated);

                // Descendants      :   query.orderByDescending() for descending queries
                // Multi-Field Sort :   query.OrderByDescending(p => p.Name).ThenByDescending(i => i.Updated);
            }

            // Pagination
            query = query.Page(pageNo - 1, pageSize);

            // Alternate: Getting pagination Data
            // int skipRecords = (pageNo - 1) * pageSize;
            // query = query.Skip(skipRecords).Take(pageSize);

            return query.ToList();



            // Using Predicate

            //var predicate = PredicateBuilder.True<SearchResultItem>();

            //predicate = predicate.And(p => p.Path.StartsWith("/sitecore/content/home"))
            //    .And(p => p.Name.Contains(str))
            //    .And(p => p.TemplateName.Equals("book"));

            //var innerPredicate = PredicateBuilder.False<SearchResultItem>()
            //    .Or(p => p.Updated >= DateTime.Now.AddDays(-5));

            //predicate = predicate.Or(innerPredicate);
            //var query = context.GetQueryable<SearchResultItem>().Where(predicate);

            //totalResults = query.Count();

            //return query.ToList();
        }
    }
}