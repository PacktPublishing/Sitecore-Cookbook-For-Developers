using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace SitecoreCookbook.Search
{
    public class BookCategory : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item != null && item.TemplateName == "Book")
            {
                MultilistField categories = item.Fields["Categories"];
                List<string> list = new List<string>();
				if(categories != null)
				{
					foreach (ID id in categories.TargetIDs)
					{
						Item category = item.Database.GetItem(id);
						if(category!=null)
							list.Add(category.DisplayName);
					}
					return list;
				}
            }
            return null;
        }

        //public object ComputeFieldValue(IIndexable indexable)
        //{
        //    Item item = indexable as SitecoreIndexableItem;
        //    if (item == null)
        //        return null;
        //    if (item.TemplateName != "Book Category")
        //        return null;
        //    return GetBooks(item);
        //}

        //private IEnumerable<string> GetBooks(Item BookCategoryItem)
        //{
        //    return (from link in Globals.LinkDatabase.GetItemReferrers(BookCategoryItem, false)
        //            let sourceItem = link.GetSourceItem()
        //            where sourceItem != null
        //            where sourceItem.TemplateName == "Book"
        //            select sourceItem.Name).ToArray();
        //}

        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
