using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

// Between these two classes, the MVC framework has provided the basics for generating and consuming the complete Identity Account database.
// However, the basic classes are extensible, so we can tailor things to suit our needs.

// Lastly, note the AccountViewModels.cs file. Here are defined the ViewModels which are actually used by the Views in our application, 
// such that only that information needed to populate a view and perform whatever actions need to be performed is exposed to the 
// public-facing web. View Models are not only an effective design component from an architectural standpoint, 
// but also prevent exposing more data than necessary.
namespace AspNetIdentity
{


    // This is the Entity Framework context used to manage interaction between our application and the 
    // database where our Account data is persisted (which may, or may not be the same database that will 
    // be used by the rest of our application). Important to note that this class inherits not from DBContext 
    // (as is the usual case with EF), but instead from IdentityDbContext. In other words, 
    // ApplicationDbContext inherits from a pre-defined DB context defined as part of 
    // Microsoft.AspNet.Identity.EntityFramework which contains the "Code-First" 
    // base classes for the Identity system.

    public class SecurityContext : IdentityDbContext<ApplicationUser>//, IDbContext//, IDbContextFactory<>
    {
        public SecurityContext()
            : base("ClientManagement")
        {
        }
    }
}