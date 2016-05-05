using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Search
{
    public class Product: SearchResultItem
    {
        [IndexField("productname")]
        public string ProductName { get; set; }

        [IndexField("description")]
        public string Description { get; set; }
    }
}
