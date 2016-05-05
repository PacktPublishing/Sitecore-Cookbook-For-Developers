using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SitecoreCookbook.Controller
{
    public class ProductController : Controller
    {
        public ActionResult GetProduct()
        {
            string productItemId = WebUtil.GetQueryString("id");
            Database db = Sitecore.Configuration.Factory.GetDatabase("master");
            Item item = db.GetItem(new ID(productItemId));


            Product product = new Product();
            product.Title = item["Title"];
            product.Price = item["Price"];
            product.ReleaseDate = item["Release Date"].Replace("Z", "") ;
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}
