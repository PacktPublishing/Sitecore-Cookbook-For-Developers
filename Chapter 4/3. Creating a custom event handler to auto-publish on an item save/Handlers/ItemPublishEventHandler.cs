using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Handlers
{
    public class ItemPublishEventHandler
    {
        protected void OnItemSaved(object sender, EventArgs args)
        {
            if ((args != null))
            {
                Item item = Event.ExtractParameter<Item>(args, 0);
                if (item != null && item.Database.Name == "master")
                {
                    if (item["Auto Publish"] == "1")
                    {
                        // Only of Auto Publish is enabled...
                        // Save item without updating statistics and in silent mode.
                        // Update statistics = update modified date, revision fields
                        // Silent means bypassing Event Queue
                        using(new EditContext(item, false, true))
                        { 
                            item.Fields["Auto Publish"].Value = "";
                        }
                        Database[] targetDBs = GetTargetDatabases();
                        PublishManager.PublishItem(item, targetDBs, item.Languages, false, false);
                    }
                }
            }
        }
        private static Database[] GetTargetDatabases()
        {
            Item publishTarget = Sitecore.Client.GetItemNotNull("/sitecore/system/publishing targets");
            List<Database> db = new List<Database>();
            foreach (Item item in publishTarget.Children)
                db.Add(Factory.GetDatabase(item["Target Database"]));
            return db.ToArray();
        }

    }
}