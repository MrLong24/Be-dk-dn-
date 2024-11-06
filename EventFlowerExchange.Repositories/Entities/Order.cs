using System;
using System.Collections.Generic;

namespace EventFlowerExchange.Repositories.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    public int? FlowerId { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual Flower? Flower { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? Seller { get; set; }
}
