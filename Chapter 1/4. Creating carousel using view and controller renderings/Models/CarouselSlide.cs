using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreCookbook.Models
{
    public class CarouselSlide: CustomItem
    {
        public CarouselSlide(Item item)
            : base(item)
        { }

        public string Title
        {
            get
            {
                return InnerItem["Title"];
            }
        }
        public MvcHtmlString Image
        {
            get
            {
                return new MvcHtmlString(FieldRenderer.Render(InnerItem, "Image"));
            }
        }

        public string Url
        {
            get
            {
                Item linkItem = Sitecore.Context.Database.GetItem(InnerItem["Link Item"]);
                if (linkItem != null)
                    return LinkManager.GetItemUrl(linkItem);
                return "";
            }
        }
    }
}