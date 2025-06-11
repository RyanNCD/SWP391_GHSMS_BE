using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int AuthorId { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? Image { get; set; }

    public virtual User Author { get; set; } = null!;
}
