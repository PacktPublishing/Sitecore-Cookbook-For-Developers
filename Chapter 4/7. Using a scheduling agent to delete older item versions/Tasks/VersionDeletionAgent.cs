using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace SitecoreCookbook.Tasks
{
    public class VersionDeletionAgent
    {
        public string RootItem { get; set; }
        public string DatabaseName { get; set; }
        public int MaxVersions { get; set; }

        public void Run()
        {
            Database database = Factory.GetDatabase(DatabaseName);
            Item item = database.GetItem(RootItem);
            if (item != null)
            {
                Item[] items = item.Axes.GetDescendants();
                foreach (Item child in items)
                {
                    foreach (Language language in child.Languages)
                    {
                        Item langItem = database.GetItem(child.Paths.Path, language);
                        if (langItem.Versions.Count > MaxVersions)
                            DeleteOlderVersions(langItem);
                    }
                }
            }
        }

        private void DeleteOlderVersions(Item item)
        {
            Sitecore.Data.Version[] versions = item.Versions.GetVersionNumbers();
            for (int i = 0; i < versions.Length - MaxVersions; i++)
            {
                Item itemToDelete = item.Database.GetItem(item.Paths.Path, item.Language, versions[i]);
                itemToDelete.Versions[versions[i]].RecycleVersion();
            }
        }
    }
}