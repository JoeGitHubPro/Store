using System;
using System.Collections.Generic;

namespace Store.Entities;

public class Cart
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? ProductId { get; set; }

    public int Count { get; set; } 

    public virtual Product? Product { get; set; }
}
