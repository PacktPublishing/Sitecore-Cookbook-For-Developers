using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SitecoreCookbook.WebControls
{
    public class Breadcrumbs: WebControl, INamingContainer
    {
        Sitecore.Collections.ItemList breadcrumbItems = new Sitecore.Collections.ItemList();
        Item currentItem = Sitecore.Context.Item;
        static string HomeId = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";


        public string BreadcrumbID { get; set; }

        protected override void DoRender(HtmlTextWriter output)
        {
            string homePath = Sitecore.Context.Site.StartPath;
            HomeId = Sitecore.Context.Database.GetItem(homePath).ID.ToString();

            CreateCrumbs(currentItem);
            if (!string.IsNullOrEmpty(BreadcrumbID))
                output.AddAttribute("Id", BreadcrumbID);
            output.RenderBeginTag("div");
            MakeCrumb(output);
            output.RenderEndTag();
        }

        private void CreateCrumbs(Item currentItem)
        {
            if (currentItem != null && currentItem.ID.ToString() != HomeId)
            {
                CreateCrumbs(currentItem.Parent);
            }
            this.breadcrumbItems.Add(currentItem);
        }

        private void MakeCrumb(HtmlTextWriter output)
        {
            output.RenderBeginTag("ol");
            foreach (Item item in breadcrumbItems)
            {
                output.RenderBeginTag("li");
                if (item.ID != currentItem.ID)
                {
                    string url = LinkManager.GetItemUrl(item);
                    output.AddAttribute("href", url);
                    output.RenderBeginTag("a");

                    output.Write(FieldRenderer.Render(item, "title"));
                    output.RenderEndTag();
                }
                else
                    output.Write(FieldRenderer.Render(item, "title"));
                output.RenderEndTag();
            }
            output.RenderEndTag();
        }
    }
}
