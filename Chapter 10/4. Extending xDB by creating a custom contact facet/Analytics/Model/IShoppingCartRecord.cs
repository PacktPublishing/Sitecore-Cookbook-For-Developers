using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreCookbook.Analytics.Model
{
    public interface IShoppingCartRecord : IElement, IValidatable
    {
        string ProductName { get; set; }
        Guid ProductItemId { get; set; }
        int Quantity { get; set; }
    }
}
