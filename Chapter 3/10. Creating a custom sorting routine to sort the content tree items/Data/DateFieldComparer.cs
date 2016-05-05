using Sitecore;
using Sitecore.Data.Comparers;
using Sitecore.Data.Items;
using System;

namespace SitecoreCookbook.Data
{
    public class DateFieldComparer : ExtractedKeysComparer
    {
        static string DateField = "News Date";
        protected override int DoCompare(Item item1, Item item2)
        {
            DateTime date1 = GetDateTime(item1);
            DateTime date2 = GetDateTime(item2);
            
            return date1.CompareTo(date2);
        }

        private DateTime GetDateTime(Item item)
        {
            return DateUtil.ParseDateTime(item[DateField], DateTime.MinValue);
        }

        public override IKey ExtractKey(Item item)
        {
            return (IKey)new KeyObj()
            {
                Item = item,
                Key = (object)GetDateTime(item)
            };
        }

        protected override int CompareKeys(IKey key1, IKey key2)
        {
            return ((DateTime)key1.Key).CompareTo((DateTime)key2.Key);
        }
    }

}
