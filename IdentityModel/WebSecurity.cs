using System.Web;
using Core;

using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AspNetIdentity
{
    public class WebSecurity : IWebSecurity
    {
        private ISecurityRepository<ApplicationUser> Repository { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        //private UserManager<ApplicationUser> userManager
        //{
            //get { return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SecurityContext())); }
        //}

        public WebSecurity(
            ISecurityRepository<ApplicationUser> repository, 
            UserManager<ApplicationUser> userManager)
        {
            Repository = repository;
            UserManager = userManager;
        }

        public ApplicationUser GetUser(string username)
        {
            return Repository
                .Get(u => u.UserName == username)
                .SingleOrDefault();
        }

        public ApplicationUser GetCurrentUser()
        {
            return GetUser(CurrentUserName);
        }

        public void CreateUser(ApplicationUser user)
        {
            ApplicationUser dbUser = GetUser(user.UserName);
            if (dbUser != null)
                throw new Exception("User with that username already exists.");
            Repository.Insert(user);
            Repository.Save();
        }

        public bool FoundUser(string username)
        {
            return GetUser(username) != null;
        }

        public string GetEmail(string username)
        {
            string email = null;
            ApplicationUser user = GetUser(username);
            if (user != null)
                email = user.Email;
            return email;
        }

        public void Register()
        {
            //SecurityContext context = new SecurityContext();
            //context.Database.Initialize(true);
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = UserManager.Find(userName, password);
            if (user != null && user.IsConfirmed)
                return true;

            return false;
        }


        public bool Login(string userName, string password, bool persistCookie = false)
        {
            //UserManager<ApplicationUser> userManager = 
            //    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context: new AspNetIdentity.SecurityContext()));
            var user = UserManager.Find(userName, password);
            if (user != null && user.IsConfirmed)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = persistCookie }, identity);

            }
            else return false;

            return true;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            //UserManager<ApplicationUser> userManager = 
            //    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SecurityContext()));
            ApplicationUser user = UserManager.FindByName(userName);
            if (user == null)
                return false;
            var result = UserManager.ChangePassword(user.Id, oldPassword, newPassword);
            return result.Succeeded;
        }

        public bool ConfirmAccount(string accountConfirmationToken)
        {
            ApplicationUser user = 
                Repository
                .Get(u => u.ConfirmationToken == accountConfirmationToken)
                .SingleOrDefault();
            if (user != null)
            {
                user.IsConfirmed = true;
                Repository.Update(user);
                Repository.Save();
                return true;
            }
            return false;
        }

        public string CreateUserAndAccount(
            string userName, 
            string firstName, 
            string lastName, 
            string password, 
            string email, 
            bool requireConfirmationToken = false)
        {

            string token = null;
            if (requireConfirmationToken)
                token = ShortGuid.NewGuid();
            bool isConfirmed = !requireConfirmationToken;

            var user = new ApplicationUser()
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                ConfirmationToken = token,
                IsConfirmed = isConfirmed
            };
            var result = UserManager.Create(user, password);
            if (!result.Succeeded)
                return string.Empty;

            return token;
        }

        public string GetUserId(string userName)
        {
            ApplicationUser user = UserManager.FindByName(userName);
            if (user == null)
                return string.Empty;

            return user.Id;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext
                    .Current
                    .GetOwinContext()
                    .Authentication;
            }
        }

        public void Logout()
        {

            AuthenticationManager.SignOut();
        }

        public bool IsAuthenticated { get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; } }

        public bool IsConfirmed(string username)
        {
            ApplicationUser user = GetUser(username);
            if (user == null)
                return false;

            return user.IsConfirmed;
        }

        public string CurrentUserName { get { return Thread.CurrentPrincipal.Identity.GetUserName(); } }

        public string GetCurrentUserId { get { return Thread.CurrentPrincipal.Identity.GetUserId(); } }

        public bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            ApplicationUser user = GetUser(username);
            if (user == null)
                return false;
            Repository.Delete(user.Id);
            Repository.Save();
            return true;
        }

        public string GeneratePasswordResetToken(string username, int tokenExpirationInMinutesFromNow = 1440)
        {
            ApplicationUser user = GetUser(username);
            if (user == null)
                return string.Empty;
            string token = ShortGuid.NewGuid();
            user.PasswordResetToken = token;
            Repository.Update(user);
            Repository.Save();
            return token;
        }

        private string GetUserIdFromPasswordToken(string passwordResetToken)
        {
            ApplicationUser user = 
                Repository
                .Get(u => u.PasswordResetToken == passwordResetToken)
                .SingleOrDefault();
            if (user == null)
                return null;
            return user.Id;
        }

        private void RemovePasswordToken(string userId)
        {
            ApplicationUser user = Repository.GetById(userId);
            if (user != null)
            {
                user.PasswordResetToken = null;
            }
            Repository.Update(user);
            Repository.Save();
        }

        public bool ResetPassword(string passwordResetToken, string newPassword)
        {
            string userId = GetUserIdFromPasswordToken(passwordResetToken);
            if (string.IsNullOrEmpty(userId))
                return false;
            //We have to remove the password before we can add it.
            IdentityResult result = UserManager.RemovePassword(userId);
            if (!result.Succeeded)
                return false;
            //We have to add it because we do not have the old password to change it.
            result = UserManager.AddPassword(userId, newPassword);
            if (!result.Succeeded)
                return false;
            //Lets remove the token so it cannot be used again.
            RemovePasswordToken(userId);
            //TODO: Should use a timestamp on the token so the reset will not work after a set time.
            return true;
        }

        public string GetConfirmationToken(string userName)
        {
            return Repository
                .Get(u => u.UserName == userName)
                .Select(x => x.ConfirmationToken)
                .SingleOrDefault();
        }

        public void MapUserToRole(string userId, string roleName)
        {
            UserManager.AddToRole(userId, roleName);
        }
    }
}
