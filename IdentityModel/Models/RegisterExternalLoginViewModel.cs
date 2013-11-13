using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity
{
    public class RegisterExternalLoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }
}
