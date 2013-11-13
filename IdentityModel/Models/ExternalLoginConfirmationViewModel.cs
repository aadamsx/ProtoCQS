using System.ComponentModel.DataAnnotations;

namespace AspNetIdentity
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}