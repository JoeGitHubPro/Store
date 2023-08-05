using Store.Extensions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Store.Entities;

public  class ProductDTO
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public double? Price { get; set; }

    [JsonIgnore]
    public string? Image { get; set; }

    public string? ImagePath => Image is null ? null : $"{Path.GetFullPath("wwwroot\\ProductsImages")}\\{Image}";

    public int? DeliveryTime { get; set; }


}
