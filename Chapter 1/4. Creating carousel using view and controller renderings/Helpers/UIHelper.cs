using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Helpers
{
    public static class UIHelper
    {
        public static bool IsFirst(this IEnumerable<CustomItem> enumerable, CustomItem element)
        {
            return enumerable == null || enumerable.First().InnerItem.ID == element.InnerItem.ID;
        }
        public static int GetPosition(this IEnumerable<CustomItem> enumerable, CustomItem element)
        {
            return enumerable.Select((a, i) => (a.Equals(element)) ? i : -1).Max();
        }
    }
}