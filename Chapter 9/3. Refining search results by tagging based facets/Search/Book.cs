using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System.Collections.Generic;

namespace SitecoreCookbook.Search
{
    public class Book : SearchResultItem
    {
        [IndexField("title")]
        public string Title { get; set; }

        [IndexField("author")]
        public string Author { get; set; }

        [IndexField("bookthumbnail")]
        public string Thumbnail { get; set; }

        [IndexField("itemurl")]
        public string BookUrl { get; set; }

        [IndexField("__semantics")]
        public string Tags { get; set; }
    }
}