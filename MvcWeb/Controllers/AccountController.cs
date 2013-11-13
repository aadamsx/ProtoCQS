using System.Web.Mvc;
using System.Web.Security;
using AspNetIdentity;
using Core;
using Postal;

namespace MvcWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //public AccountController()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        //{
        //}

        public AccountController(/*UserManager<ApplicationUser> userManager,*/ IWebSecurity webSecurity, IEmailService emailService)
        {
            this.webSecurity = webSecurity;
            this.emailService = emailService;
            //UserManager = userManager;
        }

        //public UserManager<ApplicationUser> UserManager { get; private set; }
        readonly IWebSecurity webSecurity;
        readonly IEmailService emailService;

        //
        // GET: /Account/Login
        [AllowAnonymous]        
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginModel model = new LoginModel() { IsConfirmed = true };
            return View(model);
        }

        private void ResendConfirmationEmail(string username)
        {
            string email = webSecurity.GetEmail(username);
            string token = webSecurity.GetConfirmationToken(username);
            SendEmailConfirmation(email, username, token);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            string errorMsg = "The user name or password provided is incorrect.";

            if (model.IsConfirmed)
            {
                if (ModelState.IsValid &&
                    webSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    return RedirectToLocal(returnUrl);
                }
                else if (webSecurity.FoundUser(model.UserName) && !webSecurity.IsConfirmed(model.UserName))
                {
                    model.IsConfirmed = false;
                    errorMsg =
                        "You have not completed the registration process. To complete this process look for the email that provides instructions or press the button to resend the email.";
                }
            }
            else //Need to resend confirmation email
            {
                ResendConfirmationEmail(model.UserName);
                errorMsg =
                    "The registration email has been resent. Find the email and follow the instructions to complete the registration process.";
                model.IsConfirmed = true;
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", errorMsg );
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            webSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // Using Postal for emails: https://www.nuget.org/packages/postal/
        /// <summary>
        /// Postal uses the Razor engine to create your dynamic email content that can include 
        /// content such as the user name and the custom URL with the confirmation token.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="username"></param>
        /// <param name="confirmationToken"></param>
        private void SendEmailConfirmation(string to, string username, string confirmationToken)
        {
            dynamic email = new Email("RegEmail");
            email.To = to;
            email.UserName = username;
            email.ConfirmationToken = confirmationToken;

            //email.Send();
            emailService.Send(email);
        }


        // Using ShortGuid: http://www.singular.co.nz/blog/archive/2007/12/20/shortguid-a-shorter-and-url-friendly-guid-in-c-sharp.aspx
        /// <summary>
        /// ShortGuid that takes a GUID and encodes it so that it is shorter and is URL safe.
        /// </summary>
        /// <returns></returns>
        //private string CreateConfirmationToken()
        //{
        //    // Should this be a Service?
        //    return ShortGuid.NewGuid();
        //}

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            // Updated this method to incorporate email confirmation: http://kevin-junghans.blogspot.in/2013/10/adding-email-confirmation-to-aspnet.html
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    string confirmationToken =
                        webSecurity.CreateUserAndAccount(model.UserName, model.FirstName, model.LastName, model.Password, model.Email, true);
                    SendEmailConfirmation(model.Email, model.UserName, confirmationToken);

                    return RedirectToAction("RegisterStepTwo", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterStepTwo()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult RegisterConfirmation(string Id)
        {
            if (webSecurity.ConfirmAccount(Id))
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            string emailAddress = webSecurity.GetEmail(model.UserName);
            if (!string.IsNullOrEmpty(emailAddress))
            {
                string confirmationToken =
                    webSecurity.GeneratePasswordResetToken(model.UserName);
                dynamic email = new Email("ChngPasswordEmail");
                email.To = emailAddress;
                email.UserName = model.UserName;
                email.ConfirmationToken = confirmationToken;

                //email.Send();
                emailService.Send(email);

                return RedirectToAction("ResetPwStepTwo");
            }

            return RedirectToAction("InvalidUserName");
        }

        [AllowAnonymous]
        public ActionResult InvalidUserName()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPwStepTwo()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPasswordConfirmation(ResetPasswordConfirmViewModel model)
        {
            if (webSecurity.ResetPassword(model.Token, model.NewPassword))
            {
                return RedirectToAction("PasswordResetSuccess");
            }
            return RedirectToAction("PasswordResetFailure");
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation(string Id)
        {
            ResetPasswordConfirmViewModel model = new ResetPasswordConfirmViewModel() { Token = Id };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PasswordResetFailure()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PasswordResetSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmationFailure()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }





        ///// <summary>
        ///// ConfirmAccount does a query to see if we can find a user with the confirmation token that was passed in the URL. 
        ///// If we find a user we set IsConfirmed to true and return true from the method; otherwise we return false. 
        ///// If the user is confirmed they will be able to log in.
        ///// </summary>
        ///// <param name="confirmationToken"></param>
        ///// <returns></returns>
        //private bool ConfirmAccount(string confirmationToken)
        //{
        //    SecurityContext context = new SecurityContext();
        //    ApplicationUser user = context.Users.SingleOrDefault(u => u.ConfirmationToken == confirmationToken);
        //    if (user != null)
        //    {
        //        user.IsConfirmed = true;
        //        DbSet<ApplicationUser> dbSet = context.Set<ApplicationUser>();
        //        dbSet.Attach(user);
        //        context.Entry(user).State = EntityState.Modified;
        //        context.SaveChanges();

        //        return true;
        //    }
        //    return false;
        //}



        ////
        //// POST: /Account/Disassociate
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        //{
        //    ManageMessageId? message = null;
        //    IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        //    if (result.Succeeded)
        //    {
        //        message = ManageMessageId.RemoveLoginSuccess;
        //    }
        //    else
        //    {
        //        message = ManageMessageId.Error;
        //    }
        //    return RedirectToAction("Manage", new { Message = message });
        //}

        ////
        //// GET: /Account/Manage
        //public ActionResult Manage(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
        //        : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
        //        : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : "";
        //    ViewBag.HasLocalPassword = HasPassword();
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    return View();
        //}

        ////
        //// POST: /Account/Manage
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Manage(ManageUserViewModel model)
        //{
        //    bool hasPassword = HasPassword();
        //    ViewBag.HasLocalPassword = hasPassword;
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    if (hasPassword)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // User does not have a password so remove any validation errors caused by a missing OldPassword field
        //        ModelState state = ModelState["OldPassword"];
        //        if (state != null)
        //        {
        //            state.Errors.Clear();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
        //            }
        //            else
        //            {
        //                AddErrors(result);
        //            }
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user != null)
        //    {
        //        await SignInAsync(user, isPersistent: false);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then prompt the user to create an account
        //        ViewBag.ReturnUrl = returnUrl;
        //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        //    }
        //}

        ////
        //// POST: /Account/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        //}

        ////
        //// GET: /Account/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Manage");
        //    }
        //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser() { UserName = model.UserName };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}



        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //[ChildActionOnly]
        //public ActionResult RemoveAccountList()
        //{
        //    var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
        //    ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
        //    return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && UserManager != null)
        //    {
        //        UserManager.Dispose();
        //        UserManager = null;
        //    }
        //    base.Dispose(disposing);
        //}

        //#region Helpers
        //// Used for XSRF protection when adding external logins
        //private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private bool HasPassword()
        //{
        //    var user = UserManager.FindById(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        return user.PasswordHash != null;
        //    }
        //    return false;
        //}





        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}


        //#endregion
    }
}