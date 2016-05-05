using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore;

namespace SitecoreCookbook.Models
{
    public class ItemList : RenderingModel
    {
        public List<Item> Items { get; set; }
        public string Title { get; set; }
        public string CssClass { get; set; }

        public override void Initialize(Rendering rendering)
        {
            int records = 0;
			// Changed Parameter Name. Added space as strongly typed parameters support white spaces.
            int.TryParse(rendering.Parameters["Records Count"], out records);
            CssClass = rendering.Parameters["Css Class"];

            string dataSource = rendering.DataSource;
            Item sourceItem = GetDataSource(dataSource);
            Title = sourceItem["Title"];

            Items = sourceItem.GetChildren().ToList();
            if (records > 0)
                Items = Items.Take(records).ToList();
        }

        private Item GetDataSource(string dataSource)
        {
            Item sourceItem = null;
            if (dataSource != null)
            {
                Item item = Context.Database.GetItem(dataSource);
                if (item != null)
                    sourceItem = item;
            }

            if (sourceItem == null)
                sourceItem = Context.Item;
            return sourceItem;
        }
    }
}