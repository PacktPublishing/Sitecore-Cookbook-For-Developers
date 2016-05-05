using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Pipelines.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SitecoreCookbook.Media
{
    public class UploadRestrictions : UploadProcessor
    {
        private List<string> restrictedContentType = new List<string>();
        private List<string> restrictedExtensions = new List<string>();

        protected virtual void AddRestrictedContentType(XmlNode configNode)
        {
            if (configNode == null || string.IsNullOrEmpty(configNode.InnerText))
            {
                return;
            }

            restrictedContentType.Add(configNode.InnerText);
        }
        protected virtual void AddRestrictedExtension(XmlNode configNode)
        {
            if (configNode == null || string.IsNullOrEmpty(configNode.InnerText))
                return;

            string[] extensions = configNode.InnerText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string extension in extensions)
                restrictedExtensions.Add("." + extension);
        }

        private bool IsRestrictedExtension(string extension)
        {
            return restrictedExtensions.Exists(ext => string.Equals(ext, extension, StringComparison.CurrentCultureIgnoreCase));
        }
        private bool IsRestrictedContentType(string contentType)
        {
            return restrictedContentType.Exists(type => string.Equals(type, contentType, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Process(UploadArgs args)
        {
            foreach (string fileKey in args.Files)
            {
                string fileName = args.Files[fileKey].FileName;
                string contentType = args.Files[fileKey].ContentType;
                string extension = Path.GetExtension(fileName);

                if (IsRestrictedExtension(extension) || IsRestrictedContentType(contentType))
                {
                    args.ErrorText = Translate.Text(string.Format("The file \"{0}\" cannot be uploaded", fileName));
                    Log.Error(args.ErrorText, this);
                    args.AbortPipeline();
                    break;
                }
            }
        }

    }
}
