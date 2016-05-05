using Lucene.Net.Index;
using Lucene.Net.Store;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.LuceneProvider;
using SitecoreCookbook.Search;
using SpellChecker.Net.Search.Spell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
public partial class DidYouMean : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = Request["q"];
        if (!IsPostBack && !string.IsNullOrEmpty(term))
        {
            txtSearchText.Text = term;
            string indexName = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
            LuceneIndex index = (LuceneIndex)ContentSearchManager.GetIndex(indexName);
            using (var searchContext = index.CreateSearchContext(Sitecore.ContentSearch.Security.SearchSecurityOptions.Default))
            {
                var results = GetSearchResults(term, searchContext);

                // Code for getting similar words or Did you mean
                string spellIndex = Settings.IndexFolder + "/custom_spellcheck_index";
                IndexSpellCheckDictionary(indexName, spellIndex);
                string[] suggestions = GetSuggestedWords(spellIndex, term, 3);
                ///

                bool hasNoResult = (results.Count < 1 && suggestions.Length > 0);
                if (hasNoResult)
                    results = GetSearchResults(suggestions[0], searchContext);

                if (results.Count() > 0)
                {
                    StringBuilder strSuggestion = new StringBuilder();
                    if (suggestions.Length > 0)
                    {
                        if (hasNoResult)
                            strSuggestion.Append("No results found for: <i>" + term + "</i><br/>Instead showing results for: ");
                        else
                            strSuggestion.Append("Did you mean? ");
                        lblDidYouMean.Text = strSuggestion.ToString();
                        foreach (string str in suggestions)
                            lblDidYouMean.Text += string.Format("<a href='?q={0}'>{0}</a> ", str);
                    }

                    repeaterResults.DataSource = results;
                    repeaterResults.DataBind();
                }
            }
        }
    }

    private static List<SearchResult> GetSearchResults(string query, IProviderSearchContext searchContext)
    {
        var catFilter = PredicateBuilder.False<SearchResult>()
            .Or(i => i.Title.Contains(query))
            .Or(i => i.Description.Contains(query));

        var filter = PredicateBuilder.True<SearchResult>().And(catFilter);

        return searchContext.GetQueryable<SearchResult>().Where(filter).ToList();
    }

    public void IndexSpellCheckDictionary(string dbIndexName, string spellIndex)
    {
        LuceneIndex index = (LuceneIndex)ContentSearchManager.GetIndex(dbIndexName);
        IndexReader reader = index.CreateReader(LuceneIndexAccess.ReadOnly);

        FSDirectory dir = FSDirectory.Open(spellIndex);
        var spell = new SpellChecker.Net.Search.Spell.SpellChecker(dir);

        string fieldName = "description";
        LuceneDictionary dictionary = new LuceneDictionary(reader, fieldName);
        spell.IndexDictionary(dictionary, 10, 32);
    }

    public string[] GetSuggestedWords(string spellIndex, string term, int maxCount)
    {
        FSDirectory dir = FSDirectory.Open(spellIndex);
        var spell = new SpellChecker.Net.Search.Spell.SpellChecker(dir);

        spell.SetAccuracy(0.6f);
        spell.setStringDistance(new LevenshteinDistance());

        return spell.SuggestSimilar(term, maxCount);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?q=" + txtSearchText.Text);
    }
}