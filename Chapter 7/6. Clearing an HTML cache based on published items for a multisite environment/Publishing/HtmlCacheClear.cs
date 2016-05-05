using System.Collections.Generic;
using Sitecore.Caching;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SitecoreCookbook.Publishing
{
    public class HtmlCacheClear
    {
        protected void ClearCache(object Sender, EventArgs args)
        {
            PublishEndRemoteEventArgs pubArgs = (PublishEndRemoteEventArgs)args;
            string rootID = pubArgs.RootItemId.ToString();

            Database db = Database.GetDatabase(pubArgs.TargetDatabaseName);
            Item rootItem = db.GetItem(rootID);
            if (rootItem != null)
                ClearHtmlCache(rootItem);
        }

        private static void ClearHtmlCache(Item rootItem)
        {
            List<SiteInfo> sites = Factory.GetSiteInfoList();

			// Find, to which sites the publishing item is related..
            var selectedSites = sites
                .Where(s => rootItem.Paths.Path.ToLower().StartsWith(s.RootPath.ToLower()) 
                    && s.Database.ToLower() == "web");
					
			// Clear Html cache of all these sites
            foreach (SiteInfo site in selectedSites)
            {
                ClearSiteHtmlCache(site);
            }
        }

		// Clear Html cache for the passed site.
        private static void ClearSiteHtmlCache(SiteInfo site)
        {
            string cacheName = site.Name + "[html]";
            Cache cache = CacheManager.FindCacheByName(cacheName);
            if (cache != null)
                cache.Clear();
        }

    }
}


