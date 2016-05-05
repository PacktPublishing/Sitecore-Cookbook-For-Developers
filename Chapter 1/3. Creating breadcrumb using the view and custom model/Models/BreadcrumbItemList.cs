using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace SitecoreCookbook.Models
{
    public class BreadcrumbItemList : RenderingModel
    {
        public List<BreadcrumbItem> Breadcrumbs { get; set; }
        public override void Initialize(Rendering rendering)
        {
            Breadcrumbs = new List<BreadcrumbItem>();
            List<Item> items = GetBreadcrumbItems();
            foreach (Item item in items)
            {
                Breadcrumbs.Add(new BreadcrumbItem(item));
            }
            Breadcrumbs.Add(new BreadcrumbItem(Sitecore.Context.Item));
        }

        private List<Sitecore.Data.Items.Item> GetBreadcrumbItems()
        {
            string homePath = Sitecore.Context.Site.StartPath;
            Item homeItem = Sitecore.Context.Database.GetItem(homePath);
            List<Item> items = Sitecore.Context.Item.Axes.GetAncestors()
                            .SkipWhile(item => item.ID != homeItem.ID)
                            .ToList();
            return items;
        }
    }
}