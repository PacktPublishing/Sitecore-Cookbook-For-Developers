using Sitecore.ContentSearch;
using System.Collections.Generic;

namespace SitecoreCookbook.Search
{
    public class Book: SearchResultItem
    {
        [IndexField("title")]
        public string Title{get;set;}

        [IndexField("author")]
        public string Author { get; set; }
        
        [IndexField("bookthumbnail")]
        public string Thumbnail { get; set; }

        [IndexField("itemurl")]
        public string BookUrl { get; set; }

        [IndexField("price")]
        public int Price { get; set; }

        [IndexField("bookcategories")]
        public IEnumerable<string> Categories{get;set;}
    }
}