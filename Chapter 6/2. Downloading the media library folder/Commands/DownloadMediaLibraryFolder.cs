using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.IO;
using Sitecore.Resources.Media;
using Sitecore.Shell.Framework;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Zip;
using System;
using System.IO;

namespace SitecoreCookbook.Commands
{
    public class DownloadMediaLibraryFolder : Command
    {
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                Item currentItem = context.Items[0];
                DownloadMediaFolder(currentItem);
            }
            return;
        }
        public override CommandState QueryState(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                Item currentItem = context.Items[0];
                if (currentItem.Paths.IsMediaItem)
                    return CommandState.Enabled;
                else
                    return CommandState.Hidden;
            }
            return base.QueryState(context);
        }

        private void DownloadMediaFolder(Item item)
        {
            string folder = Settings.TempFolderPath + "/MediaDownload/" +
                item.Name + DateTime.Now.ToString("HHmmss");
            string zipFile = FileUtil.MapPath(folder + ".zip");

            if (!Directory.Exists(FileUtil.MapPath(folder)))
                Directory.CreateDirectory(FileUtil.MapPath(folder));

            ChildList images = item.GetChildren();
            using (ZipWriter zipWriter = new ZipWriter(zipFile))
            {
                foreach (Item image in images)
                    AddFileToZip(zipWriter, image);
            }

            Sitecore.Context.ClientPage.ClientResponse.Download(zipFile);
            // OR
            Files.Download(zipFile);
        }

        private static void AddFileToZip(ZipWriter zipWriter, Item image)
        {
            string fileName = image.Name + "." + image["extension"];
            MediaStream mediaStream = MediaManager.GetMedia(image).GetStream();
            if (mediaStream!=null)
                zipWriter.AddEntry(fileName, mediaStream.Stream);
        }
    }
}