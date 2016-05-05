using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SitecoreCookbook.Tasks
{
    public class ResetLayoutDetailsJob
    {
        private string _jobName = "Reset Layout Details Job";
        public Job Job
        {
            get { return JobManager.GetJob(_jobName); }
        }

        public void Run(Item rootItem)
        {
            JobOptions options = new JobOptions(_jobName,
                        "Reset Layout", Context.Site.Name, this, "ResetLayoutDetails",
                        new object[] { rootItem })
            {
                EnableSecurity = true,
                ContextUser = Sitecore.Context.User,
                Priority = ThreadPriority.AboveNormal
            };
            JobManager.Start(options);
        }

        private void ResetLayoutDetails(Item rootItem)
        {
            if (Job != null && rootItem != null)
            {
                List<Item> itemList = rootItem.Axes.GetDescendants().ToList();
                itemList.Add(rootItem);

                Job.Status.Total = itemList.Count;
                Job.Status.State = JobState.Running;

                foreach (Item item in itemList)
                {
                    using (new EditContext(item))
                    {
                        item.Fields["__renderings"].Reset();
                    }
                    Job.Status.Processed++;
                }

                Job.Status.State = JobState.Finished;
            }
        }
    }
}