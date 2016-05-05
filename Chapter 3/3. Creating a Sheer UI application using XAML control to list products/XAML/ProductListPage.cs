using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using System;

namespace SitecoreCookbook.XAML
{
    public class ProductListPage : DialogForm
    {
        protected GridPanel Viewer;
        protected Button btnDelete;
        protected Listview ProductList;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Context.ClientPage.IsEvent)
            {
                LoadProducts();
                this.OK.Visible = false;
                this.Cancel.Value = "Close";
            }
        }

        private void LoadProducts()
        {
            string ProductsPath = "/sitecore/content/Home/Products/Phones";
            Item products = Factory.GetDatabase("master").GetItem(ProductsPath);
            foreach (Item product in products.Children)
            {
                ListviewItem productItem = new ListviewItem();
                Context.ClientPage.AddControl(ProductList, productItem);
                productItem.ID = Control.GetUniqueID("I");
                productItem.Header = product["Title"];
                productItem.ColumnValues["Id"] = product.ID.ToString();
                productItem.ColumnValues["Title"] = product["Title"];
                productItem.ColumnValues["Price"] = product["Price"];
            }
        }

        // Delete select products using event of method
        protected void DeleteProducts()
        {
            if (ProductList.SelectedItems.Length > 0)
            {
                foreach (ListviewItem productItem in ProductList.SelectedItems)
                {
                    Item product = Factory.GetDatabase("master").GetItem(new ID(productItem.ColumnValues["Id"].ToString()));
                    product.Recycle();
                }

                ProductList.Controls.Clear();
                LoadProducts();
                ProductList.Refresh();

                SheerResponse.Alert("Selected products are deleted!");
            }
            else
                SheerResponse.Alert("No product selected!");
        }

        // Delete selected products using Message
        public override void HandleMessage(Message message)
        {
            if (message.Name == "product:delete")
                DeleteProducts();
        }

        // You can also use below method using HandleMessage atribute instead of above one.
        //[HandleMessage("product:delete")]
        //public void DeleteSelectedProducts(Message message)
        //{
        //    DeleteProducts();
        //}

    }
}