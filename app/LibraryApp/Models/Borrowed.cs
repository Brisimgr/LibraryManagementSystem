using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class Borrowed
{
    public int BorrowedId { get; set; }

    public int? UserId { get; set; }

    public int? BookId { get; set; }

    public DateOnly? BorrowDate { get; set; }

    public DateOnly? PlannedReturnDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int Charge { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
