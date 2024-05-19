using System;
using System.Collections.Generic;

namespace DomainLayer.Entities;

public partial class BookCategory
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
