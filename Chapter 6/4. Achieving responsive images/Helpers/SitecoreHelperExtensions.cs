using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Helpers;
using Sitecore.Resources.Media;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace SitecoreCookbook.Helpers
{
    public static class SitecoreHelperExtensions
    {
        public static HtmlString RenderResponsivePicture(this SitecoreHelper helper, string fieldName, Item item = null, bool isEditable = false, List<Dimensions> dimensions = null)
        {
            if (item.Fields[fieldName] == null)
                return new HtmlString(string.Empty);

            var mediaItem = new ImageField(item.Fields[fieldName]).MediaItem;
            if (mediaItem == null)
                return new HtmlString(string.Empty);

            if (Sitecore.Context.PageMode.IsExperienceEditor)
                return helper.Field(fieldName, item);

            return GeneratePictureTag(dimensions, mediaItem);
        }


        private static HtmlString GeneratePictureTag(List<Dimensions> dimensions, Item mediaItem)
        {
            if (dimensions == null || dimensions.Count == 0)
                return new HtmlString(string.Empty);

            StringBuilder html = new StringBuilder("<picture>");
            foreach (Dimensions param in dimensions.Take(dimensions.Count - 1))
            {
                string mediaUrl = GetMediaUrl(mediaItem, param.Width, param.Height);
                html.AppendFormat("<source media=\"(min-width: {0}px)\" srcset=\"{1}\">", param.ScreenSize, mediaUrl);
            }

            Dimensions lastParam = dimensions.Last();
            string imgUrl = GetMediaUrl(mediaItem, lastParam.Width, lastParam.Height);
            html.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgUrl, mediaItem["Alt"]);

            html.Append("</picture>");
            return new HtmlString(html.ToString());
        }


        private static string GetMediaUrl(Item mediaItem, int width, int height)
        {
            MediaUrlOptions mediaUrlOptions = new MediaUrlOptions();
            if (width > 0)
                mediaUrlOptions.Width = width;
            if (height > 0)
                mediaUrlOptions.Height = height;
            return MediaManager.GetMediaUrl(mediaItem, mediaUrlOptions);
        }
    }
}
