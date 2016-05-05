using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using SitecoreCookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreCookbook.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult Carousel()
        {
            List<CarouselSlide> slides = new List<CarouselSlide>();
            MultilistField multilistField = Sitecore.Context.Item.Fields["Carousel Slides"];

            if (multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach (Item item in carouselItems)
                {
                    slides.Add(new CarouselSlide(item));
                }
            }
            return View(slides);
        }

        public ActionResult LanguageSwitcher()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            LanguageCollection langColl = LanguageManager.GetLanguages(Sitecore.Context.Database);

            foreach (Language language in langColl)
            {
                string url = GetItemUrl(Context.Item, language);
                list.Add(language.CultureInfo.DisplayName, url);
            }
            return View(list);
        }

        private static string GetItemUrl(Item item, Language language)
        {
            string url = LinkManager.GetItemUrl(item,
                new UrlOptions
                {
                    LanguageEmbedding = LanguageEmbedding.Always,
                    LanguageLocation = LanguageLocation.FilePath,
                    Language = language
                }
            );
            return url;
        }
    }
}