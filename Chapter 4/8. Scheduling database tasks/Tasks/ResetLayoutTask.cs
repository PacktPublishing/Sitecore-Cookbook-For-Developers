using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Jobs;
using Sitecore.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SitecoreCookbook.Tasks
{
    public class ResetLayoutTask
    {
        public void Execute(Item[] items, CommandItem command, ScheduleItem schedule)
        {
            foreach(Item rootItem in items)
            {
                ResetLayoutDetails(rootItem);
            }
        }
        private void ResetLayoutDetails(Item rootItem)
        {
            if (rootItem != null)
            {
                List<Item> itemList = rootItem.Axes.GetDescendants().ToList();
                itemList.Add(rootItem);

                foreach (Item item in itemList)
                {
                    using (new EditContext(item))
                    {
                        item.Fields["__renderings"].Reset();
                    }
                }
            }
        }
    }
}