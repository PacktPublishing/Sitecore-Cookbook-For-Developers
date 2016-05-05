using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace SitecoreCookbook.Gutters
{
    public class PublishGutter : GutterRenderer 
    {
        enum PublishStatus
        {
            Published, NeverPublished, Modified
        }
        private PublishStatus CheckPublishStatus(Item currentItem)
        {
            // Read current item from web database
            Database webDB = Factory.GetDatabase("web");
            Item webItem = webDB.GetItem(currentItem.ID);

            // item in web database is null, means item not found in web
            if (webItem == null)
                return PublishStatus.NeverPublished;

            // Revision field gets changed on each modification, so if this field is different on both databases, means item is modified after publish.
            if (currentItem["__Revision"] != webItem["__Revision"])
                return PublishStatus.Modified;

            return PublishStatus.Published;
        }

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            PublishStatus publishStatus = CheckPublishStatus(item);
            if (publishStatus != PublishStatus.Published)
            {
                GutterIconDescriptor desc = new GutterIconDescriptor();
                if (publishStatus == PublishStatus.NeverPublished)
                {
                    desc.Icon = "Core2/32x32/flag_red_h.png";
                    desc.Tooltip = "Item never published!";
                }
                else
                {
                    desc.Icon = "Core2/32x32/flag_yellow.png";
                    desc.Tooltip = "Item published but modified!";
                }

                // Clicking on the Gutter icon will you will jump to that item directly, that's done using "item:load(id={item id})"
                desc.Click = string.Format("item:load(id={0})", item.ID);

                return desc;
            }
            return null;
        }
    }
}
