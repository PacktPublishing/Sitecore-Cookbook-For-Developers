using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Model
{
    public interface IContactShoppingCart : IFacet, IElement, IValidatable
    {
        IElementDictionary<IShoppingCartRecord> Entries { get; }
        DateTime LastUpdatedOn { get; set; }
    }
}
