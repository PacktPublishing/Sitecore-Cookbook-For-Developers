using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace SitecoreCookbook.Search
{
    class ProductName : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
            {
                return null;
            }

            string ProductName = null;
            if (item.TemplateName == "Product")
            {
                if (!string.IsNullOrEmpty(item["Company"]))
                {
                    ID id = new ID(item["Company"]);
                    Item companyItem = item.Database.GetItem(id);
                    ProductName = companyItem.Name;

                    if (!string.IsNullOrEmpty(item["title"]))
                        ProductName += " " + item["title"];
                }

            }
            return ProductName;
        }
    }
}
