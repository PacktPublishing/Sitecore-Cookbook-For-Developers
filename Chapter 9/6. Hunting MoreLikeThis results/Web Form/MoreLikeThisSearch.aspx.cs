using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.LuceneProvider;
using Sitecore.ContentSearch.LuceneProvider.Queries;
using SitecoreCookbook.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

public partial class MoreLikeThisSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            List<SearchResult> results = new List<SearchResult>();
            if (!string.IsNullOrEmpty(Request["relatedto"]))
            {
                string indexName = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
                var index = (LuceneIndex)ContentSearchManager.GetIndex(indexName);
                var reader = index.CreateReader(LuceneIndexAccess.ReadOnly);
                var moreLikeThis = new MoreLikeThis(reader);
                CreateMLTQuery(moreLikeThis);

                string itemId = Request["relatedto"];
                var searcher = (IndexSearcher) index.CreateSearcher(LuceneIndexAccess.ReadOnly);
                int docId = GetDocumentId(itemId, searcher);

                int minimumNumberShouldMatch = 5;

                results = ShowSimilarResults(searcher, moreLikeThis, docId, minimumNumberShouldMatch);

                // OR using MoreLikeThisQuery
                // string description = SelectedItem["Description"];
                // results = ShowSimilarResultsUsingMLTQuery(searcher, description, new string[] { "title", "description" }, MinimumNumberShouldMatch);
            }
            if (!string.IsNullOrEmpty(Request["query"]))
                results = SearchResults(Request["query"]);

            repeaterResults.DataSource = results;
            repeaterResults.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string query = txtSearch.Text;
        Response.Redirect(Request.Path.ToString() + "?query=" + query);
    }

    private List<SearchResult> SearchResults(string query)
    {
        txtSearch.Text = query;
        List<SearchResult> results = new List<SearchResult>();

        using (var searchContext = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext(Sitecore.ContentSearch.Security.SearchSecurityOptions.Default))
        {
            var filter = PredicateBuilder.True<SearchResult>()
                .And(i => i.Path.StartsWith("/sitecore/content/home"));

            var orFilter = PredicateBuilder.False<SearchResult>()
                .Or(i => i.Title.Contains(query))
                .Or(i => i.Description.Contains(query));
            filter = filter.And(orFilter);

            return searchContext.GetQueryable<SearchResult>().Where(filter).ToList();
        }
    }

    private List<SearchResult> ShowSimilarResults(IndexSearcher searcher, MoreLikeThis mlt, int docId, int topHits)
    {
        BooleanQuery boolQuery = (BooleanQuery)mlt.Like(docId);
        ScoreDoc[] scoreDocs = searcher.Search(boolQuery, topHits).ScoreDocs;

        List<SearchResult> results = new List<SearchResult>();
        foreach (var scoreDoc in scoreDocs)
        {
            Document doc = searcher.Doc(scoreDoc.Doc);
            SearchResult result = new SearchResult(doc.Get("title"), doc.Get("description"), doc.Get("_group"));
            results.Add(result);
        }
        return results;
    }

    private List<SearchResult> ShowSimilarResultsUsingMLTQuery(IndexSearcher searcher, string searchPhrase, string[] fields, int TopHits)
    {
        MoreLikeThisQuery query = new MoreLikeThisQuery(searchPhrase, fields, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
        query.MinDocFreq = 1;
        query.MinTermFrequency = 1;

        ScoreDoc[] scoreDocs = searcher.Search(query, TopHits).ScoreDocs;

        List<SearchResult> results = new List<SearchResult>();
        foreach (var scoreDoc in scoreDocs)
        {
            Document doc = searcher.Doc(scoreDoc.Doc);
            SearchResult result = new SearchResult(doc.Get("title"), doc.Get("description"), doc.Get("_group"));
            results.Add(result);
        }
        return results;
    }

    private void CreateMLTQuery(MoreLikeThis query)
    {
        query.Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        query.MinTermFreq = 1;
        query.MinDocFreq = 1;
        query.MaxQueryTerms = 15;
        query.SetFieldNames(new string[] { "title", "description" });
        query.SetStopWords(StopAnalyzer.ENGLISH_STOP_WORDS_SET);
    }

    private static int GetDocumentId(string itemId, IndexSearcher searcher)
    {
        Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_30;
        StandardAnalyzer analyzer = new StandardAnalyzer(version);
        QueryParser parser = new QueryParser(version, "_group", analyzer);
        Query result = parser.Parse(itemId);
        ScoreDoc[] singleDoc = searcher.Search(result, 1).ScoreDocs;
        if (singleDoc.Length > 0)
            return singleDoc[0].Doc;
        return 0;
    }

    // You can use custom stopwords....
    public System.Collections.Generic.ISet<string> StopWords
    {
        get
        {
            string[] array = { "a", "about", "above", "after", "again", "against", "all", "am", "an", "and", "any", "are", "aren't", "as", "at", "be", "because", "been", "before", "being", "below", "between", "both", "but", "by", "can't", "cannot", "could", "couldn't", "did", "didn't", "do", "does", "doesn't", "doing", "don't", "down", "during", "each", "few", "for", "from", "further", "had", "hadn't", "has", "hasn't", "have", "haven't", "having", "he", "he'd", "he'll", "he's", "her", "here", "here's", "hers", "herself", "him", "himself", "his", "how", "how's", "i", "i'd", "i'll", "i'm", "i've", "if", "in", "into", "is", "isn't", "it", "it's", "its", "itself", "let's", "me", "more", "most", "mustn't", "my", "myself", "no", "nor", "not", "of", "off", "on", "once", "only", "or", "other", "ought", "our", "ours 	ourselves", "out", "over", "own", "same", "shan't", "she", "she'd", "she'll", "she's", "should", "shouldn't", "so", "some", "such", "than", "that", "that's", "the", "their", "theirs", "them", "themselves", "then", "there", "there's", "these", "they", "they'd", "they'll", "they're", "they've", "this", "those", "through", "to", "too", "under", "until", "up", "very", "was", "wasn't", "we", "we'd", "we'll", "we're", "we've", "were", "weren't", "what", "what's", "when", "when's", "where", "where's", "which", "while", "who", "who's", "whom", "why", "why's", "with", "won't", "would", "wouldn't", "you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves" };
            CharArraySet charArraySet = new CharArraySet(array.Length, false);
            charArraySet.AddAll(array);
            return charArraySet;
        }
    }
}