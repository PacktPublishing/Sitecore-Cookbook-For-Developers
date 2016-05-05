using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace SitecoreCookbook.Commands
{
    public class GetChildCount : Command
    {
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                // Get currently selected item in Content Editor tree
                Item currentItem = context.Items[0];

                // To read parameters passed in message to the command
                //context.Parameters["id"];

                SheerResponse.Alert(string.Format("Children count: {0}", currentItem.Children.Count));
            }
        }
        public override CommandState QueryState(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                Item currentItem = context.Items[0];

                // Hide command button when item has no child.
                if (currentItem.Children.Count == 0)
                    return CommandState.Hidden;
            }
            return base.QueryState(context);
        }
    }
}