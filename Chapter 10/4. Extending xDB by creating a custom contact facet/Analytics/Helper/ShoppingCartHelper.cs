using Sitecore.Analytics;
using Sitecore.Analytics.Tracking;
using SitecoreCookbook.Analytics.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitecoreCookbook.Analytics.Model;

namespace SitecoreCookbook.Analytics.Helper
{
    public class ShoppingCartHelper
    {
        public static void AddToCart(Guid productItemId, string productName, int quantity)
        {
            Contact contact = Tracker.Current.Contact;
            var shoppingCart = contact.GetFacet<IContactShoppingCart>("Shopping Cart");

            shoppingCart.LastUpdatedOn = DateTime.Now;

            IShoppingCartRecord cartRecord;
            if (!shoppingCart.Entries.Contains(productName))
                cartRecord = shoppingCart.Entries.Create(productName);
            else
                cartRecord = shoppingCart.Entries[productName];

            cartRecord.ProductItemId = productItemId;
            cartRecord.ProductName = productName;
            cartRecord.Quantity = quantity;
        }
    }
}
