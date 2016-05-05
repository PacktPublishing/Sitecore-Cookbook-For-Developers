using Sitecore.Links;

namespace SitecoreCookbook.Providers
{
    public class MultisiteLinkProvider2 : LinkProvider
    {
        public override UrlOptions GetDefaultUrlOptions()
        {
            UrlOptions urlOptions = new UrlOptions();
            urlOptions.AddAspxExtension = (Sitecore.Context.Site.Properties["addAspxExtension"] == "true");

            if (Sitecore.Context.Site.Properties["embedLanguage"] == "always")
                urlOptions.LanguageEmbedding = Sitecore.Links.LanguageEmbedding.Always;
            else if (Sitecore.Context.Site.Properties["embedLanguage"] == "never")
                urlOptions.LanguageEmbedding = Sitecore.Links.LanguageEmbedding.Never;
            else if (Sitecore.Context.Site.Properties["embedLanguage"] == "asneeded")
                urlOptions.LanguageEmbedding = Sitecore.Links.LanguageEmbedding.AsNeeded;

            if (Sitecore.Context.Site.Properties["languageLocation"] == "filePath")
                urlOptions.LanguageLocation = Sitecore.Links.LanguageLocation.FilePath;
            else
                urlOptions.LanguageLocation = Sitecore.Links.LanguageLocation.QueryString;


            urlOptions.LowercaseUrls = (Sitecore.Context.Site.Properties["lowercaseUrls"] == "true");
            urlOptions.ShortenUrls = (Sitecore.Context.Site.Properties["shortenUrls"] == "true");
            urlOptions.UseDisplayName = (Sitecore.Context.Site.Properties["useDisplayName"] == "true");
            urlOptions.EncodeNames = (Sitecore.Context.Site.Properties["encodeNames"] == "true");
            urlOptions.AlwaysIncludeServerUrl = (Sitecore.Context.Site.Properties["alwaysIncludeServerUrl"] == "true");

            return urlOptions;
        }
    }
}
