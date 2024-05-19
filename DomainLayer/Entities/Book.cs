using System;
using System.Collections.Generic;

namespace DomainLayer.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public double Price { get; set; }

    public bool Ordered { get; set; }

    public int CategoryId { get; set; }

    public virtual BookCategory Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
