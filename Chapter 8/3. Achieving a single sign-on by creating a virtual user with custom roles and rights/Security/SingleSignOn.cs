using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Security;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Security
{
    public class SingleSignOn
    {
        public void LoginWithVirtualUser(string UserName, string fullName, string emailAddress)
        {
            string[] roles = new string[] {
                @"sitecore\Sitecore Client Users", 
                @"external\Preview User"};

            string[] revokedItems = new string[]{
                @"/sitecore/content/Home/Events",
                @"/sitecore/content/Home/News"};

            User user = AuthenticationManager.BuildVirtualUser(UserName, true);
            if (user != null)
            {
                AssignRoles(user, roles);
				RevokeItemAccessRights(user, revokedItems);
                SetProfile(user, fullName, emailAddress);
                AuthenticationManager.Login(user);
            }
        }

        private static void SetProfile(User user, string FullName, string EmailAddress)
        {
            UserProfile profile = user.Profile;
            profile.FullName = FullName;
            profile.Email = EmailAddress;
            profile.Save();
        }

        private static void AssignRoles(User user, string[] roles)
        {
            foreach (string role in roles)
            {
                if (Role.Exists(role))
                    user.Roles.Add(Role.FromName(role));
            }
        }

        private static void RevokeItemAccessRights(User user, string[] items)
        {
            Database database = Factory.GetDatabase("master");
            foreach (string itempath in items)
            {
                Item item = database.GetItem(itempath);
                if (item != null)
                {
                    AccessRuleCollection accessRules = item.Security.GetAccessRules();
                    accessRules.Helper.AddAccessPermission(user, AccessRight.ItemRead, PropagationType.Descendants, AccessPermission.Deny);
                }
            }
        }
    }
}
