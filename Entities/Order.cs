﻿using System;
using System.Collections.Generic;

namespace Store.Entities;

public class Order
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? UserId { get; set; }

    public string? Address { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? Count { get; set; }

    public virtual Product? Product { get; set; }
}