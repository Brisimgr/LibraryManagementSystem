using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class BorrowedDetail
{
    public int BorrowedId { get; set; }

    public string User { get; set; } = null!;

    public string Book { get; set; } = null!;

    public DateOnly? BorrowDate { get; set; }

    public DateOnly? PlannedReturnDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int Charge { get; set; }
}
