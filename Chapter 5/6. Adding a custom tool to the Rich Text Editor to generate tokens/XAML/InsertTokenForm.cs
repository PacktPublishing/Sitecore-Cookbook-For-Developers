using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using System;

namespace SitecoreCookbook.XAML
{
    public class InsertTokenForm : DialogForm
    {
        protected Edit TextField;
        protected TreeviewEx TokenTree;
        protected DataContext RootItem;

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, "e");
            base.OnLoad(e);

            if (Context.ClientPage.IsEvent)
                return;

            this.RootItem.GetFromQueryString();
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull((object)args, "args");
            string tokenString = "<token item=\"{0}\" field=\"{1}\">{2}</token>";

            Item selectedItem = TokenTree.GetSelectionItem();
            string tokenText = selectedItem.DisplayName + ":" + TextField.Value;
            tokenString = string.Format(tokenString, selectedItem.Paths.Path, TextField.Value, tokenText);
            
            SheerResponse.SetDialogValue(tokenString);
            base.OnOK(sender, args);
        }
    }
}
