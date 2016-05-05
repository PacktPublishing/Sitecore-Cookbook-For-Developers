using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Dimensions
{
    public class ByBrowserVersion: VisitDimensionBase
    {
        public ByBrowserVersion(Guid dimensionId)
            : base(dimensionId)
        {
        }

        public override bool HasDimensionKey(IVisitAggregationContext context)
        {
            BrowserData browser = context.Visit.Browser;
            return (!string.IsNullOrEmpty(browser.BrowserMajorName) && !string.IsNullOrEmpty(browser.BrowserVersion)); 
        }

        public override string GetKey(IVisitAggregationContext context)
        {
            BrowserData browser = context.Visit.Browser;
            return browser.BrowserMajorName + "-" + browser.BrowserVersion;
        }
    }
}
