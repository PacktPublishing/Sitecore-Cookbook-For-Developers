using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SitecoreCookbook.Search
{
    public class BookThumbnail : IComputedIndexField
    {
        private readonly XmlNode m_Configuration;
        public BookThumbnail(XmlNode p_Configuration)
        {
            m_Configuration = p_Configuration;
        }

        protected int MaxHeight
        {
            get
            {
                int maxHeight;
                if (!Int32.TryParse(GetConfigurationValue("maxHeight"), out maxHeight))
                {
                    maxHeight = 100;
                }
                return maxHeight;
            }
        }

        protected string GetConfigurationValue(string p_ConfigurationKey)
        {
            string configurationValue = null;
            if (m_Configuration != null && m_Configuration.Attributes != null)
            {
                XmlAttribute configurationAttribute = m_Configuration.Attributes[p_ConfigurationKey];
                if (configurationAttribute != null)
                {
                    configurationValue = configurationAttribute.Value;
                }
            }
            return configurationValue;
        }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;

            if (item != null && item.TemplateName == "Book")
            {
                ImageField thumb = item.Fields["Image"];
                if (thumb != null)
                {
                    Item mediaItem = thumb.MediaItem;
                    if (mediaItem != null)
                    {
                        MediaUrlOptions options = new MediaUrlOptions();
                        options.MaxHeight = MaxHeight;
                        return Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem, options);
                    }
                }
            }
            return null;
        }

        public string GetBookThumbnail(Item book)
        {
            
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