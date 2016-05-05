using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.RenderField;
using Sitecore.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace SitecoreCookbook.Pipelines
{
    public class ReplaceRTETokens
    {
        public void Process(RenderFieldArgs args)
        {
            string TOKEN_PATTERN = @"<token[\s\S]*?/token>";
            Regex rgx = new Regex(TOKEN_PATTERN);
            if (args.FieldTypeKey == "rich text")
            {
                string fieldValue = args.FieldValue;
                foreach (Match match in rgx.Matches(fieldValue))
                {
                    fieldValue = ReplaceTokens(fieldValue, match, args);
                }
                args.Result.FirstPart = fieldValue;
            }
        }

        private static string ReplaceTokens(string fieldValue, Match match, RenderFieldArgs args)
        {
            XElement xElement = XElement.Parse(match.ToString());
            if (xElement.Attribute("field") != null
                && xElement.Attribute("item") != null)
            {
                string item = xElement.Attribute("item").Value;
                string field = xElement.Attribute("field").Value;

                Item tokenItem = Context.Database.GetItem(item);
                
                // Check the token rendering does not go in infinite loop
                if (tokenItem != null && !(tokenItem.ID == args.Item.ID && string.Equals(field, args.FieldName,System.StringComparison.OrdinalIgnoreCase)))
                    fieldValue = fieldValue.Replace(match.ToString(), FieldRenderer.Render(tokenItem, field));
            }
            return fieldValue;
        }
    }
}
