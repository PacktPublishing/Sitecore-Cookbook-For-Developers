using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using SitecoreCookbook.Analytics.Models;
using System;

namespace SitecoreCookbook.Analytics.Rules
{
    public class IsProductInCart<T> : WhenCondition<T> where T : RuleContext
    {
        public string ProductId { get; set; }
        private Guid ProductGuid { get; set; }

        protected override bool Execute(T ruleContext)
        {
            if (Tracker.IsActive)
            {
                this.ProductGuid = new Guid(this.ProductId);

                Contact contact = Tracker.Current.Contact;
                var shoppingCart = contact.GetFacet<IContactShoppingCart>("Shopping Cart");
                foreach (var entry in shoppingCart.Entries.Keys)
                {
                    var cartItem = shoppingCart.Entries[entry];
                    return (cartItem.ProductItemId == this.ProductGuid);
                }
            }
            return false;
        }
    }
}
