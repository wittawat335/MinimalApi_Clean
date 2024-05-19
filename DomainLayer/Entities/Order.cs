using System;
using System.Collections.Generic;

namespace DomainLayer.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime OrderedOn { get; set; }

    public bool? Returned { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
