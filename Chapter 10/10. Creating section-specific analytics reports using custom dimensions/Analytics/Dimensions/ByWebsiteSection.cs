using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Schema;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using Sitecore.ExperienceAnalytics.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Dimensions
{
    public class ByWebsiteSection : DimensionBase
    {
        public ByWebsiteSection(Guid dimensionId)
            : base(dimensionId)
        {
        }

        public override IEnumerable<DimensionData> GetData(IVisitAggregationContext context)
        {
            SegmentMetricsValue metrics = this.CalculateCommonMetrics(context, 0);
            ConcurrentDictionary<string, int> keyCount = this.GetDimensionKeys(context);
            foreach (string index in (IEnumerable<string>)keyCount.Keys)
            {
                int count = keyCount[index];
                SegmentMetricsValue metricsValue = metrics.Clone();
                metricsValue.Count = count;
                yield return new DimensionData()
                {
                    DimensionKey = index,
                    MetricsValue = metricsValue
                };
            }
        }

        public ConcurrentDictionary<string, int> GetDimensionKeys(IVisitAggregationContext context)
        {
            ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>();
            foreach (PageData page in context.Visit.Pages)
            {
                string url = page.Url.ToString();
                string[] urlSegments = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (urlSegments.Length > 0 && urlSegments[0].IndexOf("?") < 0)
                {
                    string dimKey = urlSegments[0];
                    concurrentDictionary.AddOrUpdate(dimKey, 1, (Func<string, int, int>)((key, oldValue) => oldValue + 1));
                }
            }
            return concurrentDictionary;
        }
    }
}

//internal class ByCity : GeoDimensionBase
//{
//    public ByCity(Guid dimensionId)
//        : base(dimensionId)
//    {
//    }

//    public override IEnumerable<DimensionData> GetData(IVisitAggregationContext context)
//    {
//        Assert.ArgumentNotNull((object)context, "context");
//        Assert.IsNotNull((object)context.Visit, "visit");
//        if (context.Visit.GeoData != null && !string.IsNullOrEmpty(context.Visit.GeoData.City))
//        {
//            string dimensionKey = new HierarchicalKeyBuilder().Add(this.ResolveGeoKey(context.Visit.GeoData.Country)).Add(this.ResolveGeoKey(context.Visit.GeoData.Region)).Add(this.ResolveGeoKey(context.Visit.GeoData.City)).ToString();
//            yield return new DimensionData()
//            {
//                DimensionKey = dimensionKey,
//                MetricsValue = this.CalculateCommonMetrics(context, 0)
//            };
//        }
//    }
//}

//public ByBrowserVersion(Guid dimensionId)
//    : base(dimensionId)
//{
//}

//protected override bool HasDimensionKey(IVisitAggregationContext context)
//{
//    return (context.Visit.Pages.Count>0);
//    //return (!string.IsNullOrEmpty(browser.BrowserMajorName) && !string.IsNullOrEmpty(browser.BrowserVersion)); 
//}

//public override IEnumerable<Sitecore.ExperienceAnalytics.Aggregation.Data.Model.DimensionData> GetData(IVisitAggregationContext context)
//{
//    return base.GetData(context);
//}
