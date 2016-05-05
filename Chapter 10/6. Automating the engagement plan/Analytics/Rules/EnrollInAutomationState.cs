using Sitecore.Rules;
using Sitecore.Rules.Actions;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using Sitecore.Analytics.Automation;
using Sitecore.Analytics.Automation.Data;
using Sitecore.Analytics;

namespace SitecoreCookbook.Analytics.Rules
{
    public class EnrollInAutomationState<T> : RuleAction<T> where T : RuleContext
    {
        public string StateId { get; set; }
        public override void Apply([NotNull] T ruleContext)
        {
            var state = Context.Database.GetItem(new ID(StateId));
            if (state != null && state.Template.Key == "engagement plan state")
            {
                var a = AutomationStateManager.Create(Tracker.Current.Contact);
                a.EnrollInEngagementPlan(state.ParentID, state.ID);
            }
        }
    }
}