using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace SitecoreCookbook.Search
{
    public class Url : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if(item!= null)
                return LinkManager.GetItemUrl(item);
            return null;
        }

        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}