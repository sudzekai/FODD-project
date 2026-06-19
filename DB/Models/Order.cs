using System;
using System.Collections.Generic;

namespace DB.models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreationDateTime { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? ReceiptCode { get; set; }

    public int StatusId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Status Status { get; set; } = null!;

    public virtual User? User { get; set; }
}
