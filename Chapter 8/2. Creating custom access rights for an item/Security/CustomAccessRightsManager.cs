using Sitecore.Data.Items;
using Sitecore.Security.AccessControl;

namespace SitecoreCookbook.Security
{
    public class CustomAccessRightsManager
    {
        string publishAccessRightName = "item:publish";
        public Item Item { get; private set; }

        public CustomAccessRightsManager(Item item)
        {
            this.Item = item;
        }

        public virtual bool IsPublishAllowed
        {
            get
            {
                var right = AccessRight.FromName(publishAccessRightName);
                if (right == null)
                    return false;
                var allowed = AuthorizationManager.IsAllowed(this.Item, right, Sitecore.Context.User);
                return allowed;
            }
        }

    }
}