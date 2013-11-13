using System.Data.Entity.Migrations;
using System.Security.Policy;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetIdentity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdentityManagement";
        }

        protected override void Seed(SecurityContext context)
        {
            AddUsers();
            AddUserAndRole();
        }

        private void AddUsers()
        {
            var manager = new UserManager<ApplicationUser>(
             new UserStore<ApplicationUser>(
                 new SecurityContext()));

            for (int i = 0; i < 4; i++)
            {
                var user = new ApplicationUser()
                {
                    UserName = string.Format("User{0}", i.ToString()),

                    // Add the following so our Seed data is complete:
                    FirstName = string.Format("FirstName{0}", i.ToString()),
                    LastName = string.Format("LastName{0}", i.ToString()),
                    Email = string.Format("Email{0}@Example.com", i.ToString()),
                    IsConfirmed = false
                };
                manager.Create(user, string.Format("Password{0}", i.ToString()));
            }
        }

        private void AddUserAndRole()
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(new SecurityContext()));
            ir = rm.Create(new IdentityRole("canView"));


            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new SecurityContext()));

            var user = new ApplicationUser()
            {
                UserName = "UserA",
                // Add the following so our Seed data is complete:
                FirstName = string.Format("FirstName{0}", "A"),
                LastName = string.Format("LastName{0}", "A"),
                Email = string.Format("Email{0}@Example.com", "A"),
                IsConfirmed = false
            };

            ir = um.Create(user, "Passw0rd1");
            if (ir.Succeeded == false)
                return;

            ir = um.AddToRole(user.Id, "canView");
        }
    }
}
