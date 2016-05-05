using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using System;

namespace SitecoreCookbook.Analytics.Helper
{
    public class ContactHelper
    {
        public static void SaveContact(string userName, string firstName, string lastName, DateTime birthDate, string emailAddress, string workEmailAddress)
        {
            Contact contact = Tracker.Current.Contact;

            contact.Identifiers.AuthenticationLevel = AuthenticationLevel.PasswordValidated;
            Tracker.Current.Session.Identify(userName);

            IContactPersonalInfo personal = contact.GetFacet<IContactPersonalInfo>("Personal");
            personal.FirstName = firstName;
            personal.Surname = lastName;
            personal.BirthDate = birthDate;

            // Storing multiple email address
            IContactEmailAddresses email = contact.GetFacet<IContactEmailAddresses>("Emails");
            var personalEmail = email.Entries.Create("personal");
            personalEmail.SmtpAddress = emailAddress;

            var workEmail = email.Entries.Create("work");
            workEmail.SmtpAddress = emailAddress;

            email.Preferred = "personal";

            // Storing multiple Addresses
            IContactAddresses address = contact.GetFacet<IContactAddresses>("Addresses");
            IAddress homeAddress = address.Entries.Create("home");
            homeAddress.StreetLine1 = "Address 1 - Home";
            homeAddress.City = "New York";
            homeAddress.StateProvince = "NY";
            homeAddress.PostalCode = "03587";

            IAddress officeAddress = address.Entries.Create("office");
            officeAddress.StreetLine1 = "Address 1 - Office";
            officeAddress.City = "New York";
            officeAddress.StateProvince = "NY";
            officeAddress.PostalCode = "03587";

            address.Preferred = "home";
         
			// Add custom details in System Facets - Tag and Extensions
			contact.Tags.Add("Username", userName);
            contact.Tags.Add("Full name", firstName + " " + lastName);

            contact.Extensions.SimpleValues["Username"] = userName;
            contact.Extensions.SimpleValues["Full Name"] = firstName + " " + lastName;
		 
        }
    }
}
