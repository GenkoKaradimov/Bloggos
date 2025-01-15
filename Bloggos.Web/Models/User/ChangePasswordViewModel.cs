using System.ComponentModel.DataAnnotations;

namespace Bloggos.Web.Models.User
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Old Password is required")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Old Password must be between 7 and 20 characters.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "New Password must be between 7 and 20 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Confirm New Password must be between 7 and 20 characters.")]
        [Compare("NewPassword", ErrorMessage = "Confirm New Password must match the New Password.")]
        public string ConfirmNewPassword { get; set; }
    }
}
