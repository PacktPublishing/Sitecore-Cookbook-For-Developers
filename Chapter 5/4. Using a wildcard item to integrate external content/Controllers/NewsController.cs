using Sitecore.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SitecoreCookbook.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult NewsListing()
        {
            string year = WebUtil.GetUrlName(0);
            string strSQL = "SELECT * FROM News WHERE year(NewsDate)=" + year;
            DataSet dataset = GetNewsFromDatabase(strSQL);

            return View(dataset.Tables[0].Rows);
        }

        
        public DataSet GetNewsFromDatabase(string strSQL)
        {
            string ConnectionString = "";
            DataSet dataset = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlDataAdapter da = new SqlDataAdapter(strSQL, con))
            {
                con.Open();
                da.Fill(dataset);
            }
            return dataset;
        }
    }
}
