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
    public class ShoppingCartRecord : Element, IShoppingCartRecord
    {
        private const string PRODUCTNAME = "Product Name";
        private const string PRODUCTITEMID = "Product Item Id";
        private const string QUANTITY = "Quantity";

        public ShoppingCartRecord()
        {
            base.EnsureAttribute<string>(PRODUCTNAME);
            base.EnsureAttribute<Guid>(PRODUCTITEMID);
            base.EnsureAttribute<int>(QUANTITY);
        }

        public string ProductName
        {
            get
            {
                return base.GetAttribute<string>(PRODUCTNAME);
            }
            set
            {
                base.SetAttribute<string>(PRODUCTNAME, value);
            }
        }

        public Guid ProductItemId
        {
            get
            {
                return base.GetAttribute<Guid>(PRODUCTITEMID);
            }
            set
            {
                base.SetAttribute<Guid>(PRODUCTITEMID, value);
            }
        }

        public int Quantity
        {
            get
            {
                return base.GetAttribute<int>(QUANTITY);
            }
            set
            {
                base.SetAttribute<int>(QUANTITY, value);
            }
        }
    }
}