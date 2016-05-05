using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SitecoreCookbook.Models
{
    public class Product
    {
        public string Title { get; set; }
        public string Price {get;set;}
        public string ReleaseDate { get; set; }
    }
}
