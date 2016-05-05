using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using SitecoreCookbook.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FacetedSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string searchQuery = txtInput.Text;
        string[] facets = null;

        lblResult.Text = "You searched for: " + searchQuery;

        string selectedFacets = HttpContext.Current.Request["facets"];
        if (!string.IsNullOrEmpty(selectedFacets))
            facets = selectedFacets.Split(',');

        FacetedSearchForBooks(searchQuery, facets);
    }


    public void FacetedSearchForBooks(string searchText, string[] facets)
    {
        var bookResult = new List<Book>();
        var facetResult = new List<Facet>();

        string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
        using (var searchContext = ContentSearchManager.GetIndex(index).
            CreateSearchContext())
        {
            // Filter results based on searched text + selected facets
            var results = ApplyTextAndFacetedSearch(searchText, facets, searchContext);

            foreach (SearchHit<Book> result in results.Hits)
            {
                bookResult.Add((Book)result.Document);
            }

            // Get facets from the filtered results
            facetResult = GetFacets(results);
        }

        // Bind facets and results
        BindSearchResults(bookResult, facetResult);
    }

    private static SearchResults<Book> ApplyTextAndFacetedSearch(string searchText, string[] facets, IProviderSearchContext searchContext)
    {
        // Simple text search
        var query = searchContext.GetQueryable<Book>()
            .Where(item => item.TemplateName == "book")
            .Where(item => item.Title.Contains(searchText));

        // Filter based on selected facets
        if (facets != null)
        {
            foreach (string facet in facets)
                query = query.Where(item => item.Tags.Contains(facet));
        }

        // Apply faceting to find facet categories
        var results = query.FacetOn(facet => facet.Tags).GetResults();
        return results;
    }

    private void BindSearchResults(List<Book> books, List<Facet> Facets)
    {
        repeaterBooks.DataSource = books;
        repeaterBooks.DataBind();

        if (Facets.Count > 0)
        {
            repeaterFacets.DataSource = Facets;
            repeaterFacets.DataBind();
        }
        repeaterFacets.Visible = (Facets.Count > 0);
    }

    private static List<Facet> GetFacets(SearchResults<Book> results)
    {
        var facets = new List<Facet>();
        // get facets from the results, which we can get using results.Facets.Categories
        foreach (var facetCategories in results.Facets.Categories)
        {
            foreach (var facet in facetCategories.Values)
            {
                string id = facet.Name;
                Item facetItem = Sitecore.Context.Database.GetItem(new ID(id));
                if (facetItem != null)
                {
                    // code to maintain checked/selected facets on postbacks
                    string isSelected = string.Empty;
                    string selectedFacets = HttpContext.Current.Request["facets"];
                    if (!string.IsNullOrEmpty(selectedFacets))
                    {
                        if (selectedFacets.IndexOf(id) >= 0)
                            isSelected = "checked";
                    }

                    facets.Add(new Facet(id, facetItem.Name, isSelected));
                }
            }
            facets = facets.OrderBy(f => f.Name).ToList();
        }
        return facets;
    }
}