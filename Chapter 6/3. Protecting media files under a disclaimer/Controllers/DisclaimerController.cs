using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SitecoreCookbook.Controllers
{
    public class DisclaimerController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Submit(string submitButton)
        {
            string MediaId = HttpContext.Session["ProtectedMedia"].ToString();
            string mediaUrl = HttpContext.Session["ProtectedMediaUrl"].ToString();

            if (submitButton == "Accept")
                HttpContext.Session["ProtectedMedia" + MediaId] = "1";
            else
                HttpContext.Session["ProtectedMedia" + MediaId] = null;
            return Redirect(mediaUrl);
        }
    }
}
