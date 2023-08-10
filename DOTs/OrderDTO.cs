using System;
using System.Collections.Generic;

namespace Store.Entities;

public  class OrderDTO
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? UserId { get; set; }

    public string? Address { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? Count { get; set; }

    public double? TotalPrice { get; set; }

}
