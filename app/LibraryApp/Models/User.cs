using System;
using System.Collections.Generic;

namespace LibraryApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? UserRole { get; set; }

    public virtual ICollection<Borrowed> Borroweds { get; set; } = new List<Borrowed>();
}
