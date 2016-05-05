using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreCookbook.Publishing
{
    public class SendEmail
    {
        public void OnPublishComplete(object sender, EventArgs args)
        {
            SitecoreEventArgs pubArgs = (SitecoreEventArgs)args;
            var optionsList = Event.ExtractParameter(pubArgs, 0) as IEnumerable<DistributedPublishOptions>;
            DistributedPublishOptions options = optionsList.First();

            Database database = Factory.GetDatabase(options.SourceDatabaseName);
            string ItemPath = string.Empty, Languages = string.Empty;
            string ItemId = options.RootItemId.ToString();
            Item item = database.GetItem(new ID(ItemId));
            if (item != null)
                return;
            ItemPath = item.Paths.Path;

            foreach (DistributedPublishOptions opts in optionsList)
            {
                Languages += opts.LanguageName + ",";
            }

            SendPublishCompletionEmail(Context.User.Name, Context.User.Profile.Email, ItemPath, Languages, options.Deep, options.PublishRelatedItems, options.PublishDate);
        }
        public void SendPublishCompletionEmail(string UserName, string Email, string ItemPath, string Languages, bool SubItems, bool RelatedItems, DateTime PublishDate)
        {

        }
    }
}
