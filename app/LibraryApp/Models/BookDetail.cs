using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class BookDetail
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string Genre { get; set; } = null!;

    public int Pages { get; set; }

    public int? PublishYear { get; set; }

    public string? Availability { get; set; }

    public string Branch { get; set; } = null!;

    public string BranchAddress { get; set; } = null!;
}
