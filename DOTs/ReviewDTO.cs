using System;
using System.Collections.Generic;

namespace Store.Entities;

public class ReviewDTO
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? UserId { get; set; }

    public int? ReviewStars { get; set; }

    public string? ReviewDescription { get; set; }

    
}
