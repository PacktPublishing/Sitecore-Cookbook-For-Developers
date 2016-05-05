using Sitecore.Data.Items;
using Sitecore.Events.Hooks;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Providers
{
    public class CDNMediaProvider: MediaProvider, IHook
    {
        public string CDNDomain { get; set; }
        public void Initialize()
        {
            MediaManager.Provider = this;
        }

        public override string GetMediaUrl(MediaItem item, MediaUrlOptions options)
        {
            string mediaUrl = item.MediaPath + "." + item.Extension;
            return string.Format("{0}{1}", CDNDomain, mediaUrl);
        }
    }
}
