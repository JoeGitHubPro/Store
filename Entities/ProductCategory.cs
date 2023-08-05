using System;
using System.Collections.Generic;

namespace Store.Entities;

public class ProductCategory
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
