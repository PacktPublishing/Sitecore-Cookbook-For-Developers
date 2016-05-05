using Sitecore.Pipelines.RenderField;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml.Xsl;
using SitecoreCookbook.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Pipelines
{
    public class GetDateTimeZoneFieldValue
    {
        public void Process(RenderFieldArgs args)
        {
            string fieldTypeKey = args.FieldTypeKey;

            // Execute this code only for our custom field type
            if (fieldTypeKey != "datetime with timezone")
               return;

            // Use existing DateRenderer to render our custom date field
            DateRenderer renderer = new DateRenderer();
            renderer.Item = args.Item;
            renderer.FieldName = args.FieldName;

            // Use the DateTimeWithTimeZoneField class to get strongly typed value of date instead of raw value
            renderer.FieldValue = new DateTimeWithTimeZoneField(args.Item.Fields[args.FieldName]).ToString();
            renderer.Parameters = args.Parameters;

            // To allow passing format parameter to our field, same as DateTime field.
            if (!string.IsNullOrEmpty(args.Parameters["format"]))
                args.WebEditParameters["format"] = args.Parameters["format"];
         
            // Allow editing from Experience Editor as well
            args.DisableWebEditContentEditing = false;
            args.WebEditClick = "return Sitecore.WebEdit.editControl($JavascriptParameters,'webedit:editdate');";
            RenderFieldResult result = renderer.Render();
            args.Result.FirstPart = result.FirstPart;
            args.Result.LastPart = result.LastPart;
        }
    }
}