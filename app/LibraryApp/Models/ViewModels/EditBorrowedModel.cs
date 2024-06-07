using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.ViewModels;

public class EditBorrowedModel
{
    [Required(ErrorMessage = "Please provide a Borrow Date")]
    public DateOnly? BorrowDate;

    [Required(ErrorMessage = "Please provide a Planned return Date")]
    public DateOnly? PlannedReturnDate;

    public DateOnly? ReturnDate;

    [Required(ErrorMessage = "Please provide a Charge amount")]
    public int Charge;
}
