using Sitecore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Caching
{
    public class SharePriceCacheManager
    {
        private static readonly SharePriceCache Cache;

        static SharePriceCacheManager()
        {
            Cache = new SharePriceCache("SharePriceCache",
                     StringUtil.ParseSizeString("10MB"));
        }

        public static string GetCache(string key)
        {
            return Cache.GetString(key);
        }

        public static void SetCache(string key, string value, DateTime expiry)
        {
            Cache.SetString(key, value, expiry);
        }

        public static void ClearSharePriceCache()
        {
            Cache.Clear();
        }
    }
}
