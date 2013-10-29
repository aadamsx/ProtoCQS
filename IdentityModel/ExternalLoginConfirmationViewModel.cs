using System.ComponentModel.DataAnnotations;

namespace IdentityUserData
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}