using Sitecore.Pipelines.HttpRequest;
using System.Web;

namespace SitecoreCookbook.Pipelines
{
    public class Set404Status : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            // If it's a custom 404 page then only
            if (Sitecore.Context.Items["Is404Page"]!= null
                && Sitecore.Context.Items["Is404Page"].ToString() == "true")
            {
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}