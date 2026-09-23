using System.ComponentModel.DataAnnotations;

namespace school.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = ("password length must be 8"))]
        [Compare("ConfirmedNewPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm New Password")]
        public string ConfirmedNewPassword { get; set; }
    }
}
