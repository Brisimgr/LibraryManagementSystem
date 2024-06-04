using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        public string? Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Mail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Mail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public string? PasswordHash { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your Password")]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
