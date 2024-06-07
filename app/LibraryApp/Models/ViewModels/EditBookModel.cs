using System.ComponentModel.DataAnnotations;

namespace LibraryApp;

public class EditBookModel
{
    [Required(ErrorMessage = "Please select availability")]
    public string? Availability;

    [Required(ErrorMessage = "Please select a branch")]
    public int? BranchId;
}
