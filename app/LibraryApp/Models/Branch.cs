using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
