using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreationDateTime { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? ReceiptCode { get; set; }

    public int StatusId { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
