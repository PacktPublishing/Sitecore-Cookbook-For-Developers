using Sitecore.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Caching
{
    public class SharePriceCache : CustomCache
    {
        public SharePriceCache(string name, long maxSize)
            : base(name, maxSize) { }

        new public void SetString(string key, string value, DateTime expiry)
        {
            base.SetString(key, value, expiry);
        }

        new public string GetString(string key)
        {
            return base.GetString(key);
        }
    }

}
