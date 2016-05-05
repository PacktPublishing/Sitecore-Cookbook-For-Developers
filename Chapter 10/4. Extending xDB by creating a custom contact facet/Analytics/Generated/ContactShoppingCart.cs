using Sitecore.Analytics.Model.Framework;
using SitecoreCookbook.Analytics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Generated
{
    [Serializable]
    public class ContactShoppingCart : Facet, IContactShoppingCart, IFacet, IElement, IValidatable
    {
        private const string LASTUPDATEDON = "Last Updated On";
        private const string ENTRIES = "Entries";

        public DateTime LastUpdatedOn
        {
            get
            {
                return base.GetAttribute<DateTime>(LASTUPDATEDON);
            }
            set
            {
                base.SetAttribute<DateTime>(LASTUPDATEDON, value);
            }
        }

        public Sitecore.Analytics.Model.Framework.IElementDictionary<IShoppingCartRecord> Entries
        {
            get
            {
                return base.GetDictionary<IShoppingCartRecord>(ENTRIES);
            }
        }

        public ContactShoppingCart()
        {
            base.EnsureAttribute<DateTime>(LASTUPDATEDON);
            base.EnsureDictionary<IShoppingCartRecord>(ENTRIES);
        }
    }
}