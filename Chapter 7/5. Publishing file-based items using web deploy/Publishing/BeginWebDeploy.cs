using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Publishing
{
    public class BeginWebDeploy : Sitecore.Publishing.WebDeploy.PublishHandler
    {
		// Actual path of layouts folder
        string siteRoot = @"D:\Sitecore\Website";
		
		// Path from where Web Deploy will sync layouts
        string deployFolder = @"D:\Sitecore\FilesToPublish";
		
		// Sublayout Item Path
        string sublayoutPath = "/sitecore/layout/Sublayouts";

        protected void PublishSublayouts(object Sender, EventArgs args)
        {
            Publisher publisher = ((Publisher)(((SitecoreEventArgs)(args)).Parameters[0]));
            Item rootItem = publisher.Options.RootItem;
            if (rootItem.Paths.Path.IndexOf(sublayoutPath) >= 0)
            {
                string sourceFile = siteRoot + rootItem["Path"];
                string targetFile = deployFolder + rootItem["Path"];
                string directory = Path.GetDirectoryName(targetFile);

				// Create directory and put publishing files to into FilesToPublish 
				// so that web deploy will sync this folder with CD server
                Directory.CreateDirectory(directory);
                File.Copy(sourceFile, targetFile, true);

				//  Sitecore will invoke Web Depoloy to sync folders between CM and CD
                base.OnPublish(Sender, args);
            }
        }
    }
}
