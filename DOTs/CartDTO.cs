using System;
using System.Collections.Generic;

namespace Store.Entities;

public  class CartDTO
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? ProductId { get; set; }


}
