using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
    public string? Login { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Mail")]
    public string? Mail { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Password")]
    public string? PasswordHash { get; set; }
}
