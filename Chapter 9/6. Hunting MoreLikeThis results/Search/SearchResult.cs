using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Search
{
    public class SearchResult: SearchResultItem
    {
        private string _desc = string.Empty;

        [IndexField("title")]
        public string Title { get; set; }

        [IndexField("description")]
        public string Description
        {
            get { return _desc; }
            set
            {
                if(value!=null)
                    _desc = value.Substring(0, value.Length >= 50 ? 50 : value.Length);
            }
        }

        [IndexField("_group")]
        public string ID { get; set; }

        public SearchResult() { }
        public SearchResult(string title, string desc, string itemid)
        {
            Title = title;
            Description = desc;
            ID = itemid;
        }
    }
}
