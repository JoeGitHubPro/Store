using System;
using System.Collections.Generic;

namespace Store.Entities;

public class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public double? Price { get; set; }

    public string? Image { get; set; }

    public int? DeliveryTime { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ProductCategory? Category { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
