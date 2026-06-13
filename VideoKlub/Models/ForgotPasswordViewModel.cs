using System.ComponentModel.DataAnnotations;

namespace VideoKlub.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}