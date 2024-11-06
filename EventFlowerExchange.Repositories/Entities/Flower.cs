using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace EventFlowerExchange.Repositories.Entities;

public partial class Flower
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public string? Condition { get; set; }

    public decimal? PricePerUnit { get; set; }

    public int? SellerId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual User? Seller { get; set; }
}
