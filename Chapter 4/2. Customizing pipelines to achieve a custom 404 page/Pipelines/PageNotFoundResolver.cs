using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using System.IO;
using System.Web;

namespace SitecoreCookbook.Pipelines
{
    public class PageNotFoundResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            string filePath = HttpContext.Current.Server.MapPath(args.Url.FilePath);

            // Return if item exists, context site is physical site or request is a physical file.
            if (IsValidItem()
            || args.LocalPath.StartsWith("/sitecore")
            || File.Exists(filePath))
                return;

            // Assign 404 page as Context Item
            Context.Item = Get404Page();
            if (Context.Item != null)
            { 
                // Below is Request Cache, which remains stored throughout the Http request
                Sitecore.Context.Items["Is404Page"] = "true";
            }
        }

        private Item Get404Page()
        {
            string itemPath = Context.Site.StartPath + "/404-Page";
            return Context.Database.GetItem(itemPath);
        }

        protected virtual bool IsValidItem()
        {
            // Item is null or it has no current language version created
            if (Context.Item == null || Context.Item.Versions.Count == 0)
                return false;

            // No layout details set to item
            if (Context.Item.Visualization.Layout == null)
                return false;

            return true;
        }
    }
}