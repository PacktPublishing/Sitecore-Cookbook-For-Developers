using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Tasks;
using Sitecore.Web.UI.WebControls;
using SitecoreCookbook.Fields;
using SitecoreCookbook.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SitecoreCookbook.Test
{
    public partial class TestDateTimezone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Item currentItem = Sitecore.Context.Database.GetItem("{8AB49317-DECD-433A-B4F6-261670F2A2C1}");
			
			if(currentItem == null)
			{
				Response.Write("To view this test page, select valid item, which have <b>DateTimeWithTimezone</b> field named 'Event Time'");
				return;
			}

            DateTimeWithTimeZoneField field = new DateTimeWithTimeZoneField(currentItem.Fields["Event Time"]);
            Response.Write("Value using Mapped field : " + 
                field.DateTime.ToString());

            Response.Write("<hr/>Value using Field Renderer : " + 
                FieldRenderer.Render(currentItem, "Event Time", "format=MM/dd/yyyy HH:mm:ss"));
        }
    }
}