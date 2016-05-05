using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Helpers
{
    public static class SiteHelper
    {
        public static Item HomeItem()
        {
            string homePath = Sitecore.Context.Site.StartPath;
            return  Sitecore.Context.Database.GetItem(homePath);           
        }
    }
}