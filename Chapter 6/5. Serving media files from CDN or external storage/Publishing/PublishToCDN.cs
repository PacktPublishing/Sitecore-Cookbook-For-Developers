using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Publishing
{
    public class PublishToCDN : PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            var item = context.PublishHelper.GetSourceItem(context.ItemId);
            if (item != null && item.ID != ItemIDs.MediaLibraryRoot &&
                item.TemplateID != TemplateIDs.MediaFolder &&
                    item.Paths.IsMediaItem)
            {
                string mediaPath = item.Paths.MediaPath;
                if (context.Action == PublishAction.DeleteTargetItem)
                    DeleteMediaFile(mediaPath);
                else
                {
                    MediaStream mediaStream = MediaManager.GetMedia(item).GetStream();
                    UploadMediaFile(mediaPath, mediaStream.Stream);
                }
            }
        }

        public bool DeleteMediaFile(string mediaPath)
        {
            // Implement your CDN delete media file code here
			// OR delete file from local directory
            return true;
        }

        public bool UploadMediaFile(string mediaPath, Stream stream)
        {
            // Implement your CDN upload media file code here
			// Or copy the file to destination folder 
            return true;
        }

    }
}


