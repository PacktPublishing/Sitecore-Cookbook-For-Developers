using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using Sitecore.Workflows;
using Sitecore.Workflows.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SitecoreCookbook.Workflow
{
    public class RejectEmailAction
    {
        public void Process(WorkflowPipelineArgs args)
        {
            User editorUser = GetEditorUser(args);
            if (editorUser != null)
            {
                User reviewerUser = Sitecore.Context.User;

                Item innerItem = args.ProcessorItem.InnerItem;
                string subject = innerItem["subject"];
                string message = innerItem["message"];

                string comment = args.CommentFields["Comments"];
                message = message.Replace("$to$", editorUser.Profile.FullName)
                 .Replace("$from$", reviewerUser.Profile.FullName)
                 .Replace("$itempath$", args.DataItem.Paths.Path)
                 .Replace("$itemlanguage$", args.DataItem.Language.Name)
                 .Replace("$itemversion$", args.DataItem.Version.ToString())
                 .Replace("$comments$", comment);

                SendEmail(reviewerUser.Profile.Email, editorUser.Profile.Email, subject, message);
            }
        }


        private User GetEditorUser(WorkflowPipelineArgs args)
        {
            Item item = args.DataItem;
            IWorkflow itemWorkflow = item.Database.WorkflowProvider.GetWorkflow(item);
            WorkflowEvent[] workflowHistory = itemWorkflow.GetHistory(item);

            if (workflowHistory.Any())
            {
                string userName = workflowHistory.Last().User;
                return User.FromName(userName, false);
            }
            return null;
        }


        private static void SendEmail(string from, string to, string subject, string message)
        {
            MailMessage email = new MailMessage(from, to);
            try
            {
                email.Subject = subject;
                email.Body = message;
                new SmtpClient().Send(email);
            }
            catch
            { }
        }
    }
}