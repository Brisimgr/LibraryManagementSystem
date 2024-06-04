using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? AuthorId { get; set; }

    public int? GenreId { get; set; }

    public int Pages { get; set; }

    public int? PublishYear { get; set; }

    public string? Availability { get; set; }

    public int? BranchId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Borrowed> Borroweds { get; set; } = new List<Borrowed>();

    public virtual Branch? Branch { get; set; }

    public virtual Genre? Genre { get; set; }
}
