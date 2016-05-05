using Sitecore.Security.Accounts;
using Sitecore.SecurityModel;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace SitecoreCookbook.Commands
{
    public class UnlockImpersonate : Command
    {
        string UnlockUsingUser = @"sitecore\admin";
        public override void Execute(CommandContext context)
        {
            using (new UserSwitcher(UnlockUsingUser, true))
            {
                if (context.Items[0].Locking.CanUnlock())
                {
                    context.Items[0].Locking.Unlock();
                    SheerResponse.Alert("Item unlocked", true);
                }
            }
        }
    }
}
