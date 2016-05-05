using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Handlers
{
    public class AuditTrailEventHandler
    {
        public bool WriteLogs { get; set; }

        public void OnItemCreated(object sender, EventArgs args)
        {
            ItemCreatedEventArgs eventArgs = Event.ExtractParameter<ItemCreatedEventArgs>(args, 0);
            if (eventArgs.Item != null)
                WriteAuditLog("created", eventArgs.Item);
        }
        public void OnItemDeleted(object sender, EventArgs args)
        {
            Item item = Event.ExtractParameter(args, 0) as Item;
            if (item != null)
                WriteAuditLog("deleted", item);
        }
        public void WriteAuditLog(string operation, Item item)
        {
            if (WriteLogs)
            {
                Log.Audit("Item " + operation + ":" + item.Paths.Path, this);
				StoreToDatase("Item " + operation, item);
            }
        }
		public void StoreToDatase(string operation, Item item)
		{
			// Write code to store audit operations to database
		}
    }
}