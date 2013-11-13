using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

    }
}
