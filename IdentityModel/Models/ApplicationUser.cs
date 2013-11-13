using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetIdentity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser 
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    // This is the basic identity unit for managing individual accounts in the ASP.NET MVC 5 Account system. 
    // This class is empty as defined in the default project code, and so brings with it only those 
    // properties exposed by the base class IdentityUser. However, we can extend the ApplicationUser class 
    // by adding our own properties, which will then be reflected in the generated database.
    public class ApplicationUser : IdentityUser
    {
        // Added these three columns after the intial setup, this class comes default with nothing inside (no properties).
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        // These three are for adding email confirmation: http://kevin-junghans.blogspot.com/2013/10/adding-email-confirmation-to-aspnet.html
        public string ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public string PasswordResetToken { get; set; }

        // Add Company Name here, so one person can sign up with more than one company with the same username/email/password?
        // but, we won't allow/make them add the company, we'll infer this when they sign up by the url they're at
        public string CompanyId { get; set; }
    }
}