using SitecoreCookbook.Analytics.Helper;
using System.Web.Mvc;

namespace SitecoreCookbook.Analytics.Controllers
{
    public class PlacesController : Controller
    {
        public ViewResult ShowNearbyPlaces()
        {
            var Places = PlacesHelper.GetNearbyPlaces();
            return View(Places);
        }
    }
}
