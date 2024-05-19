using System;
using System.Collections.Generic;

namespace DomainLayer.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public string Password { get; set; } = null!;

    public bool Blocked { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UserType { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
