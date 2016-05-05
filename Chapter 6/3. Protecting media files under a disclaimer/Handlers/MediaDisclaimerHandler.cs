using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System.Web;
using System.Web.SessionState;

namespace SitecoreCookbook.Handlers
{
    public class MediaDisclaimerHandler : MediaRequestHandler, IRequiresSessionState
    {
        protected override bool DoProcessRequest(HttpContext context, MediaRequest request, Sitecore.Resources.Media.Media media)
        {
            if (Context.Database.Name.ToLower() != "core")
            {
                string mediaId = media.MediaData.MediaItem.ID.ToString();

                Item disclaimerFolder = Context.Database.GetItem("/sitecore/content/Global/Settings/Disclaimers");
                if (disclaimerFolder != null)
                {
                    foreach (Item disc in disclaimerFolder.Children)
                    {
                        if (disc["Media To Protect"].IndexOf(mediaId) >= 0)
                        {
                            string disclaimerPageId = disc["Disclaimer Page"];
                            Item item = Context.Database.GetItem(new ID(disclaimerPageId));
                            string url = LinkManager.GetItemUrl(item);

                            ShowDisclaimer(mediaId, url);
                            break;
                        }
                    }
                }
            }
            return base.DoProcessRequest(context, request, media);
        }

        private void ShowDisclaimer(string mediaId, string disclaimerUrl)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["ProtectedMedia" + mediaId] == null)
            {
                context.Session["ProtectedMedia"] = mediaId;
                context.Session["ProtectedMediaUrl"] = context.Request.Url.ToString();
                context.Response.Redirect(disclaimerUrl, true);
            }
        }
    }
}
