using System.ComponentModel.DataAnnotations;

namespace school.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(40,MinimumLength =8,ErrorMessage =("password length must be 8"))]
        [Compare("ConfirmedPassword")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmedPassword { get; set; }

    }
}
