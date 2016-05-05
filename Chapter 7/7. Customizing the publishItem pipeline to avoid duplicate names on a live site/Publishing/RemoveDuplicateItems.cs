using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Publishing
{
    public class RemoveDuplicateItems:PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            Item sourceItem = context.PublishHelper.GetSourceItem(context.ItemId);
            if (sourceItem != null)
            {
				// Check whether any item found in target DB with same name and path of item being published.
                Item targetItem = context.PublishOptions.TargetDatabase.GetItem(sourceItem.Paths.Path);

                if (targetItem != null && targetItem.ID != sourceItem.ID)
                {
					// If found.. delete item from target database.
                    context.PublishHelper.DeleteTargetItem(targetItem.ID);
                    Log.Info("Deleted duplicate item: " + targetItem.Paths.Path, this);
                }
            }
        }
    }
}