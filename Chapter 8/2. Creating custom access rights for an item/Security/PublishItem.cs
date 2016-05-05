using Sitecore.Shell.Framework.Commands;
using SitecoreCookbook.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Security
{
    public class PublishItem : Sitecore.Shell.Framework.Commands.PublishItem
    {
        public override CommandState QueryState(CommandContext context)
        {
            if (context.Items.Length != 1)
            {
                return CommandState.Hidden;
            }

            CustomAccessRightsManager manager = new CustomAccessRightsManager(context.Items[0]);
            if(manager.IsPublishAllowed)
                return base.QueryState(context);
            else
                return CommandState.Hidden;
        }

    }
}
