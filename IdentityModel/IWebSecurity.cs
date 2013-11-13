namespace AspNetIdentity
{
    public interface IWebSecurity
    {
        ApplicationUser GetUser(string username);
        ApplicationUser GetCurrentUser();
        void CreateUser(ApplicationUser user);
        bool FoundUser(string username);
        string GetEmail(string username);
        void Register();
        bool ValidateUser(string userName, string password);
        bool Login(string userName, string password, bool persistCookie = false);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool ConfirmAccount(string accountConfirmationToken);
        string CreateUserAndAccount(string userName, string firstName, string lastName, string password, string email,
            bool requireConfirmationToken = false);
        string GetUserId(string userName);
        void Logout();
        bool IsAuthenticated { get; }
        bool IsConfirmed(string username);
        string CurrentUserName { get; }
        string GetCurrentUserId { get; }
        bool DeleteUser(string username, bool deleteAllRelatedData);
        string GeneratePasswordResetToken(string username, int tokenExpirationInMinutesFromNow = 1440);
        bool ResetPassword(string passwordResetToken, string newPassword);
        string GetConfirmationToken(string userName);
        void MapUserToRole(string userId, string roleName);
    }
}