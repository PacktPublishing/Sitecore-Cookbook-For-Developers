<%@ WebHandler Language="C#" Class="AutoComplete" %>

using System;
using System.Web;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.LuceneProvider;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

public class AutoComplete : IHttpHandler {
    
        public bool IsReusable
        {
             get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string fieldName = "productname";
            string searchText = HttpContext.Current.Request["searchText"];
            
            context.Response.ContentType = "application/json";
            string result = GetResults(fieldName, searchText);
            context.Response.Write(result);
        }

        public static string GetResults(string fieldName, string searchTerm)
        {
            string index = string.Format("sitecore_{0}_index", Sitecore.Context.Database.Name);
            IndexReader ir = ((LuceneIndex)ContentSearchManager.GetIndex(index)).CreateReader(LuceneIndexAccess.ReadOnly);

            WildcardTermEnum wte = new WildcardTermEnum(ir, new Term(fieldName, searchTerm.Trim() + '*'));
            List<string> results = new List<string>();
            while (wte.Next())
            {
                Term term = wte.Term;
                results.Add(term.Text);
            }
            return (new JavaScriptSerializer()).Serialize(results);
        }
}