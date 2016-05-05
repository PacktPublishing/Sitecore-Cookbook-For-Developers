using Sitecore.SecurityModel;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace SitecoreCookbook.Commands
{
    public class UnlockDisableSecurity : Command
    {
        public override void Execute(CommandContext context)
        {
            using (new SecurityDisabler())
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
